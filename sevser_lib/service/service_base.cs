using gowinder.sevser_lib.evnt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace gowinder.sevser_lib
{
    class service_base
    {
        protected i_event_pump _pump;
        protected int _id;
        protected Thread _thread;
        protected bool _start_own_thread;


        protected virtual i_event_pump create_pump()
        {
            return new event_pump(_id);
        }

        public bool is_running
        {
            get
            {
                if (_pump != null && _pump.is_open())
                    return true;

                return false;
            }
        }

        //         public service_base(int id)
        //         {
        //             _id = id;
        //         }

        public event_base get_new_event(String event_type)
        {
            //             if(_pump == null)
            //                 throw new exception_base(exception_base.RETURN_NULL_REF);

            return _pump.get_new_event(event_type);
        }

        public void thread_process()
        {
            while (true)
            {
                if (is_running)
                    go_tick();
            }

        }

        protected void go_tick()
        {
            maintain();

            process_event_pump();
        }

        protected virtual void maintain()
        {
            try
            {

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private string fun_name = "thread_process";

        public void start_service(bool start_own_thread)
        {
            if (is_running)
                return;

            _pump = create_pump();
            _pump.open();
            _start_own_thread = start_own_thread;
            service_thread t = new service_thread(this);
            ThreadStart threadDelegate = new ThreadStart(t.proc);
            if (start_own_thread)
            {
                _thread = new Thread(threadDelegate);
                _thread.Start();
            }
            else
            {
//                this.InvokeRepeating(fun_name, 0.1f, 1.0f);
            }
        }

        public void stop_service()
        {
            if (!is_running)
                return;

            _pump = null;
            if (_start_own_thread)
            {
                if (_thread != null)
                    _thread.Abort();
            }
            else
            {
 //               this.CancelInvoke(fun_name);
            }
        }

        protected void process_event_pump()
        {
            try
            {
                if (!is_running)
                    return;

                const int INTERVAL = 50;

                DateTime dt_start = DateTime.Now;
                event_base e = _pump.pop();
                //                 for (; ; )
                //                 {
                //                     if (e == null)
                //                         break;
                //                     try
                //                     {
                //                         e.process();
                //                     }
                //                     catch (Exception ex)
                //                     {
                //                         Debug.Log(ex.Message);
                //                     }
                //                     e.recycle();
                //                 }
                if (e != null)
                {
                    e.process();
                    e.recycle();
                }
                DateTime dt_end = DateTime.Now;
                TimeSpan ts = dt_end - dt_start;

                if (ts.Milliseconds < INTERVAL && _start_own_thread)
                    _pump.wait(INTERVAL - ts.Milliseconds);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void send_event(event_base e)
        {
            if (!is_running)
            {
                Console.WriteLine("service not running");
                return;
            }

            _pump.push(e);
        }
    }

    class service_thread
    {
        protected service_base _s;
        public service_thread(service_base s)
        {
            _s = s;
        }


        public void proc()
        {
            _s.thread_process();
        }
    }
}
