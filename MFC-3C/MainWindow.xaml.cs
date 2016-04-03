using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MFC_3C
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ObservableCollection<string> comportList = new ObservableCollection<string>();
        private ObservableCollection<int> baudList = new ObservableCollection<int>();
        MySerialPort serialComm = new MySerialPort();
        public MainWindow()
        {
            InitializeComponent();

            //COM Number 콤보 박스 초기화        
            comboBox_comPortList.ItemsSource = comportList;
            for (int i = 1; i <= 10; i++)
                comportList.Add("COM" + i.ToString());
            comboBox_comPortList.SelectedIndex = 0;
            ///////////////////////////////////////////////////////////////////

            //Baud 콤보 박스 초기화
            comboBox_baudList.ItemsSource = baudList;
            baudList.Add(9600);
            baudList.Add(19200);
            baudList.Add(57600);
            baudList.Add(115200);
            comboBox_baudList.SelectedIndex = 0;
        }

        private void click_connection(object sender, RoutedEventArgs e)
        {
            if ( btn_openPort.Content.ToString() == "포트 열기" )
            {
                int nNum = comboBox_comPortList.SelectedIndex + 1;
                int nBaud = baudList[comboBox_baudList.SelectedIndex];
                StringBuilder errMsg = new StringBuilder();
                if (serialComm.OpenComport(nNum, nBaud, errMsg))
                {
                    btn_openPort.Content = "포트 닫기";
                }
                else
                    MessageBox.Show(errMsg.ToString());
            }
            else
            {
                serialComm.ClosePort();
                btn_openPort.Content = "포트 열기";
            }

            


        }
        private byte[] StringToByte(string str)
        {
            byte[] StrByte = Encoding.UTF8.GetBytes(str);
            return StrByte;
        }

        /*
       1. VF=0 [ 어느 위치에 있어도 원점으로 복귀 ]
       2. VF=1 [ 1번 렌즈 650 ]
       3. VF=2 [ 2번 렌즈 750 ]
       4. VF=3 [ 3번 렌즈 850 ]
       5. r [ 순차적 렌즈 회전 ]
      */

        private void SendData(string data)
        {
            StringBuilder errMsg = new StringBuilder();
            if (serialComm.SendData(StringToByte(data), errMsg) == false)
            {
                MessageBox.Show(errMsg.ToString());
            }
        }
        private void click_home(object sender, RoutedEventArgs e)
        {
            SendData("VF=0\r");
        }

        private void click_Lens1(object sender, RoutedEventArgs e)
        {
            SendData("VF=2\r"); 
        }

        private void click_Lens2(object sender, RoutedEventArgs e)
        {
            SendData("VF=1\r");
        }

        private void click_Lens3(object sender, RoutedEventArgs e)
        {
            SendData("VF=3\r");
        }

        private void click_NextLens(object sender, RoutedEventArgs e)
        {
            SendData("r\r");
        }
    }
}
