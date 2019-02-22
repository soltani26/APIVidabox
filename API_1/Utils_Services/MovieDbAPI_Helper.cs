using System;
using System.IO;
using System.Net;

namespace API_1.Utils_Services
{
    public class MovieDbAPI_Helper
    {
        public static string Call_MovieDbAPI(string query, int page, string url)
        {

            //FAIRE REQUETE API DISTANTE
            string jsonString = string.Empty;
          

            HttpWebRequest WebReq = (HttpWebRequest)WebRequest.Create(string.Format(url));
            WebReq.Method = "GET";
            try
            {
                HttpWebResponse WebResp = (HttpWebResponse)WebReq.GetResponse();
                using (Stream stream = WebResp.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(stream, System.Text.Encoding.UTF8);
                    jsonString = reader.ReadToEnd();
                }
            }
            catch (Exception ex) { }
            return jsonString;
        }


        public static string Call_Details_MovieDbAPI(string url)
        {
            string jsonString = string.Empty;
           

            HttpWebRequest WebReq = (HttpWebRequest)WebRequest.Create(string.Format(url));
            WebReq.Method = "GET";
            try
            {
                HttpWebResponse WebResp = (HttpWebResponse)WebReq.GetResponse();
                using (Stream stream = WebResp.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(stream, System.Text.Encoding.UTF8);
                    jsonString = reader.ReadToEnd();
                }
            }
            catch (Exception e)
            {
                
            }
            return jsonString;

        }
    }
}