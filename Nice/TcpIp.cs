using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;

namespace Nice {
    class TcpIp {

        public void NetEntry(string ip, int port, string[] args)
        {
            //ConnectSocket(ip, port);

            if (args.Length == 0) {
                // If no server name is passed as argument to this program, use the current host name as the default.
                ip = Dns.GetHostName();
            } else {
                ip = args [0];
            }

            string result = SocketSendReceive(ip, port);
            Console.WriteLine(result);
        }

        private static Socket ConnectSocket(string server, int port)
        {
            Socket s = null;
            IPHostEntry hostEntry = null;

            // Get host related information.
            hostEntry = Dns.GetHostEntry(server);

            // Loop through the AddressList to obtain the supported AddressFamily. This is to avoid
            // an exception that occurs when the host IP Address is not compatible with the address family
            // (typical in the IPv6 case).
            foreach (IPAddress address in hostEntry.AddressList) {
                IPEndPoint ipe = new IPEndPoint(address, port);
                Socket tempSocket =
                    new Socket(ipe.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

                tempSocket.Connect(ipe);

                if (tempSocket.Connected) {
                    s = tempSocket;
                    break;
                } else {
                    continue;
                }
            }
            return s;
        }

        // This method requests the home page content for the specified server.
        private static string SocketSendReceive(string server, int port)
        {
            string request = "GET / HTTP/1.1\r\nHost: " + server +
                "\r\nConnection: Close\r\n\r\n";
            Byte [] bytesSent = Encoding.ASCII.GetBytes(request);
            Byte [] bytesReceived = new Byte [256];
            string page = "";

            // Create a socket connection with the specified server and port.
            using (Socket s = ConnectSocket(server, port)) {

                if (s == null) {
                    return ("Connection failed");
                }

                // Send request to the server.
                s.Send(bytesSent, bytesSent.Length, 0);

                // Receive the server home page content.
                int bytes = 0;
                page = "Default HTML page on " + server + ":\r\n";

                // The following will block until the page is transmitted.
                do {
                    bytes = s.Receive(bytesReceived, bytesReceived.Length, 0);
                    page = page + Encoding.ASCII.GetString(bytesReceived, 0, bytes);
                }
                while (bytes > 0);
            }

            return page;
        }

        static void Server(string ip, int port)
        {
            //创建Socket 相当于是通信的主机
            //监听打进来的电话，并转接给客服
            Socket serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            //绑定ip和端口
            serverSocket.Bind(new IPEndPoint(IPAddress.Parse(ip), port));  //绑定IP地址：端口 
            //EndPoint iped = IPEndPoint.Parse("192.168.100.150:1234");

            //监听有没有电话连接，顺便规划客服人数,如果是0就是无限个客服
            serverSocket.Listen(10);//设定最多10个排队连接请求

            //接电话
            //分配客服和客户进行一对一通信
            //阻塞方法 没有电话打进来，那么代码就执行到这里 Accept
            Socket clientScoket = serverSocket.Accept();
            System.Console.WriteLine("客户的本地端口是：" + clientScoket.LocalEndPoint.ToString());
            System.Console.WriteLine("打电话进来的客户端是：" + clientScoket.RemoteEndPoint.ToString());

            //接消息，需要一个消息的缓冲区
            byte [] readBuff = new byte [1024];

            while (true) {
                //消息拿到，并存放在缓冲区，并记住消息的长度
                int receiveCount = clientScoket.Receive(readBuff);
                string receiveMessage = Encoding.UTF8.GetString(readBuff, 0, receiveCount);
                System.Console.WriteLine("客户端发过来的消息：" + receiveMessage);

                //string backMessage = "我是你的专属客服，有什么可以帮助你的";
                clientScoket.Send(Encoding.UTF8.GetBytes("服务器已经接到你发来的消息：" + receiveMessage));
            }

        }

        //static void Client(string server, int port)
        //{
        //    string request = "GET / HTTP/1.1\r\nHost: " + server +
        //    "\r\nConnection: Close\r\n\r\n";
        //    Byte [] bytesSent = Encoding.ASCII.GetBytes(request);
        //    Byte [] bytesReceived = new Byte [256];
        //    string page = "";

        //    // Create a socket connection with the specified server and port.
        //    using (Socket s = ConnectSocket(server, port)) {

        //        if (s == null)
        //            return ("Connection failed");

        //        // Send request to the server.
        //        s.Send(bytesSent, bytesSent.Length, 0);

        //        // Receive the server home page content.
        //        int bytes = 0;
        //        page = "Default HTML page on " + server + ":\r\n";

        //        // The following will block until the page is transmitted.
        //        do {
        //            bytes = s.Receive(bytesReceived, bytesReceived.Length, 0);
        //            page = page + Encoding.ASCII.GetString(bytesReceived, 0, bytes);
        //        }
        //        while (bytes > 0);
        //    }

        //    return page;

        //}
    }
}
