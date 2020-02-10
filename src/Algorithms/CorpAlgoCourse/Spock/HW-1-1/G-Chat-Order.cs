using System;
using System.Collections.Generic;
using Xunit;

namespace CorpAlgoCourse.Spock
{
    
    /// <summary>
    /// Codeforces: Chat Order
    /// http://codeforces.com/gym/268160/problem/G
    /// </summary>
    [Trait("Category", "Spock: Corporate Algorithmic Course: HW 1.1")]
    public class ChatOrder
    {
        [Theory]
        [MemberData(nameof(InputData))]
        public void Test(string[] contacts, string[] expectedRecentChatList)
        {
            var radius = GetRecentChatList(contacts);
            Assert.Equal(expectedRecentChatList, radius);
        }

        public static IEnumerable<object[]> InputData => new TheoryData<string[], string[]>
        {
            { new[] { "alex", "ivan", "roman", "ivan" }, new[] { "ivan", "roman", "alex" } },
            { new[] { "alina", "maria", "ekaterina", "darya", "darya", "ekaterina", "maria", "alina" }, new[] { "alina", "maria", "ekaterina", "darya" } },
        };
        
        private static IEnumerable<string> GetRecentChatList(string[] contacts)
        {
            HashSet<string> countedContacts = new HashSet<string>();
            
            for (int i = contacts.Length - 1; i >= 0; i--)
            {
                var currentContact = contacts[i];
                if (countedContacts.Contains(currentContact)) continue;
                countedContacts.Add(currentContact);
                yield return currentContact;
            }
        }

        private static void Main1()
        {
            int n = int.Parse(Console.ReadLine());

            var contacts = new string[n];
            for (int i = 0; i < n; i++)
            {
                var currentContact = Console.ReadLine();
                contacts[i] = currentContact;
            }

            var recentList = GetRecentChatList(contacts);

            foreach (string contact in recentList)
            {
                Console.WriteLine(contact);
            }
        }
    }
}