using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;



namespace SerialPort
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            cmdClose.Enabled = false;
            foreach (String s in System.IO.Ports.SerialPort.GetPortNames()) 
            {
                txtPort.Items.Add(s);
            }
        }

        public System.IO.Ports.SerialPort sport;

        public void serialport_connect(String port, int baudrate , Parity parity, int databits, StopBits stopbits) 
        {
            DateTime dt = DateTime.Now;
            String dtn = dt.ToShortTimeString();

            sport = new System.IO.Ports.SerialPort(
            port, baudrate, parity, databits, stopbits);
            try
            {
                sport.Open();
                cmdClose.Enabled = true;
                cmdConnect.Enabled = false;
                txtReceive.AppendText("[" + dtn + "] " + "Connected\n");
                sport.DataReceived += new SerialDataReceivedEventHandler(sport_DataReceived);
            }
            catch (Exception ex) { MessageBox.Show(ex.ToString(), "Error"); }
        }

        private void sport_DataReceived(object sender, SerialDataReceivedEventArgs e) 
        {
            
            MethodInvoker mi = new MethodInvoker(this.test);
            this.BeginInvoke(mi, null);

        }

        private void test( )
        {
            DateTime dt = DateTime.Now;
            String dtn = dt.ToShortTimeString();
            txtReceive.AppendText("[" + dtn + "] " + "Received: " + sport.ReadExisting() + "\n");
        }

        private void cmdConnect_Click(object sender, EventArgs e)
        {
            String port = txtPort.Text;
            int baudrate = Convert.ToInt32(cmbbaudrate.Text);
            Parity parity = (Parity)Enum.Parse(typeof(Parity), cmbparity.Text);
            int databits = Convert.ToInt32(cmbdatabits.Text);
            StopBits stopbits = (StopBits)Enum.Parse(typeof(StopBits), cmbstopbits.Text);
            
            serialport_connect(port, baudrate, parity, databits, stopbits);
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DateTime dt = DateTime.Now;
            String dtn = dt.ToShortTimeString();
            String data = txtDatatoSend.Text;
            sport.Write(data);
            txtReceive.AppendText("[" + dtn + "] " + "Sent: " + data + "\n");
        }

        private void cmdClose_Click_1(object sender, EventArgs e)
        {
            DateTime dt = DateTime.Now;
            String dtn = dt.ToShortTimeString();

            if (sport.IsOpen) 
            {
                sport.Close();
                cmdClose.Enabled = false;
                cmdConnect.Enabled = true;
                txtReceive.AppendText("[" + dtn + "] " + "Disconnected\n");
            }
        }
        
    }
}
