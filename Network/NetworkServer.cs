﻿using Gazette.NetworkMessages;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;
using System.Threading.Tasks;

namespace Gazette.Network
{
	public class NetworkServer
	{
		bool running;
		TcpListener listener;
		CancellationTokenSource token = new CancellationTokenSource();
		Dictionary<TcpClient, string> clients = new Dictionary<TcpClient, string>();

		public event Action<string> OutputLog;
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
			Task.Run(() => Loop());
			return true;
		}

		async void Loop()
		{
			while(running)
			{
				try
				{
					TcpClient client = await listener.AcceptTcpClientAsync();
					_ = Task.Run(() => HandleClient(client, token.Token));
				}
				catch (ObjectDisposedException) { }
			}
		}

		public void Stop()
		{
			running = false;
			listener.Stop();
			token.Cancel();
		}

		async Task HandleClient(TcpClient client, CancellationToken token)
		{
			while(!token.IsCancellationRequested)
			{
				BinaryFormatter formatter = new BinaryFormatter();
				HandleMessage(await Task.Run(() => formatter.Deserialize(client.GetStream()), token) as NetworkMessage, client);
			}
		}

		void HandleMessage(NetworkMessage message, TcpClient client)
		{
			lock (this)
			{
				if (message is ChatMessage chatMessage)
					Log($"{clients[client]} says, \"{chatMessage.Text}\"");
				else if(message is JoinMessage joinMessage)
				{
					Log($"{joinMessage.UserID} joined the server.");
					clients.Add(client, joinMessage.UserID);
				}
			}
		}

		void Log(string text) => OutputLog.Invoke($"{DateTime.Now.ToLongTimeString()}: {text}");
	}
}
