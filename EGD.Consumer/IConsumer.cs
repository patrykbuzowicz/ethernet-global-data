using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace EGD.Consumer
{
    public interface IConsumer
    {
        void Open(string address);

        event EventHandler<DataReceivedEventArgs> DataReceived;

        void Close();
    }
}
