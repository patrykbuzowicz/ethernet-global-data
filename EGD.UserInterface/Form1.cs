using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EGD.Consumer;
using EGD.Consumer.Data;
using EGD.UserInterface.Utilities;

namespace EGD.UserInterface
{
    public partial class Form1 : Form
    {
        private RawByteParser Parser = new RawByteParser();

        private IConsumer consumer;
        public Form1()
        {
            InitializeComponent();

            consumer.DataReceived += consumer_DataReceived;
        }

        void consumer_DataReceived(object sender, DataReceivedEventArgs e)
        {
            UpdateLastReceivedDate();
            UpdateFlags(e.Packet);
            UpdateNumber(e.Packet);
        }

        private void UpdateNumber(EgdPacket egdPacket)
        {
            throw new NotImplementedException();
        }

        private void UpdateFlags(EgdPacket egdPacket)
        {
            throw new NotImplementedException();
        }

        private void UpdateLastReceivedDate()
        {
            lbLastUpdated.Text = string.Format("Last updated: {0}", DateTime.Now.ToString("HH:mm:ss.ffff"));
        }
    }
}
