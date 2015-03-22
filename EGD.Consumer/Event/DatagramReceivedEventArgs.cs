using EGD.Consumer.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EGD.Consumer
{
    public class DatagramReceivedEventArgs : EventArgs
    {
        public DatagramReceivedEventArgs(Byte[] receiveBytes)
        {
            this.Bytes = receiveBytes;

        }
        public Byte[] Bytes{get; internal set;}
    }
}
