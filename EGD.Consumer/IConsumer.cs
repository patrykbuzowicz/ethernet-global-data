using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;

namespace EGD.Consumer
{
    public interface IConsumer
    {
        void Open(string address);

        event EventHandler<DataReceivedEventArgs> DataReceived;
        void Close();
    }

    public class Udpconsumer : IConsumer
    {
        public UdpClient udpClient;
        public event EventHandler DatagramReceived;
        event EventHandler handler;
        public Byte[] receiveBytes;
        bool done = false;
        public void Open(string address)
        {
            done = false;
            handler=null;
            udpClient = new UdpClient(4746);
            udpClient.Connect(address, 4746);
            IPEndPoint RemoteIpEndPoint = new IPEndPoint(IPAddress.Any, 0);
            while(!done)
            {
                receiveBytes = udpClient.Receive(ref RemoteIpEndPoint);
                if(receiveBytes!=null)
                {
                handler = DatagramReceived;
                handler(this, EventArgs.Empty);
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
    public class ConsumerObserver
    {
        public void HandleEvent(object sender, EventArgs args)
        {
            Console.WriteLine("Datagram received " + ((Udpconsumer)sender).receiveBytes);
            //przekazanie ramki do egd
        }
    }
}
