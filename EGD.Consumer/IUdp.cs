using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;

namespace EGD.Consumer
{
    public interface IUdp
    {
        void Open(string address);

        event EventHandler<DatagramReceivedEventArgs> DatagramReceived;
        void Close();
    }

}
