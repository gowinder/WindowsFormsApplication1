// gowinder@hotmail.com
// gowinder.game_base_lib
// account_manager.cs
// 2016-05-10-14:11

#region

using System;
using System.Collections.Generic;
using gowinder.base_lib.service;
using gowinder.database;
using gowinder.database.evnt;
using gowinder.game_base_lib.data.account_async;
using gowinder.game_base_lib.evnt;
using gowinder.net_base;

#endregion

namespace gowinder.game_base_lib.data
{
    public class account_manager
    {
        public account_manager(logic_service ser)
        {
            service = ser;
            dict_account = new Dictionary<uint, game_data_default_account>();
            dict_account_unique_name = new Dictionary<string, game_data_default_account>();
            dict_token = new Dictionary<string, game_data_default_account>();
            dict_socket_id = new Dictionary<uint, game_data_default_account>();

            async_load_queue = new async_load_queue(this);
        }

        public Dictionary<uint, game_data_default_account> dict_account { get; protected set; }
        public Dictionary<string, game_data_default_account> dict_account_unique_name { get; protected set; }
        public Dictionary<string, game_data_default_account> dict_token { get; protected set; }
        public Dictionary<uint, game_data_default_account> dict_socket_id { get; protected set; }

        protected async_load_queue async_load_queue { get; set; }

        public logic_service service { get; protected set; }

        public virtual void rev_async_load_data(event_async_load_db_response response)
        {
            var res = response.data as async_load_db_response;
            var account = find_account_by_id(res.account_id);
            if (account == null)
                throw new Exception("my_account_manager.rev_async_load_data not found account");

            on_rev_async_load_data(account, response);

            account.full_loaded = true;

            async_load_queue.resume_process_wait_holder(account.id);
        }

        protected virtual void on_rev_async_load_data(game_data_default_account defalt_account,
            event_async_load_db_response response)
        {
            throw new NotImplementedException();
        }

        public virtual void init()
        {
            var str_sql = "select * from account";
            var db = service.get_database(1);
            var reader = db.create_reader(str_sql);
            while (reader.Read())
            {
                var account = on_create_account();
                account.read_from_dataset(reader);
                insert_account(account);
            }
        }

        protected void insert_account(game_data_default_account account)
        {
            dict_account.Add(account.id, account);
            dict_account_unique_name.Add(account.full_name, account);
        }

        protected virtual game_data_default_account on_create_account()
        {
            return new game_data_default_account();
        }

        public virtual game_data_default_account find_account_by_id(uint account_id)
        {
            if (dict_account.ContainsKey(account_id))
                return dict_account[account_id];
            return null;
        }

        public virtual game_data_default_account find_account_by_full_name(string full_name)
        {
            if (dict_account_unique_name.ContainsKey(full_name))
                return dict_account_unique_name[full_name];
            return null;
        }

        public virtual game_data_default_account find_account_by_platform_and_user_id(uint platform_id,
            string platform_user_id)
        {
            var full_name = game_data_default_account.get_full_name(platform_id, platform_user_id);
            return find_account_by_full_name(full_name);
        }

        public virtual game_data_default_account find_account_by_token(string token)
        {
            if (dict_token.ContainsKey(token))
                return dict_token[token];
            return null;
        }

        public virtual game_data_default_account find_account_by_socket_id(uint socket_id)
        {
            if (dict_socket_id.ContainsKey(socket_id))
                return dict_socket_id[socket_id];
            return null;
        }

        public account_check_login_result check_login(net_package package)
        {
            //             if ((net_package_type)package.type == net_package_type.login)
            //             {
            // 
            //             }
            //             else
            {
                if (package.token == "")
                    return account_check_login_result.token_timeout;
                var account = find_account_by_token(package.token);
                if (account == null)
                    return account_check_login_result.token_timeout;
                var span = DateTime.Now - account.last_operation_date;
                if (span.TotalSeconds >= 30*60)
                    return account_check_login_result.token_timeout;
            }
            return account_check_login_result.login_ok;
        }

        public virtual login_result do_login(net_package this_package, uint platform_id, string user_name,
            string user_pwd)
        {
            var full_name = game_data_default_account.get_full_name(platform_id, user_name);
            var account = find_account_by_full_name(full_name);
            if (account == null)
            {
                return login_result.no_account;
            }

            if (!account.full_loaded)
            {
                //  TODO LIST check async_load_queue has this account in wait
                if (async_load_queue.dict_wait_holder.ContainsKey(account.id))
                {
                    var wait_holder = async_load_queue.dict_wait_holder[account.id];
                    wait_holder.list_wait_action.Add(this_package);
                }
                else
                {
                    var async_load_ser =
                        service_manager.instance().get_serivce(async_load_db_service.default_name) as
                            async_load_db_service;
                    if (async_load_ser == null)
                        throw new Exception("account_manager.do_login cannot get async_load_db_service");

                    var event_load_request =
                        async_load_ser.get_new_event(event_async_load_db_request.type) as event_async_load_db_request;
                    if (event_load_request == null)
                        throw new Exception("account_manager.do_login cannot get new event_async_load_db_request");

                    var load_request = new async_load_db_request();
                    load_request.account_id = account.id;
                    load_request.type = 0;
                    load_request.db_index = 1;
                    event_load_request.set(service, async_load_ser, load_request);
                    event_load_request.send();

                    var wait_holder = new async_load_account_wait_holder(account.id);
                    wait_holder.list_wait_action.Add(this_package);
                    async_load_queue.dict_wait_holder.Add(account.id, wait_holder);
                }

                //  TODO LIST add async load queue store in account_manager to restore multiple async load request of the same account


                return login_result.login_ok_need_load;
            }

            return login_result.login_ok;
        }
        
    }
}