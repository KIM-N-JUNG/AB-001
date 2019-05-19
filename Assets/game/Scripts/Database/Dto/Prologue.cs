using System;
namespace Database.Dto
{
    public class Prologue
    {
        public int id { get; set; }
        public string content { get; set; }
        public Prologue()
        {
            id = 0;
            content = "";
        }
    }
}
