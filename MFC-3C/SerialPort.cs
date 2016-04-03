using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;


namespace MFC_3C
{
    class MySerialPort
    {
        private SerialPort serialPort;

        public void ClosePort()
        {
            serialPort.Close();

        }
        public bool OpenComport(int nPortNum, int nBaudRate, StringBuilder errMsg)
        {
            try
            {
                serialPort = new SerialPort("COM" + nPortNum.ToString(), nBaudRate);
                serialPort.Encoding = Encoding.Default;
                serialPort.Parity = Parity.None;
                serialPort.DataBits = 8;
                serialPort.StopBits = StopBits.One;
                serialPort.DataReceived += new SerialDataReceivedEventHandler(DateReceiveCallback);
                serialPort.Open();
            }
            catch( Exception e )
            {
                Console.WriteLine(e.Message);
                errMsg.Append(e.Message);
                return false;
            }
            return true;
        }
        public bool SendData(byte[] data, StringBuilder errMsg)
        {
            try
            {
                serialPort.Write(data, 0, data.Length);
            }
            catch( Exception e )
            {
                Console.WriteLine(e.Message);
                errMsg.Append(e.Message);
                return false;
            }
            return true;            
        }
        public void DateReceiveCallback(object sender, SerialDataReceivedEventArgs e)
        {

        }



    }
}
