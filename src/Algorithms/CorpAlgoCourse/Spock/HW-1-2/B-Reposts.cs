using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace CorpAlgoCourse.Spock
{
    /// <summary>
    /// Codeforces: Order Book
    /// https://codeforces.com/gym/269631/problem/B
    /// </summary>
    [Trait("Category", "Corporate Algorithmic Course: Spock, HW 1.2")]
    public class Reposts
    {
        [Theory]
        [MemberData(nameof(InputData))]
        public void Test(Repost[] reposts, int expectedLongestChain)
        {
            var result = FindLongestRepostChain(reposts);
            Assert.Equal(expectedLongestChain, result);
        }

        public static IEnumerable<object[]> InputData => new TheoryData<Repost[], int>
        {
            {
                new[]
                {
                    new Repost("tourist reposted Polycarp"),
                    new Repost("Petr reposted Tourist"),
                    new Repost("WJMZBMR reposted Petr"),
                    new Repost("sdya reposted wjmzbmr"),
                    new Repost("vepifanov reposted sdya"),
                },
                6
            },
            {
                new[]
                {
                    new Repost("Mike reposted Polycarp"),
                    new Repost("Max reposted Polycarp"),
                    new Repost("EveryOne reposted Polycarp"),
                    new Repost("111 reposted Polycarp"),
                    new Repost("VkCup reposted Polycarp"),
                    new Repost("Codeforces reposted Polycarp"),
                },
                2
            },
            {
                new[]
                {
                    new Repost("SoMeStRaNgEgUe reposted PoLyCaRp"),
                },
                2
            },
        };

        private static int FindLongestRepostChain(Repost[] reposts)
        {
            const string policarp = "polycarp";

            var postDict = new Dictionary<string, int>(StringComparer.InvariantCultureIgnoreCase);
            postDict.Add(policarp, 1);

            foreach (var repost in reposts)
            {
                int chainLength = postDict[repost.From];
                postDict.Add(repost.Author, chainLength + 1);
            }

            return postDict.Values.Max();
        }

        public class Repost
        {
            public string Author { get; }
            public string From { get; }

            public Repost(string repostLine)
            {
                var authorFrom = repostLine.Split(new string[] { " reposted " }, StringSplitOptions.None);
                Author = authorFrom[0].ToLower();
                From = authorFrom[1].ToLower();
            }
        }

        private static void Main1()
        {
            int n = int.Parse(Console.ReadLine());

            var reposts = new Repost[n];
            
            for (int i = 0; i < n; i++)
            {
                reposts[i] = new Repost(Console.ReadLine());
            }

            var chainLength = FindLongestRepostChain(reposts);
            
            Console.WriteLine(chainLength);
        }
    }
}