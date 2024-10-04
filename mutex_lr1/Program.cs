using System;
using System.Threading;

class Program
{
    // Мьютекс для синхронизации
    static Mutex mutex = new Mutex();

    static void Main(string[] args)
    {
        Console.WriteLine("Основной поток начат.");

        // Создаем и запускаем несколько потоков
        for (int i = 0; i < 3; i++)
        {
            var thread = new Thread(DoWork);
            thread.Name = $"Thread{i + 1}";
            thread.Start();
        }

        // Ожидание завершения работы основного потока
        Thread.Sleep(500);
        Console.WriteLine("Основной поток завершен.");
    }

    static void DoWork()
    {
        Console.WriteLine($"{Thread.CurrentThread.Name} пытается захватить мьютекс.");

        // Попытка захватить мьютекс
        if (mutex.WaitOne(TimeSpan.FromSeconds(5))) // Ждет до 5 секунд
        {
            try
            {
                Console.WriteLine($"{Thread.CurrentThread.Name} захватил мьютекс.");
                Thread.Sleep(2000); // Имитируем выполнение работы
            }
            finally
            {
                Console.WriteLine($"{Thread.CurrentThread.Name} освобождает мьютекс.");
                mutex.ReleaseMutex(); // Освобождение мьютекса
            }
        }
        else
        {
            Console.WriteLine($"{Thread.CurrentThread.Name} не смог захватить мьютекс.");
        }
    }
}
