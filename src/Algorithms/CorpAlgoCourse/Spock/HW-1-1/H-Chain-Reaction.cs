using System;
using System.Linq;
using Xunit;

namespace CorpAlgoCourse.Spock
{
    /// <summary>
    /// Codeforces: Chain Reaction
    /// http://codeforces.com/gym/268160/problem/H
    /// </summary>
    [Trait("Category", "Spock: Corporate Algorithmic Course: HW 1.1")]
    public class ChainReaction
    {
        private static int FindMinimumNumberOfBeaconLoss(Beacon[] beacons)
        {
            return 0;
        }

        public struct Beacon
        {
            public Beacon(int position, int power)
            {
                Position = position;
                Power = power;
            }

            public int Position { get; }
            public int Power { get; }
        }

        private static void Main2()
        {
            int n = int.Parse(Console.ReadLine());

            const int maxn = 1000000 + 5;

            int[] b = new int[maxn];
            int[] dp = new int[maxn];

            for (int i = 0; i < n; i++)
            {
                var a_b = Console.ReadLine().Split(new[] { ' ' }).Select(str => int.Parse(str)).ToArray();
                b[a_b[0]] = a_b[1];
            }

            if (b[0] > 0)
            {
                dp[0] = 1;
            }

            int mx = 0;
            for (int i = 1; i < maxn; i++)
            {
                if (b[i] == 0)
                {
                    dp[i] = dp[i - 1];
                }
                else
                {
                    if (b[i] >= i)
                    {
                        dp[i] = 1;
                    }
                    else
                    {
                        dp[i] = dp[i - b[i] - 1] + 1;
                    }
                }

                if (dp[i] > mx)
                {
                    mx = dp[i];
                }
            }

            Console.WriteLine(n - mx);
        }
    }
}