using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace EGD.Consumer
{
    public class Udp : IUdp
    {

        public event EventHandler<DatagramReceivedEventArgs> DatagramReceived;
        DatagramReceivedEventArgs args;
        private UdpClient udpClient;
        private event EventHandler<DatagramReceivedEventArgs> handler;
        private Byte[] receiveBytes;
        private IPEndPoint RemoteIpEndPoint;
        private Thread _thr;
        private int Port { get { return 18246; } }
        public Udp()
        {
        }
        public void Open(string address)
        {
            handler = null;
            udpClient = new UdpClient(Port);
            RemoteIpEndPoint = new IPEndPoint(IPAddress.Any, 0);
            Console.WriteLine("udp open ");
            _thr = new Thread(ReceivingBytes);
            _thr.Start();

        }
        private void ReceivingBytes()
        {
            while (true)
            {
                receiveBytes = udpClient.Receive(ref RemoteIpEndPoint);
                if (receiveBytes != null)
                {
                    args = new DatagramReceivedEventArgs(receiveBytes);
                    handler = DatagramReceived;
                    handler(this, args);
                    handler = null;
                    receiveBytes = null;
                }
            }
        }
        public void Close()
        {
            _thr.Abort();
            udpClient.Close();
        }
    }
    public class UdpObserver
    {
        public UdpObserver(Udp udpObservable)
        {
            udpObservable.DatagramReceived += HandleEvent;
        }
        private void HandleEvent(object sender, DatagramReceivedEventArgs args)
        {
            string str = System.Text.Encoding.UTF8.GetString(args.Bytes);
            Console.WriteLine("Datagram received " + str);
            //przekazanie ramki do egd
        }
    }
}
