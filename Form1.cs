using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Sockets;
using System.Threading.Tasks;
using System.Windows.Forms;
using Omok_Lib.Event;
using Omok_Lib.Handlers;
using Omok_Lib.Models;
using Omok_Lib.Sokets;

namespace TCP_OMOK
{
    public partial class Form1 : Form
    {
        private OmokServer omokserver;
        private ClientRoomManager roomManager;
        private TcpListener server;
        public Form1()
        {
            InitializeComponent();
            roomManager = new ClientRoomManager();
            omokserver = new OmokServer();
            omokserver.Connected += Connected;
            omokserver.Disconnected += Disconnected;
            omokserver.Received += Received;
            omokserver.RoomReceived += RoomReceived;
        }
        private Hub CreateNewStateChatHub(Hub hub, OmokState state)
        {
            return new Hub
            {
                RoomId = hub.RoomId,
                UserName = hub.UserName,
                State = state,
            };
        }
        private void AddClientMessageList(Hub hub)
        {
            string message;
            switch (hub.State)
            {
                case OmokState.Connect:
                    message = $"{hub.UserName}님이 접속하였습니다.";
                    break;
                case OmokState.Disconnect:
                    message = $"{hub.UserName}님이 종료하였습니다.";
                    break;
                default:
                    message = $"{hub.UserName}: {hub.Message}";
                    break;
            }
            lb_Log.Items.Add(message);            
        }

        private void RoomReceived(object sender, OmokEventArgs e)
        {
            List<string> RoomList = new List<string>();
            foreach (string item in lb_Roomlist.Items)
            {
                RoomList.Add(item);
            }
            Hub hub = new Hub() { UserName = e.Hub.UserName, Roomlist = RoomList, State = e.Hub.State };
            e.ClientHandler.Send(hub);
            foreach (var item in hub.Roomlist)
            {
                Debug.Print($"RoomList에 추가된 방 이름: {item}");
            }
        }
        private void Connected(object sender, OmokEventArgs e)
        {
            var hub = CreateNewStateChatHub(e.Hub, OmokState.Connect);
            roomManager.Add(e.ClientHandler);
            bool itemExist = lb_Roomlist.Items.Contains(hub.RoomId + "의방");
            if (!itemExist)
            {
                lb_Roomlist.Items.Add(hub.RoomId + "의방");
            }
            AddClientMessageList(hub);
            roomManager.SendToMyRoom(hub);
        }
        private void Disconnected(object sender, OmokEventArgs e)           
        {
            var hub = CreateNewStateChatHub(e.Hub, OmokState.Disconnect);
            roomManager.Remove(e.ClientHandler);
            roomManager.SendToMyRoom(hub);

            lb_Roomlist.Items.Remove(hub.RoomId+"의방");
        }
        private void Received(object sender, OmokEventArgs e)
        {
            roomManager.SendToMyRoom(e.Hub);
            AddClientMessageList(e.Hub);
        }

        private void btn_start_Click(object sender, EventArgs e)
        {
            omokserver.StartServer();
        }

        private void btn_close_Click(object sender, EventArgs e)
        {
            omokserver.Stop();
        }
    }
}
