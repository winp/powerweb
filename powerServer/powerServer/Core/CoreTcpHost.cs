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
// The following code defines a WASP Host. 
namespace powerServer.Core
{
	class CoreTcpClient
	{
		Socket Conn;
		byte[] TxBuff, RxBuff;

		public CoreTcpClient(Socket client)
		{
			Conn = client;
			TxBuff = new byte[1024];
			RxBuff = new byte[1024];
		}
	}

	// CoreTcpHost Class
	// 
	// Creates a TCP Host (Server) for hosting back-end services on the desired port.
	// After the TCP Host is created, it serves to connecting clients asynchronously.
	// All "Non-Core TCP" clients are quickly disconnected after connection. However,
	// if a correct Core TCP client is connected, it may be kept connected depending
	// upon its type and the request it has made.
	class CoreTcpHost
	{
		// Properties of a Core TCP Host
		public Socket Conn;
		public List<CoreTcpClient> Clients;

		// Event control of Core TCP Hosts
		private Thread ThAcceptConn;
		// private ManualResetEvent EventConn = new ManualResetEvent(true);

		// Create a new Core TCP Host on the desired port
		public CoreTcpHost(int port)
		{
			// Create a TCP Socket bound to the desired port
			IPHostEntry IpEntry = Dns.GetHostEntry(Dns.GetHostName());
			IPAddress IpAddr = IpEntry.AddressList[2];
			IPEndPoint EndPoint = new IPEndPoint(IpAddr, port);
			Conn = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
			Conn.Bind(EndPoint);
			Conn.Listen(16);
			// Initialize client list
			Clients = new List<CoreTcpClient>();
			// Jump to a separate thread and wait for connections
			ThreadStart thStart = new ThreadStart(Thread_AcceptConn);
			ThAcceptConn = new Thread(thStart);
			ThAcceptConn.Start();
		}

		private void Thread_AcceptConn()
		{
			while (true)
			{
				EventConn.Reset();
				Conn.BeginAccept(new AsyncCallback(Clbk_AcceptConn), Conn);
			}
		}

		public static void Clbk_AcceptConn(IAsyncResult res)
		{

		}
	}
}
