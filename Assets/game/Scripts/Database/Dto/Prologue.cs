using System;
namespace Ab001.Database.Dto
{
    public class Prologue
    {
        public int id { get; set; }
        public string content_type { get; set; }
        public string content { get; set; }
        public int language { get; set; }
        public Prologue()
        {
            id = 0;
            content = "";
            language = 10;
        }
    }
}
