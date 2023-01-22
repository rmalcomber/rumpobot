using System;
using System.Collections.Generic;
using System.Text;

namespace RmpDiscordBot.Dtos
{
    public class AdviceResponse
    {
        public Slip slip { get; set; }
    }

    public class Slip
    {
        public string advice { get; set; }
        public string slip_id { get; set; }
    }

}
