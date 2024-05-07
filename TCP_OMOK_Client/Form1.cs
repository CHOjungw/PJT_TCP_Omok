using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace TCP_OMOK_Client
{
    public partial class Form1 : Form
    {        
        public Form1()
        {
            InitializeComponent();
        }

        private void btn_CC_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
            form2.Show();
            Debug.Print("클라이언트가 생성되었습니다");

        }
    }
}
