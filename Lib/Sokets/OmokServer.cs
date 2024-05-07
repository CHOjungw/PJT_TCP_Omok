using Omok_Lib.Event;
using Omok_Lib.Handlers;
using System;
using System.Diagnostics;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Omok_Lib.Sokets
{
    public class OmokServer : OmokEventBase
    {
        private IPAddress addr = IPAddress.Parse("127.0.0.1");
        private int port = 53080;

        public TcpListener server;



        private bool isRunning = false;

        public override event EventHandler<OmokEventArgs> Connected;
        public override event EventHandler<OmokEventArgs> Disconnected;
        public override event EventHandler<OmokEventArgs> Received;
        public override event EventHandler<OmokEventArgs> RoomReceived;
        public override event EventHandler<OmokEventArgs> PlacedStone;        

        public OmokServer()
        {
            server = new TcpListener(addr, port);
        }

        public async Task StartServer()
        {
            if (isRunning) return;

            try
            {
                server.Start();
                isRunning = true;
                Debug.Print("서버 시작");

                while (true)
                {
                    TcpClient client = await server.AcceptTcpClientAsync();
                    Debug.Print($"클라이언트 연결 수락: {client.Client.Handle}");

                    ClientHandler clientHandler = new ClientHandler(client);
                    clientHandler.Connected += Connected;
                    clientHandler.Disconnected += Disconnected;
                    clientHandler.Received += Received;
                    clientHandler.RoomReceived += RoomReceived;

                    clientHandler.HandleClientAsync();
                }
            }
            catch (Exception ex)
            {
                Debug.Print($"서버 시작 중 오류 발생: {ex.Message}");
                isRunning = false;
            }
        }
        public void Stop()
        {
            isRunning = false;
            server.Stop();
            Debug.Print("서버 정지");
        }
    }
}

