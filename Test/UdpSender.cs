using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    class UdpSender
    {
        private UdpClient _client;

        public void Open(string addr, int port)
        {
            _client = new UdpClient();
            _client.Connect(new IPEndPoint(IPAddress.Parse(addr), port));
        }

        public void Send(byte[] data)
        {
            _client.Send(data, data.Length);
        }

        public void Close()
        {
            _client.Close();
        }
    }
}
