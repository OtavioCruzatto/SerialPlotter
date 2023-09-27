using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.DataVisualization.Charting;
using SerialPlotter.DataPacket;
using System.IO.Ports;
using SerialPlotter.Misc;
using System.Windows.Forms;

namespace SerialPlotter.App
{
    public class App
    {
        private byte receivedCommand;
        private bool decodeReceivedCommand;

        private int adcValue;
        private int pointsCounter;

        private SerialPort serialPort;
        private Chart lineChart;
        private string adcSerie = "adcSerie";

        private int stateMachine;
        private int counterTimer1;

        private DataPacketTx dataPacketTx;
        private DataPacketRx dataPacketRx;
        private byte[] payloadTxDataBytes;
        private byte[] payloadRxDataBytes;

        private int lineChartMinX;
        private int lineChartMaxX;
        private int lineChartMinY;
        private int lineChartMaxY;

        public App(Chart lineChart, SerialPort serialPort)
        {
            this.receivedCommand = 0;
            this.decodeReceivedCommand = false;

            this.counterTimer1 = 0;
            this.stateMachine = 0;

            this.adcValue = 0;
            
            this.dataPacketTx = new DataPacketTx(0xAA, 0x55);
            this.dataPacketRx = new DataPacketRx(0xAA, 0x55);
            this.payloadTxDataBytes = new byte[this.dataPacketTx.GetQtyPayloadTxDataBytes()];
            this.payloadRxDataBytes = new byte[this.dataPacketRx.GetQtyPayloadRxDataBytes()];

            Array.Clear(this.payloadTxDataBytes, 0, this.dataPacketTx.GetQtyPayloadTxDataBytes());
            Array.Clear(this.payloadRxDataBytes, 0, this.dataPacketRx.GetQtyPayloadRxDataBytes());

            this.SetLineChart(lineChart);
            this.SetSerialPort(serialPort);

            this.lineChartMinX = (int) this.lineChart.ChartAreas[0].AxisX.Minimum;
            this.lineChartMaxX = (int) this.lineChart.ChartAreas[0].AxisX.Maximum;
            this.lineChartMinY = (int) this.lineChart.ChartAreas[0].AxisY.Minimum;
            this.lineChartMaxY = (int) this.lineChart.ChartAreas[0].AxisY.Maximum;

            this.pointsCounter = this.lineChartMinX;
        }

        public void AppendNewByte(int newByte)
        {
            this.dataPacketRx.Append((byte) newByte);
        }

        public void DecodeAndExecuteCommand()
        {
            switch (this.receivedCommand)
            {
                case ((byte) Commands.AdcReadValue):
                    this.adcValue = ((this.payloadRxDataBytes[0] << 8) + this.payloadRxDataBytes[1]);
                    this.lineChart.Series[this.adcSerie].Points.AddXY(this.pointsCounter, this.adcValue);
                    
                    this.pointsCounter++;
                    if (this.pointsCounter > this.lineChartMaxX)
                    {
                        this.pointsCounter = this.lineChartMinX;
                        this.ClearChart();
                    }
                    break;
            }
        }

        public void ClearDataPacketRx()
        {
            this.dataPacketRx.Clear();
        }

        public void SaveData()
        {
            int qty_of_data_points = this.lineChart.Series[this.adcSerie].Points.Count();

            if (qty_of_data_points > 1)
            {
                int[] x_values = new int[qty_of_data_points - 1];
                int[] y_values = new int[qty_of_data_points - 1];

                for (int i = 1; i < qty_of_data_points; i++)
                {
                    x_values[i - 1] = (int) this.lineChart.Series[this.adcSerie].Points[i].XValue;
                    y_values[i - 1] = (int) this.lineChart.Series[this.adcSerie].Points[i].YValues[0];
                }

                Csv csv = new Csv();
                csv.SetColumnData(x_values, "x_values");
                csv.SetColumnData(y_values, "y_values");
                csv.PrepareData();
                csv.SaveDataToFile();
                csv.Clear();
            }
        }

        public void LoadData()
        {
            Csv csv = new Csv();
            List<int[]> data = new List<int[]>();
            int qtyOfRows = 0;
            int qtyOfColumns = 0;

            data = csv.GetDataColumns();

            if (csv.GetDataReady() == true)
            {
                qtyOfColumns = csv.GetQtyOfColumns();
                qtyOfRows = csv.GetQtyOfRows();

                int[] x_values = new int[qtyOfRows];
                int[] y_values = new int[qtyOfColumns];

                x_values = data[0];
                y_values = data[1];

                this.ClearChart();

                for (int i = 0; i < x_values.Length; i++)
                {
                    this.lineChart.Series[this.adcSerie].Points.AddXY(x_values[i], y_values[i]);
                }
            }
            else
            {
                MessageBox.Show("Load error");
            }
            
        }

        public void StartDataAquisitionSendCommand()
        {
            Array.Clear(this.payloadTxDataBytes, 0, this.dataPacketTx.GetQtyPayloadTxDataBytes());
            this.payloadTxDataBytes[0] = ((byte) DeviceSendData.Enable);
            this.dataPacketTx.SetCommand((byte) Commands.SetDeviceSendDataStatus);
            this.dataPacketTx.SetPayloadData(payloadTxDataBytes, 1);
            this.dataPacketTx.Mount();
            this.dataPacketTx.SerialSend(this.serialPort);
        }

        public void StopDataAquisitionSendCommand()
        {
            Array.Clear(this.payloadTxDataBytes, 0, this.dataPacketTx.GetQtyPayloadTxDataBytes());
            this.payloadTxDataBytes[0] = ((byte) DeviceSendData.Disable);
            this.dataPacketTx.SetCommand((byte) Commands.SetDeviceSendDataStatus);
            this.dataPacketTx.SetPayloadData(payloadTxDataBytes, 1);
            this.dataPacketTx.Mount();
            this.dataPacketTx.SerialSend(this.serialPort);
        }

        public void ClearChart()
        {
            this.lineChart.Series[this.adcSerie].Points.Clear();
            this.lineChart.Series[this.adcSerie].Points.AddXY(this.lineChartMinX - 1, this.lineChartMinY);
            this.ResetPointsCounter();
        }

        public void ResizeChartAxisX(int xMin, int xMax)
        {
            if (xMax > xMin)
            {
                double interval = (xMax - xMin) / 20;
                if (interval >= 1)
                {
                    this.lineChart.ChartAreas[0].AxisX.Minimum = xMin;
                    this.lineChart.ChartAreas[0].AxisX.Maximum = xMax;

                    this.lineChart.ChartAreas[0].AxisX.MinorGrid.Interval = interval;
                    this.lineChart.ChartAreas[0].AxisX.MinorTickMark.Interval = interval;
                    this.lineChart.ChartAreas[0].AxisX.MajorGrid.Interval = interval;
                    this.lineChart.ChartAreas[0].AxisX.MajorTickMark.Interval = interval;
                    this.lineChart.ChartAreas[0].AxisX.Interval = interval;

                    this.lineChartMinX = (int)lineChart.ChartAreas[0].AxisX.Minimum;
                    this.lineChartMaxX = (int)lineChart.ChartAreas[0].AxisX.Maximum;

                    this.ClearChart();
                }
                else
                {
                    MessageBox.Show("Max-X - Min-X >= 20", "Invalid values...", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show("Max-X shall be greater than Min-X", "Invalid values...", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        public void ResizeChartAxisY(int yMin, int yMax)
        {
            if (yMax > yMin)
            {
                double interval = (yMax - yMin) / 10;
                if (interval >= 1)
                {
                    this.lineChart.ChartAreas[0].AxisY.Minimum = yMin;
                    this.lineChart.ChartAreas[0].AxisY.Maximum = yMax;

                    this.lineChart.ChartAreas[0].AxisY.MinorGrid.Interval = interval;
                    this.lineChart.ChartAreas[0].AxisY.MinorTickMark.Interval = interval;
                    this.lineChart.ChartAreas[0].AxisY.MajorGrid.Interval = interval;
                    this.lineChart.ChartAreas[0].AxisY.MajorTickMark.Interval = interval;
                    this.lineChart.ChartAreas[0].AxisY.Interval = interval;

                    this.lineChartMinY = (int)lineChart.ChartAreas[0].AxisY.Minimum;
                    this.lineChartMaxY = (int)lineChart.ChartAreas[0].AxisY.Maximum;

                    this.ClearChart();
                }
                else
                {
                    MessageBox.Show("Max-Y - Min-Y >= 10", "Invalid values...", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show("Max-Y shall be greater than Min-Y", "Invalid values...", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        public void IncrementCounterTimer1()
        {
            this.counterTimer1++;
        }

        public void ExecuteStateMachine()
        {
            switch (this.stateMachine)
            {
                case 0:
                    if (this.counterTimer1 >= (int) Delay._25ms)
                    {
                        this.dataPacketRx.Decode();
                        this.counterTimer1 = 0;
                    }
                    this.stateMachine = 1;
                    break;

                case 1:
                    if (this.dataPacketRx.isValid())
                    {
                        this.receivedCommand = this.dataPacketRx.GetCommand();
                        byte receivedPayloadDataLength = this.dataPacketRx.GetPayloadDataLength();

                        if (receivedPayloadDataLength > 0)
                        {
                            this.SetPayloadRxDataBytes(this.dataPacketRx.GetPayloadData(), receivedPayloadDataLength);
                        }

                        this.decodeReceivedCommand = true;
                        this.dataPacketRx.Clear();
                    }
                    this.stateMachine = 2;
                    break;

                case 2:
                    if (this.decodeReceivedCommand == true)
                    {
                        this.DecodeAndExecuteCommand();
                        this.decodeReceivedCommand = false;
                    }
                    this.stateMachine = 0;
                    break;

                default:
                    this.stateMachine = 0;
                    break;
            }
        }

        public bool LineChartContainsPoints()
        {
            if (this.lineChart.Series[this.adcSerie].Points.Count() > 1)
            {
                return true;
            }

            return false;
        }

        public void SetPayloadRxDataBytes(byte[] data, byte dataLength)
        {
            if (dataLength <= this.dataPacketRx.GetQtyPayloadRxDataBytes())
            {
                Array.Copy(data, this.payloadRxDataBytes, dataLength);
            }
        }

        public void ResetPointsCounter()
        {
            this.pointsCounter = this.lineChartMinX;
        }

        public void SetLineChart(Chart lineChart)
        {
            this.lineChart = lineChart;
        }

        public void SetSerialPort(SerialPort serialPort)
        {
            this.serialPort = serialPort;
        }
    }
}
