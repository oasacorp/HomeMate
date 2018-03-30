

    using System;
    using System.Security.Cryptography;
    using System.Text;
    using System.IO;


    namespace HomeMate
    {

internal static class Crypto
        {

                    private static string encryptionPassword = "upS7LG0eYKAXZZ8WEjCuH2hfYMrGz3se";

            private static readonly byte[] salt = Encoding.ASCII.GetBytes("prolitecontrols");
        //tocheck  - catch non-valid barcode

            internal static string Encrypt(string textToEncrypt)
            {
                var algorithm = GetAlgorithm(encryptionPassword);


                if (textToEncrypt == null || textToEncrypt == "") return "";

                byte[] encryptedBytes;
                using (ICryptoTransform encryptor = algorithm.CreateEncryptor(algorithm.Key, algorithm.IV))
                {
                    byte[] bytesToEncrypt = Encoding.UTF8.GetBytes(textToEncrypt);
                    encryptedBytes = InMemoryCrypt(bytesToEncrypt, encryptor);
                }
                return Convert.ToBase64String(encryptedBytes);
            }


            internal static string Decrypt(string encryptedText)
            {
                var algorithm = GetAlgorithm(encryptionPassword);


                if (encryptedText == null || encryptedText == "") return "";

                byte[] descryptedBytes,encryptedBytes;
                using (ICryptoTransform decryptor = algorithm.CreateDecryptor(algorithm.Key, algorithm.IV))
            {
                try
                {
                    encryptedBytes = Convert.FromBase64String(encryptedText);
                    descryptedBytes = InMemoryCrypt(encryptedBytes, decryptor);
                }
                catch(System.FormatException e)
                {
                    return "NaN";
                }
               // System.Security.Cryptography.CryptographicException: Length of the data to decrypt is invalid.
                }
                return Encoding.UTF8.GetString(descryptedBytes);
            }


            private static byte[] InMemoryCrypt(byte[] data, ICryptoTransform transform)
            {
                MemoryStream memory = new MemoryStream();
            try
            {
                using (Stream stream = new CryptoStream(memory, transform, CryptoStreamMode.Write))
                {

                    stream.Write(data, 0, data.Length);

                }
            }
            catch
            {

            }
                return memory.ToArray();
            }


            private static RijndaelManaged GetAlgorithm(string encryptionPassword)
            {

                var key = new Rfc2898DeriveBytes(encryptionPassword, salt);


                var algorithm = new RijndaelManaged();
                int bytesForKey = algorithm.KeySize / 8;
                int bytesForIV = algorithm.BlockSize / 8;
                algorithm.Key = key.GetBytes(bytesForKey);
                algorithm.IV = key.GetBytes(bytesForIV);
                return algorithm;
            }


    }

}