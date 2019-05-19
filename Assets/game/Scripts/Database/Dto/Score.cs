using System;
namespace Database.Dto
{
    public class Score
    {
        public int id { get; set; }
        public int user_id { get; set; }
        public int score { get; set; }
        public string message { get; set; }

        public Score()
        {
            id = 0;
            user_id = 0;
            score = 0;
            message = "";
        }
    }
}
