using System;
using System.Collections.Generic;
using Xunit;

namespace UniLecs
{
    public class Task1SingleInstanceInString
    {
        [Theory]
        [InlineData("abcdefg ")]
        [InlineData(" ")]
        public void NoDuplicateItem(string input)
        {
            Assert.True(CheckIfUniqueItemsOnly(input));
        }

        [Theory]
        [InlineData("abcafg")]
        [InlineData(" Try   ")]
        [InlineData("")]
        public void NoUniqueItems(string input)
        {
            Assert.False(CheckIfUniqueItemsOnly(input));
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
