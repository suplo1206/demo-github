using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Tcp Server";

            var localIp = IPAddress.Any;
            var localPort = 1308;
            var localEndPoint = new IPEndPoint(localIp, localPort);

            var listener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            listener.Bind(localEndPoint);

            listener.Listen(10);

            Console.WriteLine($"Local socket bind to {localEndPoint}. Waiting for request...");
            var size = 1024;
            var receiveBuffer = new byte[size];

            while (true)
            {
                var socket = listener.Accept();
                Console.WriteLine($"Accepted connection form {localEndPoint}");

                var length = socket.Receive(receiveBuffer);

                socket.Shutdown(SocketShutdown.Receive);
                var text = Encoding.ASCII.GetString(receiveBuffer, 0, length);
                Console.WriteLine($"Received: {text}");
                var sendBuffer = Encoding.ASCII.GetBytes(text.ToUpper());
                socket.Send(sendBuffer);

                Console.WriteLine($"Sent: {text.ToUpper()}");

                socket.Shutdown(SocketShutdown.Send);

                Console.WriteLine($"Closing connection form {socket.RemoteEndPoint}");
                socket.Close();

                Array.Clear(receiveBuffer, 0, size);

            }
        }
    }
}
