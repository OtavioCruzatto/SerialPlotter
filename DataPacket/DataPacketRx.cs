using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;

namespace SerialPlotter.DataPacket
{
    public class DataPacketRx
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

        public DataPacketRx(byte starter_1, byte starter_2)
        {
            DataPacketRx.qtyOfDataPackets++;

            dataPacket = new List<byte>();
            payloadData = new List<byte>();

            this.SetStarter1(starter_1);
            this.SetStarter2(starter_2);
        }

        ~DataPacketRx()
        {
            DataPacketRx.qtyOfDataPackets--;
        }

        // END: Constructor and destructor


        // BEGIN: General methods

        public void Decode(List<Byte> dataPacket)
        {
            if (dataPacket.Count > 260)
            {
                throw new ArgumentOutOfRangeException("dataPacket", "Argument cannot have more than 260 elements.");
            }

            if (dataPacket.Count < 5)
            {
                throw new ArgumentOutOfRangeException("dataPacket", "Argument cannot have less than 5 elements.");
            }

            if (DataPacketRx.CheckCrc(dataPacket) == true)
            {
                this.SetStarter1(dataPacket[0]);
                this.SetStarter2(dataPacket[1]);
                this.SetCommand(dataPacket[2]);
                byte receivedPayloadDataLength = dataPacket[3];

                if (receivedPayloadDataLength != 0)
                {
                    List<Byte> payloadDataBytesAux = new List<Byte>();
                    payloadDataBytesAux = dataPacket.GetRange(4, dataPacket.Count - 4 - 1);
                    this.SetPayloadData(payloadDataBytesAux);
                }

                this.crc8 = dataPacket.Last();
                this.dataPacketLength = dataPacket.Count;
                this.dataPacket = dataPacket;

                if (this.payloadDataLength != receivedPayloadDataLength)
                {
                    throw new ArgumentOutOfRangeException("dataPacket", "Quantity of payload data bytes incorrect.");
                }

                this.valid = true;
            }
            else
            {
                throw new ApplicationException("Invalid given CRC. Note: CRC8 (poly 0x07)");
            }
        }

        // END: General methods


        // BEGIN: Static methods

        private static bool CheckCrc(List<Byte> dataPacket)
        {
            Byte receivedCrc8 = dataPacket.Last();
            Byte calculatedCrc8 = Crc8.CalculatesCrc8(dataPacket.GetRange(0, dataPacket.Count - 1));
            return receivedCrc8 == calculatedCrc8;
        }

        // END: Static methods


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
            return DataPacketRx.qtyOfDataPackets;
        }

        // END: Getters and setters methods
    }
}
