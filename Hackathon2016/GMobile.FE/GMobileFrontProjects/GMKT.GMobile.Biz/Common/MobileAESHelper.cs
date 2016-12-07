using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Security.Cryptography;


namespace GMKT.GMobile.Biz.Common
{
	public class MobileAESHelper
	{
		private const string MOBILE_AES_KEY = @"gmktmobile1234$#@!gmktmobile0987";

		public static string AESEncrypt(string Input)
		{
			string result = String.Empty;
			RijndaelManaged aes = new RijndaelManaged();
			try
			{
				aes.KeySize = 256;
				aes.BlockSize = 128;
				aes.Mode = CipherMode.CBC;
				aes.Padding = PaddingMode.PKCS7;
				aes.Key = Encoding.UTF8.GetBytes(MOBILE_AES_KEY);
				aes.IV = new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };

				var encrypt = aes.CreateEncryptor(aes.Key, aes.IV);
				byte[] xBuff = null;
				using (var ms = new MemoryStream())
				{
					using (var cs = new CryptoStream(ms, encrypt, CryptoStreamMode.Write))
					{
						byte[] xXml = Encoding.UTF8.GetBytes(Input);
						cs.Write(xXml, 0, xXml.Length);
					}
					xBuff = ms.ToArray();
				}
				result = Convert.ToBase64String(xBuff);
			}
			catch (Exception e)
			{
			}
			finally
			{
				if (aes != null)
					aes.Clear();
			}

			return result;
		}

		public static string AESDecrypt(string Input)
		{
			string result = String.Empty;
			RijndaelManaged aes = new RijndaelManaged();

			try
			{
				aes.KeySize = 256;
				aes.BlockSize = 128;
				aes.Mode = CipherMode.CBC;
				aes.Padding = PaddingMode.PKCS7;
				aes.Key = Encoding.UTF8.GetBytes(MOBILE_AES_KEY);
				aes.IV = new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };

				var decrypt = aes.CreateDecryptor();
				byte[] xBuff = null;
				using (var ms = new MemoryStream())
				{
					using (var cs = new CryptoStream(ms, decrypt, CryptoStreamMode.Write))
					{
						byte[] xXml = Convert.FromBase64String(Input);
						cs.Write(xXml, 0, xXml.Length);
					}
					xBuff = ms.ToArray();
				}
				result = Encoding.UTF8.GetString(xBuff);
			}
			catch {}
			finally 
			{
				if (aes != null)
					aes.Clear();
			}

			return result;
		}
	}
}
