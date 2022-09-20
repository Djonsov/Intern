using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intern
{
    static class Program
    {
        static readonly IFormatProvider _ifp = CultureInfo.InvariantCulture;

        static void Main(string[] args)
        {          
            Console.ReadKey();
        }

        class Number
        {
            readonly int _number;
            
            public Number(int number)
            {
                _number = number;
            }
            public int getNumber()
            {
                return _number;
            }
            public static Number operator + (Number num1, Number num2)
            {
                return new Number(num1._number + num2._number);
            }
            public static Number operator + (Number num1, int num2)
            {
                return new Number(num1._number + num2);
            }

            public override string ToString()
            {
                return _number.ToString(_ifp);
            }
        }

        class ElephantWriter: StringWriter
        {
            TextWriter tw = Console.Out;

            public override void WriteLine(string value) => Console.SetOut(tw);
        }

        static void TransformToElephant()
        {
            Console.WriteLine("Слон");
            Console.SetOut(new ElephantWriter());
        }

        static void FailProcess()
        {
            Process procc = Process.GetCurrentProcess();
            procc.Kill();
            throw new Exception();
        }

        static IEnumerable<int> Sort(IEnumerable<int> inputStream, int sortFactor, int maxValue)
        {
            var values = new int[maxValue + 1];
            int min = 0;

            foreach (int x in inputStream)
            {
                ++values[x];

                int max = x - sortFactor;
                while (min < max)
                {
                    while (values[min]-- > 0)
                        yield return min;
                    ++min;
                }
            }

            while (min < values.Length)
            {
                while (values[min]-- > 0)
                    yield return min;
                ++min;
            }
        }


        static public IEnumerable<(T item, int? tail)> EnumerateFromTail<T>(this IEnumerable<T> enumerable, int? tailLength)
        {
            if (tailLength == null || tailLength <= 0)
                return new List<(T item, int? tail)>();

            IList<(T item, int? tail)> items = new List<(T item, int? tail)>();
            var count = enumerable.Count();
            if (tailLength >= count)
            {
                foreach (var item in enumerable)
                {
                    items.Add((item, --count));
                }
            }
            else
                {
                var i = 0;
                var re = count - tailLength;
                foreach (var item in enumerable)
                {
                    items.Add((item, i >= re ? --tailLength : null));
                    i++;
                }
            }
            return items;
        }          
    }
}
