using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Motel
{
    internal class Program
    {
        struct Login
        {
            public String tk;
            public String mk;
        }
        static List<Login> Logins = new List<Login>();
        static String filePath = @"D:\Code_C#\login.txt";
        static bool checkLogin(String tk, String mk)
        {
            foreach (var login in Logins) { if (login.tk == tk && login.mk == mk) return true; };
            return false;
        }
        static bool checkMK(string s)
        {
            int count_lower = 0;
            int count_upper = 0;
            int count_num = 0;
            for (int i = 0; i < s.Length; i++)
            {
                if (s[i] >= 'a' && s[i] <= 'z') count_lower++;
                if (s[i] >= 'A' && s[i] <= 'Z') count_upper++;
                if (s[i] > '0' && s[i] < '9') count_num++;
            }
            if (count_lower > 0 && count_num > 0 && count_upper > 0) return true;
            else return false;
        }
        static void create_Acounts()
        {
            Console.WriteLine("Đăng kí");
            string username = "", password = "";
            if (!File.Exists(filePath))
            {
                using (FileStream fs = new FileStream(filePath, FileMode.Create, FileAccess.Write))
                {
                }
            }
            while (true)
            {
                Console.Clear();
                int check_username;
                do
                {
                    check_username = -1;
                    Console.Write("Nhập tk: "); //Enter The Username..
                    username = Console.ReadLine().Trim();
                    foreach (var login in Logins)
                    {
                        if (login.tk == username) check_username += 1;
                    }
                    if (check_username >= 0) Console.WriteLine("[!] Mời nhập lại");
                } while (check_username >= 0);
                if (username.Length == 0) break;
                do
                {
                    Console.Write("Nhập mk: "); //Enter The Password..
                    password = Console.ReadLine().Trim();
                    if (!checkMK(password)) Console.WriteLine("[!] Mời nhập lại");
                } while (!checkMK(password));
                Console.WriteLine("Đăng kí thành công");
                Console.ReadLine();
                using (FileStream fs = new FileStream(filePath, FileMode.Append, FileAccess.Write))
                {
                    StreamWriter sw = new StreamWriter(fs);
                    sw.WriteLine(username + "#" + password);
                    sw.Flush();
                }
            }
        }
        static void read_Logins()
        {
            if (File.Exists(filePath))
            {
                string[] lines = File.ReadAllLines(filePath);
                //Console.WriteLine(lines.Length);
                foreach (string line in lines)
                {
                    string[] parts = line.Split('#');
                    if (parts.Length == 2)
                    {
                        Login login = new Login();
                        login.tk = parts[0];
                        login.mk = parts[1];
                        Logins.Add(login);
                    }
                }
            }
        }
        static string read_Password()
        {
            String password = "";
            ConsoleKeyInfo keyInfo;
            do
            {
                keyInfo = Console.ReadKey(true);

                if (keyInfo.Key != ConsoleKey.Enter && keyInfo.Key != ConsoleKey.Backspace)
                {
                    password += keyInfo.KeyChar;
                    Console.Write("*");
                }
                else if (keyInfo.Key == ConsoleKey.Backspace && password.Length > 0)
                {
                    password = password.Substring(0, password.Length - 1);
                    Console.Write("\b \b");
                }
            }
            while (keyInfo.Key != ConsoleKey.Enter);
            Console.WriteLine();
            return password;
        }
        static bool loginPr()
        {
            int count = 0;
            while (true)
            {
                Console.Clear();
                Console.WriteLine("╔══════════════════════════════════════════════════════╗");
                Console.WriteLine("║                                                      ║");
                Console.WriteLine("╠══════════════════════════════════════════════════════╣");
                Console.WriteLine("║                Account:                              ║");
                Console.WriteLine("║                Password:                             ║");
                Console.WriteLine("║                                                      ║");
                Console.WriteLine("╚════════════════[Enter to Continue]═══════════════════╝");

                Console.SetCursorPosition(26, 3);
                string username = Console.ReadLine().Trim();
                Console.SetCursorPosition(27, 4);
                string password = read_Password().Trim();
                bool loginSuccess = checkLogin(username, password);
                if (loginSuccess)
                {
                    Console.SetCursorPosition(10, 6);
                    Console.WriteLine("══════[ Login successful ]════");
                    Console.ReadKey();
                    return true;
                    //Display_Program(); --> Phần Menu chính
                }
                else
                {
                    count++;
                    Console.SetCursorPosition(9, 6);
                    Console.WriteLine("[ [!] Login failed. Please try again. ]══");
                    Console.ReadLine();
                }
                if (count == 5) return false;
            }
        }//-->Complete
        static void menu_register()
        {
            Console.WriteLine("╔══════════════════════════════════════════════════════╗");
            Console.WriteLine("║            Option 1: Đăng kí tài khoản               ║");
            Console.WriteLine("╠══════════════════════════════════════════════════════╣");
            Console.WriteLine("║            Option 2: Đăng nhập tài khoản             ║");
            Console.WriteLine("╠                                                      ╣");
            Console.WriteLine("╚══════════════════════════════════════════════════════╝");
        }
        static void intro()
        {
            Console.WriteLine("╔══════════════════════════════════════════════════════════════════════════════════════╗");
            Console.WriteLine("║                      ***TRƯỜNG SƯ PHẠM KỸ THUẬT HƯNG YÊN***                          ║ ");
            Console.WriteLine("║                             KHOA CÔNG NGHỆ THÔNG TIN                                 ║ ");
            Console.WriteLine("╠══════════════════════════════════════════════════════════════════════════════════════╣");
            Console.WriteLine("║                                                                                      ║");
            Console.WriteLine("║                                                                                      ║");
            Console.WriteLine("║                                 TẬP LỚN MÔN CSDL                                     ║");
            Console.WriteLine("║                              QUẢN LÝ KHÁCH THUÊ TRỌ                                  ║");
            Console.WriteLine("║                                                                                      ║");
            Console.WriteLine("║                   ╔═══════════════════════════════════════════════╗                  ║");
            Console.WriteLine("║                   ║ Giáo viên hướng dẫn   ║  Ngô Thanh Huyền      ║                  ║");
            Console.WriteLine("║                   ║ Sinh viên thực hiện   ║  Lê Đức Tuyển         ║                  ║");
            Console.WriteLine("║                   ║                       ║                       ║                  ║");
            Console.WriteLine("║                   ╚═══════════════════════════════════════════════╝                  ║");
            Console.WriteLine("║                                                                                      ║");
            Console.WriteLine("╚══════════════════════════════════[Enter to continue]═════════════════════════════════╝");
        }
        static void register()
        {
            Console.OutputEncoding = Encoding.UTF8;
            int select;
            string n;
            while (true)
            {

                read_Logins();
                Console.Clear();
                menu_register();
                do
                {
                    Console.SetCursorPosition(14, 5);
                    Console.Write("══Nhập lựa chọn(1-2): ════════");
                    Console.SetCursorPosition(36, 5);
                    n = Console.ReadLine();
                } while (!(int.TryParse(n, out select)));
                if (select == 1)
                {
                    create_Acounts();
                    Console.ReadLine();
                }
                else if (select == 2)
                {
                    if (loginPr()) break;
                    Console.WriteLine("Press Enter to continue.");
                    Console.ReadLine();
                }
            }
            Console.Clear();
            intro();
            Console.ReadLine();
        }
        static int select_in_range(string prompt, int min, int max)
        {
            Console.Write(prompt);
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
                    if (choice == "n") return -1;
                    if (choice == i)
                    {
                        int result = int.Parse(choice);
                        return result;
                    }
                }
                Console.Write(prompt);
                choice = (Console.ReadLine());
            }
        }
        static void show_menu()
        {
            Console.WriteLine("╔══════════════════════════════════════════════════════╗");
            Console.WriteLine("║            Option 1: Thanh Toán Dịch Vụ              ║");
            Console.WriteLine("╠══════════════════════════════════════════════════════╣");
            Console.WriteLine("║            Option 2: Khách Hàng                      ║");
            Console.WriteLine("╠══════════════════════════════════════════════════════╣");
            Console.WriteLine("║            Option 3: Phiếu chi                       ║");
            Console.WriteLine("╠══════════════════════════════════════════════════════╣");
            Console.WriteLine("║            Option 4: Báo Cáo                         ║");
            Console.WriteLine("╚══════════════════════════════════════════════════════╝");
        }
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.BackgroundColor = ConsoleColor.Blue;
            Room r = new Room();
            Room_month rm = new Room_month();
            Customer cs = new Customer();
            register();
            while (true)
            {
                Console.Clear();
                show_menu();
                int select = select_in_range("Nhập lựa chọn(1-4): ", 1, 4);
                if (select == 1)
                {
                    Console.Clear();
                    rm.main();
                }
                else if (select == 2)
                {
                    cs.Main();
                }
                else if (select == 3)
                {
                    int month_spent = select_in_range("Nhập lựa chọn(1-12): ", 1, 12);
                    if (month_spent != -1) r.option_4(month_spent);
                    Console.WriteLine("Press Enter to continue.");
                    Console.ReadLine();
                }
                else if (select == 4)
                {
                    int month_report = select_in_range("Nhập lựa chọn(1-12): ", 1, 12);
                    if (month_report != -1) r.report_all(month_report);
                    Console.WriteLine("Press Enter to continue.");
                    Console.ReadLine();
                }
                else { Console.Clear(); break; }
            }
            Console.ReadKey();
        }
    }
}
