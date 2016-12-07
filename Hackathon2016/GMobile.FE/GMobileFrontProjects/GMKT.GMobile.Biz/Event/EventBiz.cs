using System;
using System.Security.Cryptography;
using System.Text;

namespace GMKT.GMobile.Biz
{
    public class EventBiz
    {
        /// <summary>
        /// GMKT 이벤트 응모 스크립트 생성
        /// </summary>
        /// <param name="custNo">CUST_NO</param>
        /// <param name="eid">EID</param>
        /// <param name="apiURL">GMKTEnvironment.GmktFrontApiServerUrl : "http://apif.gmarket.co.kr/"</param>
        /// <returns></returns>
        public string[] GetEidApplyScriptString(string custNo, int eid, string apiURL)
        {
            string plainText = string.Empty;
            char charMainDelimiter = Convert.ToChar(127);
            string strNow = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            plainText = strNow + charMainDelimiter +
                    eid.ToString() + charMainDelimiter +
                    custNo + charMainDelimiter +
                    strNow + charMainDelimiter +
                    GetSHA512Hash(custNo + eid.ToString()) + charMainDelimiter;

            string eidEncryt = GetEncryptString(plainText, apiURL);

            //정적페이지 사용을 위해 일정한 key값사용
            string md5Key = "id6znjen28n5119";
            string md5Enctypt = GMKT.Framework.Security.GMKTCryptoLibraryOption.MD5(eidEncryt + md5Key);

            string decryptoResult = getMd5Hash(eidEncryt + md5Key);

            //암호화 확인
            if (decryptoResult == md5Enctypt)
            {
                string[] result = { eidEncryt, md5Enctypt };
                return result;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Get SHA512 Hash
        /// </summary>
        /// <param name="sourceStr">소스 문자열</param>
        /// <returns>Hash된 16진수 문자열</returns>
        public string GetSHA512Hash(string sourceStr)
        {
            byte[] data = System.Text.Encoding.UTF8.GetBytes(sourceStr);
            byte[] result;
            SHA512 shaM = new SHA512Managed();
            result = shaM.ComputeHash(data);
            string hashStr = BitConverter.ToString(result).Replace("-", "");

            return hashStr;
        }

        /// <summary>
        /// 문자열 암호화
        /// </summary>
        public string GetEncryptString(string plainText, string apiURL)
        {
            //암호화
            System.Net.WebRequest req = System.Net.WebRequest.Create(apiURL + "Exa100/Crypt/Crypt.aspx?crypt_type=enc&crypt_value=" + plainText);
            System.Net.WebResponse res = req.GetResponse();
            System.IO.Stream receiveStream = res.GetResponseStream();
            int lenth = 512;
            if (receiveStream.CanSeek == true)
            {
                lenth = (int)receiveStream.Length;
            }

            Byte[] read = new Byte[lenth];
            int bytes = receiveStream.Read(read, 0, lenth);
            Encoding encode = System.Text.Encoding.Default;


            string strEncryt = string.Empty;

            while (bytes > 0)
            {
                strEncryt += encode.GetString(read, 0, lenth);
                bytes = receiveStream.Read(read, 0, lenth);
            }

            strEncryt = strEncryt.Replace("\0", string.Empty);

            return strEncryt;
        }

        /// <summary>
        /// MD5 데이터 복호화
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private string getMd5Hash(string input)
        {
            System.Security.Cryptography.MD5 md5Hasher = System.Security.Cryptography.MD5.Create();
            byte[] data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(input));
            StringBuilder sBuilder = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }
            return sBuilder.ToString();
        }
    }
}
