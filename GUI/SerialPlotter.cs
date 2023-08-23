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

        DataPacketTx dataPacketTx;
        List<byte> payloadTxDataBytes;

        public SerialPlotter()
        {
            InitializeComponent();
            InitializeComboBoxes();
            SetItemsToDisconnectedMode();

            dataPacketTx = new DataPacketTx(0xAA, 0x55);
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
            timer.Enabled = true;
        }

        private void DisableTimer()
        {
            timer.Enabled = false;
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
                    Console.Write("0x" + receivedByte.ToString("X2") + " ");
                    // bytesRecebidos.Add(byteRecebido);
                }
                Console.WriteLine();
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

        }
    }
}
