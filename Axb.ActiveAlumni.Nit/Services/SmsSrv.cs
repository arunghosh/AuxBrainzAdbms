using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Web;
using Axb.ActiveAlumni.Nit.Entities;

namespace Axb.ActiveAlumni.Nit.Services
{
    public class SmsSrv: ServiceBase
    {
        readonly bool EN_SMS_NT = true;
        const string AUTH_KEY = "60852AxMRGms42529c86a1";
        const string SENDER_ID = "NITCAA";
        const string SMS_URI = "http://manage.fonematic.in/api/sendhttp.php?authkey=60852AxMRGms42529c86a1&mobiles=9025154840&message=Thiisisgood&sender=NITCAA&route=template";
        StringBuilder _postData;

        public SmsSrv(List<string> numbers, string msg)
        {
            Init(numbers, msg);
        }

        public SmsSrv(List<User> users, string msg)
        {
            var numbers = users.Where(s => !string.IsNullOrEmpty(s.MobileNumber)).Select(m => m.MobileNumber).ToList();
            Init(numbers, msg);
        }

        void Init(List<string> numbers, string msg)
        {
            var numCsv = string.Join(",", numbers);
            _postData = new StringBuilder();
            _postData.AppendFormat("authkey={0}", AUTH_KEY);
            _postData.AppendFormat("&mobiles={0}", 9544440104);
            _postData.AppendFormat("&message={0}", HttpUtility.UrlEncode(msg));
            _postData.AppendFormat("&sender={0}", SENDER_ID);
            _postData.AppendFormat("&route={0}", "template");
        }

        public void SendSMSAsync(Action<bool> callback = null)
        {
            if (EN_SMS_NT)
            {
                new Thread(new ThreadStart(() =>
                {
                    try
                    {
                        HttpWebRequest httpWReq = (HttpWebRequest)WebRequest.Create(SMS_URI);
                        UTF8Encoding encoding = new UTF8Encoding();
                        byte[] data = encoding.GetBytes(_postData.ToString());
                        httpWReq.Method = "POST";
                        httpWReq.ContentType = "application/x-www-form-urlencoded";
                        httpWReq.ContentLength = data.Length;
                        using (Stream stream = httpWReq.GetRequestStream())
                        {
                            stream.Write(data, 0, data.Length);
                        }

                        HttpWebResponse response = (HttpWebResponse)httpWReq.GetResponse();
                        StreamReader reader = new StreamReader(response.GetResponseStream());
                        string responseString = reader.ReadToEnd();
                        reader.Close();
                        response.Close();
                        if (callback != null) callback(true);
                    }
                    catch (SystemException)
                    {
                        if (callback != null) callback(false);
                    }
                }))
                .Start();
            }
        }
    }
}