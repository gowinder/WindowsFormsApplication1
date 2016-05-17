// gowinder@hotmail.com
// gowinder.WindowsFormsApplication1
// game_data_basic.cs
// 2016-05-13-14:48

#region

using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;
using gowinder.base_lib;
using gowinder.base_lib.service;
using gowinder.database;
using gowinder.database.evnt;
using Newtonsoft.Json.Linq;

#endregion

namespace gowinder.game_base_lib.data
{
    public abstract class game_data_basic
    {
        public static bool short_json_name { get; set; }
        public enum data_edit_type
        {
            none = 0,
            update = 1,
            insert = 2,
            delete = 3,
        }

        public service_base service { get; set; }

        protected game_data_basic(service_base owner_service)
        {
            service = owner_service;
            fields = new List<object>();
            fields_name = new List<string>();
            fields_change = new BitVector32();
            edit_type = data_edit_type.update;
            
            init_fields();
        }

        protected data_edit_type edit_type { get; set; }
        protected List<object> fields { get; set; }
        protected List<string> fields_name { get; set; }
        protected BitVector32 fields_change { get; set; }
        public string table_name { get; set; }

        protected abstract void init_fields();

        public void save()
        {
            string str_sql = build_sql(this);
            if (str_sql.Length > 0)
            {
                event_async_save_db event_save = async_save_db_service.instance.get_new_event(event_async_save_db.type) as event_async_save_db;
                if(event_save == null)
                    throw new Exception("game_data_basic " + table_name + " get event_async_save_db failed");

                async_save_db_info info = new async_save_db_info();
                info.db_index = db_table_configer.instance.get_table_db_index(table_name);
                info.sql = str_sql;
                event_save.set(service, async_save_db_service.instance, info);
                event_save.send();
            }
        }

        public void to_json(JObject json_root)
        {
            JObject jobj = new JObject();

            if (game_data_basic.short_json_name)
            {
                for (var i = 0; i < fields.Count; i++)
                {
                    string str_name = $"a{i}";
                    jobj[str_name] = (JObject)fields[i];
                }
            }
            else
            {
                foreach (object t in fields)
                {
                    jobj[fields_name] = (JObject)t;
                }
            }

            json_root[table_name] = jobj;
        }

        public void change_to_json(JObject json_root)
        {
            if (fields_change.Data == 0)
                return;

            JObject jobj = new JObject();

            if (game_data_basic.short_json_name)
            {
                for (var i = 0; i < fields.Count; i++)
                {
                    if (!fields_change[i])
                        continue;
                    string str_name = $"a{i}";
                    jobj[str_name] = (JObject)fields[i];
                }
            }
            else
            {
                for (var i = 0; i < fields.Count; i++)
                {
                    if (!fields_change[i])
                        continue;
                    jobj[fields_name] = (JObject)fields[i];
                }
            }

            json_root[table_name] = jobj;
        }

        public void from_json(JObject json_root)
        {
            JObject jobj = (JObject)json_root[table_name];
            if (jobj == null)
                return;

            if (game_data_basic.short_json_name)
            {
                for (var i = 0; i < fields.Count; i++)
                {
                    string str_name = $"a{i}";
                    fields[i] = jobj[str_name];
                }
            }
            else
            {
                for (var i = 0; i < fields.Count; i++)
                {
                    fields[i] = jobj[fields_name];
                }
            }
        }

        protected static string build_sql(game_data_basic game_data)
        {
            if ((game_data.edit_type == data_edit_type.update || game_data.edit_type == data_edit_type.insert) &&
                game_data.fields_change.Data == 0)
                return "";

            StringBuilder sb_full = new StringBuilder();
            string str_where = "";
            string str_operator = "";
            string str_fields = "";
            switch (game_data.edit_type)
            {
                case data_edit_type.update:
                {
                    str_where = $" where `{game_data.fields_name[0]}` = {game_data.fields[0]} limit 1";

                    str_operator = $"update {game_data.table_name} set ";
                }
                    break;
                case data_edit_type.delete:
                {
                    str_where = $" where `{game_data.fields_name[0]}` = {game_data.fields[0]} limit 1";

                    str_operator = $"delete from {game_data.table_name} s";
                }
                    break;
                case data_edit_type.insert:
                {
                    str_operator = $"insert into {game_data.table_name} set ";
                }
                    break;
            }

            
            if (game_data.edit_type == data_edit_type.update || game_data.edit_type == data_edit_type.insert)
            {
                StringBuilder sb = new StringBuilder();
                for (var i = 0; i < game_data.fields.Count; i++)
                {
                    if (!game_data.fields_change[i])
                        continue;

                    string str_field = "";
                    object field = game_data.fields[i];
                    if (field is string)
                    {
                        str_field = $"`{game_data.fields_name[i]}` = '{game_data.fields[i]}',";
                    }
                    else
                    {
                        str_field = $"`{game_data.fields_name[i]}` = {game_data.fields[i]},";
                    }
                    sb.Append(str_field);
                }
                if (sb.Length > 0)
                {
                    str_fields = sb.ToString();
                    str_fields = str_fields.TrimEnd(',');
                }
            }

            sb_full.Append(str_operator);
            sb_full.Append(str_fields);
            sb_full.Append(str_where);

            return sb_full.ToString();
        }
    }
}