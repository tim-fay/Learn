using System.Collections.Generic;

namespace HackerRank.Search
{
    public class MissingNumbers
    {
        private static int[] FindMissingNumbers(int[] arr, int[] brr)
        {
            var dArr = new Dictionary<int, int>();

            foreach (var number in arr)
            {
                if (dArr.ContainsKey(number))
                {
                    dArr[number]++;
                }
                else
                {
                    dArr.Add(number, 1);
                }
            }

            var dBrr = new Dictionary<int, int>();
            foreach (var number in brr)
            {
                if (dBrr.ContainsKey(number))
                {
                    dBrr[number]++;
                }
                else
                {
                    dBrr.Add(number, 1);
                }
            }

            var result = new List<int>();
            
            foreach (var element in dBrr)
            {
                if (dArr.ContainsKey(element.Key))
                {
                    if (element.Value > dArr[element.Key])
                    {
                        result.Add(element.Key);
                    }
                }
                else
                {
                    result.Add(element.Key);
                }
            }
            
            result.Sort();
            return result.ToArray();
        }
    }
}