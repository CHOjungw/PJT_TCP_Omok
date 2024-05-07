using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Omok_Lib.Handlers;
using Omok_Lib.Models;

namespace Omok_Lib.Event
{
    public class OmokEventArgs : EventArgs
    {
        public OmokEventArgs(ClientHandler clientHandler, Hub hub)
        {
            ClientHandler = clientHandler;
            Hub = hub;
        }
        public OmokEventArgs(ClientRoomManager roomManager)
        {
            ClientRoomManager = roomManager;
        }
        public OmokEventArgs(int x, int y)
        {
            X = x;
            Y = y;
        }
        public OmokEventArgs(ClientHandler clientHandler, ConnectionDetails connectionDetails)
        {
            ClientHandler = clientHandler;
            ConnectionDetails = connectionDetails;            
        }
        public OmokEventArgs(string message)
        {            
            Message = message;
        }
        public int X { get; }
        public int Y { get; }
        public ClientHandler ClientHandler { get; }
        public Hub Hub { get; }
        public ConnectionDetails ConnectionDetails { get; }

        public ClientRoomManager ClientRoomManager { get; }
        public string Message { get; }
    }
}
