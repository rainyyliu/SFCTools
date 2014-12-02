using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.Threading;
using SFC_Tools.Classes;

using System.Net;
using System.Net.Sockets;

namespace SFC_Tools.Forms
{
    public partial class ucMultiThreadCommunicate : ucParentControl
    {
        private SubThread _subThread;
        private const int port = 8000;
        // 客户端多了一些线程的控制标识，为了在需要的时候控制线程
        private static ManualResetEvent connectDone = new ManualResetEvent(false);
        private static ManualResetEvent sendDone = new ManualResetEvent(false);
        private static ManualResetEvent receiveDone = new ManualResetEvent(false);

        private static String response = String.Empty;
        private static void StartClient()
        {
            try
            {
                //这里还是一样
                //IPHostEntry ipHostInfo = Dns.Resolve("host.contoso.com");
                //IPAddress ipAddress = ipHostInfo.AddressList[0];
                IPAddress ipAddress = IPAddress.Parse("192.168.137.1");
                IPEndPoint remoteEP = new IPEndPoint(ipAddress, port);
                Socket client = new Socket(AddressFamily.InterNetwork,
                    SocketType.Stream, ProtocolType.Tcp);

                // 不同的地方开始在这里，不过也是先连接，同时要定义好连接完毕后执行的操作
                client.BeginConnect(remoteEP,
                    new AsyncCallback(ConnectCallback), client);
                //等到连接成功后再继续执行
                connectDone.WaitOne();

                // 发送数据至服务器端，是先发送，与服务器端的先接收不同
                Send(client, "This is a test<EOF>");
                sendDone.WaitOne();//也需要等待

                // 接收服务器端发送的数据
                Receive(client);
                receiveDone.WaitOne();

               // Console.WriteLine("Response received : {0}", response);
                MessageBox.Show(response);

                client.Shutdown(SocketShutdown.Both);
                client.Close();

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
        private static void ConnectCallback(IAsyncResult ar)
        {
            try
            {
                Socket client = (Socket)ar.AsyncState;
                client.EndConnect(ar);
                connectDone.Set();
            }
            catch (Exception ex)
            { 
                
            }
        }

        private static void Send(Socket client,string data)
        {
            byte[] bData = System.Text.Encoding.Default.GetBytes(data);
            client.BeginSend(bData, 0, bData.Length, 0, new AsyncCallback(SendCallback), client);
        }
        private static void SendCallback(IAsyncResult ar)
        {
            Socket client = (Socket)ar.AsyncState;
            int nSend = client.EndSend(ar);
            sendDone.Set();
        }

        private static void Receive(Socket client)
        {
            try
            {
                StateObject state = new StateObject();
                state.workSocket = client;
                client.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0, new AsyncCallback(ReceiveCallback), state);
            }
            catch (Exception ex)
            { 
            }
        }

        private static void ReceiveCallback(IAsyncResult ar)
        {
            StateObject state = (StateObject)ar.AsyncState;
            Socket client = state.workSocket;
            int iRead = client.EndReceive(ar);
            if (iRead > 5)
            {
                state.sb.Append(System.Text.Encoding.ASCII.GetString(state.buffer), 0, iRead);
                client.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0, new AsyncCallback(ReceiveCallback), state);
            }
            else
            {
                if (state.sb.Length > 1)
                {
                    response = state.sb.ToString();
                }
                receiveDone.Set();
            }
        }




        public delegate void MessageHandler(MessageEventArgs e);
        public ucMultiThreadCommunicate()
        {
            InitializeComponent();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            _subThread.StartSend();
        }

        private void ucMultiThreadCommunicate_Load(object sender, EventArgs e)
        {
            this._subThread = new SubThread();
            this._subThread.MessageSend += new SubThread.MessageEventhandler(this._subThread_MessageSend);
        }

        private void Message(MessageEventArgs e)
        {
            this.lbThreadInfo.Items.Add(e.Message);
        }

        private void _subThread_MessageSend(object sender,MessageEventArgs e)
        {
            MessageHandler handler = new MessageHandler(Message);
            this.Invoke(handler, new object[] { e});
        }

        private void btnEnd_Click(object sender, EventArgs e)
        {
            _subThread.EndSend();
        }

        private void btnSockTest_Click(object sender, EventArgs e)
        {
            StartClient();
        }

    }

    public class StateObject
    {
        public Socket workSocket = null;
        public const int BufferSize = 1024;
        public byte[] buffer = new byte[BufferSize];
        public StringBuilder sb = new StringBuilder();
    }

}
