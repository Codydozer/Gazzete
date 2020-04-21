using Gazette.NetworkMessages;
using MySql.Data.MySqlClient.Memcached;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Gazette
{
	public partial class ChatMenu : Form
	{
		private TcpClient client;
		private string UserID;
		public ChatMenu()
		{
			InitializeComponent();
		}

		public void Connect(IPEndPoint address, string userID)
		{
			UserID = userID;
			client = new TcpClient();
			client.Connect(address);
			new JoinMessage() { UserID = UserID }.Send(client);
			UpdateUserLog();
		}

		private void SendButton_Click(object sender, EventArgs e)
		{
			new ChatMessage() { Text = ChatBox.Text }.Send(client);
			ChatBox.Text = "";
		}

		private void ChatBox_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Enter)
			{
				SendButton.PerformClick();
			}
		}

		private void UpdateUserLog()
		{
			UsersLog.Items.Add(UserID);
		}
	}
}
