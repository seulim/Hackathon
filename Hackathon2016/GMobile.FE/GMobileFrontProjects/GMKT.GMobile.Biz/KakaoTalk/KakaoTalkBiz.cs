//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.IO;
//using System.Net;
//using System.Web;
//using System.Runtime.Serialization;
//using System.Collections;
//using GMKT.Framework.EnterpriseServices;


//namespace GMKT.GMobile.Biz.KakaoTalk
//{
//    public class KakaoTalkBiz : BizBase
//    {
//        public string GetCertCode(string custHPNo, string plusKey)
//        {
//            HttpWebRequest wReq;
//            Stream PostDataStream;
//            Stream respPostStream;
//            StreamReader readerPost;
//            HttpWebResponse wResp;
//            StringBuilder postParams;

//            ////보낼 데이터 세팅
//            postParams = new StringBuilder();
//            //postParams.Append("data=");
//            //postParams.Append("[");

//            string parmtmpStr = string.Format(" {{\"plus_key\":\"{0}\", \"phone_number\":{1} }} "
//                , plusKey
//                , custHPNo
//                );
//            postParams.Append(parmtmpStr);

//            //postParams.Append("]");

//            Console.WriteLine(postParams.ToString());

//            Encoding encoding = Encoding.UTF8;
//            byte[] result = encoding.GetBytes(postParams.ToString());

//            wReq = (HttpWebRequest)WebRequest.Create("http://tms.kakao.com/v1/cert_codes/send");
//            wReq.Method = "POST";
//            wReq.ContentType = "application/json";
//            wReq.ContentLength = result.Length;


//            PostDataStream = wReq.GetRequestStream();
//            PostDataStream.Write(result, 0, result.Length);
//            PostDataStream.Close();

//            wResp = (HttpWebResponse)wReq.GetResponse();
//            respPostStream = wResp.GetResponseStream();
//            readerPost = new StreamReader(respPostStream, Encoding.Default);
//            String resultPost = readerPost.ReadToEnd();
//            Console.WriteLine(resultPost);

//            return resultPost;
//        }
//    }

    
//}
