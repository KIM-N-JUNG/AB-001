using System;
using Ab001.Util;

namespace Ab001.Database.Dto
{
    public class Game
    {
        public string code { get; set; }
        public string version { get; set; }
        public DateTime updated_date { get; set; }
        public Game()
        {
            code = "";
            version = "";
            updated_date = DateTimeManager.Instance.getKoreaTimeFromUTCNow();
        }
    }
}
