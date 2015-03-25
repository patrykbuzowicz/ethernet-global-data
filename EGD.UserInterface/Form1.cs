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

using System.Net;

namespace EGD.UserInterface
{
    public partial class Form1 : Form
    {
        private RawByteParser Parser = new RawByteParser();

        private IConsumer consumer = Manager.GetConsumer();
        public Form1()
        {
            InitializeComponent();

            consumer.DataReceived += consumer_DataReceived;
        }

        void consumer_DataReceived(object sender, DataReceivedEventArgs e)
        {
            UpdateLastReceivedDate();
            UpdateOutput(e.Packet);
         //   UpdateFlags(e.Packet);
         //   UpdateNumber(e.Packet);
        }
        /*
        private void UpdateNumber(EgdPacket egdPacket)
        {
        }

        private void UpdateFlags(EgdPacket egdPacket)
        {
        }
        */
        private void UpdateOutput(EgdPacket egdPacket)
        {
            byte[] b = new byte[]{1,2,3,4};
            if (Parser.SetAndParseRawData(b))
        //    if (Parser.SetAndParseRawData(egdPacket.getPacket()))
            {
                this.textBox1.Text = Parser.getDWord();
                this.checkBox1.Checked = Parser.isOnAtIndex(0);
                this.checkBox2.Checked = Parser.isOnAtIndex(1);
                this.checkBox3.Checked = Parser.isOnAtIndex(2);
                this.checkBox4.Checked = Parser.isOnAtIndex(3);
                this.checkBox5.Checked = Parser.isOnAtIndex(4);
                this.checkBox6.Checked = Parser.isOnAtIndex(5);
                this.checkBox7.Checked = Parser.isOnAtIndex(6);
                this.checkBox8.Checked = Parser.isOnAtIndex(7);
                this.checkBox9.Checked = Parser.isOnAtIndex(8);
                this.checkBox10.Checked = Parser.isOnAtIndex(9);
                this.checkBox11.Checked = Parser.isOnAtIndex(10);
                this.checkBox12.Checked = Parser.isOnAtIndex(11);
                this.checkBox13.Checked = Parser.isOnAtIndex(12);
                this.checkBox14.Checked = Parser.isOnAtIndex(13);
                this.checkBox15.Checked = Parser.isOnAtIndex(14);
                this.checkBox16.Checked = Parser.isOnAtIndex(15);
                this.Refresh();
            };
            
        }

        private void UpdateLastReceivedDate()
        {
            lbLastUpdated.Text = string.Format("Last updated: {0}", DateTime.Now.ToString("HH:mm:ss.ffff"));
        }

        private void OpenConnectionButton_Click(object sender, EventArgs e)
        {
            consumer.Open(listBox1.SelectedItem.ToString());
        }

        private void CloseConnectionButton_Click(object sender, EventArgs e)
        {
            consumer.Close();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string name = Dns.GetHostName();
            var dnsAdresses = Dns.GetHostEntry(name).AddressList;

            listBox1.BeginUpdate();
            for (int x = 2; x < dnsAdresses.Length; x = x + 2) {
                listBox1.Items.Add(dnsAdresses[x]);
            }
            listBox1.EndUpdate();
        }
        private void listBox1_MouseLeave(object sender, EventArgs e)
        {
            //listBox1.Refresh();
        }
    }
}
