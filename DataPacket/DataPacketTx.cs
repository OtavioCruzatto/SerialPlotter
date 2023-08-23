using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;

namespace SerialPlotter.DataPacket
{
    public class DataPacketTx
    {
        // BEGIN: Static attributes

        static private uint qtyOfDataPackets = 0;

        // END: Static attributes


        // BEGIN: General attributes

        private int dataPacketLength = 0;
        private byte payloadDataLength = 0;

        private List<byte> dataPacket;
        private List<byte> payloadData;

        private byte starter_1 = 0x00;
        private byte starter_2 = 0x00;
        private byte command = 0x00;
        private byte crc8 = 0x00;
        private bool valid = false;

        // END: General attributes


        // BEGIN: Constructor and destructor

        public DataPacketTx(byte starter_1, byte starter_2)
        {
            DataPacketTx.qtyOfDataPackets++;

            dataPacket = new List<byte>();
            payloadData = new List<byte>();

            this.SetStarter1(starter_1);
            this.SetStarter2(starter_2);
        }

        ~DataPacketTx()
        {
            DataPacketTx.qtyOfDataPackets--;
        }

        // END: Constructor and destructor


        // BEGIN: General methods

        public void Mount()
        {
            if ((this.starter_1 < 0x01) || (this.starter_1 > 0xfe))
            {
                throw new ApplicationException("Attribute 'starter_1' is not initialized.");
            }

            if ((this.starter_2 < 0x01) || (this.starter_2 > 0xfe))
            {
                throw new ApplicationException("Attribute 'starter_2' is not initialized.");
            }

            if ((this.command < 0x01) || (this.command > 0xfe))
            {
                throw new ApplicationException("Attribute 'command' is not initialized.");
            }

            this.dataPacket.Clear();
            this.dataPacket.Add(this.starter_1);
            this.dataPacket.Add(this.starter_2);
            this.dataPacket.Add(this.command);
            this.dataPacket.Add(this.payloadDataLength);

            if (this.payloadDataLength != 0)
            {
                this.dataPacket.AddRange(this.payloadData);
            }

            this.crc8 = Crc8.CalculatesCrc8(this.dataPacket);
            this.dataPacket.Add(this.crc8);
            this.dataPacketLength = this.dataPacket.Count;
            this.valid = true;
        }

        public void SerialSend(SerialPort serialPort)
        {
            if (this.valid == true)
            {
                byte[] disableReceiveAdcReads = this.dataPacket.ToArray();
                serialPort.Write(disableReceiveAdcReads, 0, disableReceiveAdcReads.Count());
            }
        }

        // END: General methods


        // BEGIN: Getters and setters methods

        public void SetStarter1(byte starter_1)
        {
            if ((starter_1 < 0x01) || (starter_1 > 0xfe))
            {
                throw new ArgumentOutOfRangeException("starter_1", "Argument value shall be between 1 (0x00) and 254 (0xfe).");
            }

            this.starter_1 = starter_1;
        }

        public void SetStarter2(byte starter_2)
        {
            if ((starter_2 < 0x01) || (starter_2 > 0xfe))
            {
                throw new ArgumentOutOfRangeException("starter_2", "Argument value shall be between 1 (0x00) and 254 (0xfe).");
            }

            this.starter_2 = starter_2;
        }

        public void SetCommand(byte command)
        {
            if ((command < 0x01) || (command > 0xfe))
            {
                throw new ArgumentOutOfRangeException("command", "Argument value shall be between 1 (0x00) and 254 (0xfe).");
            }

            this.command = command;
        }

        public void SetPayloadData(List<byte> payloadData)
        {
            if (payloadData.Count > 255)
            {
                throw new ArgumentOutOfRangeException("payloadData", "Argument cannot have more than 255 elements.");
            }

            this.payloadData = payloadData;
            this.payloadDataLength = (byte) payloadData.Count;
        }
        
        public byte GetStarter1()
        {
            return this.starter_1;
        }

        public byte GetStarter2()
        {
            return this.starter_2;
        }

        public byte GetCommand()
        {
            return this.command;
        }

        public List<byte> GetPayloadData()
        {
            return this.payloadData;
        }

        public byte GetPayloadDataLength()
        {
            return this.payloadDataLength;
        }

        public List<byte> GetDataPacket()
        {
            return this.dataPacket;
        }

        public int GetDataPacketLength()
        {
            return this.dataPacketLength;
        }

        public bool isValid()
        {
            return this.valid;
        }

        public byte GetCrc8()
        {
            return this.crc8;
        }

        public uint GetQtyOfPackets()
        {
            return DataPacketTx.qtyOfDataPackets;
        }

        // END: Getters and setters methods
    }
}
