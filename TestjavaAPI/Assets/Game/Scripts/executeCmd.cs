using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public static class executeCmd
{
    public static int status=0;//0 is fail statu, 1 is success
    // Start is called before the first frame update
    public static string RunCmd(string Command)
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

        process.StandardInput.WriteLine(@"C:");  //先转到系统盘下
        process.StandardInput.WriteLine(@"cd C:\WINDOWS\system32\");  //再转到CMD所在目录下
        process.StandardInput.WriteLine(Command);
        process.StandardInput.WriteLine("exit");
        process.WaitForExit();
        string str = process.StandardOutput.ReadToEnd();
        string str1 = process.StandardError.ReadToEnd();
        process.Close();
        str = getoutput(str);
        if (false)
        {
            status = 0;
            return str1;
        }
        else
        {   
            status = 1;
            return str;
        }
    }


    private static string getoutput(string input)
    {
        input = input.Trim();
        string output = "";
        string[] lines = input.Split('\n');
        int i = 8;
        while (!lines[i].EndsWith("exit") && i < lines.Length)
        {
            output = output + lines[i] + "\n";
            i++;
        }
        //UnityEngine.Debug.Log(lines.Length);
        return output;
    }
































    //fail demo for execute cmd.exe
    /***
    static string RunCmd2(string cmdExe, string cmdStr)
    {
        bool result = false;
        string line = null;
        try
        {
            using (Process myPro = new Process())
            {
                myPro.StartInfo.FileName = "C:\\Windows\\system32\\cmd.exe";
                myPro.StartInfo.UseShellExecute = false;
                myPro.StartInfo.RedirectStandardInput = true;
                myPro.StartInfo.RedirectStandardOutput = true;
                myPro.StartInfo.RedirectStandardError = true;
                myPro.StartInfo.CreateNoWindow = true;
                myPro.Start();
                //如果调用程序路径中有空格时，cmd命令执行失败，可以用双引号括起来 ，在这里两个引号表示一个引号（转义）
                string str = string.Format(@"""{0}"" {1} {2}", cmdExe, cmdStr, "&exit");

                myPro.StandardInput.WriteLine(str);
                myPro.StandardInput.AutoFlush = true;

                StreamReader reader = myPro.StandardOutput;
                line=reader.ReadLine();
                while (!reader.EndOfStream)
                {
                    str += line + "  ";
                    line = reader.ReadLine();
                }
                reader.Close();
                myPro.WaitForExit();

                result = true;
            }
        }
        catch
        {

        }
        return line;
    }

    public static string ExecuteInCmd(string cmdline)
    {
        using (var process = new Process())
        {
            process.StartInfo.StandardOutputEncoding = Encoding.UTF8;
            process.StartInfo.FileName = @"C:\\Windows\\system32\\cmd.exe";
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.RedirectStandardInput = true;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.RedirectStandardError = true;
            process.StartInfo.CreateNoWindow = true;
            process.Start();
            process.StandardInput.AutoFlush = true;

            process.StandardInput.WriteLine(cmdline);
            process.StandardInput.WriteLine("exit");

            //获取cmd窗口的输出信息  
            string output = process.StandardOutput.ReadLine();

            if (!process.StandardOutput.EndOfStream)
            {
                output = output + process.StandardOutput.ReadLine() + "\n";
            }


            process.WaitForExit();
            process.Close();
            return output;
        }
    }
    ***/

}
