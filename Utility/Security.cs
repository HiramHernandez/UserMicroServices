using System;
using System.Security.Cryptography;
using System.Text;
using System.IO;  

public static class Security
{
    private static string keyPassword = "4m4n3c3r";

    public static string EncryptText(string textToEncript, string satRGBValue,
        string algortithmEncrypt, int interactions, string initialVector, int sizeKey)
    {
        try
        {
            byte[] InitialVectorBytes = Encoding.ASCII.GetBytes(initialVector);
                byte[] saltValueBytes = Encoding.ASCII.GetBytes(satRGBValue);
                byte[] plainTextBytes = Encoding.UTF8.GetBytes(textToEncript);
 
                PasswordDeriveBytes password = 
                    new PasswordDeriveBytes(keyPassword, saltValueBytes, 
                        algortithmEncrypt, interactions);
 
                byte[] keyBytes = password.GetBytes(sizeKey / 8);
 
                RijndaelManaged symmetricKey = new RijndaelManaged();
 
                symmetricKey.Mode = CipherMode.CBC;
 
                ICryptoTransform encryptor = 
                    symmetricKey.CreateEncryptor(keyBytes, InitialVectorBytes);
 
                MemoryStream memoryStream = new MemoryStream();
 
                CryptoStream cryptoStream = 
                    new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write);
 
                cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
 
                cryptoStream.FlushFinalBlock();
 
                byte[] cipherTextBytes = memoryStream.ToArray();
 
                memoryStream.Close();
                cryptoStream.Close();
 
                string textToEncriptFinal = Convert.ToBase64String(cipherTextBytes);
 
                return textToEncriptFinal;
        } catch{ return null; }
    }

     public static string decryptText (string cryptyhText, 
            string satRGBValue, string algortithmEncrypt, 
            int interactions, string initialVector, int sizeKey)
        {
            try
            {
                byte[] InitialVectorBytes = Encoding.ASCII.GetBytes(initialVector);
                byte[] saltValueBytes = Encoding.ASCII.GetBytes(satRGBValue);
 
                byte[] cipherTextBytes = Convert.FromBase64String(cryptyhText);
 
                PasswordDeriveBytes password = 
                    new PasswordDeriveBytes(keyPassword, saltValueBytes, 
                        algortithmEncrypt, interactions);
 
                byte[] keyBytes = password.GetBytes(sizeKey / 8);
 
                RijndaelManaged symmetricKey = new RijndaelManaged();
 
                symmetricKey.Mode = CipherMode.CBC;
 
                ICryptoTransform decryptor = symmetricKey.CreateDecryptor(keyBytes, InitialVectorBytes);
 
                MemoryStream memoryStream = new MemoryStream(cipherTextBytes);
 
                CryptoStream cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);
 
                byte[] plainTextBytes = new byte[cipherTextBytes.Length];
 
                int decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);
 
                memoryStream.Close();
                cryptoStream.Close();
 
                string textoDescifradoFinal = Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount);
 
                return textoDescifradoFinal;
            }
            catch
            {
                return null;
            }
        }

}