using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using UnityEngine;


namespace helper {
    public class helperClass
    {
        public void writeFile(string path, string info)
        {
            if (!System.IO.File.Exists(path))
            {
                //û���򴴽�����ļ�
                FileStream fs1 = new FileStream(path, FileMode.Create, FileAccess.Write);//����д���ļ� 
                                                                                         //System.IO.File.SetAttributes(path, FileAttributes.Hidden);
                StreamWriter sw = new StreamWriter(fs1, System.Text.Encoding.UTF8);
                sw.WriteLine(info.Trim());//��ʼд��ֵ
                sw.Close();
                fs1.Close();
            }
            else
            {
                FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Write);
                //System.IO.File.SetAttributes(path, FileAttributes.Hidden);
                StreamWriter sr = new StreamWriter(fs,System.Text.Encoding.UTF8);
                sr.WriteLine(info.Trim());//��ʼд��ֵ
                sr.Close();
                fs.Close();
            }

        }


        public ValueTuple<string,string> RunCmd(string Command)
        {
            //System.Console.InputEncoding = System.Text.Encoding.UTF8;
            //System.Console.OutputEncoding = System.Text.Encoding.UTF8;
            string system32dir = System.Environment.GetEnvironmentVariable("windir") + "\\system32";
            Process process = new Process
            {
                StartInfo = {
                FileName = string.Format("{0}\\{1}", system32dir, "cmd.exe"), Arguments = @"C:\Windows\System32\cmd.exe", UseShellExecute = false, RedirectStandardInput = true, RedirectStandardOutput = true, Verb = "RunAs", CreateNoWindow = true, RedirectStandardError = true, StandardOutputEncoding = System.Text.Encoding.UTF8, StandardErrorEncoding = System.Text.Encoding.UTF8
            }
            };
            process.Start();

            process.StandardInput.WriteLine(@"C:");  //��ת��ϵͳ����
            process.StandardInput.WriteLine(@"cd C:\WINDOWS\system32\");  //��ת��CMD����Ŀ¼��
            process.StandardInput.WriteLine(Command);
            process.StandardInput.WriteLine("exit");
            process.WaitForExit();
            string str = process.StandardOutput.ReadToEnd();
            string str1 = process.StandardError.ReadToEnd();
            process.Close();
            return new ValueTuple<string,string>(str, str1);
            
        }

        

    }

}

