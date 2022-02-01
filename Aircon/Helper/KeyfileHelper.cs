using System;
using System.IO;
using System.Security.Cryptography;

namespace Aircon.Helper
{
    public static class KeyfileHelper
    {
        private const int SizeOfKeyFile = 4096;

        private const int KeyStartOffset = 1024;

        public static void GenerateAndPersistFileEncryptionKey()
        {
            FileStream fileStream = File.OpenWrite(Paths.KeyFile);
            try
            {
                byte[] numArray = KeyfileHelper.GenerateRandomKey(4096);
                fileStream.Write(numArray, 0, (int)numArray.Length);
            }
            finally
            {
                if (fileStream != null)
                {
                    ((IDisposable)fileStream).Dispose();
                }
            }
        }

        public static byte[] GenerateRandomKey(int sizeInBytes)
        {
            if (sizeInBytes <= 0)
            {
                throw new ArgumentException("must be a positive integer", "sizeInBytes");
            }
            //RNGCryptoServiceProvider rNGCryptoServiceProvider = new RNGCryptoServiceProvider();
            RandomNumberGenerator rNGCryptoServiceProvider = RandomNumberGenerator.Create();
            byte[] numArray = new byte[sizeInBytes];
            rNGCryptoServiceProvider.GetBytes(numArray);
            return numArray;
        }

        public static byte[] ReadFileEncryptionKey()
        {
            byte[] numArray;
            byte[] numArray1;
            string keyFile = Paths.KeyFile;
            if (File.Exists(keyFile))
            {
                FileStream fileStream = File.OpenRead(keyFile);
                try
                {
                    byte[] numArray2 = new byte[16];
                    if ((long)1024 == fileStream.Seek((long)1024, SeekOrigin.Begin))
                    {
                        if (fileStream.Read(numArray2, 0, (int)numArray2.Length) != (int)numArray2.Length)
                        {
                            numArray1 = null;
                        }
                        else
                        {
                            numArray1 = numArray2;
                        }
                        numArray = numArray1;
                    }
                    else
                    {
                        numArray = null;
                    }
                }
                finally
                {
                    if (fileStream != null)
                    {
                        ((IDisposable)fileStream).Dispose();
                    }
                }
            }
            else
            {
                numArray = null;
            }
            return numArray;
        }

        public static byte[] ReadOrGenerateNewKey()
        {
            byte[] numArray = KeyfileHelper.ReadFileEncryptionKey();
            if (null == numArray)
            {
                KeyfileHelper.GenerateAndPersistFileEncryptionKey();
                numArray = KeyfileHelper.ReadFileEncryptionKey();
            }
            return numArray;
        }
    }

}
