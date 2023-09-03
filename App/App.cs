using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.DataVisualization.Charting;
using SerialPlotter.DataPacket;
using System.IO.Ports;


namespace SerialPlotter.App
{
    public class App
    {
        static private int QTY_DATA_BYTES = 25;

        byte command;
        bool decodeCommandStatus;
        byte[] data;
        byte dataLength;

        int adcValue;
        int pointsCounter;

        Chart lineChart;
        private string adcSerie = "adcSerie";


        DataPacketTx dataPacketTx;
        byte[] payloadTxDataBytes;

        public App()
        {
            this.data = new byte[App.QTY_DATA_BYTES];

            this.command = 0;
            this.decodeCommandStatus = false;
            Array.Clear(this.data, 0, App.QTY_DATA_BYTES);
            this.dataLength = 0;

            this.adcValue = 0;
            this.pointsCounter = 0;

            this.dataPacketTx = new DataPacketTx(0xAA, 0x55);
            this.payloadTxDataBytes = new byte[this.dataPacketTx.GetQtyPayloadTxDataBytes()];
        }

        public void DecodeCommand()
        {
            switch (this.command)
            {
                case ((byte) Commands.AdcReadValue):
                    this.adcValue = ((this.data[0] << 8) + this.data[1]);
                    this.pointsCounter++;

                    if (this.pointsCounter > 500)
                    {
                        this.pointsCounter = 0;
                        this.ClearChart();
                    }

                    this.lineChart.Series[adcSerie].Points.AddXY(this.pointsCounter, this.adcValue);
                    break;
            }
        }

        public void StartDataAquisitionSendCommand(SerialPort serialPort)
        {
            Array.Clear(this.payloadTxDataBytes, 0, this.dataPacketTx.GetQtyPayloadTxDataBytes());
            this.payloadTxDataBytes[0] = ((byte) DeviceSendData.Enable);
            this.dataPacketTx.SetCommand((byte) Commands.SetDeviceSendDataStatus);
            this.dataPacketTx.SetPayloadData(payloadTxDataBytes, 1);
            this.dataPacketTx.Mount();
            this.dataPacketTx.SerialSend(serialPort);
        }

        public void StopDataAquisitionSendCommand(SerialPort serialPort)
        {
            Array.Clear(this.payloadTxDataBytes, 0, this.dataPacketTx.GetQtyPayloadTxDataBytes());
            this.payloadTxDataBytes[0] = ((byte) DeviceSendData.Disable);
            this.dataPacketTx.SetCommand((byte) Commands.SetDeviceSendDataStatus);
            this.dataPacketTx.SetPayloadData(payloadTxDataBytes, 1);
            this.dataPacketTx.Mount();
            this.dataPacketTx.SerialSend(serialPort);
        }

        public void ClearChart()
        {
            lineChart.Series[adcSerie].Points.Clear();
            lineChart.Series[adcSerie].Points.AddXY(-1, 0);
        }

        public void SetCommand(byte command)
        {
            this.command = command;
        }

        public byte GetCommand()
        {
            return this.command;
        }

        public void SetDecodeCommandStatus(bool status)
        {
            this.decodeCommandStatus = status;
        }

        public bool GetDecodeCommandStatus()
        {
            return this.decodeCommandStatus;
        }

        public void SetData(byte[] data, byte dataLength)
        {
            if (dataLength <= App.QTY_DATA_BYTES)
            {
                this.dataLength = dataLength;
                Array.Copy(data, this.data, dataLength);
            }
        }

        public int GetAdcValue()
        {
            return this.adcValue;
        }

        public int GetPointsCounter()
        {
            return this.pointsCounter;
        }

        public void ResetPointsCounter()
        {
            this.pointsCounter = 0;
        }

        public void SetLineChart(Chart lineChart)
        {
            this.lineChart = lineChart;
        }
    }
}
