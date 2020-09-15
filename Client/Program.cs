using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "TcpClient";
            Console.Write("Server Ip address: ");
            var serverIp = IPAddress.Parse(Console.ReadLine());

            Console.Write("Server Port: ");
            var serverPort = int.Parse(Console.ReadLine());

            var serverEndPoint = new IPEndPoint(serverIp, serverPort);

            var size = 1024;
            var receiveBuffer = new byte[size];

            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("# Text >>> ");
                Console.ResetColor();
                var text = Console.ReadLine();

                var socket = new Socket(SocketType.Stream, ProtocolType.Tcp);
                socket.Connect(serverEndPoint);

                var sendBuffer = Encoding.ASCII.GetBytes(text);

                socket.Send(sendBuffer);

                var length = socket.Receive(receiveBuffer);

                var result = Encoding.ASCII.GetString(receiveBuffer, 0, length);

                Array.Clear(receiveBuffer, 0, size);

                //socket.Shutdown(SocketShutdown.Receive);

                socket.Close();

                Console.WriteLine($">>> {result}");

            }


        }
    }
}
