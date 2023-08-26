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

namespace SerialPlotter
{
    public partial class SerialPlotter : Form
    {
        DataPacketRx dataPacketRx;
        DataPacketTx dataPacketTx;
        List<byte> payloadRxDataBytes;
        List<byte> payloadTxDataBytes;

        int counterTimer1 = 0;
        int counterTimer2 = 0;
        int counterTimer3 = 0;
        int counterTimer4 = 0;

        int stateMachine = 0;

        bool decodeCmd = false;
        byte receivedCmd = 0;
        byte receivedPayloadDataLength = 0;

        private string adcSerie = "adcSerie";


        public SerialPlotter()
        {
            InitializeComponent();
            InitializeComboBoxes();
            SetItemsToDisconnectedMode();
            ClearChart();

            dataPacketRx = new DataPacketRx(0xAA, 0x55);
            dataPacketTx = new DataPacketTx(0xAA, 0x55);
            payloadRxDataBytes = new List<byte>();
            payloadTxDataBytes = new List<byte>();
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
            baudrateCb.SelectedItem = "19200";
            dataBitsCb.SelectedItem = "8 bits";
            parityCb.SelectedItem = "None";
            stopBitsCb.SelectedItem = "1 bit";
            flowControlCb.SelectedItem = "None";
        }

        private void openBtn_Click(object sender, EventArgs e)
        {
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
            // timer2.Enabled = true;
        }

        private void DisableTimer()
        {
            timer1.Enabled = false;
            // timer2.Enabled = false;
        }

        private void ClearChart()
        {
            lineChart.Series[adcSerie].Points.Clear();
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
                        dataPacketRx.Append((byte) receivedByte);
                    }
                }
            }
            catch (TimeoutException)
            {
                MessageBox.Show("TimeoutException", "Houve uma excessão");
            }
        }

        private void startBtn_Click(object sender, EventArgs e)
        {
            payloadTxDataBytes.Clear();
            payloadTxDataBytes.Add((byte) DeviceSendData.Enable);

            dataPacketTx.SetCommand((byte) Commands.SetDeviceSendDataStatus);
            dataPacketTx.SetPayloadData(payloadTxDataBytes);
            dataPacketTx.Mount();
            dataPacketTx.SerialSend(serialPort);
        }

        private void stopBtn_Click(object sender, EventArgs e)
        {
            payloadTxDataBytes.Clear();
            payloadTxDataBytes.Add((byte) DeviceSendData.Disable);

            dataPacketTx.SetCommand((byte) Commands.SetDeviceSendDataStatus);
            dataPacketTx.SetPayloadData(payloadTxDataBytes);
            dataPacketTx.Mount();
            dataPacketTx.SerialSend(serialPort);
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            counterTimer1++;
            counterTimer2++;
            counterTimer3++;
            counterTimer4++;
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
                        //byte receivedCmd = dataPacketRx.GetCommand();
                        //byte receivedPayloadDataLength = dataPacketRx.GetPayloadDataLength();

                        receivedCmd = dataPacketRx.GetCommand();
                        receivedPayloadDataLength = dataPacketRx.GetPayloadDataLength();

                        if (receivedPayloadDataLength > 0)
                        {
                            payloadRxDataBytes.AddRange(dataPacketRx.GetPayloadData());
                        }

                        decodeCmd = true;
                        //dataPacketRx.Clear();
                    }
                    stateMachine = 2;
                    break;

                case 2:
                    if (decodeCmd == true)
                    {
                        /*
                        Console.WriteLine("receivedCmd: 0x" + receivedCmd.ToString("X2"));
                        Console.WriteLine("receivedPayloadDataLength: " + receivedPayloadDataLength.ToString());

                        String payloadDataBytesStr = "";
                        foreach (Byte b in payloadRxDataBytes)
                        {
                            payloadDataBytesStr += "0x" + b.ToString("X2") + " ";
                        }

                        Console.WriteLine("payloadRxDataBytes: " + payloadDataBytesStr);
                        Console.WriteLine();
                        */

                        if (receivedCmd == (byte) Commands.AdcReadValue)
                        {
                            int adcValue = ((payloadRxDataBytes[0] << 8) + payloadRxDataBytes[1]);
                            lineChart.Series[adcSerie].Points.Add(adcValue);
                        }

                        payloadRxDataBytes.Clear();
                        receivedPayloadDataLength = 0;
                        receivedCmd = 0;
                        decodeCmd = false;
                    }
                    stateMachine = 3;
                    break;

                case 3:
                    if (counterTimer2 >= (int) Delay._250ms)
                    {
                        counterTimer2 = 0;
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
            ClearChart();
        }
    }
}
