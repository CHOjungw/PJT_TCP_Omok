using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Omok_Lib.Models
{
    public class ConnectionDetails
    {
        public string UserName { get; set; } = string.Empty;
        public string RoomId { get; set; }

        public override string ToString()
        {
            return $"RoomId: {RoomId}, UserName: {UserName}";
        }
    }
}
