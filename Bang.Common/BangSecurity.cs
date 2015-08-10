using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Bang.Common
{
    public static class BangSecurity
    {
        public const string DESKEY = "tmd432ndfdsf232965r";


        public static string MD5Base64(string text)
        {
            var bytes = MD5(text);
            return Convert.ToBase64String(bytes);
        }

        public static string MD5Hex(string text)
        {
            var bytes = MD5(text);
            var sb = new StringBuilder();
            for (int i = 0; i < bytes.Length; i++)
            {
                sb.Append(bytes[i].ToString("X2"));
            }
            return sb.ToString();
        }

        public static byte[] MD5(string text)
        {
            var provider = new System.Security.Cryptography.MD5CryptoServiceProvider();
            var bytes = System.Text.Encoding.UTF8.GetBytes(text);
            var result = provider.ComputeHash(bytes);
            return result;
        }

        /// <summary>
        /// 加密函数
        /// </summary>
        /// <param name="inputText"></param>
        /// <param name="strEncrKey"></param>
        /// <returns></returns>
        public static string DESEncrypt(string inputText, string strEncrKey)
        {
            byte[] byKey = { };
            byte[] iv = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };
            try
            {
                byKey = Encoding.UTF8.GetBytes(strEncrKey.Substring(0, 8));
                DESCryptoServiceProvider des = new DESCryptoServiceProvider();
                Byte[] inputByteArray = Encoding.UTF8.GetBytes(inputText);
                MemoryStream ms = new MemoryStream();
                CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(byKey, iv), CryptoStreamMode.Write);
                cs.Write(inputByteArray, 0, inputByteArray.Length);
                cs.FlushFinalBlock();
                return Convert.ToBase64String(ms.ToArray());

            }
            catch (Exception ex)
            {
                return string.Empty;
            }
        }
        /// <summary>
        /// 解密函数
        /// </summary>
        /// <param name="inputText"></param>
        /// <param name="sDecrKey"></param>
        /// <returns></returns>
        public static string DESDecrypt(string inputText, string sDecrKey)
        {
            Byte[] byKey = { };
            Byte[] IV = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };
            Byte[] inputByteArray = new byte[inputText.Length];
            try
            {
                byKey = Encoding.UTF8.GetBytes(sDecrKey.Substring(0, 8));
                DESCryptoServiceProvider des = new DESCryptoServiceProvider();
                inputByteArray = Convert.FromBase64String(inputText);
                MemoryStream ms = new MemoryStream();
                CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(byKey, IV), CryptoStreamMode.Write);
                cs.Write(inputByteArray, 0, inputByteArray.Length);
                cs.FlushFinalBlock();
                return Encoding.UTF8.GetString(ms.ToArray());
            }
            catch (Exception ex)
            {
                return string.Empty;
            }

        }

        //默认密钥向量 
        private static byte[] _ivKey1 = {
                                          0x12, 0x34, 0x56, 0x58, 0x90, 0xAB, 0xED, 0xEF, 0x10, 0x34, 0xE6, 0x78, 0xf0, 0x4B, 0x3D, 0xE3 };

        /// <summary>
        /// AES解密
        /// </summary>
        /// <param name="cipherText">密文字符串</param>
        /// <param name="key">解密key，必须是16位</param>
        /// <returns>返回解密后的明文字符串</returns>
        public static string AESDecrypt(string showText, string key)
        {
            if (string.IsNullOrEmpty(showText))
            {
                return string.Empty;
            }
            byte[] cipherText = Convert.FromBase64String(showText);
            SymmetricAlgorithm des = Rijndael.Create();
            des.Key = Encoding.UTF8.GetBytes(key);
            des.IV = _ivKey1;
            byte[] decryptBytes = new byte[cipherText.Length];
            using (MemoryStream ms = new MemoryStream(cipherText))
            {
                using (CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Read))
                {
                    cs.Read(decryptBytes, 0, decryptBytes.Length);
                    cs.Close();
                    ms.Close();
                }
            }
            return Encoding.UTF8.GetString(decryptBytes).Replace("\0", "");   ///将字符串后尾的'\0'去掉
        }

        public static string ClientConfigAESDecrypt(string showText)
        {
            return AESDecrypt(showText, "TmnVmeuRCPxbZyZp");
        }
    }
}
