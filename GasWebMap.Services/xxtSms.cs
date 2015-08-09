using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Web;
using GasWebMap.Core;

namespace GasWebMap.Services
{
    public class clsSMS
    {
        private ILogger Log = LogFactory.GetLogger(typeof(clsSMS));
        private bool isOpend = false;
        private string portIndex ="";
        private uint baudrate = 9600;
        private myGSMModem gsm = null;
        #region Open and Close Ports
        //Open Port
        public bool   OpenPort(string p_PortName, uint p_uBaudRate = 9600)
        {

            baudrate = p_uBaudRate;
            portIndex = p_PortName;
            try
            {
                gsm = new myGSMModem(p_PortName, 9600);
                isOpend=gsm.OpenComm();
                return isOpend;

            }
            catch (Exception ex)
            {
                Log.Error("打开短信端口失败", ex);
                return false;
            }

        }

        //Close Port
        public void ClosePort()
        {
            try
            {
                if (gsm!=null)
                {
                    gsm.CloseComm();
                    gsm = null;
                }
                isOpend = false;
            }
            catch (Exception ex)
            {
                Log.Error("关闭短信设备失败", ex);
            }
        }

        #endregion



        #region Send SMS


        public bool sendMsg(string PhoneNo, string Message)
        {

            if (!isOpend)
            {
                Log.Error("端口打开失败");
               isOpend= OpenPort(portIndex, baudrate);
            }
            if (!isOpend)
            {
                return isOpend;
            }
            try
            {
                while(Message.Length>70)
                {
                    var m = Message.Substring(0, 70);
                    var bl = gsm.SendMsg(PhoneNo, m);
                    Message = Message.Remove(0, 70);
                }
                if (Message.Length>0)
                {
                    gsm.SendMsg(PhoneNo, Message);
                }
               return  true;
              
            }
            catch (Exception ex)
            {
                Log.Error("发送短信失败", ex);
                return false;
            }

        }
        #endregion

    }
}