using Omok_Lib.Event;
using Omok_Lib.Handlers;
using Omok_Lib.Models;
using System;
using System.Diagnostics;
using System.Net.Sockets;
using System.Net;
using System.Threading.Tasks;

namespace Omok_Lib.Sokets
{
    public class OmokClient : OmokEventBase
    {
        ClientRoomManager roomManager;
        private IPAddress addr = IPAddress.Parse("127.0.0.1");
        private int port = 53080;

        private TcpClient Client;
        private bool isRunning = false;
        private ClientHandler clientHandler;

        public override event EventHandler<OmokEventArgs> Connected;
        public override event EventHandler<OmokEventArgs> Disconnected;
        public override event EventHandler<OmokEventArgs> Received;
        public override event EventHandler<OmokEventArgs> RoomReceived;
        public override event EventHandler<OmokEventArgs> PlacedStone;

        private Hub ConvertChatHub(ConnectionDetails details)
        {

            var hub = new Hub
            {
                UserName = details.UserName,
                RoomId = details.RoomId,
                State = OmokState.Initial
            };
            Debug.Print($"UserName: {hub.UserName}, RoomId: {hub.RoomId}, State: {hub.State}" + "생성된 hub");
            return hub;
        }
        private Hub HubStateRoom(ConnectionDetails details)
        {
            var hub = new Hub
            {
                UserName = details.UserName,
                State = OmokState.Room,
            };
            return hub;
        }

        public OmokClient(IPAddress addr, int port)
        { }
        public async Task ConnectAsync(ConnectionDetails details)
        {
            if (isRunning) return;

            try
            {
                Client = new TcpClient();
                await Client.ConnectAsync(addr, port);
                isRunning = true;

                Hub hub = HubStateRoom(details);
                clientHandler = new ClientHandler(Client);
                Connected.Invoke(this, new OmokEventArgs(clientHandler, hub));
                clientHandler.Disconnected += ClientHandler_Disconnected;
                clientHandler.Received += Received;
                clientHandler.RoomReceived += RoomReceived;

                clientHandler.HandleClientAsync();
                clientHandler.Send(hub);
            }
            catch (Exception ex)
            {
                DisPoseClient();
                Debug.Print($"서버 연결 시도 중 오류 발생: {ex.Message}");
            }
        }

        public void createroom(ConnectionDetails details)
        {
            Hub hub = ConvertChatHub(details);
            clientHandler.Send(hub);
        }
        private void DisPoseClient()
        {
            Client.Dispose();
            isRunning = false;
        }
        private void ClientHandler_Disconnected(object sender, OmokEventArgs e)
        {
            DisPoseClient();
            Disconnected.Invoke(sender, e);
        }      

    }
}
