using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace Gazette.NetworkMessages
{
	[Serializable]
	public abstract class NetworkMessage
	{
		public byte[] Serialize()
		{
			using (MemoryStream stream = new MemoryStream())
			{
				new BinaryFormatter().Serialize(stream, this);
				return stream.ToArray();
			}
		}

		public void Send(TcpClient client)
		{
			byte[] messageBytes = Serialize();
			client.GetStream().Write(messageBytes, 0, messageBytes.Length);
		}
	}
}
