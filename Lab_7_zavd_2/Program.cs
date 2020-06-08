using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Collections;

namespace Lab_7_zavd_2
{
    public class Kasa : IComparable
    {
        private string _name;
        private DateTime _timeOfDeparture;
        private DateTime _dateOfDeparture;
        private DateTime _timeOfArrival;
        private DateTime _dateOfArrival;
        private int _priceOfTicket;
        public Kasa()
        {
            _name = "Not mentioned";
            _timeOfDeparture = new DateTime(1111, 11, 11, 00, 00, 00);
            _dateOfDeparture = new DateTime(1111, 11, 11);
            _timeOfArrival = new DateTime(1111, 11, 11, 00, 00, 00);
            _dateOfArrival = new DateTime(1111, 11, 11);
            _priceOfTicket = 0;
        }
        public Kasa(string s)
        {
            s = System.Text.RegularExpressions.Regex.Replace(s, @"\s+", " ");
            string[] str = s.Split(' ');
            _name = str[0];
            string[] time = str[1].Split(':');
            int hour = int.Parse(time[0]);
            int minute = int.Parse(time[1]);
            int second = int.Parse(time[2]);
            _timeOfDeparture = new DateTime(1111,11,11,hour,minute,second);
            string[] data = str[2].Split('.');
            int day = int.Parse(data[0]);
            int month = int.Parse(data[1]);
            int year = int.Parse(data[2]);
            _dateOfDeparture = new DateTime(year, month, day);
            time = str[3].Split(':');
            hour = int.Parse(time[0]);
            minute = int.Parse(time[1]);
            second = int.Parse(time[2]);
            _timeOfArrival = new DateTime(1111, 11, 11, hour, minute, second);
            data = str[4].Split('.');
            day = int.Parse(data[0]);
            month = int.Parse(data[1]);
            year = int.Parse(data[2]);
            _dateOfArrival = new DateTime(year, month, day);
            _priceOfTicket = int.Parse(str[5]);
        }
        public int CompareTo(object time)
        {
            Kasa t = (Kasa)time;
            if (this.TimeOfDeparture < t.TimeOfDeparture) return 1;
            if (this.TimeOfDeparture > t.TimeOfDeparture) return -1;
            return 0;
        }
        public class SortByDataAndPrice : IComparer
        {
            int IComparer.Compare(object ob1, object ob2)
            {
                Kasa p1 = (Kasa)ob1;
                Kasa p2 = (Kasa)ob2;
                if (p1.DateOfArrival < p2.DateOfArrival) return 1;
                if (p1.DateOfArrival > p2.DateOfArrival) return -1;
                if (p1.DateOfArrival == p2.DateOfArrival)
                {
                    if (p1.PriceOfTicket < p2.PriceOfTicket) return 1;
                    if (p1.PriceOfTicket > p2.PriceOfTicket) return -1;
                    return 0;
                }
                return 0;
            }
        }
        public string name
        {
            get => _name; set => _name = value;
        }
        public DateTime TimeOfDeparture
        {
            get => _timeOfDeparture; set => _timeOfDeparture = value;
        }
        public DateTime DateOfDeparture
        {
            get => _dateOfDeparture; set => _dateOfDeparture = value;
        }
        public DateTime TimeOfArrival
        {
            get => _timeOfArrival; set => _timeOfArrival = value;
        }
        public DateTime DateOfArrival
        {
            get => _dateOfArrival; set => _dateOfArrival = value;
        }
        public int PriceOfTicket
        {
            get => _priceOfTicket; set => _priceOfTicket = value;
        }
    }
    class Program
    {
        public static void AddInFile(string s)
        {
            s = System.Text.RegularExpressions.Regex.Replace(s, @"\s+", " ");
            string[] str = s.Split(' ');
            StreamWriter file = new StreamWriter(@"D:\ООП\Lab_7\Lab_7_zavd_2\Lab_7_zavd_2\TextFile1.txt", true);
            file.Write($"{str[0],-15} {str[1],15} {str[2],20} {str[3],20} {str[4],20} {str[5],20}");
            file.Write(Environment.NewLine);
            file.Close();
        }
        public static void RemoveRecords(int n)
        {
            List<string> quotelist = File.ReadAllLines(@"D:\ООП\Lab_7\Lab_7_zavd_2\Lab_7_zavd_2\TextFile1.txt").ToList();
            quotelist.RemoveAt(n - 1);
            File.WriteAllLines(@"D:\ООП\Lab_7\Lab_7_zavd_2\Lab_7_zavd_2\TextFile1.txt", quotelist.ToArray());
        }
        public static Kasa[] UpdateBasa()
        {
            int k = 0, i = 0;
            StreamReader file = new StreamReader(@"D:\ООП\Lab_7\Lab_7_zavd_2\Lab_7_zavd_2\TextFile1.txt");
            string line;
            while ((line = file.ReadLine()) != null)
            {
                k++;
            }
            file.BaseStream.Position = 0;
            Kasa[] basa = new Kasa[k];
            try
            {

                while ((line = file.ReadLine()) != null)
                {
                    basa[i] = new Kasa(line);
                    k++;
                    i++;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Помилка: " + e.Message);
            }
            file.Close();
            return basa;
        }
        public static void Search(Kasa[] d)
        {
            Console.WriteLine("Введiть назву пункту про який хочете знайти iнформацiю: ");
            string name = Console.ReadLine();
            Console.WriteLine("Ось що нам вдалося знайти: ");
            int n = 0;
            for (int i = 0; i < d.Length; i++)
            {
                if (d[i].name == name)
                {
                    Console.WriteLine($"{d[i].name,-15} {d[i].TimeOfDeparture.ToLongTimeString(),15} {d[i].DateOfDeparture.ToShortDateString(),20} {d[i].TimeOfArrival.ToLongTimeString(),20} {d[i].DateOfArrival.ToShortDateString(),20}{d[i].PriceOfTicket,20}");
                    n++;
                }

            }
            if (n == 0) Console.WriteLine("Упс, за вашим запитом не вдалося нiчого знайти, перевiрте правильнiсть написання назви, або спробуйте iншу назву.");
        }
        public static void Edit(Kasa[] d)
        {
        askLine:
            Console.Write("Введiть номер рядка, в якому хочете щось змiнити: ");
            int k = (int.Parse(Console.ReadLine())) - 1;
            if (k > d.Length)
            {
                Console.WriteLine("Такого рядка не iснує. Спробуйте iнший");
                goto askLine;
            }
            Console.WriteLine("Введiть номер поля, яке хочете змiнити. Наприклад: Назва(1), Час вiдправлення(2), Дата вiдправлення(3), Час прибуття(4), Дата прибуття(5), Цiна квитка(6)");
            int pole = int.Parse(Console.ReadLine());
        retry:
            Console.Write("Введiть нове значення: ");
            string val = Console.ReadLine();
            try
            {
                switch (pole)
                {
                    case 1: d[k].name = val; break;
                    case 2:
                        string[] time = val.Split(':');
                        int hour = int.Parse(time[0]);
                        int minute = int.Parse(time[1]);
                        int second = int.Parse(time[2]);
                        d[k].TimeOfDeparture = new DateTime(1111, 11, 11, hour, minute, second);
                        break;
                    case 3:
                        string[] data = val.Split('.', '/');
                        int day = int.Parse(data[0]);
                        int month = int.Parse(data[1]);
                        int year = int.Parse(data[2]);
                        d[k].DateOfDeparture = new DateTime(year, month, day);
                        break;
                    case 4:
                        time = val.Split(':');
                        hour = int.Parse(time[0]);
                        minute = int.Parse(time[1]);
                        second = int.Parse(time[2]);
                        d[k].TimeOfArrival = new DateTime(1111, 11, 11, hour, minute, second);
                        break;
                    case 5:
                        data = val.Split('.', '/');
                        day = int.Parse(data[0]);
                        month = int.Parse(data[1]);
                        year = int.Parse(data[2]);
                        d[k].DateOfArrival = new DateTime(year, month, day);
                        break;
                    case 6: d[k].PriceOfTicket = int.Parse(val); break;

                    default: Console.WriteLine("Поля з таким номером не iснує!"); break;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Помилка: " + e);
                goto retry;
            }
        }
        public static void UpdateFile(Kasa[] d)
        {
            StreamWriter file = new StreamWriter(@"D:\ООП\Lab_7\Lab_7_zavd_2\Lab_7_zavd_2\TextFile1.txt");
            for (int i = 0; i < d.Length; i++)
            {
                file.Write($"{d[i].name,-15} {d[i].TimeOfDeparture.ToLongTimeString(),15} {d[i].DateOfDeparture.ToShortDateString(),20} {d[i].TimeOfArrival.ToLongTimeString(),20} {d[i].DateOfArrival.ToShortDateString(),20} {d[i].PriceOfTicket,20}");
                file.Write(Environment.NewLine);
            }
            file.Close();
        }

        public static void ShowFile()
        {
            StreamReader file = new StreamReader(@"D:\ООП\Lab_7\Lab_7_zavd_2\Lab_7_zavd_2\TextFile1.txt");
            string show = file.ReadToEnd();
            if (show.Length == 0)
            {
                Console.WriteLine("Упс, файл пустий.");
            }
            else Console.WriteLine(show);
            file.Close();
        }
        static void Main(string[] args)
        {
            string str;
            char check;
            int n;
            Console.WriteLine("Меню програми:\nДодавання записiв - a\nРедагування записiв - e\nЗнищення записiв - f\nВиведення iнформацiї з файлу на екран - s\nСортувати базу за часом вiдправлення - T\nСортувати базу за датою прибуття та цiною - D\nВихiд з програми - q");
            do
            {
            userCheck:
                Console.Write("\nВведiть команду: ");
                try
                {
                    check = char.Parse(Console.ReadLine());
                }
                catch (System.FormatException)
                {
                    Console.WriteLine("Неправильна команда. Спробуйте ще раз.");
                    goto userCheck;
                }
                Kasa[] kasa = UpdateBasa();
                if (check == 'a')
                {
                AddZapis:
                    Console.WriteLine("Введiть новий запис до бази даних за принципом:\nНазва пункту          Час вiдправлення          Дата вiдправлення          Час прибуття          Дата прибуття          Цiна квитка");
                    try
                    {
                        AddInFile(Console.ReadLine());
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Помилка: " + e.Message);
                        goto AddZapis;
                    }
                }
                else if (check == 'e')
                {
                    Edit(kasa);
                    UpdateFile(kasa);
                }
                else if (check == 'f')
                {
                    Console.Write("Введiть номер рядка який хочете видалити: ");
                    n = int.Parse(Console.ReadLine());
                    RemoveRecords(n);
                    kasa = UpdateBasa();
                }
                else if (check == 's')
                {
                    Console.WriteLine($"{"Назва пункту",-15} {"Час вiдправлення",15} {"Дата вiдправлення",20} {"Час прибуття",20} {"Дата прибуття",20}{"Цiна квитка",20}");
                    ShowFile();
                }
                else if (check == 'n')
                {
                    Search(kasa);
                }
                else if (check == 'T')
                {
                    
                    Array.Sort(kasa);
                    UpdateFile(kasa);
                }
                else if (check == 'D')
                {
                    kasa = UpdateBasa();
                    Array.Sort(kasa, new Kasa.SortByDataAndPrice());
                    UpdateFile(kasa);
                }
                else
                {
                    if (check == 'q') Console.WriteLine("Програма завершена.");
                    else Console.WriteLine("На жаль, такої команди немає, спробуйте iншу.");
                }
            } while (check != 'q');
        }
    }
}
