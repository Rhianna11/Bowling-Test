using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BowlingTest.Models
{
    class Session
    {
        [JsonProperty("gameId")]
        public string GameId { get; set; }
        [JsonProperty("throws")]
        public List<string> Throws { get; set; }
    }
}
