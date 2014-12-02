namespace SFC_Tools.Classes
{
    using SFC_Tools.Classes;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Globalization;
    using System.IO;
    using System.Security.Cryptography;
    using System.Text;
    using System.Threading;

    public sealed class FileEncrypt
    {
        private const int BUFFER_SIZE = 0x20000;
        private readonly string encryptTag = "EncryptHelperTag";
        private readonly string enEncrypt = "110";
        private const ulong FC_TAG = 18158797384510146255L;
        private string PassWord = null;
        private RandomNumberGenerator rand = new RNGCryptoServiceProvider();

        private FileEncrypt()
        {
            this.PassWord = "Foxconn0cdd3d8d-ff0e-4801-8c2d-44bdc619380f36df0262-8611-4b05-a410-7d0ac180edfbc816e5e2-310d-4c46-b4de-6fb8ac4c5deb8ee945ba-c98";
        }

        private bool CheckByteArrays(byte[] b1, byte[] b2)
        {
            if (b1.Length == b2.Length)
            {
                for (int i = 0; i < b1.Length; i++)
                {
                    if (b1[i] != b2[i])
                    {
                        return false;
                    }
                }
                return true;
            }
            return false;
        }

        private bool CheckFileExists(string filePath)
        {
            return File.Exists(filePath);
        }

        public bool CopyFile(string inFile, string outFile)
        {
            try
            {
                if (File.Exists(inFile))
                {
                    File.Copy(inFile, outFile, true);
                    return true;
                }
            }
            catch (Exception exception)
            {
            }
            return false;
        }

        private SymmetricAlgorithm CreateRijndael(byte[] salt)
        {
            PasswordDeriveBytes bytes = new PasswordDeriveBytes(this.PassWord, salt, "SHA256", 0x3e8);
            SymmetricAlgorithm algorithm = Rijndael.Create();
            algorithm.KeySize = 0x100;
            algorithm.Key = bytes.GetBytes(0x20);
            algorithm.Padding = PaddingMode.PKCS7;
            return algorithm;
        }

        public bool DecryptFile(string inFile, string outFile)
        {
            try
            {
                if (this.CheckFileExists(inFile))
                {
                    using (FileStream stream = File.OpenRead(inFile))
                    {
                        using (FileStream stream2 = File.OpenWrite(outFile))
                        {
                            int length = (int) stream.Length;
                            byte[] buffer = new byte[0x20000];
                            int count = -1;
                            int num3 = 0;
                            int num4 = 0;
                            byte[] buffer2 = new byte[0x10];
                            stream.Read(buffer2, 0, 0x10);
                            byte[] buffer3 = new byte[0x10];
                            stream.Read(buffer3, 0, 0x10);
                            SymmetricAlgorithm algorithm = this.CreateRijndael(buffer3);
                            algorithm.IV = buffer2;
                            num3 = 0x20;
                            long num5 = -1L;
                            HashAlgorithm transform = SHA256.Create();
                            using (CryptoStream stream3 = new CryptoStream(stream, algorithm.CreateDecryptor(), CryptoStreamMode.Read))
                            {
                                using (CryptoStream stream4 = new CryptoStream(Stream.Null, transform, CryptoStreamMode.Write))
                                {
                                    BinaryReader reader = new BinaryReader(stream3);
                                    num5 = reader.ReadInt64();
                                    ulong num6 = reader.ReadUInt64();
                                    if (18158797384510146255L != num6)
                                    {
                                       
                                    }
                                    long num7 = num5 / 0x20000L;
                                    long num8 = num5 % 0x20000L;
                                    for (int i = 0; i < num7; i++)
                                    {
                                        count = stream3.Read(buffer, 0, buffer.Length);
                                        stream2.Write(buffer, 0, count);
                                        stream4.Write(buffer, 0, count);
                                        num3 += count;
                                        num4 += count;
                                    }
                                    if (num8 > 0L)
                                    {
                                        count = stream3.Read(buffer, 0, (int) num8);
                                        stream2.Write(buffer, 0, count);
                                        stream4.Write(buffer, 0, count);
                                        num3 += count;
                                        num4 += count;
                                    }
                                    stream4.Flush();
                                    stream4.Close();
                                    stream2.Flush();
                                    stream2.Close();
                                    byte[] hash = transform.Hash;
                                    byte[] buffer5 = new byte[transform.HashSize / 8];
                                    count = stream3.Read(buffer5, 0, buffer5.Length);
                                    if (!((buffer5.Length == count) && this.CheckByteArrays(buffer5, hash)))
                                    {
                                       
                                    }
                                    stream3.Flush();
                                    stream3.Close();
                                }
                            }
                            if (num4 != num5)
                            {
                                
                            }
                            stream.Flush();
                            stream.Close();
                        }
                    }
                    return true;
                }
            }
            catch (Exception exception)
            {
                ////Log4Net.Log.Error("解密文件報錯：" + exception.Message + exception.StackTrace);
            }
            return false;
        }

        public string DecryptString(string sValue, string sKey)
        {
            string[] strArray = sValue.Split("-".ToCharArray());
            byte[] inputBuffer = new byte[strArray.Length];
            for (int i = 0; i < strArray.Length; i++)
            {
                inputBuffer[i] = byte.Parse(strArray[i], NumberStyles.HexNumber, CultureInfo.InvariantCulture);
            }
            byte[] bytes = new DESCryptoServiceProvider { Key = Encoding.ASCII.GetBytes(sKey), IV = Encoding.ASCII.GetBytes(sKey) }.CreateDecryptor().TransformFinalBlock(inputBuffer, 0, inputBuffer.Length);
            return Encoding.UTF8.GetString(bytes);
        }

        public string EncryptCode(string message)
        {
            string str = string.Empty;
            try
            {
                byte[] bytes = new UnicodeEncoding().GetBytes(message);
                str = BitConverter.ToString(((HashAlgorithm) CryptoConfig.CreateFromName("MD5")).ComputeHash(bytes)).Replace("-", "");
            }
            catch (Exception exception)
            {
               // //Log4Net.Log.Error("字符串md5加密錯誤" + exception.Message + exception.StackTrace);
                //PublicMethod.NetWorkMessage();
            }
            return str;
        }

        public bool EncryptFile(string inFile, string outFile)
        {
            try
            {
                if (this.CheckFileExists(inFile))
                {
                    using (FileStream stream = File.OpenRead(inFile))
                    {
                        using (FileStream stream2 = File.OpenWrite(outFile))
                        {
                            long length = stream.Length;
                            int num2 = (int) length;
                            byte[] buffer = new byte[0x20000];
                            int count = -1;
                            int num4 = 0;
                            byte[] buffer2 = this.GenerateRandomBytes(0x10);
                            byte[] salt = this.GenerateRandomBytes(0x10);
                            SymmetricAlgorithm algorithm = this.CreateRijndael(salt);
                            algorithm.IV = buffer2;
                            stream2.Write(buffer2, 0, buffer2.Length);
                            stream2.Write(salt, 0, salt.Length);
                            HashAlgorithm transform = SHA256.Create();
                            using (CryptoStream stream3 = new CryptoStream(stream2, algorithm.CreateEncryptor(), CryptoStreamMode.Write))
                            {
                                using (CryptoStream stream4 = new CryptoStream(Stream.Null, transform, CryptoStreamMode.Write))
                                {
                                    BinaryWriter writer = new BinaryWriter(stream3);
                                    writer.Write(length);
                                    writer.Write((ulong) 18158797384510146255L);
                                    while ((count = stream.Read(buffer, 0, buffer.Length)) != 0)
                                    {
                                        stream3.Write(buffer, 0, count);
                                        stream4.Write(buffer, 0, count);
                                        num4 += count;
                                    }
                                    stream4.Flush();
                                    stream4.Close();
                                    byte[] hash = transform.Hash;
                                    stream3.Write(hash, 0, hash.Length);
                                    stream3.Flush();
                                    stream3.Close();
                                    stream.Flush();
                                    stream.Close();
                                }
                            }
                        }
                    }
                    return true;
                }
            }
            catch (Exception exception)
            {
                ////Log4Net.Log.Error("加密文件失敗：" + exception.Message + exception.StackTrace);
                //PublicMethod.NetWorkMessage();
            }
            return false;
        }

        public string EncryptString(string sValue, string sKey)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(sValue);
            return BitConverter.ToString(new DESCryptoServiceProvider { Key = Encoding.ASCII.GetBytes(sKey), IV = Encoding.ASCII.GetBytes(sKey) }.CreateEncryptor().TransformFinalBlock(bytes, 0, bytes.Length));
        }

        public string GenerateKey()
        {
            DESCryptoServiceProvider provider = (DESCryptoServiceProvider) DES.Create();
            return Encoding.ASCII.GetString(provider.Key);
        }

        private byte[] GenerateRandomBytes(int count)
        {
            byte[] data = new byte[count];
            this.rand.GetBytes(data);
            return data;
        }

        public static FileEncrypt GetInstance()
        {
            return Nested.sEncrypt;
        }

        public bool IsEncrypt(string strPath)
        {
            bool flag = false;
            Process process = new Process();
            Random random = new Random(0x12ce47);
            try
            {
                process.StartInfo.FileName = "cmd.exe";
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.RedirectStandardInput = true;
                process.StartInfo.RedirectStandardOutput = true;
                process.StartInfo.RedirectStandardError = true;
                process.StartInfo.CreateNoWindow = true;
                process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                process.Start();
                if (File.Exists(ConstData.ApplicationPath + @"\tool\7za.exe"))
                {
                    process.StandardInput.WriteLine("cd " + ConstData.ApplicationPath);
                    process.StandardInput.WriteLine("cd  tool");
                    process.StandardInput.WriteLine("7za.exe t \"" + strPath + "\"");
                    Thread.Sleep(0x3e8);
                    process.StandardInput.WriteLine(random.ToString());
                    process.StandardInput.WriteLine("exit");
                    if (process.StandardOutput.ReadToEnd().Contains("Everything is Ok"))
                    {
                        flag = false;
                    }
                    else
                    {
                        flag = true;
                    }
                }
                else
                {
                    flag = false;
                }
                process.Close();
            }
            catch (Exception exception)
            {
                if (!process.HasExited)
                {
                    process.Kill();
                }
                ////Log4Net.Log.Error("判斷加密失敗：" + exception.Message + exception.StackTrace);
                //PublicMethod.NetWorkMessage();
                flag = false;
            }
            return flag;
        }

        public bool IsEncryptPro(string strPath)
        {
            bool flag = false;
            Process process = new Process();
            Random random = new Random(0x12ce47);
            try
            {
                if (File.Exists(ConstData.ApplicationPath + @"\tool\7za.exe"))
                {
                    process.StartInfo.FileName = ConstData.ApplicationPath + @"\tool\7za.exe";
                    process.StartInfo.UseShellExecute = false;
                    process.StartInfo.RedirectStandardInput = true;
                    process.StartInfo.RedirectStandardOutput = true;
                    process.StartInfo.RedirectStandardError = true;
                    process.StartInfo.CreateNoWindow = true;
                    process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                    process.StartInfo.Arguments = "t \"" + strPath + "\"";
                    process.Start();
                    Thread.Sleep(0x3e8);
                    process.StandardInput.WriteLine(random.ToString());
                    process.StandardInput.WriteLine("exit");
                    if (process.StandardOutput.ReadToEnd().Contains("Everything is Ok"))
                    {
                        flag = false;
                    }
                    else
                    {
                        flag = true;
                    }
                }
                else
                {
                    flag = false;
                }
                process.Close();
            }
            catch (Exception exception)
            {
                if (!process.HasExited)
                {
                    process.Kill();
                }
                ////Log4Net.Log.Error("判斷加密失敗：" + exception.Message + exception.StackTrace);
                //PublicMethod.NetWorkMessage();
                flag = false;
            }
            return flag;
        }
/*
        private void ProcessExited(object sender, EventArgs e)
        {
           // //Log4Net.Log.Debug("進程關閉：OK");
            try
            {
                StaticVariable.ZipPCStatus = true;
            }
            catch (Exception exception)
            {
               // //Log4Net.Log.Error("系統關閉應用程序異常：" + exception.Message + exception.StackTrace);
               // PublicMethod.NetWorkMessage();
            }
        }*/
/*
        private bool WaitProcessClose(bool isStart)
        {
            bool zipPCStatus = false;
            do
            {
                Thread.Sleep(100);
                zipPCStatus = StaticVariable.ZipPCStatus;
                //Log4Net.Log.Debug("測試等待。。。");
            }
            while (!StaticVariable.ZipPCStatus);
            StaticVariable.ZipPCStatus = false;
            return zipPCStatus;
        }
        */
        /*
        public bool ZipFileToForder(List<string> listPath, string strPassWord, string zipName, string strGuid, ref string pathTmp, ref string result)
        {
            bool isStart = false;
            int count = 0;
            string attFileTempPath = new PublicMethod().GetAttFileTempPath(zipName, ref strGuid);
            string extName = PublicMethod.GetExtName(zipName);
            string path = ConstData.ApplicationPath + @"\tool\7za.exe";
            Process process = null;
            try
            {
                process = new Process();
                StringBuilder builder = new StringBuilder();
                ProcessStartInfo info = new ProcessStartInfo();
                if (File.Exists(path))
                {
                    info.FileName = path;
                    info.UseShellExecute = false;
                    info.RedirectStandardInput = true;
                    info.RedirectStandardOutput = true;
                    info.RedirectStandardError = true;
                    info.CreateNoWindow = true;
                    info.WindowStyle = ProcessWindowStyle.Hidden;
                    process.EnableRaisingEvents = true;
                    process.Exited += new EventHandler(this.ProcessExited);
                    if (listPath != null)
                    {
                        count = listPath.Count;
                        if (count > 0)
                        {
                            if (string.IsNullOrEmpty(extName))
                            {
                                builder.Append("a -tzip -p" + strPassWord + " \"");
                                attFileTempPath = attFileTempPath + ".zip";
                            }
                            else if (".zip".Equals(extName))
                            {
                                builder.Append("a -tzip -p" + strPassWord + " \"");
                            }
                            else if (".7z".Equals(extName))
                            {
                                builder.Append("a -t7z -p" + strPassWord + " \"");
                            }
                            else
                            {
                                builder.Append("a -tzip -p" + strPassWord + " \"");
                                attFileTempPath = attFileTempPath + ".zip";
                            }
                            builder.Append(attFileTempPath + "\"");
                            for (int i = 0; i < count; i++)
                            {
                                builder.Append(" \"" + listPath[i] + "\"");
                            }
                            info.Arguments = builder.ToString();
                            process.StartInfo = info;
                            isStart = process.Start();
                            string str4 = process.StandardOutput.ReadToEnd();
                            if (str4.Contains("Everything is Ok"))
                            {
                                pathTmp = attFileTempPath;
                                return this.WaitProcessClose(isStart);
                            }
                            result = str4;
                            return false;
                        }
                    }
                }
                process.Close();
            }
            catch (Exception exception)
            {
                //Log4Net.Log.Error("文件壓縮失敗：" + exception.Message + exception.StackTrace);
            }
            finally
            {
                if (process != null)
                {
                    process.Close();
                }
            }
            return isStart;
        }
        */
        public string EncryptTag
        {
            get
            {
                return this.encryptTag;
            }
        }

        public string ENEncrypt
        {
            get
            {
                return this.enEncrypt;
            }
        }

        private class Nested
        {
            internal static readonly FileEncrypt sEncrypt = new FileEncrypt();
        }
    }
}

