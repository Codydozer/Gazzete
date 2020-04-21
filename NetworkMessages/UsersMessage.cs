using System;

namespace Gazette.NetworkMessages
{
	[Serializable]
	public class UsersMessage : NetworkMessage
	{
		public string[] Users;
	}
}
