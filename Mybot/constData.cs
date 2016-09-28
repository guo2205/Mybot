using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Mybot
{
    public static class constData
    {
        static public async Task<string> AsyncPost(string uri, string paramStr, Encoding encoding = null)
        {
            if (encoding == null)
                encoding = Encoding.UTF8;

            string result = string.Empty;

            WebClient wc = new WebClient();

            // 采取POST方式必须加的Header
            wc.Headers.Add("Content-Type", "application/x-www-form-urlencoded; charset=UTF-8");


            byte[] postData = encoding.GetBytes(paramStr);

            //if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password))
            //{
            //    wc.Credentials = GetCredentialCache(uri, username, password);
            //    wc.Headers.Add("Authorization", GetAuthorization(username, password));
            //}
            byte[] responseData = null;
            responseData = await wc.UploadDataTaskAsync(uri, "POST", postData); // 得到返回字符流
            return encoding.GetString(responseData);// 解码     
        }


        static public async Task<string> AsyncGet(string url, Encoding encoding)
        {
            if (encoding == null)
                encoding = Encoding.UTF8;

            string result = string.Empty;

            WebClient wc = new WebClient();

            byte[] responseData = await wc.DownloadDataTaskAsync(url); // 得到返回字符流
            return encoding.GetString(responseData);// 解码   
        }

        static public string Post(string uri, string paramStr, Encoding encoding = null)
        {
            if (encoding == null)
                encoding = Encoding.UTF8;

            string result = string.Empty;

            WebClient wc = new WebClient();

            // 采取POST方式必须加的Header
            wc.Headers.Add("Content-Type", "application/x-www-form-urlencoded; charset=UTF-8");


            byte[] postData = encoding.GetBytes(paramStr);

            //if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password))
            //{
            //    wc.Credentials = GetCredentialCache(uri, username, password);
            //    wc.Headers.Add("Authorization", GetAuthorization(username, password));
            //}
            byte[] responseData = null;
            responseData = wc.UploadData(uri, "POST", postData); // 得到返回字符流
            return encoding.GetString(responseData);// 解码     
        }


        static public string Get(string url, Encoding encoding)
        {
            if (encoding == null)
                encoding = Encoding.UTF8;

            string result = string.Empty;

            WebClient wc = new WebClient();

            byte[] responseData = wc.DownloadData(url); // 得到返回字符流
            return encoding.GetString(responseData);// 解码   
        }



    }
}