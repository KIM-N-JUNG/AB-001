using System;
namespace Database.Dto
{
    public class User
    {
        public string nick_name { get; set; }
        public string email { get; set; }
        public int country { get; set; }
        public string user_id { get; set; }
        public string user_image { get; set; }
        public int visit_count { get; set; }
        public string user_name { get; set; }
        public DateTime last_date { get; set; }
        public User()
        {
            nick_name = "";
            email = "";
            country = 10;
            user_id = "";
            user_image = "";
            visit_count = 1;
            user_name = "";
            last_date = DateTime.Now;
        }

        public override string ToString()
        {
            return String.Format(
                "[nick_name:{0}, email: {1}, country: {2}, user_id: {3}, visit_count: {4}, user_name: {5}, last_date: {6}]",
                nick_name,
                email,
                country,
                user_id,
                visit_count,
                user_name,
                last_date.ToString("yyyy-MM-dd hh:mm:ss"));
        }
    }
}
