using Gazette.NetworkMessages;
using System.Linq;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;
using System.Threading.Tasks;

namespace Gazette.Network
{
	public class NetworkClient
	{
		public string Name = "";

		private readonly TcpClient tcpClient;
		private readonly BinaryFormatter binaryFormatter = new BinaryFormatter();

		public NetworkClient(TcpClient tcpClient)
		{
			this.tcpClient = tcpClient;
		}

		public async Task<NetworkMessage> GetMessageAsync(CancellationToken token)
		{
			return await Task.Run(() => binaryFormatter.Deserialize(tcpClient.GetStream()), token) as NetworkMessage;
		}

		public async Task SendMessageAsync(NetworkMessage message, CancellationToken token)
		{
			byte[] messageBytes = message.Serialize();
			await tcpClient.GetStream().WriteAsync(messageBytes, 0, messageBytes.Length);
		}

		public bool IsConnected()
		{
			return IPGlobalProperties.GetIPGlobalProperties().GetActiveTcpConnections().SingleOrDefault(x => x.LocalEndPoint.Equals(tcpClient.Client.LocalEndPoint))?.State == TcpState.Established;
		}
	}
}
