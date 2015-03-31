using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Text;

namespace UdpMcEchoClient
{
	public class ClientMcEchoUdp
	{
		IPEndPoint mcEndPoint;
		UdpClient client = null;

		public ClientMcEchoUdp (IPEndPoint mcEndPoint)
		{
			byte prefix = mcEndPoint.Address.GetAddressBytes ()[0];
			prefix &= 240;
			if (prefix != 224)//if (addressBytes [0] < 224 | addressBytes [0] > 239)
				throw new Exception ("Adresse multicast doit avoir le préfixe 224.0.0.0/4 !");
			this.mcEndPoint = mcEndPoint;

			client = new UdpClient (mcEndPoint.Port);
			client.JoinMulticastGroup (mcEndPoint.Address);
			//client.Client.SetSocketOption(SocketOptionLevel.IP,SocketOptionName.AddMembership,new MulticastOption(mcEndPoint.Address,mcEndPoint.Port));
			Console.WriteLine ("Client abonné à {0}", mcEndPoint);
		}

		public void Envoyer(string msg, IPEndPoint serveur)
		{
			IPEndPoint epRcv = new IPEndPoint (IPAddress.Any, 0);
			byte[] tabEnvoi = UTF8Encoding.UTF8.GetBytes (msg);
			client.Send (tabEnvoi,tabEnvoi.Length,serveur);
			Console.WriteLine ("Envoyé \"" + msg+"\" à "+serveur.ToString());

			byte[] tabRcv = client.Receive (ref epRcv);
			Console.WriteLine("Reçu \""+ UTF8Encoding.UTF8.GetString(tabRcv)+"\" de "+epRcv.ToString());

			//client.Client.SetSocketOption(SocketOptionLevel.IP,SocketOptionName.DropMembership,new MulticastOption(mcEndPoint.Address,mcEndPoint.Port));
			client.DropMulticastGroup (mcEndPoint.Address);
		}


	}
}

