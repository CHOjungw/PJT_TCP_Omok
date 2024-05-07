using Omok_Lib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Omok_Lib.Models
{
    public class Hub : ConnectionDetails
    {
        public static Hub Parse(string json) => JsonSerializer.Deserialize<Hub>(json);        

        public OmokState State { get; set; }

        public string Message { get; set; } = string.Empty;

        public List<string> Roomlist { get; set; }

        public string ToJsonString() => JsonSerializer.Serialize(this);
    }
}
