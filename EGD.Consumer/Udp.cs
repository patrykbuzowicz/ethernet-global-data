using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace EGD.Consumer
{
    class Udp
    {
        
        event EventHandler<DatagramReceivedEventArgs> DatagramReceived;
        DatagramReceivedEventArgs args;
        public UdpClient udpClient;
        event EventHandler<DatagramReceivedEventArgs> handler;
        public Byte[] receiveBytes;
        bool done;
        public IPEndPoint RemoteIpEndPoint;
        public Udp()
        {
            done = false;
        }
        public void Open(string address)
        {
            done = false;
            handler=null;
            udpClient = new UdpClient(4746);
            udpClient.Connect(address, 4746);
            RemoteIpEndPoint = new IPEndPoint(IPAddress.Any, 0);
            while(!done)
            {
                receiveBytes = udpClient.Receive(ref RemoteIpEndPoint);
                if(receiveBytes!=null)
                {
                args=new DatagramReceivedEventArgs(receiveBytes);
                handler = DatagramReceived;
                handler(this, args);
                handler = null;
                receiveBytes = null;
                }
            }
        }
        public void Close() 
        {
            done = true;
            udpClient.Close();
        }
}
    public class UdpObserver
    {
        public void HandleEvent(object sender, DatagramReceivedEventArgs args)
        {
            string str=System.Text.Encoding.UTF8.GetString(args.Bytes);
            Console.WriteLine("Datagram received " + str);
            //przekazanie ramki do egd
        }
    }
}
