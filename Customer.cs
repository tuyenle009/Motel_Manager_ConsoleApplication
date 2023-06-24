using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Motel
{
    internal class Customer
    {
        public string room, date_start, date_end;
        public float deposit;
        public Custormer_IF infor = new Custormer_IF();
        static string link_cs = @"D:\Code_C#\file_big_project\Customer\khach_hang.txt";
        static string link_id = @"D:\Code_C#\file_big_project\Customer\id_room.txt";
        //static string link_id_hire = @"D:\Code_C#\file_big_project\month\month1\hire_id_room.txt";
        static string link_contract = @"D:\Code_C#\file_big_project\Customer\contract.txt";
        static List<Customer> play_customer = new List<Customer>();

        public Customer(string room, string date_start, string date_end, float deposit, Custormer_IF infor)
        {
            this.room = room;
            this.date_start = date_start;
            this.date_end = date_end;
            this.deposit = deposit;
            this.infor = infor;
        }
        public Customer()
        {
        }
        //static int check_num()
        //{
        //    string score_string = Console.ReadLine();
        //    int type_score;
        //    while (!(int.TryParse(score_string, out type_score) && int.Parse(score_string) >= 0))
        //    {
        //        Console.Write($"[!] Mời nhập lại: ");
        //        score_string = Console.ReadLine();
        //    }
        //    return int.Parse(score_string);
        //}
        static string idRoom() // trả về lst id của tháng 1
        {
            string all_id = "";
            List<string> lst_id = new List<string>();
            check_exist_file(link_id);
            using (FileStream fs = new FileStream(link_id, FileMode.Open, FileAccess.Read))
            {
                StreamReader sr = new StreamReader(fs);
                string id = sr.ReadLine();
                lst_id.AddRange(id.Split('#')); // thêm id vào lst_id
            }
            foreach (string i in lst_id)// hiện thị ID
            {
                //Console.Write(i + " ");
                all_id += i + " ";
            }
            return all_id;
        }
        static string check_id(List<Customer> play_customer)
        {
            List<string> id_hire = new List<string>();
            string all_id_hire = "";
            for (int i = 0; i < play_customer.Count(); i++)
            {
                all_id_hire += play_customer[i].room + " ";
            }
            return all_id_hire;
        }
        static string import_id()
        {
            string id = idRoom();
            string id_hire = check_id(play_customer);
            Console.Write("Phòng: ");
            Console.WriteLine(id);
            Console.Write("Phòng đã thuê: ");
            Console.WriteLine(id_hire);
            string room;
            do // kiểm tra xem room có nằm trong id không 
            {
                Console.Write("Nhập lựa chọn phòng: ");
                room = Console.ReadLine().Trim().ToUpper();
                if (!(id.Contains(room) && !id_hire.Contains(room))) Console.WriteLine("[!] Thông tin không hợp lệ");
            } while (!(id.Contains(room) && !id_hire.Contains(room)));
            return room;
        }
        static string import_date(string input)
        {
        case1:
            Console.Write($"Nhập ngày {input}(tháng/ngày/năm): ");
            string date = Console.ReadLine().Trim();
            DateTime date_real;
            if (DateTime.TryParseExact(date, "MM/dd/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out date_real))
                return date;
            else goto case1;
        }
        static Custormer_IF importIF_customer()
        {
            string name, phone, birth, CMND, HKTT, sex;
            int n;
            double m;
            do
            {
                Console.Write("Nhập tên: ");
                name = Console.ReadLine().Trim();
                if (name.Length <= 0) Console.WriteLine("[!] Thông tin không hợp lệ");
            } while (name.Length <= 0);
            do
            {
                Console.Write("Nhập SĐT: ");
                phone = Console.ReadLine().Trim();
                if (!(int.TryParse(phone, out n) && phone.Length == 10 && phone[1] != '0' && phone[0] == '0')) Console.WriteLine("[!] Thông tin không hợp lệ");
            } while (!(int.TryParse(phone, out n) && phone.Length == 10 && phone[1] != '0' && phone[0] == '0'));
            do
            {
                Console.Write("Nhập giới tính(nam/nu): ");
                sex = Console.ReadLine().Trim();
                if (!(sex.ToUpper() == "NAM" || sex.ToUpper() == "NU")) Console.WriteLine("[!] Thông tin không hợp lệ");
            } while (!(sex.ToUpper() == "NAM" || sex.ToUpper() == "NU"));
            do
            {

                Console.Write("Nhập năm sinh: ");
                birth = Console.ReadLine();
                if (!(int.TryParse(birth, out n) && birth.Length > 0 && int.Parse(birth) <= 2010 && int.Parse(birth) >= 1900)) Console.WriteLine("[!] Thông tin không hợp lệ");

            } while (!(int.TryParse(birth, out n) && birth.Length > 0 && int.Parse(birth) <= 2010 && int.Parse(birth) >= 1900));
            do
            {
                Console.Write("Nhập CMND: ");
                CMND = Console.ReadLine().Trim();
                if (!(double.TryParse(CMND, out m) && CMND.Length == 12 && CMND[0] == '0')) Console.WriteLine("[!] Thông tin không hợp lệ");

            } while (!(double.TryParse(CMND, out m) && CMND.Length <= 12 && CMND[0] == '0'));
            do
            {
                Console.Write("Nhập HKTT: ");
                HKTT = Console.ReadLine().Trim();
                if (name.Length <= 0) Console.WriteLine("[!] Thông tin không hợp lệ");
            } while (name.Length <= 0);
            Custormer_IF cIF = new Custormer_IF(name, phone, sex, birth, CMND, HKTT);
            return cIF;
        }
        //__________________________READ CUSTORMER_________________________________
        static Customer import_customer()
        {

            float deposit = 0;
            string room;
            room = import_id();
        cs2:
            string date1 = import_date("đến");
            DateTime date_start = DateTime.Parse(date1);
            DateTime date_now = DateTime.Now;
            if (date_start > date_now)
            { Console.WriteLine("[!] Thông tin không hợp lệ"); goto cs2; }
        cs3:
            string date2 = import_date("đi");
            DateTime date_end = DateTime.Parse(date2);
            if (date_start > date_end)
            { Console.WriteLine("[!] Thông tin không hợp lệ"); goto cs3; }

            do
            {
                Console.Write("Nhập tiền cọc: ");
                string check_float = (Console.ReadLine());
                float.TryParse(check_float, out deposit);
                if (deposit == 0) Console.WriteLine("[!] Thông tin không hợp lệ");
            } while (deposit == 0);

            Custormer_IF cIF = importIF_customer();
            Customer cs = new Customer(room, date1, date2, deposit, cIF);
            return cs;
        }
        static void import_customers(List<Customer> play_customer)
        {

            string check;
            do
            {
                Console.Clear();
                string id = idRoom();
                string id_hire = check_id(play_customer);
                Console.Write("Phòng: ");
                Console.WriteLine(id);
                Console.Write("Phòng đã thuê: ");
                Console.WriteLine(id_hire);
                Console.Write("Yêu cầu thêm khách hàng(Y/N): ");
                check = Console.ReadLine().Trim().ToUpper();
                Console.Clear();
                if (check == "Y")
                {
                    play_customer.Add(import_customer());
                    write_customers_txt(play_customer);
                }
            } while (check != "N");
        }
        //__________________________PRINT CUSTORMER_________________________________

        static void print_custormer(List<Customer> lst_cs, int i)
        {
            Console.Write($"║{i + 1,3} | {lst_cs[i].room,5} | {lst_cs[i].date_start,11} | ");
            if (DateTime.Parse(lst_cs[i].date_end) < DateTime.Now)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write($"{lst_cs[i].date_end,11}");
                Console.ResetColor();//đặt lại màu ban đầu
                Console.BackgroundColor = ConsoleColor.Blue;
                Console.ForegroundColor = ConsoleColor.Yellow;
            }
            else
            {
                Console.Write($"{lst_cs[i].date_end,11}");
            }
            Console.Write($" | {lst_cs[i].deposit,10} | {lst_cs[i].infor.name,15} | {lst_cs[i].infor.phone,10} | {lst_cs[i].infor.sex,6} | {lst_cs[i].infor.birth,9} | {lst_cs[i].infor.CMND,10} | {lst_cs[i].infor.HKTT,10}  \n");

        }

        static void print_customers(List<Customer> lst_cs)
        {
            Console.WriteLine("╔═════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════╗");
            Console.WriteLine($"║{"STT",3} | {"Phòng",5} | {"Ngày ",11} | {"Ngày   ",11} | {"Tiền cọc",10} | {"Khách hàng",15} | {"SĐT ",10} | {"G.tính",6} | {"Năm sinh",9} | {"CMND  ",10} | {"HKTC",10}");
            Console.WriteLine($"║{"   ",3} | {"     ",5} | {"ký HĐ",11} | {"hết hạn",11} | {"        ",10} | {"          ",15} | {"    ",10} | {"      ",6} | {"        ",9} | {"      ",10} | {"    ",10}");
            Console.WriteLine("╠═════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════╣");

            for (int i = 0; i < lst_cs.Count(); i++)
            {
                print_custormer(lst_cs, i);
            }
            Console.WriteLine("╚═════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════╝");

        }
        //______________________________________WRITE CUSTOMER TXT_____________________________
        static void write_customer_txt(StreamWriter sw, Customer lst_cs)
        {

            sw.Write(lst_cs.room);
            sw.Write("#");

            sw.Write(lst_cs.date_start);
            sw.Write("#");

            sw.Write(lst_cs.date_end);
            sw.Write("#");

            sw.Write(lst_cs.deposit);
            sw.Write("#");

            sw.Write(lst_cs.infor.name);
            sw.Write("#");

            sw.Write(lst_cs.infor.phone);
            sw.Write("#");

            sw.Write(lst_cs.infor.sex);
            sw.Write("#");

            sw.Write(lst_cs.infor.birth);
            sw.Write("#");

            sw.Write(lst_cs.infor.CMND);
            sw.Write("#");

            sw.Write(lst_cs.infor.HKTT);
            sw.WriteLine();

        }
        static void write_customers_txt(List<Customer> lst_cs)
        {
            using (FileStream fs = new FileStream(link_cs, FileMode.Create, FileAccess.Write))
            {
                StreamWriter sw = new StreamWriter(fs);
                int count = lst_cs.Count;
                sw.WriteLine(Convert.ToString(count));
                for (int i = 0; i < count; i++)
                {
                    write_customer_txt(sw, lst_cs[i]);
                }
                sw.Flush();
            }

        }
        //__________________________READ_TXT_________________________________
        static Customer read_customer_from_txt(StreamReader sr, List<string> lst_if)
        {
            lst_if = sr.ReadLine().Split('#').ToList();
            Custormer_IF csIF = new Custormer_IF(lst_if[4], lst_if[5], lst_if[6], lst_if[7], lst_if[8], lst_if[9]);
            Customer cs = new Customer(lst_if[0], lst_if[1], lst_if[2], float.Parse(lst_if[3]), csIF);
            return cs;
        }

        static List<Customer> read_customers_from_txt()
        {
            List<Customer> lst_cs_txt = new List<Customer>();
            using (FileStream fs = new FileStream(link_cs, FileMode.Open, FileAccess.Read))
            {
                List<string> lst_if = new List<string>();
                StreamReader sr = new StreamReader(fs);
                int count = int.Parse(sr.ReadLine());
                for (int i = 0; i < count; i++)
                {
                    Customer cs = read_customer_from_txt(sr, lst_if);
                    lst_cs_txt.Add(cs);
                }
            }
            return lst_cs_txt;
        }



        //___________________________SUPER_FIND_____________________________________

        static string check_status_find(string content)
        {
        head:
            int cot_left = Console.CursorLeft;
            int hang_top = Console.CursorTop;
            //Console.WriteLine("Cursor position: ({0}, {1})", cot_left, hang_top);
            string status = Console.ReadLine().ToUpper();
            if (status == "Y")
                return "Yes";
            else if (status == "N")
                return "No";
            else
            {
                Console.SetCursorPosition(0, 9);
                Console.Write($"[!] Thông tin không hợp lệ\t\t\t\t\t\nMời nhập lại {content}(Y/N):\t\t\t\t\t\t ");
                Console.SetCursorPosition(29, 10);
                goto head;
            }
        }
        static void super_find_row(string import, List<Customer> result_find, List<String> lst_if, int n)
        {
            bool check = false;
            for (int i = 0; i < lst_if.Count; i++)
            {
                if (lst_if[i].Contains(import)) check = true;
            }
            if (check) result_find.Add(play_customer[n]);
        }
        static List<Customer> super_find_rows(string import)
        {
            List<Customer> result_find = new List<Customer>();      // những thông tin tìm kiếm trùng thì thêm vào list
            List<string> lst_if = new List<string>();
            using (FileStream fs = new FileStream(link_cs, FileMode.Open, FileAccess.Read))
            {
                StreamReader sr = new StreamReader(fs);
                int rows = int.Parse(sr.ReadLine());
                for (int i = 0; i < rows; i++)
                {
                    string st = sr.ReadLine();
                    lst_if.AddRange(st.Split('#'));
                    super_find_row(import, result_find, lst_if, i);
                    lst_if.Clear();
                }
            }
            return result_find;
        }
        static int print_super_find(string require)
        {
            List<Customer> lst_find = new List<Customer>();
            lst_find = play_customer;
            string inf, select;
            do
            {
                //____________check vị trí
                Console.WriteLine("");
                Console.WriteLine("");
                int cot_left = Console.CursorLeft;
                int hang_top = Console.CursorTop;
                //Console.WriteLine("Cursor position: ({0}, {1})", cot, hang);adsf
                //_______________________
                print_customers(lst_find);
                Console.SetCursorPosition(0, 0);
                Console.Write("Nhập thông tin muốn tìm: ");
                inf = Console.ReadLine();
                if (inf == "n") return -1;
                lst_find = super_find_rows(inf);
                Console.Clear();

            } while (lst_find.Count != 1);
            Console.WriteLine($"Nhập thông tin muốn tìm: {inf}");
            Console.WriteLine("");
            print_customers(lst_find);
            Console.Write($"Bạn muốn {require} thông tin này(Y/N): ");
            select = check_status_find("lựa chọn");
            int site = 0;
            // kiểm tra vị trí để chút thao tác chỉ cần lấy vị trí ra mà dùng thôi
            if (select == "Yes")
            {
                for (int i = 0; i < play_customer.Count; i++)
                {
                    if (play_customer[i].room == lst_find[0].room)
                    {
                        site = i;
                    }
                }
                return site;
            }
            else return site = -1;
        }
        // ________________________create | edit |  remove-----------------------------------
        static float check_space_float(string i_f, float value1)//Hàm nhận 1 chuỗi sẽ trả về độ dài của chuỗi theo yêu cầu| nếu chuỗi đó chưa đáp ứng độ dài yêu cầu
        {
            Console.Write("Nhập tiền cọc: ");
            string score_string = Console.ReadLine();
            if (score_string == "")
            {
                return value1;
            }
            float type_score;
            while (!(float.TryParse(score_string, out type_score) && float.Parse(score_string) >= 0))
            {
                Console.Write($"[!] Thông tin không hợp lệ\nMời nhập lại {i_f}: ");
                score_string = Console.ReadLine();
            }
            return float.Parse(score_string);
        }
        static string import_date_space(string input, string value)
        {
            DateTime date_real;
            while (true)
            {
                Console.Write($"Nhập ngày {input}(tháng/ngày/năm): ");
                string date = Console.ReadLine().Trim();
                if (date == "") return value;
                if (DateTime.TryParseExact(date, "MM/dd/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out date_real)) return date;
            }
        }
        static List<Customer> edit_customer(List<Customer> play_customer)
        {
            string name, phone, birth, CMND, HKTT, sex;

            Custormer_IF infor = new Custormer_IF();
            while (true)
            {
                Console.Clear();
                int site_edit = print_super_find("sửa");
                //____________________________________________
                string room;
                float deposit = 0;
                if (site_edit == -1) return play_customer;
                room = play_customer[site_edit].room;
            cs2:
                string date1 = import_date_space("đến", play_customer[site_edit].date_start);
                DateTime date_start = DateTime.Parse(date1);
                DateTime date_now = DateTime.Now;
                if (date_start > date_now)
                { Console.WriteLine("[!] Thông tin không hợp lệ"); goto cs2; }
            cs3:
                string date2 = import_date_space("đi", play_customer[site_edit].date_end);
                DateTime date_end = DateTime.Parse(date2);
                if (date_start > date_end)
                { Console.WriteLine("[!] Thông tin không hợp lệ"); goto cs3; }
                deposit = check_space_float("tiền cọc", play_customer[site_edit].deposit);
                int n;
                double m;

                Console.Write("Nhập tên: ");
                name = Console.ReadLine().Trim();
                if (name == "") name = play_customer[site_edit].infor.name;

                do
                {
                    Console.Write("Nhập SĐT: ");
                    phone = Console.ReadLine().Trim();
                    if (phone == "") phone = play_customer[site_edit].infor.phone;
                    if (!(int.TryParse(phone, out n) && phone.Length <= 10 && phone.Length > 0 || phone == "")) Console.WriteLine("[!] Thông tin không hợp lệ");
                } while (!(int.TryParse(phone, out n) && phone.Length <= 10 && phone.Length > 0 || phone == ""));
                do
                {
                    Console.Write("Nhập giới tính(nam/nữ): ");
                    sex = Console.ReadLine().Trim();
                    if (sex == "") sex = play_customer[site_edit].infor.sex;

                    if (!(sex.ToUpper() == "NAM" || sex.ToUpper() == "NỮ" || sex == "")) Console.WriteLine("[!] Thông tin không hợp lệ");
                } while (!(sex.ToUpper() == "NAM" || sex.ToUpper() == "NỮ" || sex == ""));
                do
                {
                    Console.Write("Nhập năm sinh: ");
                    birth = Console.ReadLine();
                    if (birth == "") birth = play_customer[site_edit].infor.birth;

                    if (!(int.TryParse(birth, out n) && int.Parse(birth) <= 2010 && int.Parse(birth) >= 1900 || birth == "")) Console.WriteLine("[!] Thông tin không hợp lệ");

                } while (!(int.TryParse(birth, out n) && int.Parse(birth) <= 2010 && int.Parse(birth) >= 1900 || birth == ""));
                do
                {
                    Console.Write("Nhập CMND: ");
                    CMND = Console.ReadLine().Trim();
                    if (CMND == "") CMND = play_customer[site_edit].infor.CMND;

                    if (!(double.TryParse(CMND, out m) && CMND.Length <= 12 || CMND == "")) Console.WriteLine("[!] Thông tin không hợp lệ");
                } while (!(double.TryParse(CMND, out m) && CMND.Length <= 12 || CMND == ""));

                Console.Write("Nhập HKTT: ");
                HKTT = Console.ReadLine().Trim();
                if (HKTT == "") HKTT = play_customer[site_edit].infor.HKTT;
                Custormer_IF cIF = new Custormer_IF(name, phone, sex, birth, CMND, HKTT);
                Customer cs = new Customer(room, date1, date2, deposit, cIF);
                play_customer[site_edit] = cs;
                write_customers_txt(play_customer);
            }

        }
        static List<Customer> remove_customer(List<Customer> play_customer)
        {
            while (true)
            {
                Console.Clear();
                int site_edit = print_super_find("xóa");
                if (site_edit == -1) return play_customer;
                play_customer.RemoveAt(site_edit);
                write_customers_txt(play_customer);
            }
        }
        //_____________________________LUA CHON THU TU________________________________
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
                    if (choice == "") return -1;
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
        static void check_exist_file(string link) //Kiem tra su ton tai
        {
            if (!File.Exists(link))
            {
                using (FileStream fs = new FileStream(link, FileMode.Create, FileAccess.Write))
                {
                    StreamWriter sw = new StreamWriter(fs);
                    sw.WriteLine("0");
                    sw.Flush();
                }
            }
        }
        static void show_menu_edit()
        {
            Console.WriteLine("╔══════════════════════════════════════════════════════╗");
            Console.WriteLine("║            Option 1: Xem Danh Sách KH                ║");
            Console.WriteLine("╠══════════════════════════════════════════════════════╣");
            Console.WriteLine("║            Option 2: Thêm Khách Hàng                 ║");
            Console.WriteLine("╠══════════════════════════════════════════════════════╣");
            Console.WriteLine("║            Option 3: Sửa Khách Hàng                  ║");
            Console.WriteLine("╠══════════════════════════════════════════════════════╣");
            Console.WriteLine("║            Option 4: Xóa Khách Hàng                  ║");
            Console.WriteLine("╠══════════════════════════════════════════════════════╣");
            Console.WriteLine("║            Option 5: In Hợp Đồng                     ║");
            Console.WriteLine("╚══════════════════════════════════════════════════════╝");
        }
        static void print_contract(List<Customer> play_customer)
        {
            DateTime currentDate = DateTime.Now;
            string formattedDate = currentDate.ToString("dd/MM/yyyy");

            int site = print_super_find("in hợp đồng");
            if (site != -1)
            {
                using (FileStream fs = new FileStream(link_contract, FileMode.Create, FileAccess.Write))
                {
                    StreamWriter sw = new StreamWriter(fs);
                    sw.WriteLine(@"                                                                        CỘNG HÒA XÃ HỘI CHỦ NGHĨA VIỆT NAM");
                    sw.WriteLine(@"                                                                            Độc Lập - Tự Do - Hạnh Phúc");
                    sw.WriteLine(@"                                                                              -------Ꝏ○Ꝏ-------");
                    sw.WriteLine(@"");
                    sw.WriteLine(@"			                                  HỢP ĐỒNG CHO THUÊ PHÒNG TRỌ");
                    sw.WriteLine(@"");
                    sw.WriteLine(@"                                           Hôm nay ngày " + $"{currentDate.ToString("dd")}" + "  tháng" + $" {currentDate.ToString("MM")}" + "   năm 2023. Tại số nhà 101 Giai Phạm, Hưng Yên.");
                    sw.WriteLine(@"");
                    sw.WriteLine(@"                                           BÊN CHO THUÊ        : (Bên A)");
                    sw.WriteLine(@"                                           - Do ông            : LÊ ĐỨC TUYỂN");
                    sw.WriteLine(@"                                           - Chức vụ           : GIÁM ĐỐC");
                    sw.WriteLine(@"                                           - Trụ sở chính      :  101 GIAI PHẠM YÊN MỸ HƯNG YÊN");
                    sw.WriteLine(@"                                           - Điện thoại        :  0344001111 - tuyenle@gmai.com");
                    sw.WriteLine(@"                                           - Tài khoản         :  0344001111 MB ");
                    sw.WriteLine(@"                                           BÊN THUÊ            : (Bên B)");
                    sw.WriteLine(@"                                           - Do ông(bà)        : " + $"{play_customer[site].infor.name.ToUpper()}");
                    sw.WriteLine(@"                                           - Sinh năm          : " + $"{play_customer[site].infor.birth}");
                    sw.WriteLine(@"                                           - Cmnd  số          : " + $"{play_customer[site].infor.CMND}");
                    sw.WriteLine(@"                                           - Sđt               : " + $"{play_customer[site].infor.phone}");
                    sw.WriteLine(@"                                           - Hktt              :" + $"{play_customer[site].infor.HKTT}");
                    sw.WriteLine(@"		                                      Hai bên cùng nhất trí thống nhất kỹ hợp đồng với các điều khoản sau:");
                    sw.WriteLine(@"");
                    sw.WriteLine(@"                                           ĐIỀU 1: NỘI DUNG HỢP ĐỒNG");
                    sw.WriteLine(@"                                                       * Bên A đồng ý. Bên B nhất trí thuê 01 mặt bằng số    :" + $"{play_customer[site].room}");
                    sw.WriteLine(@"                                                       * Kể từ ngày                                          : " + $"{play_customer[site].date_start}");
                    sw.WriteLine(@"                                                       * Đến ngày                                            : " + $"{play_customer[site].date_end}");
                    sw.WriteLine(@"                                           ĐIỀU 2: TIỀN ĐẶT CỌC");
                    sw.WriteLine(@"                                                       * Tiền thuê nhà bên B thanh toán cho bên A mùng 1 mỗi đầu tháng");
                    sw.WriteLine(@"                                                       * Bên B đặt cọc trước:     " + $"{play_customer[site].deposit}" + " VNĐ");
                    sw.WriteLine(@"                                                          cho bên A. tiền cọc sẽ trả lại đầy đủ cho bên thuê khi hết hợp đồng thuê");
                    sw.WriteLine(@"                                                          và thanh toán đầy đủ tiền điện nước. Phí dịch vụ và khoản khác liên quan");
                    sw.WriteLine(@"                                                       * Bên B ngừng hợp đồng trước sẽ bị mất tiền cọc.Ngược lại nếu bên A");
                    sw.WriteLine(@"                                                          ngừng hợp đồng trước thì phải bồi thường gấp đôi cho bên B");
                    sw.WriteLine(@"");
                    sw.WriteLine(@"                                                 ĐẠI DIỆN BÊN B                                                      ĐẠI DIỆN BÊN A");
                    sw.Flush();
                }
            }

        }

        public void Main()
        {
            Console.OutputEncoding = Encoding.UTF8;

            List<Customer> imp_cs = new List<Customer>();
            play_customer = read_customers_from_txt();
            while (true)
            {
                Console.Clear();
                show_menu_edit();
                int choice = select_in_range("Nhập lựa chọn(1-5): ", 1, 5);
                if (choice == 1)
                {
                    Console.Clear();
                    print_customers(play_customer);
                    Console.WriteLine("Press Enter to continue.");
                    Console.ReadLine();
                }
                else if (choice == 2)
                {
                    Console.Clear();
                    import_customers(play_customer);
                    Console.WriteLine("Press Enter to continue.");
                    Console.ReadLine();
                }
                else if (choice == 3)
                {
                    Console.Clear();
                    play_customer = edit_customer(play_customer);
                    Console.WriteLine("Press Enter to continue.");
                    Console.ReadLine();
                }
                else if (choice == 4)
                {
                    Console.Clear();
                    play_customer = remove_customer(play_customer);
                    Console.WriteLine("Press Enter to continue.");
                    Console.ReadLine();
                }
                else if (choice == 5)
                {
                    Console.Clear();
                    print_contract(play_customer);
                    Console.WriteLine("Press Enter to continue.");
                    Console.ReadLine();
                }
                else
                {
                    Console.Clear();
                    break;
                }
            }
        }
    }
}
