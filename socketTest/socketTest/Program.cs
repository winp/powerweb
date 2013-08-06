using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;

namespace socketTest
{
	class Program
	{
		static Socket LocalSkt, RemoteSkt;
		static Dictionary<string, string> Data;

		static void Main(string[] args)
		{
			string cmdline;
			string[] cmd;
			char[] sep = new char[] {' ', ',', ';'};
			Data = new Dictionary<string, string>();

			Console.WriteLine("Socket Test, A low level TCP socket command interface");
			Console.WriteLine("Copyright (c), 2013. Subhajit Sahu.");
			Console.WriteLine("All rights reserved.\n");
			// Infinitely accept command lines until exit is entered
			LocalSkt = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
			while (true)
			{
				Console.Write("\ncmd> ");
				cmdline = Console.ReadLine();
				cmdline = cmdline.Trim();
				cmd = cmdline.Split(sep);
				if (cmd.Length == 0) continue;
				cmd[0] = cmd[0].ToLower();
				switch (cmd[0])
				{
					case "set":
						cmdSet(cmd);
						break;
					case "get":
						cmdGet(cmd);
						break;
					case "localname":
						cmdLocalName();
						break;
					case "address":
						cmdAddress(cmd);
						break;
					case "bind":
						cmdBind(cmd);
						break;
					case "listen":
						cmdListen();
						break;
					case "accept":
						cmdAccept();
						break;
					case "conn":
						cmdConn(cmd);
						break;
					case "send":
						cmdSend(cmd);
						break;
					case "recv":
						cmdRecv(cmd);
						break;
					case "disconn":
						cmdDisconn();
						break;
					case "exit":
						return;
				}
			}
		}
		static void cmdLocalName()
		{
			Console.WriteLine("localname: This computer\'s name is " + Dns.GetHostName());
		}
		static void cmdAddress(string[] cmd)
		{
			try
			{
				Console.WriteLine("address: {0}'s local addresses are: ", cmd[1]);
				foreach(IPAddress addr in Dns.GetHostAddresses(cmd[1]))
				{
					Console.WriteLine(addr);
				}
				Console.WriteLine();
			}
			catch(Exception)
			{
				Console.WriteLine("address: Invalid Host name");
			}
		}
		static void cmdBind(string[] cmd)
		{
			try
			{
				LocalSkt.Bind(new IPEndPoint(IPAddress.Parse(cmd[1]), int.Parse(cmd[2])));
			}
			catch(Exception)
			{
				Console.WriteLine("bind: Failed to bind address to local socket");
			}
		}
		static void cmdListen()
		{
			try
			{
				LocalSkt.Listen(16);
			}
			catch (Exception)
			{
				Console.WriteLine("listen: Failed to listen");
			}
		}
		static void cmdAccept()
		{
			Console.WriteLine("accept: Waiting for a connection...");
			try
			{
				if (RemoteSkt != null) RemoteSkt.Close();
				RemoteSkt = LocalSkt.Accept();
			}
			catch (Exception)
			{
				Console.WriteLine("accept: Failed to accept");
			}
		}
		static void cmdConn(string[] cmd)
		{
			if(RemoteSkt != null)
			{
				Console.WriteLine("conn: Already connected to a host");
				return;
			}
			try
			{
				LocalSkt = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
				LocalSkt.Connect(IPAddress.Parse(cmd[1]), int.Parse(cmd[2]));
				RemoteSkt = LocalSkt;
			}
			catch (Exception)
			{
				Console.WriteLine("conn: Failed to connect to host");
			}
		}
		static void cmdSend(string[] cmd)
		{
			if (RemoteSkt == null || RemoteSkt.Connected == false)
			{
				Console.WriteLine("send: Not connected to remote host");
				return;
			}
			try
			{
				byte[] msg = Encoding.ASCII.GetBytes(Data[cmd[1]]);
				RemoteSkt.Send(msg);
			}
			catch (Exception)
			{
				Console.WriteLine("send: Sending to remote host failed.");
			}
		}
		static void cmdRecv(string[] cmd)
		{
			if (RemoteSkt == null || RemoteSkt.Connected == false)
			{
				Console.WriteLine("recv: Not connected to remote host");
				return;
			}
			try
			{
				byte[] msg = new byte[RemoteSkt.Available];
				RemoteSkt.Receive(msg);
				Data[cmd[1]] = Encoding.ASCII.GetString(msg);
			}
			catch (Exception)
			{
				Console.WriteLine("recv: Recieveing from remote host failed.");
			}
		}
		static void cmdDisconn()
		{
			if (RemoteSkt == null)
			{
				Console.WriteLine("disconn: Alredy disconnected");
				return;
			}
			try
			{
				RemoteSkt.Disconnect(false);
				RemoteSkt = null;
				if (LocalSkt.Connected) LocalSkt.Disconnect(true);
			}
			catch (Exception)
			{
				Console.WriteLine("disconn: Failed to disconnect");
			}
		}
		static void cmdSet(string[] cmd)
		{
			try
			{
				Data[cmd[1]] = cmd[2];
			}
			catch(Exception)
			{
				Console.WriteLine("set: Error in setting variable value");
			}
		}
		static void cmdGet(string[] cmd)
		{
			try
			{
				Console.WriteLine(Data[cmd[1]]);
			}
			catch (Exception)
			{
				Console.WriteLine("get: Error in getting variable value");
			}
		}
	}
}
