using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace SpringChallenge2020
{
    class Program
    {
        static void Main(string[] args)
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(Assembly.GetExecutingAssembly().Location).Parent.Parent.Parent.Parent.Parent.GetDirectories().Where(x => x.Name.Contains("SpringChallenge2020.Core")).FirstOrDefault();
            Console.WriteLine(directoryInfo.FullName);
            Console.WriteLine(string.Empty);
            StringBuilder all = new StringBuilder();
            all.AppendLine("using System;");
            all.AppendLine("using System.Linq;");
            all.AppendLine("using System.IO;");
            all.AppendLine("using System.Text;");
            all.AppendLine("using System.Collections;");
            all.AppendLine("using System.Collections.Generic;");
            CheckDirectory(directoryInfo, all);
            TextCopy.Clipboard.SetText(all.ToString());
        }
        private static void CheckDirectory(DirectoryInfo directoryInfo, StringBuilder all)
        {
            foreach (FileInfo fileInfo in directoryInfo.GetFiles().Where(x => x.Name.EndsWith(".cs")))
            {
                Console.WriteLine("--------------------------------------");
                Console.WriteLine(fileInfo.FullName);
                Console.WriteLine("--------------------------------------");
                using (StreamReader sr = new StreamReader(fileInfo.FullName))
                {
                    StringBuilder stringBuilder = new StringBuilder();
                    int state = 0;
                    while (!sr.EndOfStream)
                    {
                        string value = sr.ReadLine();
                        if (state == 2 && value.Length > 1)
                            stringBuilder.AppendLine(value.Substring(4));
                        else if (state == 0 && value.Contains("namespace"))
                            state = 1;
                        else if (state == 1 && value.StartsWith("{"))
                            state = 2;
                    }
                    all.AppendLine(stringBuilder.ToString());
                }
            }
            foreach (DirectoryInfo directory in directoryInfo.GetDirectories())
                CheckDirectory(directory, all);
        }
    }
}
