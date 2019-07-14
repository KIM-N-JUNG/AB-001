using System;
using Ab001.Util;

namespace Ab001.Database.Dto
{
    public class Ab001Score
    {
        public int id { get; set; }
        public string user_id { get; set; }
        public int score { get; set; }
        public string message { get; set; }
        public int level { get; set; }
        public DateTime score_date { get; set; }
        public int time { get; set; }
        public Ab001Score()
        {
            id = 0;
            user_id = "";
            score = 0;
            message = "";
            level = 0;
            score_date = DateTimeManager.Instance.getKoreaTimeFromUTCNow();
            time = 0;
        }

        public override string ToString()
        {
            return String.Format("Ab001Score: [id: {0}, user_id: {1}, score: {2}, message: {3}, time: {4}, level: {5}, score_date: {6}"
                , id, user_id, score, message, time, level, score_date.ToString("yyyy-MM-dd"));
        }
    }
}
