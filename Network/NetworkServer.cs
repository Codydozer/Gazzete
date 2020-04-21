using Gazette.NetworkMessages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

namespace Gazette.Network
{
	public class NetworkServer
	{
		public event Action<string[]> ClientsChanged;
		public event Action<string> OutputLog;

		private bool running;
		private TcpListener listener;
		private CancellationTokenSource tokenSource = new CancellationTokenSource();
		private List<NetworkClient> clients = new List<NetworkClient>();

		public NetworkServer(IPEndPoint address) => listener = new TcpListener(address);

		public bool Start()
		{
			if(running)
			{
				Log("Server is already running!");
				return false;
			}

			Log("Starting server.");
			listener.Start();
			running = true;
			Task.Run(() => NewConnectionLoop());
			Task.Run(() => ClientLoop());
			Log("Server started.");
			return true;
		}

		public void Stop()
		{
			running = false;
			listener.Stop();
			tokenSource.Cancel();
		}

		private async void NewConnectionLoop()
		{
			while(running)
			{
				try
				{
					NetworkClient client = new NetworkClient(await listener.AcceptTcpClientAsync());
					_ = Task.Run(() => HandleClient(client));
				}
				catch (ObjectDisposedException) { }
			}
		}

		private async void ClientLoop()
		{
			while (running)
			{
				// We scan every second.
				await Task.Delay(TimeSpan.FromSeconds(1));
				bool shouldUpdate = false;

				foreach(NetworkClient client in clients.ToList())	// ToList() creates a copy so we can edit it
				{
					// If the clients are connected, do nothing
					if (client.IsConnected())
						continue;

					// Otherwise remove them and tell listeners about the change
					clients.Remove(client);
					Log($"{client.Name} disconnected from the server.");
					shouldUpdate = true;
				}

				if (shouldUpdate)
					UpdateClients();
			}
		}

		private async Task HandleClient(NetworkClient client)
		{
			while (running)
				HandleMessage(await client.GetMessageAsync(tokenSource.Token), client);
		}

		void HandleMessage(NetworkMessage message, NetworkClient client)
		{
			lock (this)
			{
				if (message is ChatMessage chatMessage)
					Log($"{client.Name} says, \"{chatMessage.Text}\"");
				else if(message is JoinMessage joinMessage)
				{
					client.Name = joinMessage.Name;
					clients.Add(client);
					UpdateClients();

					Log($"{client.Name} joined the server.");
				}
			}
		}

		private void UpdateClients()
		{
			string[] clientNames = clients.Select((c) => c.Name).ToArray();
			ClientsChanged.Invoke(clientNames);

			foreach (NetworkClient client in clients)
				Task.Run(() => client.SendMessageAsync(new UsersMessage() { Users = clientNames }, tokenSource.Token));
		}

		void Log(string text) => OutputLog.Invoke($"{DateTime.Now.ToLongTimeString()}: {text}");
	}
}
