using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Configuration;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Messaging;
using System.Security.Cryptography;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace Motel
{
    public class Room
    {
        public string id_room, status;
        public double garbage, rent, num_months, old_debt, all_bil, remain, collect_money;
        static double spent = 0;
        public Solve_elect elec_bil;
        public Solve_water water_bil;
        static string link = @"D:\Code_C#\file_big_project\month\month1\phong_tro.txt";
        static string link_bill = @"D:\Code_C#\file_big_project\month\month1\bill.txt";
        static string link_all_bill = @"D:\Code_C#\file_big_project\month\month1\all_bill.txt";
        static string link_all_spend = @"D:\Code_C#\file_big_project\month\month1\spend.txt";
        static string link_report = @"D:\Code_C#\file_big_project\month\month1\report.txt";
        static List<Room> play_rooms = new List<Room>();
        public Room(string id_room, string status, Solve_elect elec_bil, Solve_water water_bil, double garbage, double rent, double num_months, double old_debt, double all_bil, double collect_money, double remain)
        {
            this.id_room = id_room;
            this.status = status;
            this.elec_bil = elec_bil;
            this.water_bil = water_bil;
            this.garbage = garbage;
            this.rent = rent;
            this.num_months = num_months;
            this.old_debt = old_debt;
            this.all_bil = all_bil;
            this.collect_money = collect_money;
            this.remain = remain;
        }
        public Room()
        {
            this.id_room = "";
            this.status = "";
            this.elec_bil = new Solve_elect();
            this.water_bil = new Solve_water();
            this.garbage = 0;
            this.rent = 0;
            this.old_debt = 0;
            this.all_bil = 0;
            this.collect_money = 0;
            this.remain = 0;

        }
        #region kiem tra yes/ no

        //_______________________________YES/NO__________________________
        static string check_status(string content)
        {
            do
            {
                Console.Write($"{content}: ");
                string status = Console.ReadLine().ToUpper();
                if (status == "Y")
                    return "Yes";
                else if (status == "N")
                    return "No";
            } while (true);
        }
        #endregion

        //_______________________________ID___________________________________
        static List<string> lst_ID(ref int stt)
        {
            List<string> lst_id = new List<string>();
            List<string> lst_split = new List<string>();
            using (FileStream fs = new FileStream(link, FileMode.Open, FileAccess.Read))
            {
                StreamReader sr = new StreamReader(fs);
                stt = int.Parse(sr.ReadLine());
                for (int i = 0; i < stt; i++)
                {
                    string st = sr.ReadLine();
                    lst_split.AddRange(st.Split('#'));
                    lst_id.Add(lst_split[0]);
                    lst_split.Clear();
                }
            }
            using (FileStream fs = new FileStream(@"D:\Code_C#\file_big_project\Customer\id_room.txt", FileMode.Create, FileAccess.Write))
            {
                StreamWriter sw = new StreamWriter(fs);
                foreach (string i in lst_id)
                {
                    sw.Write(i);
                    sw.Write("#");
                }
                sw.Flush();
            }
            return lst_id;
        }
        static string check_ID()
        {
            int stt = 0;
            List<string> lst_id = lst_ID(ref stt);
            bool check;
            string id;
            do
            {
                check = false;
                id = "P" + stt;
                for (int i = 0; i < lst_id.Count(); i++)
                {
                    if (id == lst_id[i])
                    {
                        check = true;
                        stt += 1;
                    }
                }
            } while (check);
            return id;
        }


        #region kiểm tra xem 1 chuỗi có phải int hay ko, return int
        static int check_num(string i_f)
        {
            string score_string = Console.ReadLine();
            int type_score;
            while (!(int.TryParse(score_string, out type_score) && int.Parse(score_string) >= 0))
            {
                Console.Write($"[!] Mời nhập lại {i_f}: ");
                score_string = Console.ReadLine();
            }
            return int.Parse(score_string);
        }
        #endregion
        #region kiểm tra xem 1 chuỗi có phải số thực hay không trả về giá trị ép kiểu số double
        static double check_num_double(string i_f)
        {
            string score_string = Console.ReadLine();
            double type_score;
            while (!(double.TryParse(score_string, out type_score) && double.Parse(score_string) >= 0))
            {
                Console.Write($"[!] Mời nhập lại {i_f}: ");
                score_string = Console.ReadLine();
            }
            return double.Parse(score_string);
        }
        #endregion
        //________________________________DIEN/NUOC__________________________________________
        #region tính tiền điện xog cho vào list chứa class thông tin tiền điện
        static Solve_elect solve_elec()
        {
        case1:
            int old_num, new_num;
            Console.Write("Nhập số cũ: ");
            old_num = check_num("số cũ");// số cũ
            Console.Write("Nhập số mới: ");
            new_num = check_num("số mới"); // số mới
            if (new_num < old_num)
            {
                Console.WriteLine("[!] Thông tin không hợp lệ");
                goto case1;
            }
            double total_cost; // tổng tiền điện
            int use = new_num - old_num;
            if (use <= 50)
            {
                total_cost = use * 1700;
            }
            else if (use <= 100)
            {
                total_cost = 50 * 1700 + (use - 50) * 1800;
            }
            else if (use <= 200)
            {
                total_cost = 50 * 1700 + 50 * 1800 + (use - 100) * 2000;
            }
            else if (use <= 300)
            {
                total_cost = 50 * 1700 + 50 * 1800 + 100 * 2000 + (use - 200) * 2500;
            }
            else
            {
                total_cost = 50 * 1700 + 50 * 1800 + 100 * 2000 + 100 * 2500 + (use - 300) * 3000;
            }
            Console.WriteLine("______________________________________");
            Console.WriteLine("Tổng tiền điện: " + total_cost);
            Solve_elect solve_elect = new Solve_elect(old_num, new_num, use, 1700, total_cost);

            return solve_elect;
        }
        #endregion
        #region tính tiền nước
        static Solve_water solve_water()
        {
        case2:
            int old_num, new_num;
            Console.Write("Nhập số cũ: ");
            old_num = check_num("số cũ");// số cũ
            Console.Write("Nhập số mới: ");
            new_num = check_num("số mới"); // số mới
            if (new_num < old_num)
            {
                Console.WriteLine("[!] Thông tin không hợp lệ");
                goto case2;
            }
            double total_cost; // tổng tiền nước
            int use = new_num - old_num;
            if (use <= 10)
                total_cost = use * 6000;
            else if (use <= 20)
                total_cost = 10 * 6000 + (use - 10) * 7000;
            else if (use <= 30)
                total_cost = 10 * 6000 + 10 * 7000 + (use - 20) * 9000;
            else
                total_cost = 10 * 6000 + 10 * 7000 + 10 * 9000 + (use - 30) * 16000;

            Console.WriteLine("______________________________________");
            Console.WriteLine("Tổng tiền nước: " + total_cost);
            Solve_water solve_water = new Solve_water(old_num, new_num, use, 6000, total_cost);
            return solve_water;
        }
        #endregion
        #region ghi vào class room 1 database
        //________________________________________NHAP THONG TIN__________________________________

        static Room read_room(string id)
        {
            string id_room, status;
            double garbage, rent, num_months, old_debt, all_bil, remain, collect_money;
            Solve_elect elec_bil;
            Solve_water water_bil;
            status = check_status("Tình trạng phòng(Y/N)");
            Console.WriteLine("              Electric");
            elec_bil = solve_elec();
            Console.WriteLine("              Water");
            water_bil = solve_water();
            Console.Write("Nhập giá rác: ");
            garbage = check_num_double("giá rác");
            Console.Write("Nhập giá thuê: ");
            rent = check_num_double("giá thuê");
            Console.Write("Nhập số tháng thuê: ");
            num_months = check_num_double("tháng thuê");
            Console.Write("Nhập nợ cũ: ");
            old_debt = check_num_double("nợ cũ");
            Console.Write("Nhập tiền đã trả: ");
            collect_money = check_num_double("tiền đã trả");
            id_room = id;
            all_bil = elec_bil.total_cost + water_bil.total_cost + garbage + rent * num_months + old_debt;
            remain = all_bil - collect_money;
            Room room = new Room(id_room, status, elec_bil, water_bil, garbage, rent, num_months, old_debt, all_bil, collect_money, remain);
            return room;
        }
        #endregion
        #region ghi nhieu thong tin vao class --> list

        static void read_rooms(List<Room> play_room)
        {
            string id, check;
            Room room = new Room();
            //List<Room> lst_room = new List<Room>();
            do
            {
                Console.Clear();
                check = check_status("Yêu cầu thêm phòng(Y/N)");
                if (check == "Yes")
                {
                    id = check_ID();
                    room = read_room(id);
                    play_room.Add(room);
                    write_rooms_txt(play_room);

                }
            } while (check != "No");
            //return gather_lst;
        }
        #endregion
        #region in thông tin 1 phòng
        //________________________________IN THONG TIN__________________________________________

        static void print_room(Room room, int i)
        {

            Console.WriteLine($"║{i,4}| {room.id_room,4} | {room.status,6} | {room.elec_bil.old_num,8} | {room.elec_bil.new_num,8} | {room.elec_bil.use,8} | {(room.elec_bil.total_cost),8} | {room.water_bil.old_num,8} | {room.water_bil.new_num,8} | {room.water_bil.use,8} | {(room.water_bil.total_cost),8} | {room.garbage,8} | {room.rent,8} | {room.num_months,6} | {room.old_debt,8} | {(room.all_bil),8} | {room.collect_money,6} | {room.remain,4}");

            //17
        }
        #endregion
        #region in thông tin nhiều phòng

        static void print_rooms(List<Room> Rooms)
        {
            Console.WriteLine("╔══════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════╗");
            Console.WriteLine($"║{"Stt",4}| {"ID",4} | {"Hiện",6} | {"",8}   {"Điện",8}   {"",8}   {"",8} | {"",8}   {"Nước",8}   {"",8}   {"",8} | {"Rác",8} | {"Tiền ",8} | {"Số",6} | {"Nợ ",8} | {"Tổng",8} | {"Đã",6} | {"Còn ",4}");
            Console.WriteLine($"║{"",4}| {"",4} | {"trạng",6} | {"S.Cũ",8} | {"S.Mới",8} | {"T.Thụ",8} | {"T.tiền",8} | {"S.Cũ",8} | {"S.Mới",8} | {"T.Thụ",8} | {"T.tiền",8} | {"",8} | {"",8} | {"tháng",6} | {"cũ",8} | {"",8} | {"thu",6} | {"lại ",4}");
            Console.WriteLine("╠══════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════╣");

            for (int i = 0; i < Rooms.Count(); i++)
            {
                print_room(Rooms[i], i + 1);
            }
            Console.WriteLine("╚══════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════╝");

        }
        #endregion
        //________________________________GHI THONG TIN VAO TXT__________________________________________

        #region ghi 1 database vào tệp ngăn bởi #
        static void write_room_txt(StreamWriter file, Room room)
        {
            file.Write(room.id_room); //1
            file.Write("#");
            file.Write(room.status);
            file.Write("#");
            file.Write(room.elec_bil.old_num.ToString());
            file.Write("#");
            file.Write(room.elec_bil.new_num.ToString());
            file.Write("#");
            file.Write(room.elec_bil.use.ToString());
            file.Write("#");
            file.Write(room.elec_bil.total_cost.ToString());
            file.Write("#");
            file.Write(room.water_bil.old_num.ToString());
            file.Write("#");
            file.Write(room.water_bil.new_num.ToString());
            file.Write("#");
            file.Write(room.water_bil.use.ToString());
            file.Write("#");
            file.Write(room.water_bil.total_cost.ToString());
            file.Write("#");
            file.Write(room.garbage.ToString());
            file.Write("#");
            file.Write(room.rent.ToString());
            file.Write("#");
            file.Write(room.num_months.ToString());
            file.Write("#");
            file.Write(room.old_debt.ToString());
            file.Write("#");
            file.Write(room.all_bil.ToString());
            file.Write("#");
            file.Write(room.collect_money.ToString());
            file.Write("#");
            file.Write(room.remain.ToString()); //17
            file.WriteLine();
        }
        #endregion 
        #region ghi nhiều database vào tệp ngăn bởi #
        static void write_rooms_txt(List<Room> rooms)
        {
            using (FileStream fs = new FileStream(link, FileMode.Create, FileAccess.Write))
            {
                StreamWriter file = new StreamWriter(fs);
                int count = rooms.Count;
                file.WriteLine(Convert.ToString(count));
                for (int i = 0; i < count; i++)
                {
                    write_room_txt(file, rooms[i]);
                }
                file.Flush();
            }
        }
        #endregion
        #region Đọc dữ liệu từ file sau đó ghi vào List_txt

        //________________________________DOC THONG TIN TU TXT__________________________________________

        static Room read_room_from_txt(StreamReader file, List<string> lst_if)
        {
            lst_if = file.ReadLine().Split('#').ToList();
            Solve_elect elec_bil = new Solve_elect(int.Parse(lst_if[2]), int.Parse(lst_if[3]), int.Parse(lst_if[4]), 1700, double.Parse(lst_if[5]));
            Solve_water water_bil = new Solve_water(int.Parse(lst_if[6]), int.Parse(lst_if[7]), int.Parse(lst_if[8]), 6000, double.Parse(lst_if[9]));
            Room rooms = new Room(lst_if[0], lst_if[1], elec_bil, water_bil, double.Parse(lst_if[10]), double.Parse(lst_if[11]), double.Parse(lst_if[12]), double.Parse(lst_if[13]), double.Parse(lst_if[14]), double.Parse(lst_if[15]), double.Parse(lst_if[16]));
            return rooms;
        }
        // ghi nhiều dữ liệu từ file vào list rooms
        static List<Room> read_rooms_from_txt()
        {
            List<Room> lst_room_txt = new List<Room>();
            using (FileStream fs = new FileStream(link, FileMode.Open, FileAccess.Read))
            {
                List<string> lst_if = new List<string>();
                StreamReader file = new StreamReader(fs);
                int count = int.Parse(file.ReadLine());
                for (int i = 0; i < count; i++)
                {
                    Room rooms = read_room_from_txt(file, lst_if);
                    lst_room_txt.Add(rooms);
                }
                return lst_room_txt;
            }

        }
        #endregion
        // ________________________EDIT |  REMOVE_________________________________________
        #region Ghộp list lại để tiện kiểm soát thông tin _ create
        #endregion
        //________________________________REMOVE__________________________________________

        static List<Room> remove_room(List<Room> play_rooms)
        {
            //int count = play_rooms.Count();
            while (true)
            {
                Console.Clear();
                int site_remove = print_super_find("Xóa");
                if (site_remove == -1) return play_rooms;
                play_rooms.RemoveAt(site_remove);
                write_rooms_txt(play_rooms);
            }

        }
        // kiểm tra xem nếu khà enter thì sẽ lấy dữ liệu cũ
        static double check_space_double(string i_f, double value1)
        {
            string score_string = Console.ReadLine();
            if (score_string == "")
            {
                return value1;
            }
            double type_score;
            while (!(double.TryParse(score_string, out type_score) && double.Parse(score_string) >= 0))
            {
                Console.Write($"[!] Thông tin không hợp lệ\nMời nhập lại {i_f}: ");
                score_string = Console.ReadLine();
            }
            return double.Parse(score_string);
        }
        //chỉnh sửa phòng cụ thể
        //________________________________EDIT__________________________________________

        static Room edit_new_room(string id, string f_sta, Solve_elect f_elec, Solve_water f_wat, double f_gar, double f_rent, double f_month, double f_deb, double f_bil, double f_collec, double f_remain)
        {
            // ghi các thông tin
            string id_room, status;
            double garbage, rent, num_months, old_debt, all_bil, remain, collect_money;
            Solve_elect elec_bil;
            Solve_water water_bil;
            //string select;
            //Console.Write("Nhập tình trạng phòng (Y/N): ");
            status = check_status("Tình trạng phòng (Y/N)");
            Console.WriteLine("              Electric");
            elec_bil = solve_elec();
            Console.WriteLine("              Water");
            water_bil = solve_water();
            Console.Write("Nhập giá rác: ");
            garbage = check_space_double("giá rác", f_gar);
            Console.Write("Nhập giá thuê: ");
            rent = check_space_double("giá thuê", f_rent);
            Console.Write("Nhập tháng thuê: ");
            num_months = check_space_double("tháng thuê", f_month);
            Console.Write("Nhập nợ cũ: ");
            old_debt = check_space_double("nợ cũ", f_deb);
            Console.Write("Nhập tiền đã trả: ");
            collect_money = check_space_double("tiền đã trả", f_collec);
            //Console.Cleark();
            id_room = id;
            all_bil = elec_bil.total_cost + water_bil.total_cost + garbage + rent * num_months + old_debt;
            remain = all_bil - collect_money;
            // giữ lại thông tin cũ hoặc lấy thông tin mới

            Room room = new Room(id_room, status, elec_bil, water_bil, garbage, rent, num_months, old_debt, all_bil, collect_money, remain);
            return room;
        }
        // thêm phòng vào list
        static List<Room> edit_room(List<Room> play_rooms)
        {
            string id_room;
            string f_sta;
            Solve_elect f_elec;
            Solve_water f_wat;
            double f_gar, f_rent, f_month, f_deb, f_bil, f_collec, f_remain;
            Room room_new = new Room();
            while (true)
            {
                Console.Clear();
                int site_edit = print_super_find("sửa");
                int count = play_rooms.Count();
                if (site_edit == -1) return play_rooms;
                for (int i = 0; i < count; i++)
                {
                    if (site_edit == i)
                    {
                        id_room = play_rooms[i].id_room;
                        f_sta = play_rooms[i].status;
                        f_elec = play_rooms[i].elec_bil;
                        f_wat = play_rooms[i].water_bil;
                        f_gar = play_rooms[i].garbage;
                        f_rent = play_rooms[i].rent;
                        f_month = play_rooms[i].num_months;
                        f_deb = play_rooms[i].old_debt;
                        f_bil = play_rooms[i].all_bil;
                        f_collec = play_rooms[i].collect_money;
                        f_remain = play_rooms[i].remain;
                        room_new = edit_new_room(id_room, f_sta, f_elec, f_wat, f_gar, f_rent, f_month, f_deb, f_bil, f_collec, f_remain);
                        play_rooms[i] = room_new;
                        write_rooms_txt(play_rooms);
                    }
                }
            }
        }
        //-----------------------------------LUA CHON--------------------------------------------

        #region chọn chỉ số

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
        #endregion
        // kiểm tra xem file có tồn tại không (exist)


        //___________________________SUPER FIND_____________________________________

        //check_status_for_superfind
        static string check_status_find(string content)
        {
        case3:
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
                goto case3;
            }
        }
        static void super_find_row(string import, List<Room> result_find, List<String> lst_if, int n)
        {
            bool check = false;
            for (int i = 0; i < lst_if.Count; i++)
            {
                if (lst_if[i].Contains(import)) check = true;
            }
            if (check)
            {
                result_find.Add(play_rooms[n]);
            }
        }
        static List<Room> super_find_rows(string import)
        {
            List<Room> result_find = new List<Room>();      // những thông tin tìm kiếm trùng thì thêm vào list
            List<string> lst_if = new List<string>();
            using (FileStream fs = new FileStream(link, FileMode.Open, FileAccess.Read))
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


        // tìm phòng gần đúng theo tên và trả về vị trí của phòng đó trong list
        static int print_super_find(string require)
        {
            List<Room> lst_find = new List<Room>();
            lst_find = play_rooms;
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
                print_rooms(lst_find);
                Console.SetCursorPosition(0, 0);
                Console.Write("Nhập thông tin muốn tìm: ");
                inf = Console.ReadLine();
                if (inf == "n") return -1;
                lst_find = super_find_rows(inf);
                Console.Clear();

            } while (lst_find.Count != 1);
            Console.WriteLine($"Nhập thông tin muốn tìm: {inf}");
            Console.WriteLine("");
            print_rooms(lst_find);
            Console.Write($"Bạn muốn {require} thông tin này(Y/N): ");
            select = check_status_find("lựa chọn");
            int site = 0;
            // kiểm tra vị trí để chút thao tác chỉ cần lấy vị trí ra mà dùng thôi
            if (select == "Yes")
            {
                for (int i = 0; i < play_rooms.Count; i++)
                {
                    if (play_rooms[i].id_room == lst_find[0].id_room) site = i;
                }
                return site;
            }
            else return site = -1;
        }
        //_____________________________PRINT BILL____________________________________
        static void print_bill(List<Room> lst_bill)
        {
            int site = print_super_find("in bill");
            if (site != -1)
            {
                using (FileStream fs = new FileStream(link_bill, FileMode.Create, FileAccess.Write))
                {
                    StreamWriter sr = new StreamWriter(fs);
                    sr.WriteLine(@"			      MOTEL TUYEN LE");
                    sr.WriteLine(@"			  ĐC: 89 Giai Pham - Hung Yen");
                    sr.WriteLine(@"			  ĐT: 0344001111");
                    sr.WriteLine(@"		***************************************");
                    sr.WriteLine(@"			PHIẾU THANH TOÁN");
                    sr.WriteLine(@"		Phòng:                                       " + $"{lst_bill[site].id_room}");
                    sr.WriteLine(@"		Tháng:                                       " + $"{lst_bill[site].num_months}/2023");
                    sr.WriteLine(@"		1. Tiền Thuê:                                " + $"{lst_bill[site].rent}");
                    sr.WriteLine(@"		2. Điện ( 1700 KW / VNĐ ) ");
                    sr.WriteLine(@"		     - Số mới:                               " + $"{lst_bill[site].elec_bil.new_num}");
                    sr.WriteLine(@"		     - Số cũ:                                " + $"{lst_bill[site].elec_bil.old_num}");
                    sr.WriteLine(@"		     - Tiêu thụ:                             " + $"{lst_bill[site].elec_bil.use}");
                    sr.WriteLine(@"		     - Thành tiền:                           " + $"{lst_bill[site].elec_bil.total_cost}");
                    sr.WriteLine(@"		3. Nước ( 6000 M3 / VNĐ )");
                    sr.WriteLine(@"		     - Số mới:                               " + $"{lst_bill[site].water_bil.new_num}");
                    sr.WriteLine(@"		     - Số cũ:                                " + $"{lst_bill[site].water_bil.old_num}");
                    sr.WriteLine(@"		     - Tiêu thụ:                             " + $"{lst_bill[site].water_bil.use}");
                    sr.WriteLine(@"		     - Thành tiền:                           " + $"{lst_bill[site].water_bil.total_cost}");
                    sr.WriteLine(@"		4. Rác:                                      " + $"{lst_bill[site].garbage}");
                    sr.WriteLine(@"		5. Nợ cũ:                                    " + $"{lst_bill[site].old_debt}");
                    sr.WriteLine(@"		────────────────────────────────────");
                    sr.WriteLine(@"		Tổng thanh toán:                             " + $"{lst_bill[site].all_bil}");
                    sr.WriteLine(@"		────────────────────────────────────");
                    sr.WriteLine(@"		Ghi chú:");
                    sr.WriteLine(@"		     ┌─────────────────────────────┐");
                    sr.WriteLine(@"");
                    sr.WriteLine(@"		     └─────────────────────────────┘");
                    sr.WriteLine(@"			♥Xin chân thành cảm ơn♥");
                    sr.Flush();
                }
            }

        }
        //________________________________CĂN LỀ_______________________________________
        static string alignment_double(int len_default, double data)
        {
            string data_str = " " + data.ToString().Trim();
            int lenght_data;
            while (true)
            {
                lenght_data = data_str.Length;
                if (lenght_data == len_default) return data_str;
                data_str += " ";
            }
        }

        static string alignment_string(int len_default, string data)
        {
            int lenght_data;
            string data_str = " " + data;
            if (data_str.Length >= len_default) return data_str;
            while (true)
            {
                lenght_data = data_str.Length;
                if (lenght_data == len_default) return data_str;
                data_str += " ";
            }
        }

        //________________________________IN TẤT CẢ HÓA ĐƠN_______________________________________

        static void print_all_bill(List<Room> play_rooms, int select_month)
        {
            double all_elec = 0, all_water = 0, collected = 0, collect = 0, all = 0, customer = 0, blank = 0;
            for (int i = 0; i < play_rooms.Count; i++)
            {
                all_elec += play_rooms[i].elec_bil.total_cost;
                all_water += play_rooms[i].water_bil.total_cost;
                if (play_rooms[i].collect_money == 0) collect += 1;
                if (play_rooms[i].collect_money != 0) collected += 1;
                if (play_rooms[i].status == "Yes") customer += 1;
                if (play_rooms[i].status == "No") blank += 1;
                all += play_rooms[i].remain;
            }
            using (FileStream fs = new FileStream(link_all_bill, FileMode.Create, FileAccess.Write))
            {

                StreamWriter sr = new StreamWriter(fs);
                sr.WriteLine(@"								PHIẾU IN TỔNG HỢP");
                sr.WriteLine(@"══════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════");
                sr.WriteLine($"MOTEL TUYEN LE	    	Tháng {select_month}/2023		Đã thu:	{collected}		Chưa thu: {collect}	 	Tổng: {all}VNĐ		      Có khách: {customer}	         Trống: {blank}");
                sr.WriteLine($"Điện: {all_elec}VNĐ		Nước: {all_water}VNĐ");
                sr.WriteLine(@"══════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════");
                sr.WriteLine("");
                for (int i = 0; i < play_rooms.Count; i++)
                {
                    sr.Write($"{alignment_string("      ".Length, (i + 1).ToString())}|");
                    sr.Write($"{alignment_string("          ".Length, "P: " + play_rooms[i].id_room)}|");
                    sr.Write($"{alignment_string(" Yes    ".Length, play_rooms[i].status)}|");
                    sr.Write($"{alignment_string(" S.moi:  1 ".Length, "Điện( S.mới: " + play_rooms[i].elec_bil.old_num.ToString())}|");
                    sr.Write($"{alignment_string(" S.cũ:  1 ".Length, "S.cũ: " + play_rooms[i].elec_bil.new_num.ToString())}|");
                    sr.Write($"{alignment_string(" S.cũ:  1 ".Length, "Tổng: " + play_rooms[i].elec_bil.total_cost.ToString())})|");
                    sr.Write($"{alignment_string(" S.moi:  1 ".Length, "Nước( S.mới: " + play_rooms[i].water_bil.old_num.ToString())}|");
                    sr.Write($"{alignment_string(" S.cũ:  1 ".Length, "S.cũ: " + play_rooms[i].water_bil.new_num.ToString())}|");
                    sr.Write($"{alignment_string(" S.cũ:  1 ".Length, "Tổng: " + play_rooms[i].water_bil.total_cost.ToString())})|");
                    sr.Write($"{alignment_string(" S.cũ:  1 ".Length, "Rác: " + play_rooms[i].garbage.ToString())}|");
                    sr.Write($"{alignment_string(" S.cũ:  1 ".Length, "T.Thuê: " + play_rooms[i].rent.ToString())}|");
                    sr.Write($"{alignment_string(" S.cũ:  1 ".Length, "Tháng: " + play_rooms[i].num_months.ToString())}|");
                    sr.Write($"{alignment_string(" S.cũ:  1 ".Length, "Nợ cũ: " + play_rooms[i].old_debt.ToString())}|");
                    sr.Write($"{alignment_string(" S.cũ:  1 ".Length, "Tổng: " + play_rooms[i].all_bil.ToString())}|");
                    sr.WriteLine(@"────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────");


                }
                sr.Flush();
                //Console.ReadKey();
            }
        }

        //______________________________REPORT ALL________________________________

        public void report_all(int month)
        {
            Console.Clear();
            List<List<string>> lst_spent = read_spent_fromt_txt(month); // trong hàm read... sẽ gán số tiền vào spent 
            edit_link(month);
            check_exist_file(link);
            List<Room> play_rooms = read_rooms_from_txt();
            Console.WriteLine("");
            double all_elec = 0, all_water = 0, garbage = 0, rent = 0, collected = 0, collect = 0, debt = 0, all = 0, collected_money = 0, remain = 0, all_customer = 0, customer = 0, blank = 0, kw = 0, m3 = 0;
            for (int i = 0; i < play_rooms.Count; i++)
            {
                all_elec += play_rooms[i].elec_bil.total_cost;
                all_water += play_rooms[i].water_bil.total_cost;
                if (play_rooms[i].collect_money == 0) collect += 1;
                if (play_rooms[i].collect_money != 0) { collected += 1; collected_money += play_rooms[i].collect_money; }
                if (play_rooms[i].remain != 0) { remain += play_rooms[i].remain; }
                if (play_rooms[i].old_debt != 0) { debt += play_rooms[i].old_debt; }
                if (play_rooms[i].garbage != 0) { garbage += play_rooms[i].garbage; }
                if (play_rooms[i].rent != 0) { rent += play_rooms[i].rent; }
                if (play_rooms[i].elec_bil.use != 0) { kw += play_rooms[i].elec_bil.use; }
                if (play_rooms[i].water_bil.use != 0) { m3 += play_rooms[i].water_bil.use; }
                if (play_rooms[i].status == "Yes") { customer += 1; all_customer += 1; }
                if (play_rooms[i].status == "No") { blank += 1; all_customer += 1; }
                all += play_rooms[i].remain;
            }
            Console.WriteLine("                        Từ:   " + month + "/2023            Đến:   " + month + "/2023");
            Console.WriteLine("");
            Console.WriteLine("             1.Thông tin phòng                  2.Thống kê thu tiền ");
            Console.WriteLine("                *Tổng    :    " + alignment_double(15, all_customer) + "     Đã thu  : " + alignment_double(15, collected));
            Console.WriteLine("                *Có khách:    " + alignment_double(15, customer) + "     Chưa thu: " + alignment_double(15, collect));
            Console.WriteLine("                *Trống   :    " + alignment_double(15, blank));
            Console.WriteLine("              3.Mức tiêu thụ");
            Console.WriteLine("                *Điện (KW) :  " + kw);
            Console.WriteLine("                *Nước (M3):   " + m3);
            Console.WriteLine("              4.Dịch vụ");
            Console.WriteLine("                *Tiền điện :  " + all_elec);
            Console.WriteLine("                *Tiền nước :  " + all_water);
            Console.WriteLine("                *Tiền rác  :  " + garbage);
            Console.WriteLine("                *Tiền thuê :  " + rent);
            Console.WriteLine("                *Nợ cũ     :  " + debt);
            Console.WriteLine("                *Tổng cộng :  " + all);
            Console.WriteLine("                *Đã thu    :  " + collected_money);
            Console.WriteLine("                *Còn lai   :  " + remain);
            Console.WriteLine("    ____________________________________________________________________");
            Console.WriteLine("");
            Console.WriteLine("               Đã chi   : " + spent);
            Console.WriteLine("               Lợi nhuận: " + (all - spent));
            Console.WriteLine("");
            Console.WriteLine("");
            //GHI REPORT VÀO TXT
            using (FileStream fs = new FileStream(link_report, FileMode.Create, FileAccess.Write))
            {
                StreamWriter sw = new StreamWriter(fs);
                sw.WriteLine("                                   BÁO CÁO THU CHI HÀNG THÁNG");
                sw.WriteLine("");
                sw.WriteLine("                        Từ:   " + month + "/2023            Đến:   " + month + "/2023");
                sw.WriteLine("");
                sw.WriteLine("             1.Thông tin phòng                  2.Thống kê thu tiền ");
                sw.WriteLine("                *Tổng    :    " + alignment_double(15, all_customer) + "     Đã thu  : " + alignment_double(15, collected));
                sw.WriteLine("                *Có khách:    " + alignment_double(15, customer) + "     Chưa thu: " + alignment_double(15, collect));
                sw.WriteLine("                *Trống   :    " + alignment_double(15, blank));
                sw.WriteLine("              3.Mức tiêu thụ");
                sw.WriteLine("                *Điện (KW) :  " + kw);
                sw.WriteLine("                *Nước (M3):   " + m3);
                sw.WriteLine("              4.Dịch vụ");
                sw.WriteLine("                *Tiền điện :  " + all_elec);
                sw.WriteLine("                *Tiền nước :  " + all_water);
                sw.WriteLine("                *Tiền rác  :  " + garbage);
                sw.WriteLine("                *Tiền thuê :  " + rent);
                sw.WriteLine("                *Nợ cũ     :  " + debt);
                sw.WriteLine("                *Tổng cộng :  " + all);
                sw.WriteLine("                *Đã thu    :  " + collected_money);
                sw.WriteLine("                *Còn lai   :  " + remain);
                sw.WriteLine("    ____________________________________________________________________");
                sw.WriteLine("");
                sw.WriteLine("               Đã chi   : " + spent);
                sw.WriteLine("               Lợi nhuận: " + (all - spent));
                sw.WriteLine("");
                sw.Flush();
            }
        }

        //______________________________FILTER________________________________

        static void filter_blank(List<Room> play_rooms)
        {
            List<Room> lst_blank = new List<Room>();
            for (int i = 0; i < play_rooms.Count; i++)
            {
                if (play_rooms[i].status == "No")
                {
                    lst_blank.Add(play_rooms[i]);
                }
            }
            print_rooms(lst_blank);
        }
        static void filter_customer(List<Room> play_rooms)
        {
            List<Room> lst = new List<Room>();
            for (int i = 0; i < play_rooms.Count; i++)
            {
                if (play_rooms[i].status == "Yes")
                {
                    lst.Add(play_rooms[i]);
                }
            }
            print_rooms(lst);
        }
        static void filter_collected(List<Room> play_rooms)
        {
            List<Room> lst = new List<Room>();
            for (int i = 0; i < play_rooms.Count; i++)
            {
                if (play_rooms[i].collect_money != 0) lst.Add(play_rooms[i]);
            }
            print_rooms(lst);
        }
        static void filter_collect(List<Room> play_rooms)
        {
            List<Room> lst = new List<Room>();
            for (int i = 0; i < play_rooms.Count; i++)
            {
                if (play_rooms[i].collect_money == 0) lst.Add(play_rooms[i]);
            }
            print_rooms(lst);
        }



        //_____________________________LINK____________________________________
        static void edit_link(int n)//CHỈNH SỬA LINK THEO THÁNG NHẬP VÀO
        {
            string n_str = n.ToString();
            string l_m = @"D:\Code_C#\file_big_project\month\month" + $"{n_str}" + @"\phong_tro.txt";
            string l_bill = @"D:\Code_C#\file_big_project\month\month" + $"{n_str}" + @"\bill.txt";
            string l_all_bill = @"D:\Code_C#\file_big_project\month\month" + $"{n_str}" + @"\all_bill.txt";
            string l_all_spend = @"D:\Code_C#\file_big_project\month\month" + $"{n_str}" + @"\spend.txt";
            string l_report = @"D:\Code_C#\file_big_project\month\month" + $"{n_str}" + @"\report.txt";
            link = l_m;
            link_bill = l_bill;
            link_all_bill = l_all_bill;
            link_all_spend = l_all_spend;
            link_report = l_report;

        }
        //______________________________MENU________________________________
        static void show_menu_edit()
        {
            Console.WriteLine("╔══════════════════════════════════════════════════════╗");
            Console.WriteLine("║            Option 1: Xem Phòng                       ║");
            Console.WriteLine("╠══════════════════════════════════════════════════════╣");
            Console.WriteLine("║            Option 2: Thêm Phòng                      ║");
            Console.WriteLine("╠══════════════════════════════════════════════════════╣");
            Console.WriteLine("║            Option 3: Sửa Phòng                       ║");
            Console.WriteLine("╠══════════════════════════════════════════════════════╣");
            Console.WriteLine("║            Option 4: Xóa Phòng                       ║");
            Console.WriteLine("╚══════════════════════════════════════════════════════╝");
        }


        static void show_menu_bill()
        {
            Console.WriteLine("╔══════════════════════════════════════════════════════╗");
            Console.WriteLine("║            Option 1: In 1 hóa đơn                    ║");
            Console.WriteLine("╠══════════════════════════════════════════════════════╣");
            Console.WriteLine("║            Option 2: In nhiều hóa đơn                ║");
            Console.WriteLine("╚══════════════════════════════════════════════════════╝");
        }
        static void show_menu_filter()
        {
            Console.WriteLine("╔══════════════════════════════════════════════════════╗");
            Console.WriteLine("║            Option 1: Lọc danh sách phòng trống       ║");
            Console.WriteLine("╠══════════════════════════════════════════════════════╣");
            Console.WriteLine("║            Option 2: Lọc danh sách phòng có khách    ║");
            Console.WriteLine("╠══════════════════════════════════════════════════════╣");
            Console.WriteLine("║            Option 3: Lọc danh sách phòng đã thu      ║");
            Console.WriteLine("╠══════════════════════════════════════════════════════╣");
            Console.WriteLine("║            Option 4: Lọc danh sách phòng chưa thu    ║");
            Console.WriteLine("╚══════════════════════════════════════════════════════╝");
        }

        static void show_menu_main()
        {
            Console.WriteLine("╔══════════════════════════════════════════════════════╗");
            Console.WriteLine("║            Option 1: Thao Tác Phòng                  ║");
            Console.WriteLine("╠══════════════════════════════════════════════════════╣");
            Console.WriteLine("║            Option 2: Lọc Danh Sách                   ║");
            Console.WriteLine("╠══════════════════════════════════════════════════════╣");
            Console.WriteLine("║            Option 3: In Hóa Đơn                      ║");
            Console.WriteLine("╚══════════════════════════════════════════════════════╝");
        }

        static void option_1()
        {
            while (true)
            {
                string id = check_ID();
                Console.Clear();
                show_menu_edit();
                int choice = select_in_range("Nhập lựa chọn(1-4): ", 1, 4);
                if (choice == 1)
                {
                    Console.Clear();
                    print_rooms(play_rooms);
                    Console.WriteLine("Press Enter to continue.");
                    Console.ReadLine();
                }
                else if (choice == 2)
                {
                    Console.Clear();
                    read_rooms(play_rooms);
                    Console.WriteLine("Press Enter to continue.");
                    Console.ReadLine();

                }
                else if (choice == 3)
                {
                    Console.Clear();
                    play_rooms = edit_room(play_rooms);
                    Console.WriteLine("Press Enter to continue.");
                    Console.ReadLine();

                }
                else if (choice == 4)
                {
                    Console.Clear();
                    play_rooms = remove_room(play_rooms);
                    Console.WriteLine("Press Enter to continue.");
                    Console.ReadLine();

                }
                else { Console.Clear(); break; }

            }
        }
        static void option_2()
        {
            int select;
            do
            {
                Console.Clear();
                show_menu_filter();
                select = select_in_range("Nhập lựa chọn(1-4): ", 1, 4);
                if (select == 1)
                {
                    Console.Clear();
                    filter_blank(play_rooms);
                    Console.WriteLine("Press Enter to continue.");
                    Console.ReadLine();
                }
                else if (select == 2)
                {
                    Console.Clear();
                    filter_customer(play_rooms);
                    Console.WriteLine("Press Enter to continue.");
                    Console.ReadLine();
                }
                else if (select == 3)
                {
                    Console.Clear();
                    filter_collected(play_rooms);
                    Console.WriteLine("Press Enter to continue.");
                    Console.ReadLine();
                }
                else if (select == 4)
                {
                    Console.Clear();
                    filter_collect(play_rooms);
                    Console.WriteLine("Press Enter to continue.");
                    Console.ReadLine();
                }
            } while (select != -1);
        }
        static void option_3(int select_month)
        {
            int select;
            do
            {
                Console.Clear();
                show_menu_bill();
                select = select_in_range("Nhập lựa chọn(1-2): ", 1, 2);
                if (select == 1)
                {
                    Console.Clear();
                    print_bill(play_rooms);
                    Console.WriteLine("Press Enter to continue.");
                    Console.ReadLine();
                }
                else if (select == 2)
                {
                    print_all_bill(play_rooms, select_month);
                    Console.WriteLine("Press Enter to continue.");
                    Console.ReadLine();
                }
            } while (select != -1);
        }
        //________________________________CHECK EXIST_______________________________________

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
        public void main(int select_month)
        {
            Console.OutputEncoding = Encoding.UTF8;
            edit_link(select_month);
            check_exist_file(link);

            //------------------------------------
            play_rooms = read_rooms_from_txt();
            //------------------------------------

            while (true)
            {
                Console.Clear();
                int choice_menu;
                show_menu_main();
                choice_menu = select_in_range("Nhập lựa chọn(1-3): ", 1, 3);
                if (choice_menu == 1) { Console.Clear(); option_1(); }
                else if (choice_menu == 2) { Console.Clear(); option_2(); }
                else if (choice_menu == 3) { Console.Clear(); option_3(select_month); }
                else break;
            }
        }
        //____________________________SPEND_________________________________
        static List<string> read_spend()
        {
            List<string> lst = new List<string>();
            DateTime date = DateTime.Now;
            string name, content, cost;
            int n;
            string dateToStr = date.ToString("MM/dd");
            do
            {
                Console.Write("Người tạo: ");
                name = Console.ReadLine();
                if (name.Length <= 0) Console.WriteLine("[!] Thông tin không hợp lệ");
            } while (name.Length <= 0);
            //Console.WriteLine();
            do
            {
                Console.Write("Nội dung: ");
                content = Console.ReadLine();
                if (content.Length <= 0) Console.WriteLine("[!] Thông tin không hợp lệ");
            } while (content.Length <= 0);
            do
            {
                Console.Write("Số tiền: ");
                cost = Console.ReadLine().Trim();
                if (!(int.TryParse(cost, out n) && cost.Length <= 10 && cost.Length > 0)) Console.WriteLine("[!] Thông tin không hợp lệ");
            } while (!(int.TryParse(cost, out n) && cost.Length <= 10 && cost.Length > 0));
            lst.Add(dateToStr);
            lst.Add(name);
            lst.Add(content);
            lst.Add(cost);
            return lst;
        }

        static List<List<string>> read_spends()
        {
            List<string> lst = new List<string>();
            List<List<string>> lst2 = new List<List<string>>();
            Console.Write("Nhập số lượng chi: ");
            int n = check_num("lượng chi");
            for (int i = 0; i < n; i++)
            {
                Console.WriteLine($"Lượng chi {i + 1}: ");
                lst = read_spend();
                lst2.Add(lst);
            }
            return lst2;
        }
        static void write_spend_txt(List<List<string>> lst)
        {
            using (FileStream fs = new FileStream(link_all_spend, FileMode.Create, FileAccess.Write))
            {
                StreamWriter sw = new StreamWriter(fs);
                sw.WriteLine(lst.Count());
                for (int i = 0; i < lst.Count(); i++)
                {
                    sw.Write(lst[i][0]);
                    sw.Write("#");
                    sw.Write(lst[i][1]);
                    sw.Write("#");
                    sw.Write(lst[i][2]);
                    sw.Write("#");
                    sw.Write(lst[i][3]);
                    sw.WriteLine();
                }
                sw.Flush();
            }
        }
        static List<List<string>> read_spent_fromt_txt(int month)
        {
            List<string> lst = new List<string>();
            edit_link(month);
            List<List<string>> lst2 = new List<List<string>>();
            check_exist_file(link_all_spend);
            using (FileStream fs = new FileStream(link_all_spend, FileMode.Open, FileAccess.Read))
            {
                StreamReader sr = new StreamReader(fs);
                int n = int.Parse(sr.ReadLine());
                for (int i = 0; i < n; i++)
                {
                    string s1 = sr.ReadLine();
                    lst = s1.Split('#').ToList();
                    lst2.Add(lst);
                }

            }
            double plus = 0;
            spent = 0;
            for (int i = 0; i < lst2.Count; i++) // lấy thông tin từ trong lst gán giá trị vào số tiền đã chi 
            {
                plus += double.Parse(lst2[i][3]);
                spent = plus;
            }
            return lst2;
        }
        static void print_spent(List<string> lst, int i)
        {
            Console.WriteLine($"{i,4} | {lst[0],8} | {lst[1],15} | {lst[2],20} | {lst[3],10} ");
        }
        static void print_spents(List<List<string>> lst2)
        {
            Console.WriteLine($"{"STT",4} | {"Ngày tạo",8} | {"Người tạo",15} | {"Nội dung",20} | {"Số tiền",10} ");
            Console.WriteLine("_______________________________________________________________________________");
            for (int i = 0; i < lst2.Count(); i++)
            {
                print_spent(lst2[i], i + 1);
            }
        }
        static void remove_spent(List<List<string>> lst2)
        {
            while (true)
            {
                Console.Clear();
                print_spents(lst2);
                string site = lst2.Count().ToString();
                if (1 > lst2.Count()) break;
                int select = select_in_range("Xóa vị trí thứ(1," + site + "): ", 1, lst2.Count());
                if (select == -1) break;
                lst2.RemoveAt(select - 1);
                write_spend_txt(lst2);
            }

        }
        static void edit_spent(List<List<string>> lst2)
        {
            List<string> lst = new List<string>();
            while (true)
            {
                Console.Clear();
                print_spents(lst2);
                string site = lst2.Count().ToString();
                if (1 > lst2.Count()) break;
                int select = select_in_range("Sửa vị trí thứ(1," + site + "): ", 1, lst2.Count());
                if (select == -1) break;
                lst = read_spend();
                lst2[select - 1] = lst;
                write_spend_txt(lst2);
            }
        }
        static void show_menu_spent()
        {
            Console.WriteLine("╔══════════════════════════════════════════════════════╗");
            Console.WriteLine("║            Option 1: Xem Phiếu Chi                   ║");
            Console.WriteLine("╠══════════════════════════════════════════════════════╣");
            Console.WriteLine("║            Option 2: Thêm Phiếu chi                  ║");
            Console.WriteLine("╠══════════════════════════════════════════════════════╣");
            Console.WriteLine("║            Option 3: Xóa phiếu chi                   ║");
            Console.WriteLine("╠══════════════════════════════════════════════════════╣");
            Console.WriteLine("║            Option 4: Sửa phiếu chi                   ║");
            Console.WriteLine("╚══════════════════════════════════════════════════════╝");
        }
        public void option_4(int month)
        {
            edit_link(month);
            check_exist_file(link_all_spend);
            Console.WriteLine("");
            List<List<string>> lst = new List<List<string>>();
            List<List<string>> lst2 = read_spent_fromt_txt(month);
            while (true)
            {
                Console.Clear();
                show_menu_spent();
                int select = select_in_range("Nhập lựa chọn thứ(1,4): ", 1, 4);
                if (select == 1) { Console.Clear(); print_spents(lst2); Console.WriteLine("Press Enter to continue."); Console.ReadLine(); }
                else if (select == 2)
                {
                    Console.Clear();
                    lst = read_spends();
                    lst2.AddRange(lst);
                    write_spend_txt(lst2);
                    Console.WriteLine("Press Enter to continue.");
                    Console.ReadLine();
                }
                else if (select == 3)
                {
                    Console.Clear();
                    remove_spent(lst2);
                    Console.WriteLine("Press Enter to continue.");
                    Console.ReadLine();
                }
                else if (select == 4)
                {
                    Console.Clear();
                    edit_spent(lst2);
                    Console.WriteLine("Press Enter to continue.");
                    Console.ReadLine();
                }
                else break;
                //ghi vào số phòng vào file customer

            }
        }
    }
}


