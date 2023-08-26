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

        static private int qtyOfDataPackets = 0;

        static private int MIN_PAYLOAD_TX_DATA_BYTES = 0;
        static private int MAX_PAYLOAD_TX_DATA_BYTES = 255;
        static private int MIN_PACKET_TX_BYTES = 5;
        static private int MAX_PACKET_TX_BYTES = (MIN_PACKET_TX_BYTES + MAX_PAYLOAD_TX_DATA_BYTES);

        static private int QTY_PAYLOAD_TX_DATA_BYTES = 25;
        static private int QTY_PACKET_TX_BYTES = (MIN_PACKET_TX_BYTES + QTY_PAYLOAD_TX_DATA_BYTES);

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

        private bool valid;

        // END: General attributes


        // BEGIN: Constructor and destructor

        public DataPacketTx(byte starter_1, byte starter_2)
        {
            DataPacketTx.qtyOfDataPackets++;

            this.dataPacket = new byte[DataPacketTx.QTY_PACKET_TX_BYTES];
            this.payloadData = new byte[DataPacketTx.QTY_PAYLOAD_TX_DATA_BYTES];

            this.SetStarter1(starter_1);
            this.SetStarter2(starter_2);
            this.command = 0x00;
            this.payloadDataLength = 0;
            Array.Clear(this.payloadData, 0, DataPacketTx.QTY_PAYLOAD_TX_DATA_BYTES);
            this.crc8 = 0x00;
            Array.Clear(this.dataPacket, 0, DataPacketTx.QTY_PACKET_TX_BYTES);
            this.dataPacketLength = 0;
            this.valid = false;
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

            Array.Clear(this.dataPacket, 0, DataPacketTx.QTY_PACKET_TX_BYTES);

            this.dataPacket[0] = this.starter_1;
            this.dataPacket[1] = this.starter_2;
            this.dataPacket[2] = this.command;
            this.dataPacket[3] = this.payloadDataLength;
            Array.Copy(this.payloadData, 0, this.dataPacket, 4, this.payloadDataLength);
            this.dataPacketLength = this.payloadDataLength + 4 + 1;
            this.crc8 = Crc8.CalculatesCrc8(this.dataPacket, this.dataPacketLength - 1);
            this.dataPacket[this.payloadDataLength + 4] = this.crc8;
            this.valid = true;
        }

        public void SerialSend(SerialPort serialPort)
        {
            if (this.valid == true)
            {
                serialPort.Write(this.dataPacket, 0, this.dataPacketLength);
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

        public void SetPayloadData(byte[] payloadData, byte payloadDataLenght)
        {
            this.valid = false;
            Array.Copy(payloadData, this.payloadData, payloadDataLenght);
            this.payloadDataLength = payloadDataLenght;
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

        public int GetQtyOfPackets()
        {
            return DataPacketTx.qtyOfDataPackets;
        }

        public int GetQtyPayloadTxDataBytes()
        {
            return DataPacketTx.QTY_PAYLOAD_TX_DATA_BYTES;
        }
        
        // END: Getters and setters methods
    }
}
