using System;

namespace Gazette.NetworkMessages
{
	[Serializable]
	public class ChatMessage : NetworkMessage
	{
		public string Text;
	}
}
