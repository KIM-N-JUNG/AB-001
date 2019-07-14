using System;
using Ab001.Util;

namespace Ab001.Database.Dto
{
    public class R_UserGame
    {
        public string user_id { get; set; }
        public string game_code { get; set; }
        public string nick_name { get; set; }
        public DateTime create_date { get; set; }

        public R_UserGame()
        {
            user_id = "";
            game_code = "";
            nick_name = "";
            create_date = DateTimeManager.Instance.getKoreaTimeFromUTCNow();
        }

        public override string ToString()
        {
            return String.Format(
                "[user_id: {0}, game_code: {1}, nick_name: {2}, create_date: {3}]",
                user_id,
                game_code,
                nick_name,
                create_date.ToString("yyyy-MM-dd hh:mm:ss"));
        }
    }
}
