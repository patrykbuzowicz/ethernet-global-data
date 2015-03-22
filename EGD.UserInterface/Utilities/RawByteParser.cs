using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*  Parser takes 4 bytes and converts 2 first of them into a "binary" Sting with 16 characters    
 *  2 last are converted into one List of Booleans (true: 1 was recieved; false: 2 was received)
 * 
 * input methods: 
 *  setProductionData(List<byte>) -- raw byte data from EGD
 *  -- returns true if List has 4 elements
 *  --        false otherwise
 *  parseBytes()                   -- parse previously set List
 *  SetAndParseData(List<byte>)    -- combination of those two above
 *  
 * output mrthods:
 * getDWord()                      -- get String representation of first 2 bytes
 * getIntDword()                   -- get unsigned int representation of first 2 bytes
 * getCheckboxList()               -- get full list of Booleans -- representation of last 2 bytes
 * isOnAtIndex(int index)          -- get boolean value at index place of CheckboxList
 * */

namespace EGD.UserInterface.Utilities
{
    class RawByteParser
    {
        private List<byte> EgdPacketProductionData;
        public const int byte_amount = 4;
        public const int byte_size = 8;
        private String DWord;
        private List<Boolean> CheckboxList;
        private /*const*/ int checkboxListSize;

        // construtors
        public RawByteParser()
        {
            this.checkboxListSize = 2 * RawByteParser.byte_size * sizeof(byte);
            this.CheckboxList = new List<Boolean>(this.checkboxListSize);
            this.EgdPacketProductionData = new List<byte>(RawByteParser.byte_amount);
            DWord = "";
        }
        // test only
        public RawByteParser(int t)
            : this()
        {
            this.setDefault();
        }

        public void setDefault()
        {
            Byte[] dummy = new Byte[1];
            Random rnd = new Random();
            for (int i = 0; i < RawByteParser.byte_amount; i++)
            {
                rnd.NextBytes(dummy); ;
                this.EgdPacketProductionData.Add(dummy[0]);
            }
        }

        // gets
        public String getDWord()
        {
            return this.DWord;
        }

        public int getIntDword()
        {
            return (this.DWord == null || ("Error").Equals(this.DWord)) ? 0 :
                Convert.ToUInt16(this.DWord, 2);
        }

        public List<Boolean> getCheckboxList()
        {
            return this.CheckboxList;
        }

        public bool isOnAtIndex(int index)
        {
            if (index < 0 || this.CheckboxList.Count < index)
            {
                return false;
            }
            return this.CheckboxList.ElementAt(index);
        }

        // set input raw data
        public Boolean setProductionData(List<byte> newList)
        {
            if (newList.Count != RawByteParser.byte_amount)
            {
                return false;
            }
            this.EgdPacketProductionData = newList;
            return true;
        }

        // parse raw data
        public void parseBytes() {
            String afterParse;
            this.CheckboxList = new List<Boolean>();
            DWord = "";
            if (this.EgdPacketProductionData == null || this.EgdPacketProductionData.Count != RawByteParser.byte_amount)
            {
                DWord = "Error";
                for (int i = 0; i < this.checkboxListSize; i++)
                {
                   this.CheckboxList.Add(false);   
                }
            }
            else
            {
                for (int i = 0; i < this.EgdPacketProductionData.Count; i++)
                {
                    if (i < 2)
                    {
                       afterParse = Convert.ToString(this.EgdPacketProductionData.ElementAt(i), 2);
                       for (int j = 0; j < byte_size - afterParse.Length; j++)
                       {
                           this.DWord = this.DWord + "0";
                       }
                       this.DWord = this.DWord + Convert.ToString(this.EgdPacketProductionData.ElementAt(i), 2);
                    }
                    else {
                        for (int j = 0; j < RawByteParser.byte_size * sizeof(byte); j++)
                        {
                            this.CheckboxList.Add((this.EgdPacketProductionData.ElementAt(i) & (128 >> j )) != 0);  // crude but effective
                        }
                    }
                }
            }
          //  Console.WriteLine(DWord +" parsing ended"); -- some logger could be useful
        }

        // insert and parse raw data
        public Boolean SetAndParseData(List<byte> newList)
        {
            if (this.setProductionData(newList))
            {
                this.parseBytes();
                return true;
            }
            return false;
        }
    }
}
