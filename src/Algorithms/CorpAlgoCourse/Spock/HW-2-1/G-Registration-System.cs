using System;
using System.Collections.Generic;
using Xunit;

namespace CorpAlgoCourse.Spock
{
    /// <summary>
    /// Codeforces: Order Book
    /// http://codeforces.com/gym/273845/problem/G
    /// </summary>
    [Trait("Category", "Corporate Algorithmic Course: Spock, HW 2.1")]
    public class RegistrationSystem
    {
        private static IEnumerable<string> RegisterUsers(List<string> users)
        {
            var registrar = new Dictionary<string, int>(users.Count);

            foreach (var user in users)
            {
                int userNamePostfix = 0;
                if (registrar.TryGetValue(user, out userNamePostfix))
                {
                    userNamePostfix++;
                    registrar[user] = userNamePostfix;
                    yield return string.Format("{0}{1}", user, userNamePostfix);
                }
                else
                {
                    registrar.Add(user, 0);
                    yield return "OK";
                }
            }    
        }

        private static void Main1()
        {
            int n = int.Parse(Console.ReadLine());

            var users = new List<string>(n);
            
            for (int i = 0; i < n; i++)
            {
                users.Add(Console.ReadLine());
            }

            foreach (var userRegResult in RegisterUsers(users))
            {
                Console.WriteLine(userRegResult);
            }
        }
    }
}