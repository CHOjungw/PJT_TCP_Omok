using Omok_Lib.Models;
using System.Collections.Generic;
using System.Text.Json;


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
