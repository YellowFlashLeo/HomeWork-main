using Number.Db;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;

namespace Number.Core
{
    public class InputServices:IinputServices
    {
        static List<int> seq = new List<int>();
        static Dictionary<List<int>, List<int>> cachedFibSeq = new Dictionary<List<int>, List<int>>();
        List<int> fibSequence = new List<int>();

        private AppDbContext _context;
        public InputServices(AppDbContext context)
        {
            _context = context;
        }

        public List<int> GetFobanacci(Input input)
        {
            int firstIndex = input.FirstIndex;
            int lastIndex = input.LastIndex;

            if (cachedFibSeq.ContainsKey(new List<int> { firstIndex, lastIndex }) && input.isCached == true)
            {
                return  cachedFibSeq[new List<int> { firstIndex, lastIndex }];
            }
            else 
            {

                var stopwatch = new Stopwatch();
                stopwatch.Start();
                int firstNumber = FibNumber(firstIndex);
                //System.Threading.Thread.Sleep(2);
                stopwatch.Stop();
                var elapsed_time = stopwatch.ElapsedMilliseconds;

                if (elapsed_time > input.Time)
                    return fibSequence;


                int lastNumber = FibNumber(lastIndex);

                Fibonacci_Recursive(lastIndex + 1);
                fibSequence = seq.Skip(firstIndex).Take(lastIndex + 1 - firstIndex).ToList();


                cachedFibSeq.Add(new List<int> { input.FirstIndex, input.LastIndex }, fibSequence);

                return fibSequence;
            }            
        }


        private static int FibNumber(int n)
        {  
            if (n <= 1)
            {
                return n;
            }
            return FibNumber(n - 1) + FibNumber(n - 2);
        }
        private static void Fibonacci_Recursive(int len)
        {
            Fibonacci_Get_Range(0, 1, 1, len);
        }
        private static void Fibonacci_Get_Range(int a, int b, int counter, int len)
        {
            if (counter <= len)
            {
                seq.Add(a);
                Fibonacci_Get_Range(b, a + b, counter + 1, len);
            }
        }


        public List<string> InputValidation(int firstIndex, int secondIndex)
        {
            List<string> errors = new List<string>();
            if (firstIndex < 0 || !(firstIndex.GetType() == typeof(int)))
                errors.Add("Error was cause due to either not corrent input data type (can only be whole numbers)" +
                    "or first index value is negative (can only be positive)");
            if (secondIndex < 0 || !(secondIndex.GetType() == typeof(int)))
                errors.Add("Error was cause due o either not corrent input data type (can only be whole numbers)" +
                    "or second index value is negative (can only be positive)");
            if (secondIndex <= firstIndex)
                errors.Add("Second Index should be bigger than 1 to provide range of FibonachiNumbers");

            return errors;
        }


        //  Task manager object
    }
}
