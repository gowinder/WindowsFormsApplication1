using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace gowinder.base_lib.evnt
{
    public class event_pump : i_event_pump
    {
        protected Queue<event_base> _queue;
        //protected Queue<event_base> _queue_recycle;
        protected Dictionary<String, Queue<event_base>> _map_recycle;
        protected Object _locker;
        protected ManualResetEvent _waiter;
        protected bool _is_open = false;
        public service_base service {get;protected set;}


        protected int _id;
        public int id
        {
            get
            {
                return _id;
            }
        }


        public event_pump(int id, service_base ser)
        {
            _id = id;
            _waiter = new ManualResetEvent(false);
            _locker = new Object();
            _queue = new Queue<event_base>();
            _map_recycle = new Dictionary<String, Queue<event_base>>();
            //event_builder = new base_event_builder();
            service = ser;
        }


        public void push(event_base e)
        {
            lock (_locker)
            {
                if (!_is_open)
                    throw new Exception("event pump not open");


                _queue.Enqueue(e);
                _waiter.Set();
            }
        }

        public event_base pop()
        {
            lock (_locker)
            {
                if (!_is_open)
                    throw new Exception("event pump not open");

                if (_queue.Count < 1)
                    return null;
                else
                {
                    event_base e = _queue.Dequeue();
                    return e;
                }
            }
        }

        public int size()
        {
            lock (_locker)
            {
                return _queue.Count;
            }
        }

        public bool wait(int mill_second)
        {
            if (size() > 0)
                return true;

            _waiter.Reset();
            return _waiter.WaitOne(mill_second);
        }

        public void close()
        {
            throw new NotImplementedException();
        }

        public void open()
        {
            lock (_locker)
            {
                _is_open = true;
            }
        }

        public bool is_open()
        {
            return _is_open;
        }


        public void recycle(event_base e)
        {
            if (_map_recycle.ContainsKey(e.event_type))
            {
                Queue<event_base> queue_recyle = _map_recycle[e.event_type];
                queue_recyle.Enqueue(e);
            }
            else
            {
                Queue<event_base> queue_recyle = new Queue<event_base>();
                queue_recyle.Enqueue(e);
                _map_recycle[e.event_type] = queue_recyle;
            }
        }


        public event_base get_new_event(System.String event_type)
        {
            lock (_locker)
            {
                if (_map_recycle.ContainsKey(event_type))
                {
                    Queue<event_base> queue_recyle = _map_recycle[event_type];
                    if (queue_recyle.Count < 1)
                        return queue_recyle.Dequeue();
                }
                else
                    return service.event_builder.build_event(event_type);
            }

            return null;
        }
    }
}
