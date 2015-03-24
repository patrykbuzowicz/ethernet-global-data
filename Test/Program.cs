using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using EGD.Consumer;

namespace Test
{
    class Program
    {
        private static string IpAddr
        {
            get { return "127.0.0.1"; }
        }

        private static int Port
        {
            get { return 18246; }
        }

        static UdpSender udp = new UdpSender();
        static IUdp udpServer = new Udp();

        static void Main(string[] args)
        {
            udpServer.Open(IpAddr);
            udp.Open(IpAddr, Port);
            udpServer.DatagramReceived += UdpServerOnDataReceived;

            while (true)
            {
                Console.WriteLine("type the data to send to the Udp Server");
                var input = Console.ReadLine();
                if (!string.IsNullOrEmpty(input))
                {
                    var data = Encoding.ASCII.GetBytes(input);
                    udp.Send(data);
                    Console.WriteLine("the data has been sent");
                }
                else
                {
                    Console.WriteLine("no input given");
                    break;
                }
            }

            Console.WriteLine("hit enter to leave");
            Console.ReadLine();

            udp.Close();
            udpServer.Close();
        }

        private static void UdpServerOnDataReceived(object sender, DatagramReceivedEventArgs binaryDataReceivedEventArgs)
        {
            Console.WriteLine("received data: \"{0}\"", Encoding.UTF8.GetString(binaryDataReceivedEventArgs.Bytes));
        }
    }
}
