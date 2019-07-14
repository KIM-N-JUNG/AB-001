using System;
using Ab001.Util;

namespace Ab001.Database.Dto
{
    public class User
    {
		public string user_id { get; set; }
        public string auth { get; set; }
		public string user_name { get; set; }
        public string email { get; set; }
        public int country { get; set; }
        public string user_image { get; set; }
        public int visit_count { get; set; }
        public DateTime last_date { get; set; }

		public User()
        {
			user_id = "";
			auth = "";
			user_name = "";
            email = "";
            country = 10;
            user_image = "";
            visit_count = 1;
            
            last_date = DateTimeManager.Instance.getKoreaTimeFromUTCNow();
        }

        public override string ToString()
        {
            return String.Format(
				"[user_id: {0}, auth: {1}, user_name: {2}, email: {3}, country: {4}, visit_count: {5}, last_date: {6}]",
				user_id,
                auth,
				user_name,
                email,
                country,
                visit_count,
                last_date.ToString("yyyy-MM-dd hh:mm:ss"));
        }
    }
}
