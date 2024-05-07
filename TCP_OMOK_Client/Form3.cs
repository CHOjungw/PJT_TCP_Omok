using Omok_Lib.Event;
using Omok_Lib.Handlers;
using Omok_Lib.Models;
using Omok_Lib.Sokets;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Schema;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ToolTip;

namespace TCP_OMOK_Client
{
    public partial class Form3 : Form
    {
        public event EventHandler<OmokEventArgs> PlacedStone;
        public event EventHandler<OmokEventArgs> SendChat;
        Form2 form2;
        Graphics graphic;
        Pen pen;
        Brush Wbrush, Bbrush;
        ClientHandler clientHandler;

        int margin = 10;
        int Size = 25;
        int StoneSize = 23;
        int CenterSize = 7;
        bool drawpanelflag = true;       
        
        int turnflag;
        int startstone;        

        STONE[,] CheckerBoard = new STONE[19, 19];
        public Form3(int turnflag)
        {
            InitializeComponent();                        
            this.turnflag = turnflag;            
            pen = new Pen(Color.Black);
            Bbrush = new SolidBrush(Color.Black);
            Wbrush = new SolidBrush(Color.White);
            record();
            startstone = turnflag;
            
        }
        public void setUser(string message)
        {
            string[] parts = message.Split(',');
            textBox1.Text = parts[0];            
            textBox2.Text = parts[1];
        }

        private void DrawPanel()
        {
            panel1.BackColor = Color.Peru;
            graphic = panel1.CreateGraphics();
            for (int i = 0; i < 19; i++)
            {
                graphic.DrawLine(pen, new Point(margin + i * Size, margin), new Point(margin + i * Size, margin + 18 * Size));
                graphic.DrawLine(pen, new Point(margin, margin + i * Size), new Point(margin + 18 * Size, margin + i * Size));
            }

            for (int x = 3; x <= 15; x += 6)
                for (int y = 3; y <= 15; y += 6)
                {
                    graphic.FillEllipse(Bbrush, margin + Size * x - CenterSize / 2, margin + Size * y - CenterSize / 2, CenterSize, CenterSize);
                }
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            drawpanelflag = false;
            int x = (e.X - margin + Size / 2) / Size;
            int y = (e.Y - margin + Size / 2) / Size;
            if (DupStone(x, y))
            {                
                PlacedStone?.Invoke(this, new OmokEventArgs(x, y));
            }
            
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
           
            if (drawpanelflag)
            {
                DrawPanel();                
            }
        }
        public void CreateStone(int x, int y)
        {                        
            Rectangle r = new Rectangle(margin + Size * x - StoneSize / 2, margin + Size * y - StoneSize / 2, StoneSize, StoneSize);            
            {
                switch (turnflag)
                {
                    case 0:
                        Bitmap bmp1 = new Bitmap("../../black.png");
                        graphic.DrawImage(bmp1, r);
                        CheckerBoard[x, y] = STONE.black;
                        this.turnflag = 1;
                        break;
                    case 1:
                        Bitmap bmp2 = new Bitmap("../../white.png");
                        graphic.DrawImage(bmp2, r);
                        CheckerBoard[x, y] = STONE.white;
                        this.turnflag = 0;
                        break;
                }
            }
            CheckOmok(x, y);
            
            Console.WriteLine("사용자가 둔 수");                        
            Console.WriteLine($"{x},{y},{turnflag}");
        }
        private bool DupStone(int x, int y)
        {            
            if (CheckerBoard[x, y] != STONE.none || turnflag == 1)
                return false;
            else return true;
        }
        private void CheckOmok(int x, int y)
        {
            int cnt = 1;
            for (int i = x + 1; i <= 18; i++)
                if (CheckerBoard[i, y] == CheckerBoard[x, y])
                    cnt++;
                else
                    break;

            for (int i = x - 1; i >= 0; i--)
                if (CheckerBoard[i, y] == CheckerBoard[x, y])
                    cnt++;
                else
                    break;
            if (cnt >= 5)
            {
                OmokComplete(x, y);
                return;
            }
            cnt = 1;
            for (int i = y + 1; i <= 18; i++)
                if (CheckerBoard[x, i] == CheckerBoard[x, y])
                    cnt++;
                else
                    break;
            for (int i = y - 1; i >= 0; i--)
                if (CheckerBoard[x, i] == CheckerBoard[x, y])
                    cnt++;
                else
                    break;
            if (cnt >= 5)
            {
                OmokComplete(x, y);
                return;
            }
            cnt = 1;
            for (int i = x + 1, j = y - 1; i <= 18 && j >= 0; i++, j--)
                if (CheckerBoard[i, j] == CheckerBoard[x, y])
                    cnt++;
                else
                    break;
            for (int i = x - 1, j = y + 1; i >= 0 && j <= 18; i--, j++)
                if (CheckerBoard[i, j] == CheckerBoard[x, y])
                    cnt++;
                else
                    break;
            if (cnt >= 5)
            {
                OmokComplete(x, y);
                return;
            }
            cnt = 1;
            for (int i = x - 1, j = y - 1; i >= 0 && j >= 0; i--, j--)
                if (CheckerBoard[i, j] == CheckerBoard[x, y])
                    cnt++;
                else
                    break;
            for (int i = x + 1, j = y + 1; i <= 18 && j <= 18; i++, j++)
                if (CheckerBoard[i, j] == CheckerBoard[x, y])
                    cnt++;
                else
                    break;
            if (cnt >= 5)
            {
                OmokComplete(x, y);
                return;
            }
            cnt = 1;
        }
        private void OmokComplete(int x, int y)
        {
            DialogResult res = MessageBox.Show(CheckerBoard[x, y].ToString().ToUpper() + "Wins!\n새로운 게임을 시작할까요", "게임종료", MessageBoxButtons.YesNo);
            if (CheckerBoard[x, y].ToString() == "black")
            {
                Bwin++; Wlose++;
            }
            else
            {
                Blose++; Wwin++;
            }
            
            if (res == DialogResult.Yes)
            {
                NewGame();
                record();
            }

            else if (res == DialogResult.No)
            {
                this.Close();
                Bwin = Blose = Wwin = Wlose = 0;
                record();
            }
        }
        private void NewGame()
        {
            for (int x = 0; x < 19; x++)
                for (int y = 0; y < 19; y++)
                    CheckerBoard[x, y] = STONE.none;
            drawpanelflag = true;
            
            panel1.Refresh();
            DrawPanel();            
        }

        int Bwin = 0, Blose = 0, Wwin = 0, Wlose = 0;      

        private void textBox5_KeyDown(object sender, KeyEventArgs e)
        {
            string message = textBox5.Text;            
            if(e.KeyCode ==Keys.Enter)
            {
                SendChat?.Invoke(this, new OmokEventArgs(message));
                textBox5.Text = "";
            }
        }

        private void btn_chatsend_Click(object sender, EventArgs e)
        {
            string message = textBox5.Text;
            SendChat?.Invoke(this, new OmokEventArgs(message));
            textBox5.Text = "";
        }

        private void record()
        {
            if(startstone==0)
            {
                textBox3.Text = $"{Bwin}승 {Blose}패";
                textBox4.Text = $"{Wwin}승 {Wlose}패";
            }
            else
            {
                textBox4.Text = $"{Bwin}승 {Blose}패";
                textBox3.Text = $"{Wwin}승 {Wlose}패";
            }
            
        }
        public void ChatLogAdd(string message)
        {
            lb_chat.Items.Add(message);
        }

    }
}
