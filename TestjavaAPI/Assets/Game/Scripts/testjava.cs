using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using System.IO;
using System.Diagnostics;
using System.Text;
using System.Threading;

public class testjava : MonoBehaviour
{

    public InputField input;
    public Text output;
    public string javaName;
    public string path;

    // Start is called before the first frame update
    void Start()
    {
        System.Console.InputEncoding = System.Text.Encoding.UTF8;
        System.Console.OutputEncoding = System.Text.Encoding.UTF8;
        string[] lines = System.IO.File.ReadAllLines(path);
        foreach (string line in lines) {
            input.text +=line+ "\n";
        }
        
        output.text = "Output>\n";
    }

    // Update is called once per frame

    public void initial(string javapath, string name) {
        this.javaName= name;
        this.path= javapath;
        this.gameObject.SetActive(true);
    }

    public void compile() {
        /**
        string javacontent = input.text;
        string text = executeCmd.RunCmd(javacontent);
        output.text = text;
        UnityEngine.Debug.Log(text);  
        **/
        
        string javacontent = input.text;
        writeFile(path, javacontent);//写入java源码

        string javacCmd = @"javac -encoding UTF-8 " + path + " -d " + Application.dataPath + "/Game/javafile/class";
        //string javacCmd = "javac -encoding UTF-8 " + path ;
        UnityEngine.Debug.Log(javacCmd);    
        string outCmd1=executeCmd.RunCmd(javacCmd);//编译
        UnityEngine.Debug.Log(outCmd1);      
        
        
    }

    public void RunJava()
    {
        string javaCmd = @"java" + " -cp " + Application.dataPath + "/Game/javafile/class/;. " + javaName;
        UnityEngine.Debug.Log(javaCmd);
        string outCmd2 = executeCmd.RunCmd(javaCmd);//执行
        output.text = "Output>\n" + outCmd2;
        UnityEngine.Debug.Log(executeCmd.status);
    }

    void writeFile(string path, string info)
    {
        if (!System.IO.File.Exists(path))
        {
            //没有则创建这个文件
            FileStream fs1 = new FileStream(path, FileMode.Create, FileAccess.Write);//创建写入文件 
            //System.IO.File.SetAttributes(path, FileAttributes.Hidden);
            StreamWriter sw = new StreamWriter(fs1);
            sw.WriteLine(info.Trim());//开始写入值
            sw.Close();
            fs1.Close();
        }
        else
        {
            FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Write);
            //System.IO.File.SetAttributes(path, FileAttributes.Hidden);
            StreamWriter sr = new StreamWriter(fs);
            sr.WriteLine(info.Trim());//开始写入值
            sr.Close();
            fs.Close();
        }

    }


}
