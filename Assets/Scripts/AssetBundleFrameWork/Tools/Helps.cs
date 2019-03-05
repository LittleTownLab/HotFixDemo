using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Security;
using System.Text;
using System.Security.Cryptography;

namespace HotUpdateModel
{
    public static class Helps
    {
        public static string GetMD5Vlues(string filePath)
        {
            StringBuilder sb = new StringBuilder();

            filePath = filePath.Trim();

            using (FileStream fs = new FileStream(filePath, FileMode.Open))
            {
                MD5 md5 = new MD5CryptoServiceProvider();

                byte[] result = md5.ComputeHash(fs);

                for(int i = 0; i < result.Length; i++)
                {
                    sb.Append(result[i].ToString("x2"));
                }
            }

            return sb.ToString();
        }
    }
}