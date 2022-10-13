using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;



namespace FinalTask
{
    [Serializable]

}
internal class Program
{
    static void Main(string[] args)
    {
        ChooseTask();
    }

    static string GetPath()
    {
        Console.WriteLine("Введите правильный путь к каталогу, пожалуйста.");
        string path = Console.ReadLine();
        try
        {
            DirectoryInfo dirInfo = new DirectoryInfo(path);
            if (dirInfo.Exists)
            {
                path = dirInfo.FullName;
                return path;
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Путь неверен.");

            }
        }


        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }


        return GetPath();
    }
    static string GetFile()
    {
        Console.WriteLine("Введите правильный путь к файлу, пожалуйста.");
        string path = Console.ReadLine();
        try
        {
            FileInfo fileInfo = new FileInfo(path);
            if (fileInfo.Exists)
            {
                path = fileInfo.FullName;
                return path;
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Путь неверен.");

            }
        }


        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }


        return GetFile();
    }
    static void DeleteUnused(DirectoryInfo path)
    {
        DirectoryInfo[] dirs = path.GetDirectories();
        Console.WriteLine($"\n--->>> Directories <<<---\n Всего каталогов в корневом каталоге: {dirs.Length}");
        foreach (DirectoryInfo dir in dirs)
        {


            if (DateTime.Now - dir.LastAccessTime >= TimeSpan.FromMinutes(30))
            {

                try
                {
                    dir.Delete(true);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }
        FileInfo[] files = path.GetFiles();
        Console.WriteLine($"\n--->>> Files <<<---\n Общее количество файлов в корне: {files.Length}");
        foreach (FileInfo file in files)
        {
            if (DateTime.Now - file.LastAccessTime >= TimeSpan.FromMinutes(30))
            {

                try
                {
                    file.Delete();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }

            }
        }
    }
    static long CalcSpace(DirectoryInfo path)
    {
        long space = 0;
        FileInfo[] files = path.GetFiles();

        foreach (FileInfo file in files)
        {
            try
            {
                space += file.Length;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        DirectoryInfo[] dirs = path.GetDirectories();

        foreach (DirectoryInfo dir in dirs)
        {
            try
            {
                space += CalcSpace(dir);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        return space;
    }
    static void Task1()
    {
        Console.Clear();
        Console.WriteLine("Task #1 был выбран.");
        DirectoryInfo path = new DirectoryInfo(GetPath());
        DeleteUnused(path);
        Console.ReadKey();
    }
    static void Task2()
    {
        Console.Clear();
        Console.WriteLine("Task #2 был выбран.");
        DirectoryInfo path = new DirectoryInfo(GetPath());


        Console.WriteLine($"Размер каталога = {CalcSpace(path)} byte(s)");
        Console.ReadKey();

    }
    static void Task3()
    {
        Console.Clear();
        Console.WriteLine("Task #3 был выбран.");
        DirectoryInfo path = new DirectoryInfo(GetPath());
        long initialSpace = CalcSpace(path);
        DeleteUnused(path);
        long currentSpace = CalcSpace(path);
        long deletedSpace = initialSpace - currentSpace;
        Console.WriteLine($"\n Первоначальный размер был: {initialSpace} byte(s) \n Byte(s) deleted: {deletedSpace} \n Текущее пространство: {currentSpace} byte(s) ");
        Console.ReadKey();


    }

    static void ChooseTask()
    {
        Console.Clear();
        Console.WriteLine("Выбрать task \n 1 - Task #1 \n 2 - Task #2 \n 3 - Task #3");



        switch (Console.ReadLine())
        {
            case "1":
                Task1();
                break;

            case "2":
                Task2();
                break;

            case "3":
                Task3();
                break;

            default:

                ChooseTask();
                break;
        }


    }


}
}
