using EGD.Consumer.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EGD.Consumer
{
    public class DataReceivedEventArgs : EventArgs
    {
        public DataReceivedEventArgs(EgdPacket packet)
        {
            this.Packet = packet;
        }

        public EgdPacket Packet { get; internal set; }
    }
}
