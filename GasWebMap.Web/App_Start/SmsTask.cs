using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Timers;
using System.Web;
using GasWebMap.Core;
using GasWebMap.Domain;
using GasWebMap.Services;
using Timer = System.Threading.Timer;

namespace GasWebMap.Web.App_Start
{
    public class SmsTask
    {
        private static  ILogger Logger = LogFactory.GetLogger(typeof(SmsTask));
        private SmsTask()
        {
            
        }

      
        protected static int SmsTime = 8;
        private static string SmsPort = "";
        public static void Start()
        {
            
            int s = 0;
            SmsTime = System.Configuration.ConfigurationManager.AppSettings["SmsTime"].ToInt(8);
            var dt = DateTime.Now;
            if (SmsTime > dt.Hour)
            {
                DateTime dt2 = new DateTime(dt.Year, dt.Month, dt.Day, SmsTime, 0, 0);
                s = (int) (dt2 - dt).TotalMilliseconds - 100;
            }
            else
            {
                DateTime dt3 = new DateTime(dt.Year, dt.Month, dt.Day+1, SmsTime, 0, 0);
                s = (int)(dt3 - dt).TotalMilliseconds - 100;
            }
            s = 1000*60;

            
            SmsPort =System.Configuration.ConfigurationManager.AppSettings["SMSport"];
            var oTimer = new Timer(new TimerCallback(objTimer_Elapsed),null,s,24*60*60*1000);
            
        }

        private static void objTimer_Elapsed(object sender)
        {
             SendSms();
        }

        private static  string CharacterToCoding(string character)
        {
            string coding = "";

            for (int i = 0; i < character.Length; i++)
            {
                byte[] bytes = System.Text.Encoding.Unicode.GetBytes(character.Substring(i, 1));

                //取出二进制编码内容  
                string lowCode = System.Convert.ToString(bytes[0], 16);

                //取出低字节编码内容（两位16进制）  
                if (lowCode.Length == 1)
                {
                    lowCode = "0" + lowCode;
                }

                string hightCode = System.Convert.ToString(bytes[1], 16);

                //取出高字节编码内容（两位16进制）  
                if (hightCode.Length == 1)
                {
                    hightCode = "0" + hightCode;
                }

                coding += (hightCode + lowCode);

            }

            return coding;
        }



        private static void SendSms()
        {
            clsSMS sms = new clsSMS();
            sms.OpenPort(SmsPort);

            //到期的
            SendSubSms(sms);
            //还有15天的
            SendSubSms(sms,15);
            //还有30天的
            SendSubSms(sms, 30);
            sms.ClosePort();
        }

        private static void SendSubSms(clsSMS sms,int days=0)
        {
            var rep = AppEx.Container.GetRepository<User>();
            var repUserRole = AppEx.Container.GetRepository<UserRole>();
            var repRole = AppEx.Container.GetRepository<Role>();
            var adminRole = repRole.GetEntity(t => t.Name == "管理员");
            var lstUserAdmin = repUserRole.GetEntities(t => t.RoleID == adminRole.Id).Select<UserRole, Guid>(t => t.UserID);
            var lstAdmin = rep.GetEntities(t => lstUserAdmin.Contains(t.Id));

            var rep2 = AppEx.Container.GetRepository<DeviceInfo>();

            var dt = DateTime.Now.Date.AddDays(days);
            var lstDev = rep2.GetEntities(t => t.ValidDate == dt);

            var lstDepart = lstDev.Distinct<DeviceInfo, Guid?>(t => t.DepartmentID);

            foreach (Guid? depart in lstDepart)
            {
                var ll = lstDev.Where(t => t.DepartmentID == depart.Value).Distinct(t => t.Name);

                string strContext = "";
                foreach (var device in ll)
                {
                    strContext += device + ",";
                }
                if (strContext.Length > 0)
                {
                    strContext = strContext.Substring(0, strContext.Length - 1);
                }
                var lst = rep.GetEntities(t => (t.SMS == true && t.DepartmentID == depart.Value)).ToList();
                foreach (var admin in lstAdmin)
                {
                    if (!lst.Contains(admin) && !admin.Telephone.IsNull())
                    {
                        lst.Add(admin);
                    }
                }
                strContext = string.Format("共有{2}台设备将在{1}日后到期：{0}。",strContext,days,ll.Count());
                SendSms2(sms, lst, strContext);

            }
           // sms.ClosePort();
        }

        private static void SendSms2(clsSMS sender, IList<User> users, string msg)
        {
            foreach (var user in users)
            {
                if (!user.Telephone.IsNull())
                {
                    bool bl = sender.sendMsg(user.Telephone, msg);
                    if (bl)
                    {
                        Logger.Info("短信猫状态：发送成功");
                    }
                    else
                    {
                        Logger.Error("短信猫状态：发送成功, 手机号 {0}，内容{1}", user.Telephone, msg);
                    }
                }
            }

        }
    }
}