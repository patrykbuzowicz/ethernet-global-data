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
    public class Udp: IUdp
    {
        
        public event EventHandler<DatagramReceivedEventArgs> DatagramReceived;
        DatagramReceivedEventArgs args;
        private UdpClient udpClient;
        private event EventHandler<DatagramReceivedEventArgs> handler;
        private Byte[] receiveBytes;
        private bool done;
        private IPEndPoint RemoteIpEndPoint;
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
            Thread Thr = new Thread(()=>ReceivingBytes());
            
        }
        private void ReceivingBytes(){
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
        public UdpObserver(Udp udpObservable)
        {
            udpObservable.DatagramReceived += HandleEvent;
        }
        private void HandleEvent(object sender, DatagramReceivedEventArgs args)
        {
            string str=System.Text.Encoding.UTF8.GetString(args.Bytes);
            Console.WriteLine("Datagram received " + str);
            //przekazanie ramki do egd
        }
    }
}
