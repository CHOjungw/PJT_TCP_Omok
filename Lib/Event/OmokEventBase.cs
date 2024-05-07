using Omok_Lib.Event;
using System;

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
