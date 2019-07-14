using System;
namespace Ab001.Database.Dto
{
    public class Notice
    {
        public int id { get; set; }
        public string title { get; set; }
        public string content { get; set; }

        public Notice()
        {
            id = 0;
            title = "";
            content = "";
        }
    }
}
