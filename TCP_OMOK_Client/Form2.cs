using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Omok_Lib.Event;
using Omok_Lib.Handlers;
using Omok_Lib.Models;
using Omok_Lib.Sokets;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace TCP_OMOK_Client
{
    public partial class Form2 : Form
    {        
        IPAddress addr = IPAddress.Parse("127.0.0.1"); 
        int port = 53080;
        private OmokClient client;
        private ClientHandler clientHandler;
        private string RoomId;
        Form3 form3;
        List<string> UserList= new List<string>();
        private string UserName => name.Text;    
        private string fillmessage;
        public Form2()
        {
            InitializeComponent();
            client = new OmokClient(addr, port);
            client.Connected += Connected;
            client.Disconnected += Disconnected;
            client.Received += Received;
            client.RoomReceived += RoomReceived;            
        }        
        private void FilledRoom(object sender, OmokEventArgs e)
        {
            clientHandler.Send(e.Hub);
        }
        private void Connected(object sender, OmokEventArgs e)
        {
            clientHandler = e.ClientHandler;
        }
        private void Disconnected(object sender, OmokEventArgs e)
        {
            clientHandler = null;          
        }
        private void UserListManager(Hub hub)
        {
            if (!(UserList.Contains(hub.UserName)))
            {
                UserList.Add(hub.UserName);
                if (UserList.Count == 2)
                {
                    foreach (var item in UserList)
                    {
                        fillmessage += item.ToString() + ",";
                    }
                    Hub hub2 = new Hub { RoomId = hub.RoomId, UserName = hub.UserName, Message = fillmessage, State = OmokState.FilledRoom };
                    clientHandler.Send(hub2);
                }
            }
        }

        private void Received(object sender, OmokEventArgs e)
        {
            Hub hub = e.Hub;
            string message;
            switch (hub.State)
            {
                case OmokState.Connect:
                    message = $"{hub.UserName}님이 접속하셨습니다.";
                    form3.ChatLogAdd(message);
                    UserListManager(hub);
                    break;
                case OmokState.Disconnect:
                    message = $"{hub.UserName}님이 종료하였습니다.";
                    form3.ChatLogAdd(message);
                    break;
                case OmokState.FilledRoom:
                    form3.setUser(hub.Message);
                    break;
                case OmokState.Chat:
                    form3.ChatLogAdd(hub.UserName + ": " + hub.Message);
                    break;
                default:
                    message = $"{hub.UserName}: {hub.Message}";
                    string[] parts = hub.Message.Split(',');
                    int x = int.Parse(parts[0]);
                    int y = int.Parse(parts[1]); 
                    form3.CreateStone(x, y);
                    break;
            }            
        }

        private void RoomReceived(object sender, OmokEventArgs e)
        {
            List<string> roomlist = e.Hub.Roomlist;
            if (!(e.Hub.Roomlist == null))
            {
                foreach (string item in roomlist)
                {
                    listBox2.Items.Add(item);
                }
            }
        }
               
        private async void btnserverjoin_Click(object sender, EventArgs e)
        {
            var connectionDetails = new ConnectionDetails
            {
                UserName = UserName
            };
            await client.ConnectAsync(connectionDetails);
            RoomId = name.Text;
        }

        private void join_Click(object sender, EventArgs e)
        {
            var connectionDetails = new ConnectionDetails
            {
                RoomId = listBox2.SelectedItem.ToString().Substring(0, listBox2.SelectedItem.ToString().IndexOf("의방")),
                UserName = UserName,
            };
            Debug.Print("RoomId :" + connectionDetails.RoomId + " UserName :" + connectionDetails.UserName);

            client.createroom(connectionDetails);
            RoomId = listBox2.SelectedItem.ToString().Substring(0, listBox2.SelectedItem.ToString().IndexOf("의방"));
            form3 = new Form3(1);
            form3.Show();
            form3.PlacedStone += PlacedStone;
            form3.SendChat += SendChat;
            

        }

        private void createroom_Click(object sender, EventArgs e)
        {
            var connectionDetails = new ConnectionDetails
            {
                RoomId = RoomId,
                UserName = UserName,
            };
            Debug.Print("RoomId :" + connectionDetails.RoomId + " UserName :" + connectionDetails.UserName);            
            client.createroom(connectionDetails);
            form3 = new Form3(0);
            form3.Show();
            form3.PlacedStone += PlacedStone;
            form3.SendChat += SendChat;
            
                       
            
        }
        private void PlacedStone(object sender,OmokEventArgs e)
        {
            string message = $"{e.X},{e.Y}";
                clientHandler?.Send(new Hub
                {
                    RoomId = RoomId,
                    UserName = UserName,
                    Message = message
                });                       
        }   
        private void SendChat(object sender, OmokEventArgs e)
        {            
            
            clientHandler?.Send(new Hub
            {
                RoomId = RoomId,
                UserName = UserName,
                Message = e.Message,
                State = OmokState.Chat
              
            });
        }
    }
}

