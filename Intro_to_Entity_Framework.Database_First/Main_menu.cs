using System;
using System.Security.Cryptography;
using System.Text;


namespace Intro_to_Entity_Framework.Database_First
{
    public class Main_menu
    {
        private Model1Container ctx;
        private int currentId = 0;

        public Main_menu()
        {
            ctx = new Model1Container();
        }

        public void Log_Menu()
        {
            int chois;
            do
            {
                Console.WriteLine("1.Login");
                Console.WriteLine("2.Registration");
                Console.WriteLine("3.Exit");
                Console.WriteLine();
                chois = int.Parse(Console.ReadLine());
                string name;
                string password;
                UsersInfo user = new UsersInfo();

                switch (chois)
                {
                    case 1:

                        Console.Write("Enter name:");
                        name = Console.ReadLine();
                        Console.Write("Enter password:");
                        password = Console.ReadLine();
                        user.Name = name;
                        user.Password = ComputeSha256Hash(password);
                        int count = 0;
                        foreach (var u in ctx.UsersInfo)
                        {
                            if (u.Name == user.Name && u.Password == user.Password)
                            {
                                currentId = user.Id;
                                count = 1;
                            }
                        }
                        if (count == 0)
                        {
                            Console.WriteLine("not found");
                        }
                        else
                        {
                            MainMenu();
                        }
                        Console.ReadKey();
                        Console.Clear();
                        break;
                    case 2:

                        Console.Write("Enter name: ");
                        name = Console.ReadLine();
                        Console.Write("Enter password: ");
                        password = Console.ReadLine();
                        user.Name = name;
                        user.Password = ComputeSha256Hash(password);
                        try
                        {
                            ctx.UsersInfo.Add(user);
                            ctx.SaveChanges();
                            currentId = user.Id;
                            MainMenu();
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine($"{e.Message}");
                        }
                        Console.ReadKey();
                        Console.Clear();
                        break;
                    case 3:                     
                        break;                  
                }
            } while (chois != 3);
        }



        public void MainMenu()
        {
            int chois;
            do
            {
                Console.WriteLine("1 Order");
                Console.WriteLine("2 end the rental");
                Console.WriteLine("3 View rentals ");
                Console.WriteLine("4 Exit");
                Console.WriteLine();
                chois = int.Parse(Console.ReadLine());
                Console.WriteLine();

                switch (chois)
                {
                    case 1:
                        int area;
                        DateTime firstDate, secondDate;
                        int day, month, year;
                        Console.WriteLine("Enter area");
                        area = int.Parse(Console.ReadLine());

                        Console.WriteLine("Enter first date");
                        Console.Write("Enter day:");
                        day = int.Parse(Console.ReadLine());
                        Console.Write("Enter month:");
                        month = int.Parse(Console.ReadLine());
                        Console.Write("Enter year:");
                        year = int.Parse(Console.ReadLine());
                        firstDate = new DateTime(year, month, day);

                        Console.WriteLine("Enter second date");
                        Console.Write("Enter day:");
                        day = int.Parse(Console.ReadLine());
                        Console.Write("Enter month:");
                        month = int.Parse(Console.ReadLine());
                        Console.Write("Enter year:");
                        year = int.Parse(Console.ReadLine());
                        secondDate = new DateTime(year, month, day);
                        int count = 0;
                        foreach (var room in ctx.RoomsInfo)
                        {
                            if (room.Area >= area && firstDate >= DateTime.Now &&  secondDate > firstDate && room.UsersInfoId == null)
                            {
                                Console.WriteLine($"[Id: {room.Id}] City: {room.City}, Address: {room.AccommodationAddress}");
                                count = 1;
                            }
                        }
                        if (count == 0)
                            Console.WriteLine("Nothing found");
                        else
                        {
                            Console.WriteLine("Enter Id");
                            int id_first = int.Parse(Console.ReadLine());
                            foreach (var room in ctx.RoomsInfo)
                            {
                                if (room.Id == id_first)
                                {
                                    room.UsersInfoId = currentId;
                                    room.StartRent = firstDate;
                                    room.EndRent = secondDate;
                                    ctx.SaveChanges();
                                }
                            }
                        }
                        Console.ReadKey();
                        Console.Clear();
                        break;
                    case 2:
                        foreach (var room in ctx.RoomsInfo)
                        {
                            if (room.UsersInfoId == currentId && room.EndRent > DateTime.Now)
                            {
                                Console.WriteLine($"Id: {room.Id} City: {room.City}, Address: {room.AccommodationAddress}");
                            }
                        }
                        Console.WriteLine("Enter Id");
                        int id = int.Parse(Console.ReadLine());

                        foreach (var room in ctx.RoomsInfo)
                        {
                            if (room.Id == id)
                            {
                                room.UsersInfoId = null;
                                room.StartRent = null;
                                room.EndRent = null;
                                ctx.SaveChanges();
                            }
                        }
                        Console.ReadKey();
                        Console.Clear();
                        break;
                    case 3:
                        foreach (var room in ctx.RoomsInfo)
                        {
                            if (room.UsersInfoId == currentId)
                            {
                                Console.WriteLine($"Id: {room.Id} City: {room.City}, Address: {room.AccommodationAddress}");
                            }
                        }                    
                        break;
                    case 4:                    
                        break;         
                }
            } while (chois != 4);
        }

        static string ComputeSha256Hash(string rawData)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));

                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }
    }
}
