using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfTest.GUdpServer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// receive udp client opject
        /// </summary>
        public UdpClient ReceiveUdpClient;

        /// <summary>  
        /// 侦听端口名称  
        /// </summary>  
        public int PortNumber = 8080;

        /// <summary>  
        /// 本地地址  
        /// </summary>  
        public IPEndPoint LocalIPEndPoint;

        /// <summary>  
        /// 本地IP地址  
        /// </summary>  
        public IPAddress MyIPAddress;

        /// <summary>
        /// thread that listens to the message from udp
        /// </summary>
        private Thread listenThread;

        IPEndPoint receiveAddress;

        public MainWindow()
        {
            InitializeComponent();
            GetLocalIpAddress();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            tbIpAddress.Text = MyIPAddress.ToString();
            tbPortNumber.Text = PortNumber.ToString();
            receiveAddress = new IPEndPoint(IPAddress.Any, 0);
        }

        /// <summary>  
        /// 接收数据  
        /// </summary>  
        private void ReceiveData()
        {
            IPEndPoint local;

            try
            {
                local = new IPEndPoint(MyIPAddress, PortNumber);
                ReceiveUdpClient = new UdpClient(local);
            }
            catch(Exception ex)
            {
                local = null;
                this.Dispatcher.Invoke(new Action(() =>
                {
                    tbLog.Text = ex.ToString();
                }));
                return;
            }
            
            while (true)
            {
                try
                {
                    //关闭udpClient 时此句会产生异常  
                    byte[] receiveBytes = ReceiveUdpClient.Receive(ref receiveAddress);
                    string receiveMessage = Encoding.Default.GetString(receiveBytes, 0, receiveBytes.Length);

                    // update ui in a thread
                    this.Dispatcher.Invoke(new Action( () =>
                        {
                            tbLog.AppendText(string.Format("\n{0}来自{1}:{2}", DateTime.Now.ToString(), receiveAddress, receiveMessage));
                        }));
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    break;
                }
            }
        }

        /// <summary>
        /// get local ip address as the default server address
        /// </summary>
        private void GetLocalIpAddress()
        {
            //获取本机可用IP地址  
            IPAddress[] ips = Dns.GetHostAddresses(Dns.GetHostName());
            foreach (IPAddress ipa in ips)
            {
                if (ipa.AddressFamily == AddressFamily.InterNetwork)
                {
                    MyIPAddress = ipa;//获取本地IP地址  
                    break;
                }
            }
        }

        /// <summary>
        /// open server to receive data
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOpenServer_Click(object sender, RoutedEventArgs e)
        {
            if (!Int32.TryParse(tbPortNumber.Text, out PortNumber))
            {
                tbLog.Text = "port number format wrong, please rewrite it.";
                return;
            }

            if( listenThread == null || listenThread.ThreadState == ThreadState.Aborted )
            {
                //创建一个线程接收远程主机发来的信息  
                listenThread = new Thread(ReceiveData);
                listenThread.IsBackground = true;
                listenThread.Start();
                tbLog.Text = "server opened, listening now...";
            }
            else
            {
                tbLog.Text = String.Format("server can not be opened now, thread state is {0}", listenThread.ThreadState);
            }
        }

        /// <summary>
        /// close server
        /// close udp client to throw exception to abort the thread.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCloseServer_Click(object sender, RoutedEventArgs e)
        {
            if( listenThread != null && listenThread.ThreadState == ThreadState.Background )
            {
                ReceiveUdpClient.Close();
                listenThread.Abort();

                tbLog.Text = "closing now...";
                while (listenThread.ThreadState != ThreadState.Aborted)
                {
                    Thread.Sleep(10);
                }
                tbLog.Text = "server closed now";
            }
            else
            {
                tbLog.Text = "thread not running now!!";
            }
        }


    }
}
