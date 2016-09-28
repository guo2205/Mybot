using System.IO;
using System.Net;
using System.Text;

public class TuLingTool
{
    public static string postdata(string str, string userid, int roleid)
    {
        string url = "http://www.tuling123.com/openapi/api";
        //string key = "2585370322d24d759f5dfe9c7dfb3ede";
        string key;
        switch (roleid)
        {
            case 0:
                key = "05b14814e3888bc141cbc066dd577768";//王鑫华
                break;
            case 1:
                key = "ccbb9b948dbd40aa92fcdca79410f3fc";//王洛灵
                break;
            default:
                key = "";
                break;
        }
        MyJson.JsonNode_Object json = new MyJson.JsonNode_Object();
        json.SetDictValue("key", key);
        json.SetDictValue("info", get_uft8(str));
        json.SetDictValue("userid", userid);

        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
        request.Method = "POST";
        request.Accept = "application/json";

        request.ContentType = "application/json";

        using (Stream outStream = request.GetRequestStream())
        {
            StreamWriter sw = new StreamWriter(outStream);
            sw.WriteLine(json);
            sw.Flush();
            sw.Close();
        }

        HttpWebResponse response = (HttpWebResponse)request.GetResponse();

        using (Stream inStream = response.GetResponseStream())
        {
            StreamReader sr = new StreamReader(inStream);
            MyJson.JsonNode_Object myjson = MyJson.Parse(sr.ReadToEnd()) as MyJson.JsonNode_Object;
            return myjson["text"].ToString();
        }
    }

    public static string get_uft8(string unicodeString)
    {
        UTF8Encoding utf8 = new UTF8Encoding();
        byte[] encodedBytes = utf8.GetBytes(unicodeString);
        string decodedString = utf8.GetString(encodedBytes);
        return decodedString;
    }
}