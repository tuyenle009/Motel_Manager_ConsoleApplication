using Motel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Motel
{
    public class Room_month
    {

        public Room_month()
        {
        }
        #region lựa chọn phạm vi
        static int select_in_range(string prompt, int min, int max)
        {
            Console.WriteLine("╔═════════════════════════════════╗");
            Console.WriteLine("║" + prompt);
            Console.WriteLine("╚═════════════════════════════════╝");
            Console.SetCursorPosition(23, 1);
            //Console.WriteLine("Cursor position: ({0}, {1})", cot_left, hang_top);
            string choice = (Console.ReadLine().Trim());
            List<string> lst_choice = new List<string>();
            for (int i = min; i <= max; i++)
            {
                lst_choice.Add(i.ToString());
            }
            while (true)
            {
                foreach (string i in lst_choice)
                {
                    if (choice == "") return -1;
                    if (choice == i)
                    {
                        int result = int.Parse(choice);
                        return result;
                    }
                }
                Console.SetCursorPosition(1, 1);
                Console.Write(prompt + "                                                                 ");
                Console.SetCursorPosition(23, 1);
                choice = (Console.ReadLine());
            }
        }
        #endregion
        #region lấy thông tin của tháng trước, refrest và add vào tháng sau
        static List<string> merge_string(List<string> lst)
        {
            List<string> lst_str = new List<string>();
            string merge = "";
            int sum = int.Parse(lst[10]) + int.Parse(lst[11]) + int.Parse(lst[16]);
            merge = lst[0] + "#" + lst[1] + "#" + lst[3] + "#" + "0" + "#" + "0" + "#" + "0" + "#" + lst[7] + "#" + "0" + "#" + "0" + "#" + "0" + "#" + lst[10] + "#" + lst[11] + "#" + "1" + "#" + lst[16] + "#" + sum.ToString() + "#" + "0" + "#" + sum.ToString();
            lst_str.Add(merge);
            return lst_str;
        }
        #endregion
        #region ghi và đọc file
        private void write_file(int n, List<string> lst_str, string l_m_import)
        {
            using (FileStream fs = new FileStream(l_m_import, FileMode.Create, FileAccess.Write))
            {
                StreamWriter writer = new StreamWriter(fs);
                writer.WriteLine(n.ToString());
                for (int i = 0; i < lst_str.Count; i++)
                {
                    writer.WriteLine(lst_str[i]);
                }
                writer.Flush();
            }
        }
        private void read_file(string l_m_import, string l_m_before)
        {
            check_exist_file(l_m_before);
            List<string> lst = new List<string>();
            List<string> lst_merge = new List<string>();
            using (FileStream fs = new FileStream(l_m_before, FileMode.Open, FileAccess.Read))
            {
                StreamReader reader = new StreamReader(fs);
                int n = int.Parse(reader.ReadLine());
                for (int i = 0; i < n; i++)
                {
                    string st = reader.ReadLine();
                    lst.AddRange(st.Split('#'));
                    lst_merge.AddRange(merge_string(lst));
                    lst.Clear();
                }
                write_file(n, lst_merge, l_m_import);
            }
        }
        #endregion

        private string edit_link(int n)
        {
            string n_str = n.ToString();
            string l_m_n = @"D:\Code_C#\file_big_project\month\month" + $"{n_str}" + @"\phong_tro.txt";
            return l_m_n;
        }
        //--------------------------------------------------
        #region câu hỏi yes no
        internal string check_status(string content)
        {
        case3:
            string status = Console.ReadLine().ToUpper().Trim();
            if (status=="Y")
                return "Yes";
            else if (status == "N")
                return "No";
            else
            {
                Console.Write($"[!] Thông tin không hợp lệ\nMời nhập lại {content}: ");
                goto case3;
            }
        }
        #endregion
        //______________________________________DOI THANG___________________________
        internal void question(string l_m_import, string l_m_before, string check, int select_month)
        {
            Console.Write("Lấy dữ liệu từ tháng trước không(Y/N): ");
            check = check_status("lựa chọn(Y/N)");
            if (check == "Yes")
            {
                if (select_month == 1)
                {
                    l_m_import = edit_link(select_month);
                    l_m_before = edit_link(12);

                }
                else
                {
                    l_m_import = edit_link(select_month);
                    l_m_before = edit_link(select_month - 1);
                }
                read_file(l_m_import, l_m_before);
            }
            Console.Clear();
        }
        //____________________________________KIEM TRA SU TON TAI_________________________
        static void check_exist_file(string link1)
        {
            //string filePath = link1;
            if (!File.Exists(link1))
            {
                using (FileStream fs = new FileStream(link1, FileMode.Create, FileAccess.Write))
                {
                    StreamWriter sw = new StreamWriter(fs);
                    sw.WriteLine("0");
                    sw.Flush();
                }
            }
        }
        public void main()
        {
            Console.OutputEncoding = Encoding.UTF8;
            while (true)
            {
                Console.Clear();
                string l_m_import = "", l_m_before = "", check = "";
                int select_month = select_in_range("Mời nhập tháng(1,12): ", 1, 12);
                Console.WriteLine("");
                if (select_month == -1) break;
                else
                {
                    question(l_m_import, l_m_before, check, select_month);
                    Room room = new Room();
                    room.main(select_month);
                }

            }
        }
    }
}
