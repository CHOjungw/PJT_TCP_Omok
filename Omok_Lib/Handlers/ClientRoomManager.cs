using Omok_Lib.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Omok_Lib.Models;

namespace Omok_Lib.Handlers
{
    public class ClientRoomManager
    {
        private Dictionary<string, List<ClientHandler>> roomHandlerDict = new Dictionary<string, List<ClientHandler>>();

        public void Add(ClientHandler clientHandler)
        {
            string roomId = clientHandler.InitialData.RoomId;

            if (roomHandlerDict.TryGetValue(roomId, out _))
            {
                roomHandlerDict[roomId].Add(clientHandler);
            }
            else
            {
                roomHandlerDict[roomId] = new List<ClientHandler>() { clientHandler };
            }
        }
        public void Remove(ClientHandler clientHandler)
        {
            string roomId = clientHandler.InitialData.RoomId;
            if (roomHandlerDict.TryGetValue(roomId, out List<ClientHandler> roomHandlers)) { roomHandlerDict[roomId] = roomHandlers.FindAll(handler => handler.Equals(clientHandler)); }
        }

        public void SendToMyRoom(Hub hub)
        {
            if (roomHandlerDict.TryGetValue(hub.RoomId, out List<ClientHandler> roomHandlers))
            {
                roomHandlers.ForEach(handler => handler.Send(hub));
            }
        }
    }
}
