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

        static private int MIN_PAYLOAD_RX_DATA_BYTES = 0;
        static private int MAX_PAYLOAD_RX_DATA_BYTES = 255;
        static private int MIN_PACKET_RX_BYTES = 5;
        static private int MAX_PACKET_RX_BYTES = (MIN_PACKET_RX_BYTES + MAX_PAYLOAD_RX_DATA_BYTES);

        static private int QTY_PAYLOAD_RX_DATA_BYTES = 25;
        static private int QTY_PACKET_RX_BYTES = (MIN_PACKET_RX_BYTES + QTY_PAYLOAD_RX_DATA_BYTES);

        // END: Static attributes


        // BEGIN: General attributes

        private byte starter_1;
        private byte starter_2;
        private byte command;
        private byte payloadDataLength;
        private byte[] payloadData;
        private byte crc8;

        private byte[] dataPacket;
        private int dataPacketLength;
        
        private int currentRxByteIndex = 0;
        private bool containsStarterBytes = false;
        private int starterBytesIndex = 0;

        private bool valid = false;

        // END: General attributes


        // BEGIN: Constructor and destructor

        public DataPacketRx(byte starter_1, byte starter_2)
        {
            this.dataPacket = new byte[DataPacketRx.QTY_PACKET_RX_BYTES];
            this.payloadData = new byte[DataPacketRx.QTY_PAYLOAD_RX_DATA_BYTES];

            this.SetStarter1(starter_1);
            this.SetStarter2(starter_2);
            this.command = 0x00;
            this.payloadDataLength = 0;
            Array.Clear(this.payloadData, 0, DataPacketRx.QTY_PAYLOAD_RX_DATA_BYTES);
            this.crc8 = 0x00;
            Array.Clear(this.dataPacket, 0, DataPacketRx.QTY_PACKET_RX_BYTES);
            this.dataPacketLength = 0;
            this.currentRxByteIndex = 0;
            this.containsStarterBytes = false;
            this.starterBytesIndex = 0;
            this.valid = false;

            DataPacketRx.qtyOfDataPackets++;
        }

        ~DataPacketRx()
        {
            DataPacketRx.qtyOfDataPackets--;
        }

        // END: Constructor and destructor


        // BEGIN: General methods

        public void Append(byte newByte)
        {
            this.dataPacket[this.currentRxByteIndex] = newByte;
            this.currentRxByteIndex++;
            this.dataPacketLength++;

            if (this.currentRxByteIndex >= DataPacketRx.QTY_PACKET_RX_BYTES)
            {
                this.Clear();
                this.currentRxByteIndex = 0;
                this.dataPacketLength = 0;
            }
        }

        public void Decode()
        {
            if (this.containsStarterBytes == true)
            {
                this.SetCommand(this.dataPacket[this.starterBytesIndex + 2]);
                this.payloadDataLength = this.dataPacket[this.starterBytesIndex + 3];

                if (this.payloadDataLength == 0)
                {
                    byte receivedCrc8 = this.dataPacket[this.starterBytesIndex + 4];
                    byte[] dataPacketWithoutCrc8 = new byte[DataPacketRx.QTY_PACKET_RX_BYTES];
                    Array.Copy(this.dataPacket, this.starterBytesIndex, dataPacketWithoutCrc8, 0, 4);
                    this.crc8 = Crc8.CalculatesCrc8(dataPacketWithoutCrc8, 4);

                    if (this.crc8 == receivedCrc8)
                    {
                        this.currentRxByteIndex = 0;
                        this.valid = true;
                    }
                }
                else
                {
                    byte receivedCrc8 = this.dataPacket[this.starterBytesIndex + this.payloadDataLength + 4];
                    byte[] dataPacketWithoutCrc8 = new byte[DataPacketRx.QTY_PACKET_RX_BYTES];
                    Array.Copy(this.dataPacket, this.starterBytesIndex, dataPacketWithoutCrc8, 0, this.payloadDataLength + 4);
                    this.crc8 = Crc8.CalculatesCrc8(dataPacketWithoutCrc8, this.payloadDataLength + 4);

                    if (this.crc8 == receivedCrc8)
                    {
                        this.SetPayloadData();
                        this.currentRxByteIndex = 0;
                        this.valid = true;
                    }
                }
            }
            else
            {
                if (this.dataPacketLength >= DataPacketRx.MIN_PACKET_RX_BYTES)
                {
                    for (int index = 0; index < this.dataPacketLength; index++)
                    {
                        if ((this.dataPacket[index] == this.starter_1) && (this.dataPacket[index + 1] == this.starter_2))
                        {
                            this.containsStarterBytes = true;
                            this.starterBytesIndex = index;
                            break;
                        }
                    }

                    if (this.containsStarterBytes == false)
                    {
                        this.Clear();
                    }
                }
            }
        }

        public void Clear()
        {
            this.valid = false;
            this.containsStarterBytes = false;
            this.currentRxByteIndex = 0;
            this.dataPacketLength = 0;
            this.payloadDataLength = 0;
            this.command = 0;
            this.crc8 = 0;
            this.starterBytesIndex = 0;
            Array.Clear(this.payloadData, 0, DataPacketRx.QTY_PAYLOAD_RX_DATA_BYTES);
            Array.Clear(this.dataPacket, 0, DataPacketRx.QTY_PACKET_RX_BYTES);
        }

        // END: General methods


        // BEGIN: Static methods

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

        private void SetCommand(byte command)
        {
            if ((command < 0x01) || (command > 0xfe))
            {
                throw new ArgumentOutOfRangeException("command", "Argument value shall be between 1 (0x00) and 254 (0xfe).");
            }

            this.command = command;
        }

        private void SetPayloadData()
        {
            if (this.payloadDataLength > DataPacketRx.QTY_PAYLOAD_RX_DATA_BYTES)
            {
                throw new ArgumentOutOfRangeException("payloadData", "Argument cannot have more than 'QTY_PAYLOAD_RX_DATA_BYTES' elements.");
            }

            this.valid = false;
            Array.Copy(this.dataPacket, this.starterBytesIndex + 4, this.payloadData, 0, this.payloadDataLength);
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

        public byte[] GetPayloadData()
        {
            return this.payloadData;
        }

        public byte GetPayloadDataLength()
        {
            return this.payloadDataLength;
        }

        public byte[] GetDataPacket()
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

        public int GetQtyPayloadRxDataBytes()
        {
            return DataPacketRx.QTY_PAYLOAD_RX_DATA_BYTES;
        }

        // END: Getters and setters methods
    }
}
