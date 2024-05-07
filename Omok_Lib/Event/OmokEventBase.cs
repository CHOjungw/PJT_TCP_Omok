using Omok_Lib.Event;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Omok_Lib.Event
{
    public abstract class OmokEventBase
    {
        public abstract event EventHandler<OmokEventArgs> Connected;
        public abstract event EventHandler<OmokEventArgs> Disconnected;
        public abstract event EventHandler<OmokEventArgs> Received;
        public abstract event EventHandler<OmokEventArgs> RoomReceived;
        public abstract event EventHandler<OmokEventArgs> PlacedStone;        
    }
}
