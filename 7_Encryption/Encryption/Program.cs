using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Principal;
using System.IO;
using System.Security.Cryptography;

namespace Encryption
{
    class Program
    {
        static void GetUserName()
        {
            WindowsIdentity currentIdentity = WindowsIdentity.GetCurrent();
            Console.WriteLine("Current user: {0}", currentIdentity.Name);
        }

        static void Encrypt(string inFile, string outFile, string keyFile)
        {
            using (var inputFile = new FileStream(inFile, FileMode.Open, FileAccess.Read))
            using (var outputFile = new FileStream(outFile, FileMode.Create, FileAccess.Write))
            {
                SymmetricAlgorithm myAlg = new RijndaelManaged();

                string password = "Qwdhfk";
                var salt = Encoding.ASCII.GetBytes("This is my salt");
                var key = new Rfc2898DeriveBytes(password, salt);
                myAlg.Key = key.GetBytes(myAlg.KeySize / 8);
                using (var ks = new FileStream(keyFile, FileMode.Create, FileAccess.Write))
                {
                    ks.Write(myAlg.Key, 0, myAlg.Key.Length);
                    ks.Flush();
                }
                var fileData = new byte[inputFile.Length];
                inputFile.Read(fileData, 0, fileData.Length);
                ICryptoTransform encryptor = myAlg.CreateEncryptor();
                using (var encryptStream = new CryptoStream(outputFile, encryptor, CryptoStreamMode.Write))
                {
                    encryptStream.Write(fileData, 0, fileData.Length);
                    encryptStream.Flush();
                }
            }
        }

        static void Decrypt(string inFile, string outFile, string keyFile)
        {
            using (var inputFile = new FileStream(inFile, FileMode.Open, FileAccess.Read))
            using (var outputFile = new FileStream(outFile, FileMode.Create, FileAccess.Write))
            using (var ks = new FileStream(keyFile, FileMode.Open, FileAccess.Read))
            {
                SymmetricAlgorithm myAlg = new RijndaelManaged();

                var keyData = new byte[ks.Length];
                ks.Read(keyData, 0, keyData.Length);
                myAlg.Key = keyData;
                ICryptoTransform decryptor = myAlg.CreateDecryptor();
                using (var decryptStream = new CryptoStream(inputFile, decryptor, CryptoStreamMode.Read))
                {
                    using (StreamReader srDecrypt = new StreamReader(decryptStream))
                    {
                        var s = srDecrypt.ReadToEnd();
                        using (var sw = new StreamWriter(outputFile))
                        {
                            sw.Write(s);
                        }
                    }
                }
            }
        }


        static void Main(string[] args)
        {
            string inFile = @"C:\Users\k.kataeva\dir1\Anecds.txt";
            string outFile = @"C:\Users\k.kataeva\dir1\Anecds.txt.enc";
            string keyFile = @"C:\Users\k.kataeva\dir1\key.txt";
            GetUserName();
            Encrypt(inFile, outFile, keyFile);
            Decrypt(outFile, inFile, keyFile);
            Console.ReadKey();
        }
    }
}
