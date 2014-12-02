using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SFC_Tools.Classes
{
    public class MessageEventArgs : EventArgs
    {
        public string Message;//传递字符串信息
        public MessageEventArgs(string message)
        {
            this.Message = message;
        }
    }
    class SubThread
    {
        /*定义代理
         * 名称MessageEventHandler;
         * 参数
         * object 是发送者
         * MessageEventArgs是发送的信息
         */
        public delegate void MessageEventhandler(object sender, MessageEventArgs e);
        //定义事件
        public MessageEventhandler MessageSend;
        /*
         *说明：定义事件处理函数，当然这里也可以不用直接在引发事件时调用this.MessageSend(sender,e)
         *这里的参数要和代理的参数一样
         */
        public void OnMessageSend(object sender, MessageEventArgs e)
        {
            if (MessageSend != null)
            {
                this.MessageSend(sender,e);
            }
        }

        //定义一个线程
        public System.Threading.Thread sendThread;
        //线程处理函数，每秒像界面发送一次信息
        public void Sendding()
        {
            try
            {
                while (true)
                {
                    System.Threading.Thread.Sleep(200);
                    this.OnMessageSend(this, new MessageEventArgs(DateTime.Now.ToString()));
                }
            }
            catch (Exception ex)
            { }
        }

        public void StartSend()
        {
            sendThread = new System.Threading.Thread(new System.Threading.ThreadStart(Sendding));
            sendThread.Start();
        }
        public void EndSend()
        {
            sendThread.Abort();
        }
    }
}
