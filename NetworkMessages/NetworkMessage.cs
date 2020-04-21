using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

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
	}
}
