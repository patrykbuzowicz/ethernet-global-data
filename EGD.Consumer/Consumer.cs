using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using EGD.Consumer.Data;

namespace EGD.Consumer
{
    class EGDFrame //ramka protokołu EGD
    {

        public ushort PDUTypeVersion
        {
            get;
            set;
        }
        public ushort RequestID
        {
            get;
            set;
        }
        public ulong ProducerID
        {
            get;
            set;
        }
        public ulong ExchangeID
        {
            get;
            set;
        }
        public ulong TimeStampSec
        {
            get;
            set;
        }
        public ulong TimeStampNanoSec
        {
            get;
            set;
        }
        public ulong Status
        {
            get;
            set;
        }
        public ulong ConfigSignature
        {
            get;
            set;

        }
        public ulong Reserved
        {
            get;
            set;
        }
        public Byte[] ProductionData
        {
            get;
            set;
        }
        public EGDFrame()
        {
            ProductionData = new Byte[1460]; //rozmiar ramki EGD
        }

    }

    static class functions
    {
        public static string DecToBin2(int number) // zamiana liczby dziesiętnej (typu int) na postać binarna
        {
            string result = "";
            do
            {
                if ((number & 1) == 0)
                    result += "0";
                else
                    result += "1";

                number >>= 1;
            } while (number != 0);
            int n = result.Length;
            StringBuilder sb = new StringBuilder(result);
            for (int i = 0; i < result.Length; i++)
            {
                sb[n - i - 1] = result[i];
            }
            result = sb.ToString();
            return result;
        }

        public static int BinToDec(string number)  //zamiana ciągu znaków do postaci binarnej
        {
            int result = 0, pow = 1;
            for (int i = number.Length - 1; i >= 0; --i, pow <<= 1)
                result += (number[i] - '0') * pow;

            return result;
        }

        public static long BinToLong(string number) //zamiana ciągu znaków do liczby typu long
        {
            int result = 0, pow = 1;
            for (int i = number.Length - 1; i >= 0; --i, pow <<= 1)
                result += (number[i] - '0') * pow;
            return result;
        }

        public static string DecToBin(Byte n)  //zamiana jednego bajtu danych do ciągu znaków
        {
            string nToString = n.ToString();
            int tmp = int.Parse(nToString);
            string result = DecToBin2(tmp);
            if (result.Length == 8)
            {
                return result;
            }
            else
            {
                string t = System.String.Empty;
                for (int i = 0; i <= 8 - result.Length - 1; i++)
                {
                    t += "0";
                }
                result = t + result;
                return result;
            }
        }
    }


    class Consumer : IConsumer
    {

        public void Open(string address)
        {
            (new Udp()).Open(address);
            Thread thr = new Thread(TransformData);
            thr.Start();
        }
        private void TransformData()
        {
            EgdPacket Packet = new EgdPacket();
            EGDFrame A = new EGDFrame();
            DataReceivedEventArgs tab = new DataReceivedEventArgs(Packet);
            byte[] wsk = tab.Packet.Data;
            while (true)
            {
                if((tab.Packet.Data.Length % 1460) == 0)
                {
                    A.PDUTypeVersion = (ushort)(functions.BinToDec(functions.DecToBin(wsk[wsk.Length - 1460]) + functions.DecToBin(wsk[wsk.Length - 1460 + 1])));
                    A.RequestID = (ushort)(functions.BinToDec(functions.DecToBin(wsk[wsk.Length - 1460 + 2]) + functions.DecToBin(wsk[wsk.Length - 1460 + 3])));
                    A.ProducerID = (ulong)(functions.BinToLong(functions.DecToBin(wsk[wsk.Length - 1460 + 4]) + functions.DecToBin(wsk[wsk.Length - 1460 + 5]) + functions.DecToBin(wsk[wsk.Length - 1460 + 6]) + functions.DecToBin(wsk[wsk.Length - 1460 + 7]) + functions.DecToBin(wsk[wsk.Length - 1460 + 8]) + functions.DecToBin(wsk[wsk.Length - 1460 + 9]) + functions.DecToBin(wsk[wsk.Length - 1460 + 10]) + functions.DecToBin(wsk[wsk.Length - 1460 + 11])));
                    A.ExchangeID = (ulong)(functions.BinToLong(functions.DecToBin(wsk[wsk.Length - 1460 + 12]) + functions.DecToBin(wsk[wsk.Length - 1460 + 13]) + functions.DecToBin(wsk[wsk.Length - 1460 + 14]) + functions.DecToBin(wsk[wsk.Length - 1460 + 15]) + functions.DecToBin(wsk[wsk.Length - 1460 + 16]) + functions.DecToBin(wsk[wsk.Length - 1460 + 17]) + functions.DecToBin(wsk[wsk.Length - 1460 + 18]) + functions.DecToBin(wsk[wsk.Length - 1460 + 19])));
                    A.TimeStampSec = (ulong)(functions.BinToLong(functions.DecToBin(wsk[wsk.Length - 1460 + 20]) + functions.DecToBin(wsk[wsk.Length - 1460 + 21]) + functions.DecToBin(wsk[wsk.Length - 1460 + 22]) + functions.DecToBin(wsk[wsk.Length - 1460 + 23]) + functions.DecToBin(wsk[wsk.Length - 1460 + 24]) + functions.DecToBin(wsk[wsk.Length - 1460 + 25]) + functions.DecToBin(wsk[wsk.Length - 1460 + 26]) + functions.DecToBin(wsk[wsk.Length - 1460 + 27])));
                    A.TimeStampNanoSec = (ulong)(functions.BinToLong(functions.DecToBin(wsk[wsk.Length - 1460 + 28]) + functions.DecToBin(wsk[wsk.Length - 1460 + 29]) + functions.DecToBin(wsk[wsk.Length - 1460 + 30]) + functions.DecToBin(wsk[wsk.Length - 1460 + 31]) + functions.DecToBin(wsk[wsk.Length - 1460 + 32]) + functions.DecToBin(wsk[wsk.Length - 1460 + 33]) + functions.DecToBin(wsk[wsk.Length - 1460 + 34]) + functions.DecToBin(wsk[wsk.Length - 1460 + 35])));
                    A.Status = (ulong)(functions.BinToLong(functions.DecToBin(wsk[wsk.Length - 1460 + 36]) + functions.DecToBin(wsk[wsk.Length - 1460 + 37]) + functions.DecToBin(wsk[wsk.Length - 1460 + 38]) + functions.DecToBin(wsk[wsk.Length - 1460 + 39]) + functions.DecToBin(wsk[wsk.Length - 1460 + 40]) + functions.DecToBin(wsk[wsk.Length - 1460 + 41]) + functions.DecToBin(wsk[wsk.Length - 1460 + 42]) + functions.DecToBin(wsk[wsk.Length - 1460 + 43])));
                    A.ConfigSignature = (ulong)(functions.BinToLong(functions.DecToBin(wsk[wsk.Length - 1460 + 44]) + functions.DecToBin(wsk[wsk.Length - 1460 + 45]) + functions.DecToBin(wsk[wsk.Length - 1460 + 46]) + functions.DecToBin(wsk[wsk.Length - 1460 + 47]) + functions.DecToBin(wsk[wsk.Length - 1460 + 48]) + functions.DecToBin(wsk[wsk.Length - 1460 + 49]) + functions.DecToBin(wsk[wsk.Length - 1460 + 50]) + functions.DecToBin(wsk[wsk.Length - 1460 + 51])));
                    A.Reserved = (ulong)(functions.BinToLong(functions.DecToBin(wsk[wsk.Length - 1460 + 52]) + functions.DecToBin(wsk[wsk.Length - 1460 + 53]) + functions.DecToBin(wsk[wsk.Length - 1460 + 54]) + functions.DecToBin(wsk[wsk.Length - 1460 + 55]) + functions.DecToBin(wsk[wsk.Length - 1460 + 56]) + functions.DecToBin(wsk[wsk.Length - 1460 + 57]) + functions.DecToBin(wsk[wsk.Length - 1460 + 58]) + functions.DecToBin(wsk[wsk.Length - 1460 + 59])));
                    for (int m = 0; m < 1400; m++)
                    {
                        A.ProductionData[m] = wsk[wsk.Length - 1460 + m + 60];
                    }
                }
            }

        }
        public event EventHandler<DataReceivedEventArgs> DataReceived;
        public void Close()
        {
            (new Udp()).Close();
        }
    }
}
