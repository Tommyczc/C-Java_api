using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class createjava : MonoBehaviour
{
    public InputField name;
    public string javaName;
    public string path;
    public GameObject coding;

    void Start()
    {
        System.Console.InputEncoding = System.Text.Encoding.UTF8;
        System.Console.OutputEncoding = System.Text.Encoding.UTF8;
    }

    public void createjavafile() { 
        javaName=name.text;
        updatejavafile(javaName);
        coding.GetComponent<testjava>().initial(path,javaName);
        this.gameObject.SetActive(false);
    }

    public void updatejavafile(string name) {
        string javacontent = "public class "+name+" {\n" +
            "    public static void main(String[] args) {\n" +
            "    }\n" +
            "}";
        path= Application.dataPath + "/Game/javafile/java/"+name+".java";
        writeFile(path, javacontent);
    }


    void writeFile(string path, string info)
    {
        if (!System.IO.File.Exists(path))
        {
            //没有则创建这个文件
            FileStream fs1 = new FileStream(path, FileMode.Create, FileAccess.Write);//创建写入文件 
            //System.IO.File.SetAttributes(path, FileAttributes.Hidden);
            StreamWriter sw = new StreamWriter(fs1, System.Text.Encoding.UTF8);
            sw.WriteLine(info.Trim());//开始写入值
            sw.Close();
            fs1.Close();
        }
        else
        {
            FileStream fs = new FileStream(path, FileMode.Append, FileAccess.Write);
            //System.IO.File.SetAttributes(path, FileAttributes.Hidden);
            StreamWriter sr = new StreamWriter(fs, System.Text.Encoding.UTF8);
            sr.WriteLine(info.Trim());//开始写入值
            sr.Close();
            fs.Close();
        }

    }
}
