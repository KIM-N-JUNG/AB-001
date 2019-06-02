﻿using System;
namespace Database.Dto
{
    public class User
    {
        public int id { get; set; }
        public string nick_name { get; set; }
        public string email { get; set; }
        public int country { get; set; }
        public string user_id { get; set; }
        public string user_image { get; set; }
        public int visit_count { get; set; }
        public string user_name { get; set; }
        public User()
        {
            id = 0;
            nick_name = "";
            email = "";
            country = 10;
            user_id = "";
            user_image = "";
            visit_count = 1;
            user_name = "";
        }
    }
}
