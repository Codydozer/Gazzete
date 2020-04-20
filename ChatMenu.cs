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
		TcpClient client;
		string UserID;
		public ChatMenu()
		{
			InitializeComponent();
		}

		public void Connect(IPEndPoint address, string userID)
		{
			UserID = userID;
			client = new TcpClient();
			client.Connect(address);
			SendMessage(new JoinMessage() { UserID = UserID });
			
		}

		private void SendButton_Click(object sender, EventArgs e)
		{
			SendMessage(new ChatMessage() { Text = ChatBox.Text });
			ChatBox.Text = "";
		}

		void SendMessage(NetworkMessage message)
		{
			byte[] messageBytes = message.Serialize();
			client.GetStream().Write(messageBytes, 0, messageBytes.Length);
		}

		private void ChatBox_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Enter)
				SendButton.PerformClick();
		}
	}
}
