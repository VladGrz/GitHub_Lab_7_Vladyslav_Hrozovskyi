using Microsoft.VisualBasic;
using System;
using System.Collections;

namespace Lab_7_zavd_1
{
    public class Product : IComparable
    {
        private double _weight;
        private double _price;
        private int _quality;
        private string _placeOfProduction;
        private string _nameOfCompany;
        public Product()
        {
            _weight = 0;
            _price = 0;
            _quality = 0;
            _placeOfProduction = "No place was given.";
            _nameOfCompany = "No name of company was given.";
        }
        public Product(string s)
        {
            s = System.Text.RegularExpressions.Regex.Replace(s, @"\s+", " ");
            string[] str = s.Split(' ');
            _nameOfCompany = str[0];
            _placeOfProduction = str[1];
            if (str[2].Contains(',')) _weight = double.Parse(str[2]);
            else if (str[2].Contains('.')) throw new System.FormatException();
            if (str[3].Contains(',')) _price = double.Parse(str[3]);
            else if (str[3].Contains('.')) throw new System.FormatException();
            _quality = int.Parse(str[4]);
        }
        public int CompareTo(object prod)
        {
            Product p = (Product)prod;
            if (this.Weight < p.Weight) return 1;
            if (this.Weight > p.Weight) return -1;
            return 0;
        }
        public class SortByPriceAndQuality: IComparer
        {
            int IComparer.Compare(object ob1, object ob2)
            {
                Product p1 = (Product)ob1;
                Product p2 = (Product)ob2;
                if (p1.Price < p2.Price) return 1;
                if (p1.Price > p2.Price) return -1;
                if (p1.Price == p2.Price)
                {
                    if (p1.Quality < p2.Quality) return 1;
                    if (p1.Quality > p2.Quality) return -1;
                    return 0;
                }
                return 0;
            }
        }
        public class SortByPrice : IComparer
        {
            int IComparer.Compare(object ob1, object ob2)
            {
                Product p1 = (Product)ob1;
                Product p2 = (Product)ob2;
                if (p1.Price < p2.Price) return 1;
                if (p1.Price > p2.Price) return -1;
                return 0;
            }
        }
        public double Weight
        {
            get => _weight; set => _weight = value;
        }
        public double Price
        {
            get => _price; set => _price = value;
        }
        public int Quality
        {
            get => _quality; set => _quality = value;
        }
        public string PlaceOfProduction
        {
            get => _placeOfProduction; set => _placeOfProduction = value;
        }
        public string NameOfCompany
        {
            get => _nameOfCompany; set => _nameOfCompany = value;
        }
    }
    public class Products: IEnumerable
    {
        private Product[] _mas;
        int n;
        public Products()
        {
            _mas = new Product[10];
            n = 0;
        }
        public IEnumerator GetEnumerator()
        {
            for (int i = 0; i < n; ++i) yield return _mas[i];
        }
        public void SortByPrice()
        {
            Array.Sort(_mas, new Product.SortByPrice());
        }
        public void Add(Product m)
        {
            if (n >= 10) return;
            _mas[n] = m;
            ++n;
        }
        public void Print()
        {
            for(int i = 0; i<_mas.Length; i++)
            {
                Console.WriteLine(_mas[i]);
            }
        }

    }
    class Program
    {
        static void Main(string[] args)
        {
            Products prod = new Products();
            Product[] products = new Product[5];
            Console.WriteLine("Введiть данi про вирiб:");
            Console.WriteLine($"{"Назва компанiї",-20}{"Мiсце розташування складу",20}{"Вага(кг)",15}{"Цiна($)",20}{"Оцiнка якостi(0-12)",25}");
            for (int i = 0; i < products.Length; i++)
            {
            enterString:
                try
                {
                    products[i] = new Product(Console.ReadLine());
                }
                catch (System.FormatException)
                {
                    Console.WriteLine("Упс, щось пiшло не так перевiрте чи вага або цiна не написанi через крапку. Варто писати через кому.");
                    goto enterString;
                }
            }
            Console.WriteLine("\nВашi данi вiдсортованi за вагою: ");
            Console.WriteLine($"{"Назва компанiї",-20}{"Мiсце розташування складу",20}{"Вага(кг)",15}{"Цiна($)",20}{"Оцiнка якостi(0-12)",25}");
            Array.Sort(products);
            for (int i = 0; i < products.Length; i++)
            {
                Console.WriteLine($"{products[i].NameOfCompany,-20}{products[i].PlaceOfProduction,20}{products[i].Weight,15}{products[i].Price,20}{products[i].Quality,25}");
            }
            Console.WriteLine("\nВашi данi вiдсортованi за цiною та за якiстю: ");
            Console.WriteLine($"{"Назва компанiї",-20}{"Мiсце розташування складу",20}{"Вага(кг)",15}{"Цiна($)",20}{"Оцiнка якостi(0-12)",25}");
            Array.Sort(products, new Product.SortByPriceAndQuality());
            for (int i = 0; i < products.Length; i++)
            {
                Console.WriteLine($"{products[i].NameOfCompany,-20}{products[i].PlaceOfProduction,20}{products[i].Weight,15}{products[i].Price,20}{products[i].Quality,25}");
            }
            for (int i = 0; i < products.Length; i++)
            {
                prod.Add(products[i]);
            }
            Console.WriteLine("\nВашi данi вiдсортованi за цiною: ");
            Console.WriteLine($"{"Назва компанiї",-20}{"Мiсце розташування складу",20}{"Вага(кг)",15}{"Цiна($)",20}{"Оцiнка якостi(0-12)",25}");
            Array.Sort(products, new Product.SortByPrice());
            for (int i = 0; i < products.Length; i++)
            {
                Console.WriteLine($"{products[i].NameOfCompany,-20}{products[i].PlaceOfProduction,20}{products[i].Weight,15}{products[i].Price,20}{products[i].Quality,25}");
            }
        }
        
    }
}
