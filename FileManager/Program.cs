using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using FileManager.MyClasses;

namespace FileManager
{
  
    public class Program
    {
        
        public static void Main(string[] args)

        {
            Var var = new Var();
            Var.Command com;
        
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("Добро пожаловать в файловый менеджер!(Для ознакомления с командами введите help)");

            #region Функционал
        goBack:
            Console.ForegroundColor= ConsoleColor.DarkYellow;
            Console.Write("Введите команду:");
            switch (Var.command)
            {
                case "":
                    goto goBack;

                case "help":
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("Доступные команды:\nBrowse-переход в папку\n" +
                        "InfoFoulder-получение информации о каталоге\n" +
                        "InfoFiles-получение информации о файле\n" +
                        "DiscInfo-получить системную информацию о дисках\nCreate-создать новый файл\nRead-чтение файла\n" +
                        "Record-запись в файл");
                    goto goBack;

                case "DiscInfo":
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine("Системная информация о дисках:");
                    DiscInformation disc = new DiscInformation();
                    DiscInformation.Disc disk;
                    DiscInformation.DiscInfo=Console.ReadLine();
                    DiscInformation.TotalDiscSize = Convert.ToInt64(Console.ReadLine());
                    DiscInformation.FreeSpace=Convert.ToInt64(Console.ReadLine());
                    DiscInformation.DiscSize = Convert.ToInt64(Console.ReadLine());
                    DriveInfo[] driveArray = DriveInfo.GetDrives();

                    foreach (DriveInfo drive in driveArray)
                    {
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        long DiscSize = (long)(drive.TotalFreeSpace / Math.Pow(1024, 2));
                        long TotalDiscSize = (long)(drive.TotalSize / Math.Pow(1024, 2));
                        long FreeSpace = (long)(drive.AvailableFreeSpace / Math.Pow(1024, 2));
                        Console.WriteLine($"Название:{drive.Name}");
                        Console.WriteLine($"Метка:{drive.VolumeLabel}");
                        Console.WriteLine($"Файловая система:{drive.DriveFormat}");
                        Console.WriteLine($"Тип:{drive.DriveType}");
                        Console.WriteLine($"Количество свободного места:{FreeSpace} Мб");
                        Console.WriteLine($"Свободное место на диске:{DiscSize} Мб");
                        Console.WriteLine($"Объём диска:{TotalDiscSize} Мб");
                        Console.WriteLine("============================");
                    }
                    goto goBack;

                case "Browse":
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    Console.Write("Укажите путь до каталога > ");
                    Var.location = Console.ReadLine();
                    Console.Write($"Текущий каталог: {Var.location}");
                    Console.WriteLine("\nСодержимое текущего каталога:");
                    if (Directory.Exists(Var.location))
                    {
                        Console.WriteLine("Подкаталоги:");
                        string[] dirs = Directory.GetDirectories(Var.location);
                        foreach (string dir in dirs)
                        {
                            Console.WriteLine(dir);
                        }
                        Console.WriteLine("============================");

                        Console.WriteLine("Файлы в каталоге:");
                        string[] files = Directory.GetFiles(Var.location);
                        foreach (string file in files)
                        {
                            Console.WriteLine(file);
                        }
                    }
                    goto goBack;

                case "InfoFoulder":
                    Console.ForegroundColor = ConsoleColor.DarkBlue;
                    Console.Write("Укажите путь до каталога >");
                    Var.location = Console.ReadLine();
                    DirectoryInfo dirInfo = new DirectoryInfo(Var.location);
                    Console.WriteLine($"\nНазвание каталога:{dirInfo.Name}");
                    Console.WriteLine($"Полное название каталога:{dirInfo.FullName}");
                    Console.WriteLine($"Время создания каталога:{dirInfo.CreationTime}");
                    Console.WriteLine($"Корневой каталог:{dirInfo.Root}");
                    goto goBack;

                case "InfoFiles":
                    Console.Write("Укажите путь до файла > ");
                    Var.location = Console.ReadLine();
                    FileInfo fileInfo = new FileInfo(Var.location);
                    if (fileInfo.Exists)
                    {
                        Console.WriteLine($"\nИмя файла:{fileInfo.Name}");
                        Console.WriteLine($"Время создания:{fileInfo.CreationTime}");
                        Console.WriteLine($"Размер файла:{fileInfo.Length}");
                    }
                    goto goBack;

                case "Create":
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("Введите путь создания файла >");
                    Var.location = Console.ReadLine();
                    Console.Write("Дайте имя файлу:");
                    Var.nameFile = Convert.ToString(Console.ReadLine());
                    Console.Write("Тип нового файла:");
                    Var.typeFile = Console.ReadLine();
                    FileInfo fileinf = new FileInfo($"{Var.location}{Var.nameFile}.{Var.typeFile}");
                    fileinf.Create();
                    Console.WriteLine("======================" + "\nФайл успешно создан");
                    goto goBack;

                case "Read":
                    Console.ForegroundColor = ConsoleColor.DarkMagenta;
                    Console.Write("Введите путь файла > ");
                    using (FileStream readfStream = File.OpenRead(Var.location))
                    {
                        byte[] ArrayData = new byte[readfStream.Length];
                        readfStream.Read(ArrayData, 0, ArrayData.Length);
                        Var.Encryption=Console.ReadLine();
                        Var.Encryption =Encoding.Default.GetString(ArrayData);
                        Console.WriteLine(Var.Encryption);
                    }
                    goto goBack;

                case "Record":
                    Console.Write("Введите путь файла > ");
                    Var.location = Console.ReadLine();
                    Console.Write("Введите строчку для записи в файл: ");
                    Var.text = Console.ReadLine();
                    using (FileStream recordfStream = new FileStream(Var.location, FileMode.OpenOrCreate))
                    {
                        byte[] ArrByte =Encoding.Default.GetBytes(Var.text + "\n" + Var.Encryption + DateTime.Now.ToString());
                        recordfStream.Write(ArrByte, 0, ArrByte.Length);
                        Console.WriteLine("======================" + "\nТекст успешно записан!");
                    }
                    goto Exit;
            }

            Exit:
            Console.ResetColor();
            Console.WriteLine("Вы хотите завершить программу?");
            Var.answer = Console.ReadLine();

            switch (Var.answer)
            {
                case "Да":
                    Console.Write("Спасибо, что воспользовались файловым менеджером!");
                    break;
                case "Нет":
                    goto goBack;

                default:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Ошибка!");
                    goto Exit;
            }
            Console.ReadKey();
            #endregion
        }
    }
}