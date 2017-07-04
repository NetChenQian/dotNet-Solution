using System;
using System.IO;
using System.Threading.Tasks;

namespace ExceptionHandler
{
    public class LogHelper
    {
        private static readonly long Max_File_Size = 10 * 1024 * 1024;
        private static object obj = new object();
        public static void LogError(params Exception[] exceptions)
        {
            Task.Factory.StartNew(() => {
                if (exceptions != null && exceptions.Length > 0)
                {
                    // directory path
                    string dir = Environment.CurrentDirectory + "\\Log";
                    if (!Directory.Exists(dir))
                    {
                        Directory.CreateDirectory(dir);
                    }
                    DateTime now = DateTime.Now;
                    dir += "\\" + now.ToLongDateString();
                    if (!Directory.Exists(dir))
                    {
                        Directory.CreateDirectory(dir);
                    }
                    // file name
                    string filename = now.ToLongDateString() + now.Hour;
                    FileInfo[] fileInfos = new DirectoryInfo(dir).GetFiles();
                    if (fileInfos != null && fileInfos.Length > 0)
                    {
                        FileInfo fileInfo = fileInfos[fileInfos.Length - 1];
                        if (fileInfo.Length >= Max_File_Size)
                        {
                            string lastFileName = fileInfo.Name;
                            // file already exist
                            if (lastFileName.IndexOf(filename) > 0)
                            {
                                if (lastFileName.Length > filename.Length)
                                {
                                    int suffix = Convert.ToInt32(lastFileName.Substring(filename.Length + 2)) + 1;
                                    filename = filename + suffix;
                                }
                                else
                                {
                                    filename = filename + "_1";
                                }
                            }
                        }
                    }
                    // absolute path
                    string path = dir + "\\" + filename + ".log";
                    // write log
                    try
                    {
                        using (StreamWriter sw = new StreamWriter(path, true))
                        {
                            lock (obj)
                            {
                                foreach (Exception ex in exceptions)
                                {
                                    sw.Write("Error Message: " + ex.Message);
                                    sw.Write(Environment.NewLine);
                                    sw.Write(ex.StackTrace);
                                    sw.Write(Environment.NewLine);
                                }
                            }
                        }
                    }
                    catch
                    {
                    }
                }
            });
        }
    }
}

