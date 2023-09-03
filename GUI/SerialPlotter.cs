using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;
using SerialPlotter.DataPacket;
using SerialPlotter.App;

namespace SerialPlotter
{
    public partial class SerialPlotter : Form
    {
        DataPacketRx dataPacketRx;

        int counterTimer1 = 0;
        int stateMachine = 0;
        App.App serialPlotterApp;

        public SerialPlotter()
        {
            InitializeComponent();

            dataPacketRx = new DataPacketRx(0xAA, 0x55);

            serialPlotterApp = new App.App();
            serialPlotterApp.SetLineChart(lineChart);

            InitializeComboBoxes();
            SetItemsToDisconnectedMode();
            serialPlotterApp.ClearChart();
        }

        private void InitializeComboBoxes()
        {
            SetAvailablePorts();
            SetPredefinedItems();
        }

        private void SetAvailablePorts()
        {
            string[] ports = SerialPort.GetPortNames();
            comPortCb.Items.AddRange(ports);
        }

        private void SetPredefinedItems()
        {
            baudrateCb.SelectedItem = "115200";
            dataBitsCb.SelectedItem = "8 bits";
            parityCb.SelectedItem = "None";
            stopBitsCb.SelectedItem = "1 bit";
            flowControlCb.SelectedItem = "None";
        }

        private void openBtn_Click(object sender, EventArgs e)
        {
            dataPacketRx.Clear();

            try
            {
                if (comPortCb.Text != "")
                {
                    ConfigSerialPort();
                    serialPort.Open();

                    if (serialPort.IsOpen == true)
                    {
                        SetItemsToConnectedMode();
                        EnableTimer();
                    }
                    else
                    {
                        SetItemsToDisconnectedMode();
                        DisableTimer();
                    }

                }
                else
                {
                    MessageBox.Show("Please select a valid port...", "Connection failure");
                }
            }
            catch (UnauthorizedAccessException ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void EnableTimer()
        {
            timer1.Enabled = true;
        }

        private void DisableTimer()
        {
            timer1.Enabled = false;
        }

        private void ConfigSerialPort()
        {
            serialPort.PortName = comPortCb.Text;
            serialPort.BaudRate = Convert.ToInt32(baudrateCb.Text);

            switch (parityCb.SelectedItem)
            {
                case "None":
                    serialPort.Parity = Parity.None;
                    break;
                case "Even":
                    serialPort.Parity = Parity.Even;
                    break;
                case "Odd":
                    serialPort.Parity = Parity.Odd;
                    break;
                default:
                    serialPort.Parity = Parity.None;
                    break;
            }

            switch (dataBitsCb.SelectedItem)
            {
                case "7 bits":
                    serialPort.DataBits = 7;
                    break;
                case "8 bits":
                    serialPort.DataBits = 8;
                    break;
                default:
                    serialPort.DataBits = 8;
                    break;
            }

            switch (stopBitsCb.SelectedItem)
            {
                case "1 bit":
                    serialPort.StopBits = StopBits.One;
                    break;
                case "1.5 bit":
                    serialPort.StopBits = StopBits.OnePointFive;
                    break;
                case "2 bit":
                    serialPort.StopBits = StopBits.Two;
                    break;
                default:
                    serialPort.StopBits = StopBits.One;
                    break;
            }

            switch (flowControlCb.SelectedItem)
            {
                case "None":
                    serialPort.Handshake = Handshake.None;
                    break;
                case "Xon/Xoff":
                    serialPort.Handshake = Handshake.XOnXOff;
                    break;
                default:
                    serialPort.Handshake = Handshake.None;
                    break;
            }
        }

        private void SetItemsToConnectedMode()
        {
            openBtn.Enabled = false;
            comPortCb.Enabled = false;
            baudrateCb.Enabled = false;
            dataBitsCb.Enabled = false;
            parityCb.Enabled = false;
            stopBitsCb.Enabled = false;
            flowControlCb.Enabled = false;
            closeBtn.Enabled = true;
            startBtn.Enabled = true;
            stopBtn.Enabled = true;
            portStatusPb.Value = 100;
        }

        private void closeBtn_Click(object sender, EventArgs e)
        {
            dataPacketRx.Clear();

            if (serialPort.IsOpen)
            {
                serialPort.Close();

                if (serialPort.IsOpen == false)
                {
                    SetItemsToDisconnectedMode();
                    DisableTimer();
                }
            }
        }

        private void SetItemsToDisconnectedMode()
        {
            openBtn.Enabled = true;
            comPortCb.Enabled = true;
            baudrateCb.Enabled = true;
            dataBitsCb.Enabled = true;
            parityCb.Enabled = true;
            stopBitsCb.Enabled = true;
            flowControlCb.Enabled = true;
            closeBtn.Enabled = false;
            startBtn.Enabled = false;
            stopBtn.Enabled = false;
            portStatusPb.Value = 0;
        }

        private void serialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                while (serialPort.BytesToRead > 0)
                {
                    int receivedByte = serialPort.ReadByte();
                    if (receivedByte >= 0)
                    {
                        dataPacketRx.Append((byte)receivedByte);
                    }
                }
            }
            catch (TimeoutException)
            {
                MessageBox.Show("TimeoutException", "There was an exception.");
            }
        }

        private void startBtn_Click(object sender, EventArgs e)
        {
            serialPlotterApp.StartDataAquisitionSendCommand(serialPort);
        }

        private void stopBtn_Click(object sender, EventArgs e)
        {
            serialPlotterApp.StopDataAquisitionSendCommand(serialPort);
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            counterTimer1++;
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            switch (stateMachine)
            {
                case 0:
                    if (counterTimer1 >= (int) Delay._25ms)
                    {
                        dataPacketRx.Decode();
                        counterTimer1 = 0;
                    }
                    stateMachine = 1;
                    break;

                case 1:
                    if (dataPacketRx.isValid())
                    {
                        byte receivedCmd = dataPacketRx.GetCommand();
                        byte receivedPayloadDataLength = dataPacketRx.GetPayloadDataLength();

                        if (receivedPayloadDataLength > 0)
                        {
                            serialPlotterApp.SetData(dataPacketRx.GetPayloadData(), receivedPayloadDataLength);
                        }

                        serialPlotterApp.SetCommand(receivedCmd);
                        serialPlotterApp.SetDecodeCommandStatus(true);
                        dataPacketRx.Clear();
                    }
                    stateMachine = 2;
                    break;

                case 2:
                    if (serialPlotterApp.GetDecodeCommandStatus() == true)
                    {
                        serialPlotterApp.DecodeCommand();
                        serialPlotterApp.SetDecodeCommandStatus(false);
                    }
                    stateMachine = 0;
                    break;

                default:
                    stateMachine = 0;
                    break;
            }
        }

        private void clearBtn_Click(object sender, EventArgs e)
        {
            serialPlotterApp.ClearChart();
            serialPlotterApp.ResetPointsCounter();
        }
    }
}
