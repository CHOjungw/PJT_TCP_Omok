using Omok_Lib.Event;
using System;
using System.Diagnostics;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Omok_Lib.Models;

namespace Omok_Lib.Handlers
{
    public class ClientHandler : OmokEventBase
    {
        private TcpClient client;
        NetworkStream stream;

        public override event EventHandler<OmokEventArgs> Connected;
        public override event EventHandler<OmokEventArgs> Disconnected;
        public override event EventHandler<OmokEventArgs> Received;
        public override event EventHandler<OmokEventArgs> RoomReceived;
        public override event EventHandler<OmokEventArgs> PlacedStone;
        

        public Hub InitialData { get; private set; }
        public ClientHandler(TcpClient client)
        {
            this.client = client;
            this.stream = client.GetStream();
        }
        public async Task HandleClientAsync()
        {
            byte[] sizeBuffer = new byte[4];
            int read;

            try
            {
                while (true)
                {
                    read = await stream.ReadAsync(sizeBuffer, 0, sizeBuffer.Length);
                    if (read == 0)
                        break;
                    int size = BitConverter.ToInt32(sizeBuffer, 0);
                    byte[] buffer = new byte[size];

                    read = await stream.ReadAsync(buffer, 0, buffer.Length);
                    if (read == 0) break;

                    string message = Encoding.UTF8.GetString(buffer, 0, read);

                    var hub = Hub.Parse(message);
                    if (hub.State == OmokState.Initial)
                    {
                        InitialData = hub;
                        Debug.Print("클라이언트 연결 이벤트 발생");
                        Connected?.Invoke(this, new OmokEventArgs(this, hub));
                    }
                    else if (hub.State == OmokState.Room)
                    {
                        RoomReceived.Invoke(this, new OmokEventArgs(this, hub));
                    }                   
                    else
                    {
                        Debug.Print("클라이언트 데이터 수신 이벤트 발생");
                        Received?.Invoke(this, new OmokEventArgs(this, hub));
                    }
                }
            }
            catch (Exception ex) { Debug.Print($"클라이언트 요청 처리 중 오류 발생 : {ex.Message}"); }
            finally
            {
                client.Close();
                Disconnected?.Invoke(this, new OmokEventArgs(this, InitialData));
            }
        }
        public void Send(Hub hub)
        {
            try
            {
                byte[] buffer = Encoding.UTF8.GetBytes(hub.ToJsonString());
                byte[] lengthBuffer = BitConverter.GetBytes(buffer.Length);

                stream.Write(lengthBuffer, 0, lengthBuffer.Length);
                stream.Write(buffer, 0, buffer.Length);
            }
            catch (Exception ex)
            {
                Debug.Print($"클라이언트 요청 처리 중 오류 발생 : {ex.Message}");
            }
        }

    }
}

