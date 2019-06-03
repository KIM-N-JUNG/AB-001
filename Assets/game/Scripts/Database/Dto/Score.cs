using System;
namespace Database.Dto
{
    public class Score
    {
        public int id { get; set; }
        public string user_id { get; set; }
        public int score { get; set; }
        public string message { get; set; }
        public int level { get; set; }
        public DateTime score_date { get; set; }
        public Score()
        {
            id = 0;
            user_id = "";
            score = 0;
            message = "";
            level = 0;
            score_date = System.DateTime.Now;
        }
    }
}
