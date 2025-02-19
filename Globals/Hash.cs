﻿using System.Security.Cryptography;
using System.Text;

namespace EcosystemApp.Globals
{
    public class Hash
    {
        public static string ComputeSha256Hash(string rawData)
        {
            // ComputeHash - returns byte array
            byte[] bytes = SHA256.HashData(Encoding.UTF8.GetBytes(rawData));

            // Convert byte array to a string
            StringBuilder builder = new();
            for (int i = 0; i < bytes.Length; i++)
            {
                builder.Append(bytes[i].ToString("x2"));
            }
            return builder.ToString();
        }

        public static bool ValidateHash(string rawData, string currentHash)
        {
            if (rawData != null && currentHash != null)
            {
                string dataHashed = ComputeSha256Hash(rawData);
                if (currentHash == dataHashed) return true;
                else return false;
            }
            else return false;
        }
    }
}
