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
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Gazette
{
	public partial class ChatMenu : Form
	{
		private TcpClient client;
		private string userID;
		private CancellationTokenSource tokenSource = new CancellationTokenSource();
		public ChatMenu()
		{
			InitializeComponent();
		}

		public void Connect(IPEndPoint address, string userID)
		{
			this.userID = userID;
			client = new TcpClient();
			client.Connect(address);
			new JoinMessage() { UserID = userID }.Send(client);
			Task.Run(() => ClientLoop(tokenSource.Token));
			UpdateUserLog();
		}

		private async Task ClientLoop(CancellationToken token)
		{
			while (!token.IsCancellationRequested)
			{
				BinaryFormatter formatter = new BinaryFormatter();
				HandleMessage(await Task.Run(() => formatter.Deserialize(client.GetStream()), token) as NetworkMessage);
			}
		}

		private void HandleMessage(NetworkMessage message)
		{
			{
				if (message is UsersMessage usersMessage)
				{
					BeginInvoke((Action)(() => {
						ChatLog.Items.AddRange(usersMessage.Users);
					}));
				}
			}
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
			UsersLog.Items.Add(userID);
		}
	}
}
