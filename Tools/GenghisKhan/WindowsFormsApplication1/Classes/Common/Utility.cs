using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Windows.Forms;
using System.Collections;
using System.Diagnostics;
using System.IO;
using System.Runtime.Serialization;

namespace WindowsFormsApplication1
{
    public class Utility
    {

        public static void BindDataGridView(ref DataGridView gridView, IEnumerable<object[]> data)
        {
            gridView.Rows.Clear();
            foreach(object[] objectRow in data)
            {
                DataGridViewRow row = new DataGridViewRow();

                foreach(object obj in objectRow)
                {
                    DataGridViewTextBoxCell textbox = new DataGridViewTextBoxCell();
                    textbox.Value = obj;
                    row.Cells.Add(textbox);
                }

                gridView.Rows.Add(row);
            }
        }

        public static void ArrayRandomShuffle<T>(ref T[] list)
        {
            Random rnd = new Random();

            for (int i= 0 ; i< list.Count(); i++)
            {
                int shuffleIndex = rnd.Next(list.Count());
                T tmpObject = list[shuffleIndex];

                // 交换位置
                list[shuffleIndex] = list[i];
                list[i] = tmpObject;
            }
        }

        public static void GetTopRandomArrayList<T>(IEnumerable<T> list, int top,ref List<T> outList)
        {
            T[] array = list.ToArray();
            ArrayRandomShuffle<T>(ref array);

            if (top > list.Count())
                top = list.Count();

            for (int i = 0; i < top; i++)
            {
                outList.Add(array[i]);
            }
        }

        public static void RunCmd(string cmd, out string output)
        {
            string CmdPath = @"C:\Windows\System32\cmd.exe";
            cmd = cmd.Trim().TrimEnd('&') + "&exit";//说明：不管命令是否成功均执行exit命令，否则当调用ReadToEnd()方法时，会处于假死状态
            using (Process p = new Process())
            {
                p.StartInfo.FileName = CmdPath;
                p.StartInfo.UseShellExecute = false;        //是否使用操作系统shell启动
                p.StartInfo.RedirectStandardInput = true;   //接受来自调用程序的输入信息
                p.StartInfo.RedirectStandardOutput = true;  //由调用程序获取输出信息
                p.StartInfo.RedirectStandardError = true;   //重定向标准错误输出
                p.StartInfo.CreateNoWindow = true;          //不显示程序窗口
                p.Start();//启动程序

                //向cmd窗口写入命令
                p.StandardInput.WriteLine(cmd);
                p.StandardInput.AutoFlush = true;

                //获取cmd窗口的输出信息
                output = p.StandardOutput.ReadToEnd();
                p.WaitForExit();//等待程序执行完退出进程
                p.Close();
            }
        }

        public static void WriteText(StringBuilder content, string fileName)
        {
            var encoding = new UTF8Encoding(true);

            fileName = fileName.Replace(":", "-").Replace("\\", "-");

            string path = Path.Combine(System.Environment.CurrentDirectory, fileName);
            File.WriteAllText(path, content.ToString(), encoding);
        }

        /// <summary>
        /// 将数据转换成json文本
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="standardFormat">是否采用标准格式，如果选择false则会压缩文本</param>
        /// <returns></returns>
        public static string parseDataToJsonString<T>(System.Collections.Generic.ICollection<T> list, bool standardFormat)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("[");
            if (standardFormat)
                sb.Append(Environment.NewLine);

            for (int i = 0; i < list.Count(); i++)
            {
                T config = list.ElementAt(i);
                sb.Append("\t{");
                if (standardFormat)
                    sb.Append(Environment.NewLine);

                var result = from p in config.GetType().GetProperties()
                             let attr = p.GetCustomAttributes(typeof(DataMemberAttribute), false)
                             where attr.Length > 0
                             select p;

                int count = 0;
                int allCount = result.Count();

                foreach (var pI in result)
                {
                    Type type = pI.PropertyType;
                    string name = pI.Name;
                    Object value = pI.GetValue(config);

                    if (type == typeof(String))
                    {
                        if (value == null)
                        {
                            value = String.Empty;
                        }
                        sb.AppendFormat("\t\t\"{0}\":\"{1}\"", name, value.ToString());
                    }
                    else
                    {
                        sb.AppendFormat("\t\t\"{0}\":{1}", name, value.ToString());
                    }

                    count++;
                    if (count < allCount)
                    {
                        sb.Append(",");
                        if (standardFormat)
                            sb.Append(Environment.NewLine);
                    }
                }

                if (i < list.Count() - 1)
                {
                    if (standardFormat)
                        sb.Append(Environment.NewLine + "\t}," + Environment.NewLine);
                    else
                        sb.Append("\t},");
                }
                else
                {
                    if (standardFormat)
                        sb.Append(Environment.NewLine + "\t}" + Environment.NewLine);
                    else
                        sb.Append("\t}");
                }
            }

            sb.Append("]");
            return sb.ToString();
        }
    }
}
