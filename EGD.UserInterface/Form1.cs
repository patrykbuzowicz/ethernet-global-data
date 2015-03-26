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
            if (Parser.SetAndParseRawData(egdPacket.Data)) 
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

        // Method responsible for opening UDP connection with selected (in listBox) IP.
        private void OpenConnectionButton_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem == null)
                MessageBox.Show("You have to choose IP address before trying to open connection");
            else 
            {
                consumer.Open(listBox1.SelectedItem.ToString());
                OpenConnetionButton.Enabled = false;
                CloseConnectionButton.Enabled = true;
            }            
        }

        // Method responsible for closing UDP connection.
        private void CloseConnectionButton_Click(object sender, EventArgs e)
        {
            consumer.Close();
            CloseConnectionButton.Enabled = false;
            OpenConnetionButton.Enabled = true;
        }

        // Method responsible for getting available IP addresses during first load of application.
        // Addresses are added to listBox1.
        private void Form1_Load(object sender, EventArgs e)
        {
            IPHostEntry host;
            string localIP;
            host = Dns.GetHostEntry(Dns.GetHostName());

            foreach (IPAddress ip in host.AddressList)
            {
                if (ip.AddressFamily.ToString() == "InterNetwork")
                {
                    localIP = ip.ToString();
                    listBox1.Items.Add(localIP);
                }
            }
            CloseConnectionButton.Enabled = false;
        }
    }
}
