using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DotCopter.GroundControl
{
    public partial class Form1 : Form
    {
        SerialPort serialPort = new SerialPort();
        public Form1()
        {
            InitializeComponent();
            serialPort.BaudRate = 115200;
            serialPort.ReceivedBytesThreshold = 12;
            serialPort.DataReceived += new SerialDataReceivedEventHandler(serialPort_DataReceived);
        }

        void serialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            byte[] buffer = new byte[serialPort.ReceivedBytesThreshold];
            while (serialPort.BytesToRead >= serialPort.ReceivedBytesThreshold)
            {
                serialPort.Read(buffer, 0, buffer.Length);
                SetTextBoxPitchText(BitConverter.ToSingle(buffer, 0).ToString());
                SetTextBoxRollText(BitConverter.ToSingle(buffer, 4).ToString());
                SetTextBoxYawText(BitConverter.ToSingle(buffer, 8).ToString());
            }
        }

        private void comboBox1_DropDown(object sender, EventArgs e)
        {
            comboBox1.Items.Clear();
            comboBox1.Items.AddRange(SerialPort.GetPortNames());

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(serialPort.IsOpen)
            {
                serialPort.Close();
                button1.Text = "Open";
            }
            else
            {
                serialPort.PortName = comboBox1.Text;
                serialPort.Open();
                button1.Text = "Close";
            }
        }

        private void comboBox1_TextChanged(object sender, EventArgs e)
        {
            if (comboBox1.Text == "" && !serialPort.IsOpen)
                button1.Enabled = false;
            else
                button1.Enabled = true;
        }

        private delegate void SetTextCallback(string text);
        private void SetTextBoxPitchText(string text)
        {
            if (textBoxPitch.InvokeRequired)
            {
                SetTextCallback callback = SetTextBoxPitchText;
                Invoke(callback, new object[] { text });
            }
            else
                textBoxPitch.Text = text;
        }
        private void SetTextBoxRollText(string text)
        {
            if (textBoxRoll.InvokeRequired)
            {
                SetTextCallback callback = SetTextBoxRollText;
                Invoke(callback, new object[] { text });
            }
            else
                textBoxRoll.Text = text;
        }
        private void SetTextBoxYawText(string text)
        {
            if (textBoxYaw.InvokeRequired)
            {
                SetTextCallback callback = SetTextBoxYawText;
                Invoke(callback, new object[] { text });
            }
            else
                textBoxYaw.Text = text;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }


        private void buttonPitchProportional_Click(object sender, EventArgs e)
        {
            if (!serialPort.IsOpen) return;
            byte[] buffer = new byte[5];
            buffer[0] = 0x01;
            BitConverter.GetBytes(float.Parse(textBoxPitchProportional.Text)).CopyTo(buffer,1);
            serialPort.Write(buffer,0,buffer.Length);
        }

        private void buttonPitchIntegral_Click(object sender, EventArgs e)
        {
            if (!serialPort.IsOpen) return;
            byte[] buffer = new byte[5];
            buffer[0] = 0x02;
            BitConverter.GetBytes(float.Parse(textBoxPitchIntegral.Text)).CopyTo(buffer, 1);
            serialPort.Write(buffer, 0, buffer.Length);
        }

        private void buttonPitchDerivative_Click(object sender, EventArgs e)
        {
            if (!serialPort.IsOpen) return;
            byte[] buffer = new byte[5];
            buffer[0] = 0x03;
            BitConverter.GetBytes(float.Parse(textBoxPitchDerivative.Text)).CopyTo(buffer, 1);
            serialPort.Write(buffer, 0, buffer.Length);
        }
    }
}
