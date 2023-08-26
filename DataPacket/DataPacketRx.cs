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

        static private int min_payload_rx_data_bytes = 0;
        static private int max_payload_rx_data_bytes = 255;
        static private int min_packet_rx_bytes = 5;
        static private int max_packet_rx_bytes = (min_packet_rx_bytes + max_payload_rx_data_bytes);

        static private int qty_payload_rx_data_bytes = 25;
        static private int qty_packet_rx_bytes = (min_packet_rx_bytes + qty_payload_rx_data_bytes);

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

        private bool containsStarterBytes = false;
        private int starterBytesIndex = 0;

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

        public void Append(byte newByte)
        {
            this.dataPacket.Add(newByte);
            this.dataPacketLength++;

            if (this.dataPacket.Count > DataPacketRx.qty_packet_rx_bytes)
            {
                this.dataPacket.Clear();
                this.dataPacketLength = 0;
            }
        }

        public void Decode()
        {
            if (this.containsStarterBytes == true)
            {
                if (this.containsStarterBytes == true)
                {
                    this.SetCommand(this.dataPacket[this.starterBytesIndex + 2]);
                    this.payloadDataLength = this.dataPacket[this.starterBytesIndex + 3];

                    if (this.payloadDataLength == 0)
                    {
                        byte receivedCrc8 = this.dataPacket[this.starterBytesIndex + 4];
                        this.crc8 = Crc8.CalculatesCrc8(this.dataPacket.GetRange(this.starterBytesIndex, 4));

                        if (this.crc8 == receivedCrc8)
                        {
                            this.valid = true;
                        }
                    }
                    else
                    {
                        byte receivedCrc8 = this.dataPacket[this.starterBytesIndex + this.payloadDataLength + 4];
                        this.crc8 = Crc8.CalculatesCrc8(this.dataPacket.GetRange(this.starterBytesIndex, this.payloadDataLength + 4));

                        if (this.crc8 == receivedCrc8)
                        {
                            this.SetPayloadData(this.dataPacket.GetRange(this.starterBytesIndex + 4, this.payloadDataLength));
                            this.valid = true;
                        }
                    }
                }
            }
            else
            {
                if (this.dataPacket.Count >= DataPacketRx.min_packet_rx_bytes)
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
                        this.dataPacket.Clear();
                        this.dataPacketLength = 0;
                    }
                }
            }
        }

        public void Clear()
        {
            this.valid = false;
            this.containsStarterBytes = false;
            this.dataPacketLength = 0;
            this.payloadDataLength = 0;
            this.command = 0;
            this.crc8 = 0;
            this.starterBytesIndex = 0;
            this.dataPacket.Clear();
            this.payloadData.Clear();
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
