using System;
using System.Collections.Generic;
using System.Text;

namespace RmpDiscordBot.Dtos
{

    public class CatResponse
    {
        public CatObj[] Cats { get; set; }
    }

    public class CatObj
    {
        public object[] breeds { get; set; }
        public string id { get; set; }
        public string url { get; set; }
    }

}
