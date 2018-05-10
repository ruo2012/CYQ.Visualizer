using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CYQ.VisualierSetup
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("软件按以下规则寻找VS安装路径：");
            Console.WriteLine("*:\\Program Files*\\Microsoft Visual Studio*\\Common7\\Packages\\Debugger\\Visualizers");
            Console.WriteLine("如果你的安装路径不符合，可手工Copy：CYQ.Data.dll、CYQ.Visualizer.dll到对应的路径即可！");
            Console.WriteLine("------------------------------------------------------");
            ClearCYQData();
            try
            {
                string runPath = AppDomain.CurrentDomain.BaseDirectory;
                List<string> cd = new List<string>();
                cd.Add("C:\\Program Files\\");
                cd.Add("D:\\Program Files\\");
                cd.Add("E:\\Program Files\\");
                cd.Add("F:\\Program Files\\");
                cd.Add("G:\\Program Files\\");
                cd.Add("H:\\Program Files\\");

                cd.Add("C:\\Program Files (x86)\\");
                cd.Add("D:\\Program Files (x86)\\");
                cd.Add("E:\\Program Files (x86)\\");
                cd.Add("F:\\Program Files (x86)\\");
                cd.Add("G:\\Program Files (x86)\\");
                cd.Add("H:\\Program Files (x86)\\");
                Dictionary<string, string> dic = new Dictionary<string, string>();
                dic.Add("Microsoft Visual Studio 8", "2005");
                dic.Add("Microsoft Visual Studio 8.0", "2005");
                dic.Add("Microsoft Visual Studio 9", "2008");
                dic.Add("Microsoft Visual Studio 9.0", "2008");
                dic.Add("Microsoft Visual Studio 10", "2010");
                dic.Add("Microsoft Visual Studio 10.0", "2010");
                dic.Add("Microsoft Visual Studio 11", "2012");
                dic.Add("Microsoft Visual Studio 11.0", "2012");
                dic.Add("Microsoft Visual Studio 12", "2013");
                dic.Add("Microsoft Visual Studio 12.0", "2013");
                dic.Add("Microsoft Visual Studio 14", "2015");
                dic.Add("Microsoft Visual Studio 14.0", "2015");
                dic.Add("Microsoft Visual Studio 2017\\Community", "2017");
                //读取VS安装路径
                string vPath = "\\Common7\\Packages\\Debugger\\Visualizers";
                string mPath = "\\VC#\\Snippets\\2052\\Visual C#";

                //需要引入Debug模式生成的DLL，才能避免调试时弹出的提示。
                string cyqdataDll = runPath + "\\CYQ.Data.dll";
                foreach (string item in cd)
                {
                    foreach (KeyValuePair<string, string> kv in dic)
                    {
                        string vFolder = item + kv.Key + vPath;
                        if (Directory.Exists(vFolder))
                        {
                            string dll = runPath + kv.Value + "\\CYQ.Visualizer.dll";

                            if (File.Exists(dll))
                            {
                                File.Copy(dll, vFolder + "\\CYQ.Visualizer.dll", true);
                                File.Copy(cyqdataDll, vFolder + "\\CYQ.Data.dll", true);
                                Console.WriteLine("To：" + vFolder + "\\CYQ.Visualizer.dll");
                            }
                        }
                        string mFoler = item + kv.Key + mPath;
                        if (Directory.Exists(mFoler) && Directory.Exists(runPath + "\\snippet"))
                        {
                            string[] files = Directory.GetFiles(runPath + "\\snippet", "*.snippet");
                            foreach (string file in files)
                            {
                                File.Copy(file, mFoler + "\\" + Path.GetFileName(file), true);
                                Console.WriteLine("To：" + mFoler + "\\" + Path.GetFileName(file));
                            }
                        }
                    }
                }
                Console.WriteLine("Completed!（Press enter to exit）");
            }
            catch (Exception err)
            {
                Console.WriteLine(err.Message);
                Console.WriteLine("若权限问题：右键，用管理员身份运行即可！");
                Console.WriteLine("If the authority: Right, with the administrator can run!");
            }
            Console.Read();
        }
        static void ClearCYQData()
        {
            string runPath = AppDomain.CurrentDomain.BaseDirectory;
            try
            {
                if (File.Exists(runPath + "\\CYQ.Data.pdb"))
                {
                    File.Delete(runPath + "\\CYQ.Data.pdb");
                }
                if (File.Exists(runPath + "\\CYQ.Data.xml"))
                {
                    File.Delete(runPath + "\\CYQ.Data.xml");
                }
                if (File.Exists(runPath + "\\CYQ.VisualierSetup.pdb"))
                {
                    File.Delete(runPath + "\\CYQ.VisualierSetup.pdb");
                }
            }
            catch (Exception err)
            {
                Console.WriteLine(err.Message);
            }
            try
            {
                string[] folders = Directory.GetDirectories(runPath);
                if (folders != null)
                {

                    foreach (string item in folders)
                    {
                        if (File.Exists(item + "\\Microsoft.VisualStudio.DebuggerVisualizers.dll"))
                        {
                            File.Delete(item + "\\Microsoft.VisualStudio.DebuggerVisualizers.dll");
                        }
                        if (File.Exists(item + "\\CYQ.Visualizer.pdb"))
                        {
                            File.Delete(item + "\\CYQ.Visualizer.pdb");
                        }

                        string[] files = Directory.GetFiles(item, "CYQ.Data.*");
                        if (files != null)
                        {
                            foreach (string file in files)
                            {
                                File.Delete(file);
                            }

                        }
                    }
                }
            }
            catch (Exception err)
            {
                Console.WriteLine(err.Message);
            }
        }
    }
}
