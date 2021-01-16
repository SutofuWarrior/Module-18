using System;
using System.Diagnostics;

namespace SF.Module18
{
    class Program
    {
        static void Main()
        {
            Console.Write("Int1 = ");
            string value1 = Console.ReadLine();

            Console.Write("Int2 = ");
            string value2 = Console.ReadLine();

            ILogger logger;
            ISummator summator;
            int sum;

            try
            {
                logger = new ConsoleLogger();
                summator = new Summator1(logger);

                sum = summator.Sum(value1, value2);
                Console.WriteLine(sum);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

            Console.WriteLine();

            try
            {
                logger = new DebugLogger();
                summator = new Summator1(logger);

                sum = summator.Sum(value1, value2);
                Console.WriteLine(sum);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

            Console.ReadKey();
        }
    }

    public interface ILogger
    {
        void LogInfo(string message);

        void LogError(string message);
    }

    public class ConsoleLogger : ILogger
    {
        void ILogger.LogError(string message)
        {
            var prevColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Red;

            Console.WriteLine(message);

            Console.ForegroundColor = prevColor;
        }

        void ILogger.LogInfo(string message)
        {
            var prevColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Blue;

            Console.WriteLine(message);

            Console.ForegroundColor = prevColor;
        }
    }

    public class DebugLogger : ILogger
    {
        void ILogger.LogError(string message)
        {
            Debug.WriteLine(message);
        }

        void ILogger.LogInfo(string message)
        {
            Debug.WriteLine(message);
        }
    }

    public interface ISummator
    {
        int Sum(object value1, object value2);
    }

    public class Summator1 : ISummator
    {
        private readonly ILogger logger;

        public Summator1(ILogger _logger)
        {
            logger = _logger;
        }

        int ISummator.Sum(object value1, object value2)
        {
            try
            {
                int v1 = Convert.ToInt32(value1);
                int v2 = Convert.ToInt32(value2);

                logger.LogInfo($"Сложение чисел {v1} и {v2}");
                return v1 + v2;
            }
            catch (FormatException e)
            {
                logger.LogError("Сумматор складывает только числа.");
                throw e;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
