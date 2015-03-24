using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EGD.Consumer.Data;

namespace EGD.Consumer
{
    class MockConsumer : IConsumer
    {
        IUdp udpService = new Udp();

        public MockConsumer()
        {
            this.udpService.DatagramReceived += UdpServiceOnDatagramReceived;
        }

        private void UdpServiceOnDatagramReceived(object sender, DatagramReceivedEventArgs datagramReceivedEventArgs)
        {
            EventHandler<DataReceivedEventArgs> handler = this.DataReceived;

            if (handler != null)
            {
                EgdPacket packet = this.GetEgdPacket(datagramReceivedEventArgs.Bytes);
                handler(this, new DataReceivedEventArgs(packet));
            }
        }

        private EgdPacket GetEgdPacket(byte[] bytes)
        {
            return new EgdPacket
            {
                Data = bytes
            };
        }

        public void Open(string address)
        {
            this.udpService.Open(address);
        }

        public event EventHandler<DataReceivedEventArgs> DataReceived;

        public void Close()
        {
            this.udpService.Close();
        }
    }
}
