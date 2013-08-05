using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using powerServer.Core;

namespace powerServer
{
    public partial class winMain : Form
    {
		WaspHost Srv, Clnt;

        public winMain()
        {
            InitializeComponent();
		}

		private void StartSrv_Click(object sender, EventArgs e)
		{
			Srv = new WaspHost(80);
			Clnt = new WaspHost(81);
		}

		private void TxtClient_TextChanged(object sender, EventArgs e)
		{
			Clnt.Conn.Connect(Srv.Conn.LocalEndPoint);
			string s_req = TxtClient.Text;
			byte[] req = Encoding.ASCII.GetBytes(s_req);
			Clnt.Conn.Send(req);
			byte[] msg = new byte[Clnt.Conn.Available];
			Clnt.Conn.Receive(msg);
			string s_msg = Encoding.ASCII.GetString(msg);
			TxtServer.Text = s_msg;
			Clnt.Conn.Shutdown(System.Net.Sockets.SocketShutdown.Both);
			Clnt.Conn.Close();
		}
    }
}
