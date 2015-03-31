using System;
using System.Net;

namespace UdpMcEchoClient
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			IPEndPoint mcEp = new IPEndPoint(IPAddress.Parse("224.0.0.1"),1338);
			ClientMcEchoUdp echoClient = new ClientMcEchoUdp(mcEp);
			Console.WriteLine ("Client crée…");

			IPEndPoint serveur = new IPEndPoint(IPAddress.Parse("10.42.43.2"),1337);
			echoClient.Envoyer ("Ceci est un nouveau message.", serveur);
		}
	}
}
