using System.Collections.Generic;
using Xunit;

namespace UniLecs
{
    [Collection("UniLecs")]
    public class Task1SingleInstanceInString
    {
        [Theory]
        [Trait("Task 1", "Test 1")]
        [InlineData("abcdefg ", true)]
        [InlineData(" ", true)]
        [InlineData("abcafg", false)]
        [InlineData(" Try   ", false)]
        [InlineData("", false)]
        [InlineData(null, false)]
        public void CheckForUniqueItems(string input, bool expected)
        {
            Assert.Equal(expected, CheckIfUniqueItemsOnly(input));
        }

        private bool CheckIfUniqueItemsOnly(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return false;
            }

            var hashSet = new HashSet<char>();

            foreach (var symbol in input)
            {
                if (hashSet.Contains(symbol))
                {
                    return false;
                }

                hashSet.Add(symbol);
            }

            return true;
        }
    }
}
