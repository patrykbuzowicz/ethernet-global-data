using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EGD.Consumer
{
    public class Manager
    {
        public static IConsumer GetConsumer()
        {
            return new MockConsumer();
        }
    }
}
