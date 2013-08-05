using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Net;
using System.Net.Sockets;


// WASP
// Wirelessly Attached Sensor Protocol
//
// Wait, this is just a test code.
namespace powerServer.Core
{
	// WaspHost Class
	// 
	// Creates a Wasp Host (Server) for hosting back-end services on the UDP port 92 TCP port 93.
	// After Wasp Host is created, it serves to connecting clients asynchronously. However, if a
	// correct Wasp client is connected, it may be kept connected depending upon its type and the
	// request it has made.
	class WaspHost
	{
		// Properties of a Core TCP Host
		public Socket Conn;
		public List<Socket> Clients;

		// Event control of Core TCP Hosts
		private Thread ThAcceptConn;
		// private ManualResetEvent EventConn = new ManualResetEvent(true);

		// Create a new Core TCP Host on the desired port
		public WaspHost(int port)
		{
			// Createk a TCP Socket bound to the desired port
			IPHostEntry IpEntry = Dns.GetHostEntry(Dns.GetHostName());
			IPAddress IpAddr = IpEntry.AddressList[1];
			IPEndPoint EndPoint = new IPEndPoint(IpAddr, port);
			Conn = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
			Conn.Bind(EndPoint);
			if(port == 80) Conn.Listen(16);
			// Initialize client list
			Clients = new List<Socket>();
			// Jump to a separate thread and wait for connections
			if (port == 80)
			{
				ThreadStart thStart = new ThreadStart(Thread_AcceptConn);
				ThAcceptConn = new Thread(thStart);
				ThAcceptConn.Name = "AcceptConn";
				ThAcceptConn.Start();
			}
		}

		private void Thread_AcceptConn()
		{
			Socket client;
			// Accept incoming connections one after another
			while (true)
			{
				client = Conn.Accept();
				Console.WriteLine("Hi!");
				byte[] req = new byte[client.Available];
				client.Receive(req);
				string s_req = Encoding.ASCII.GetString(req);
				string s_msg = "Request Sent: \n" + s_req;
				byte[] msg = Encoding.ASCII.GetBytes(s_msg);
				client.Send(msg);
				client.Shutdown(SocketShutdown.Receive);
				// client.Disconnect(false);
				client.Close();
			}
			Console.WriteLine("Exited");
		}
	}
}
