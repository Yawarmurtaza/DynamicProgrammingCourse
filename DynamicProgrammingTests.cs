namespace DynamicProgrammingCourse.Tests
{
    using System.Collections.Generic;
    using NUnit.Framework;

    [TestFixture]
    public class DynamicProgrammingTests
    {
        private DynamicProgramming runner;

        [SetUp]
        public void Setup()
        {
            runner = new DynamicProgramming();
        }

        [Test]
        public void AllWordBreakTabulationTest_Aaaaaaa()
        {
            string target = "aaaa";
            var dict = new[] { "a", "aa", "aaa", "aaaa", "aaaaa", "aaaaaa", "aaaaaaa" };
            List<List<string>> result = runner.AllWordBreakTabulation(target, dict);
            Assert.AreEqual(8, result.Count);
        }

        [Test]
        public void AllWordBreakTabulationTest_Aaaaaaaz()
        {
            string target = "Aaaz";
            var dict = new[] { "a", "aa", "aaa", "aaaa", "aaaaa", "aaaaaa", "aaaaaaa" };
            List<List<string>> result = runner.AllWordBreakTabulation(target, dict);
            Assert.IsNull(result);
        }

        [Test]
        public void AllWordBreakTabulationTest_aaaaaaaa()
        {
            string target = "aaaaaaaaa";
            var dict = new[] { "a", "aa", "aaa", "aaaa", "aaaaa", "aaaaaa", "aaaaaaa" };
            List<List<string>> result = runner.AllWordBreakTabulation(target, dict);
            Assert.AreEqual(253, result.Count);
        }

        [Test]
        public void AllWordBreakTabulationTest_Purple()
        {
            // Arrange.
            string target = "purple";
            var dict = new[] { "purp", "p", "ur", "le", "purpl" };

            // Act.
            List<List<string>> result = runner.AllWordBreakTabulation(target, dict);

            // Assert.
            Assert.AreEqual(2, result.Count); // there should be only 2 possible options.

            Assert.AreEqual("purp", result[0][0]);
            Assert.AreEqual("le", result[0][1]);

            Assert.AreEqual("p", result[1][0]);
            Assert.AreEqual("ur", result[1][1]);
            Assert.AreEqual("le", result[1][3]);
            Assert.AreEqual("p", result[1][2]);
        }

        [Test]
        public void AllWordBreakTabulationTest_Abcdef()
        {
            // Arrange.
            const string target = "abcdef";
            var dict = new[] { "ab", "abc", "cd", "def", "abcd", "ef", "c" };
            var result = runner.AllWordBreakTabulation(target, dict);

            // Assert.
            Assert.AreEqual(4, result.Count); // there should be only 4 possible options.
            Assert.AreEqual("abc", result[0][0]);
            Assert.AreEqual("def", result[0][1]);
            
            Assert.AreEqual("ab", result[1][0]);
            Assert.AreEqual("c", result[1][1]);
            Assert.AreEqual("def", result[1][2]);

            Assert.AreEqual("abcd", result[2][0]);
            Assert.AreEqual("ef", result[2][1]);

            Assert.AreEqual("ab", result[3][0]);
            Assert.AreEqual("cd", result[3][1]);
            Assert.AreEqual("ef", result[3][2]);

        }

        [TestCase("skateboard", new[] { "bo", "rd", "ate", "t", "ska", "sk", "boar" }, 0)]
        [TestCase("enterapotentpot", new[] { "a", "p", "ent", "enter", "ot", "o", "t" }, 4)]
        [TestCase("purple", new[] { "purp", "p", "ur", "le", "purpl" }, 2)]
        [TestCase("abcdef", new[] { "ab", "abc", "cd", "def", "abcd" }, 1)]
        [TestCase("eeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeef", new[] { "e", "ee", "eee", "eeee", "eeeee", "eeeeee" }, 0)]
        public void CountWordBreakTabulationTest(string target, string[] dictArr, int expectedResult)
        {
            int count = runner.CountWordBreakTabulation(target, dictArr);
            Assert.AreEqual(expectedResult, count);
        }


        [TestCase("fastracecar", new[] { "race", "car", "fast" }, true)]
        [TestCase("fastracecar", new[] { "yawar", "car", "fast" }, false)]
        [TestCase("fastracecar", new[] { "race", "car1", "fast" }, false)]
        [TestCase("fastracecar", new[] { "racecar", "race", "fast" }, true)]
        [TestCase("fastracecar", new[] { "astra", "fast", "race" }, false)]
        [TestCase("fastracecar", new[] { "astra", "fast", "race", "car" }, true)]
        [TestCase("fastracecar", new[] { "astra", "race", "car", "fast" }, true)]
        [TestCase("fastracecar", new[] { "rac", "fast", "car", "racecar" }, true)]
        [TestCase("skateboard", new[] { "bo", "rd", "ate", "t", "ska", "sk", "boar" }, false)]
        [TestCase("enterapotentpot", new[] { "a", "p", "ent", "enter", "ot", "o", "t" }, true)]
        [TestCase("abcdef", new[] { "ab", "abc", "cd", "def", "abcd" }, true)]
        [TestCase("eeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeef", new[] { "e", "ee", "eee", "eeee", "eeeee", "eeeeee" }, false)]

        [TestCase("abcdef", new[] { "ab", "abc", "cd", "def", "abcd" }, true)]
        public void CanWordBreakTabulationTest(string targetWord, string[] words, bool expectedResult)
        {
            bool result = runner.CanWordBreakTabulation(targetWord, words);
            Assert.AreEqual(expectedResult, result);
        }


        [TestCase(new[] { 2, 3, 5 }, 8, new[] { 5, 3 })]
        [TestCase(new[] { 5, 3, 4, 7 }, 7, new[] { 7 })]
        [TestCase(new[] { 1, 4, 5 }, 8, new[] { 4, 4 })]
        [TestCase(new[] { 1, 2, 5, 25 }, 100, new[] { 25, 25, 25, 25 })]
        public void BestSumTabulation(int[] arr, int target, int[] expectedItems)
        {
            var items = runner.BestSumTabulation(arr, target);

            HashSet<int> set = new HashSet<int>(expectedItems);
            foreach (int item in items)
            {
                Assert.IsTrue(set.Contains(item));
            }
        }

        [TestCase(new[] { 1, 2, 3 }, 0, new int[0])] // should be null.
        [TestCase(new[] { 2, 4 }, 7, null)]
        [TestCase(new[] { 2, 3, 5 }, 8, new[] { 5, 3 })]
        [TestCase(new[] { 10, 10 }, 7, null)]
        [TestCase(new[] { 3, 2, 7, 1 }, 8, new[] { 7, 1 })]
        [TestCase(new[] { 5, 3, 4, 7 }, 7, new[] { 7 })]
        [TestCase(new[] { 7, 14 }, 300, null)]

        [TestCase(new[] { 5, 3, 4 }, 7, new[] { 4, 3 })]
        [TestCase(new[] { 5, 3, 4, 7 }, 7, new[] { 7 })]
        public void HowSumTabulationV2(int[] arr, int target, int[] expectedResult)
        {
            var items = runner.HowSumTabulationV2(arr, target);
            if (expectedResult == null)
            {
                Assert.IsNull(items);
            }
            else
            {
                HashSet<int> set = new HashSet<int>(expectedResult);
                foreach (int item in items)
                {
                    Assert.IsTrue(set.Contains(item));
                }
            }
        }

        [TestCase(new[] { 2, 3, 100 }, 7, true)]
        [TestCase(new[] { 3, 2, 7, 1 }, 8, true)]
        [TestCase(new[] { 3, 2 }, 11, true)]
        [TestCase(new[] { 4, 2 }, 7, false)]
        [TestCase(new[] { 10, 10 }, 7, false)]
        [TestCase(new[] { 4, 3, 4, 7 }, 7, true)]
        [TestCase(new[] { 2, 3, 5 }, 8, true)]
        [TestCase(new[] { 7, 14 }, 300, false)]
        [TestCase(new[] { 5, 3, 4 }, 7, true)]
        public void CanSumTabulation(int[] arr, int target, bool expectedResult)
        {
            bool result = runner.CanSumTabulation(arr, target);
            Assert.AreEqual(expectedResult, result);
        }

        [TestCase(1, 1, (uint)1)]
        [TestCase(2, 2, (uint)2)]
        [TestCase(2, 3, (uint)3)]
        [TestCase(3, 3, (uint)6)]
        [TestCase(18, 18, (uint)2333606220)]
        //[TestCase(18, 18, (uint)1758125314)]
        public void GridTravelerTabulation(int m, int n, uint expectedResult)
        {
            uint result = runner.GridTravelerTabulation(m, n);
            Assert.AreEqual(expectedResult, result);
        }

        [TestCase(50, 12586269025.0d)]
        [TestCase(40, 102334155.0d)]
        [TestCase(30, 832040.0d)]
        [TestCase(7, 13)]
        [TestCase(6, 8)]
        public void FibTabulation(int n, double expectedResult)
        {
            double result = runner.FibTabulation(n);
            Assert.AreEqual(expectedResult, result);
        }


        [TestCase(new[] { 2, 3, 5 }, 8, new[] { 5, 3 })]
        [TestCase(new[] { 5, 3, 4, 7 }, 7, new[] { 7 })]
        [TestCase(new[] { 1, 4, 5 }, 8, new[] { 4, 4 })]
        [TestCase(new[] { 1, 2, 5, 25 }, 100, new[] { 25, 25, 25, 25 })]
        public void BestSum(int[] arr, int target, int[] expectedItems)
        {
            var items = runner.BestSum(arr, target);

            HashSet<int> set = new HashSet<int>(expectedItems);
            foreach (int item in items)
            {
                Assert.IsTrue(set.Contains(item));
            }
        }

        [TestCase(new[] { 1, 2, 3 }, 0, new int[0])] // should be null.
        [TestCase(new[] { 2, 4 }, 7, null)]
        [TestCase(new[] { 2, 3, 5 }, 8, new[] { 2, 2, 2, 2 })]
        [TestCase(new[] { 10, 10 }, 7, null)]
        [TestCase(new[] { 3, 2, 7, 1 }, 8, new[] { 3, 2, 3 })]
        [TestCase(new[] { 5, 3, 4, 7 }, 7, new[] { 3, 4 })]
        [TestCase(new[] { 7, 14 }, 300, null)]
        public void HowSum(int[] arr, int target, int[] expectedItems)
        {
            var items = runner.HowSum(arr, target);
            if (expectedItems == null)
            {
                Assert.IsNull(items);
            }
            else
            {
                HashSet<int> set = new HashSet<int>(expectedItems);
                foreach (int item in items)
                {
                    Assert.IsTrue(set.Contains(item));
                }
            }
        }

        [TestCase(new[] { 2, 3 }, 7, true)]
        [TestCase(new[] { 3, 2, 7, 1 }, 8, true)]
        [TestCase(new[] { 3, 2 }, 11, true)]
        [TestCase(new[] { 4, 2 }, 7, false)]
        [TestCase(new[] { 10, 10 }, 7, false)]
        [TestCase(new[] { 4, 3, 4, 7 }, 7, true)]
        [TestCase(new[] { 2, 3, 5 }, 8, true)]
        [TestCase(new[] { 7, 14 }, 300, false)]
        public void CanSum(int[] arr, int target, bool expectedResult)
        {
            bool result = runner.CanSum(arr, target);
            Assert.AreEqual(expectedResult, result);
        }

        [TestCase(1, 1, (uint)1)]
        [TestCase(2, 2, (uint)2)]
        [TestCase(2, 3, (uint)3)]
        [TestCase(3, 3, (uint)6)]
        //[TestCase(18, 18, (uint)2333606220)]
        [TestCase(18, 18, (uint)1758125314)]
        public void GridTraveler(int rows, int cols, uint expectedRoutes)
        {
            uint possibleRoutes = runner.GridTraveler(rows, cols);
            Assert.AreEqual(expectedRoutes, possibleRoutes);
        }


        [Test]
        public void ChopTopRow()
        {
            var grid = new[]
            {
                new[] {1, 2, 3},
                new[] {4, 5, 6},
                new[] {7, 8, 9},
            };


            int[][] newGrid = runner.ChopTopRow(grid);
            Assert.AreEqual(4, newGrid[0][0]);
            Assert.AreEqual(5, newGrid[0][1]);
            Assert.AreEqual(6, newGrid[0][2]);
        }

        [Test]
        public void ChopLeftColumn()
        {
            var grid = new[]
            {
                new[] {1, 2, 3},
                new[] {4, 5, 6},
                new[] {7, 8, 9},
            };

            int[][] newGrid = runner.ChopLeftColumn(grid);
            Assert.AreEqual(2, newGrid[0][0]);
            Assert.AreEqual(3, newGrid[0][1]);
            Assert.AreEqual(5, newGrid[1][0]);
        }

        [TestCase(new[] { 1, 5, 12, 19 }, 20, 2)]
        [TestCase(new[] { 1, 5, 12, 19 }, 16, 4)]
        [TestCase(new[] { 1, 5, 12, 19 }, 19, 1)]
        [TestCase(new[] { 2 }, 3, -1)]
        public void ChangeMaking(int[] denominations, int target, int expectedNumberOfCoins)
        {
            int result = runner.ChangeMaking(denominations, target);
            Assert.AreEqual(expectedNumberOfCoins, result);
        }

        [TestCase(new[] { 3, 10, 3, 1, 2 }, 12)]
        [TestCase(new[] { 9, 10, 9 }, 18)]
        [TestCase(new[] { 9, 10, 9, 7 }, 18)]
        [TestCase(new[] { 9, 10, 8, 10, 2, 1, 1 }, 21)]
        [TestCase(new[] { 8, 10, 9, 6, 6, 8 }, 25)]
        [TestCase(new[] { 3, 10, 3, 4, 2, 1, 1, 20 }, 35)]

        public void FlowerBox(int[] arr, int expectedResult)
        {
            int result = runner.FlowerBox(arr);
            Assert.AreEqual(expectedResult, result);
        }

        //[TestCase(50, 12586269025.0d)] //wont run
        //[TestCase(40, 102334155.0d)] // wont run.
        [TestCase(30, 832040.0d)]
        [TestCase(7, 13)]
        public void FibWithRecursionTest(int n, double expectedFibNumber)
        {
            double result = runner.FibWithRecursion(n);
            Assert.AreEqual(expectedFibNumber, result);
        }


        [TestCase(50, 12586269025.0d)]
        [TestCase(40, 102334155.0d)]
        [TestCase(30, 832040.0d)]
        [TestCase(7, 13)]
        public void FibWithIterationTest(int n, double expectedFibNumber)
        {
            double result = runner.FibWithIteration(n);
            Assert.AreEqual(expectedFibNumber, result);
        }

        [TestCase(50, 12586269025.0d)]
        [TestCase(40, 102334155.0d)]
        [TestCase(30, 832040.0d)]
        [TestCase(7, 13)]
        public void FibWithMemTest(int n, double expectedFibNumber)
        {
            double result = runner.FibWithMem(n);
            Assert.AreEqual(expectedFibNumber, result);
        }

        [TestCase(50, 12586269025.0d)]
        [TestCase(40, 102334155.0d)]
        [TestCase(30, 832040.0d)]
        [TestCase(7, 13)]
        public void FibIterativeMemTest(int n, double expectedFibNumber)
        {
            double result = runner.FibIterativeMem(n);
            Assert.AreEqual(expectedFibNumber, result);
        }

        [TestCase("12", 2)]
        [TestCase("216", 3)]
        [TestCase("1214", 5)]
        [TestCase("0", 0)]
        [TestCase("1", 1)]
        [TestCase("0", 0)]
        public void DecodeNumbersTest(string input, int expectedResult)
        {
            int result = runner.DecodeNumbers(input);
            Assert.AreEqual(expectedResult, result);
        }

        [TestCase("12", 2)]
        [TestCase("216", 3)]
        [TestCase("1214", 5)]
        [TestCase("0", 0)]
        [TestCase("1", 1)]
        [TestCase("0", 0)]
        public void DecodeNumbersV2Test(string input, int expectedResult)
        {
            int result = runner.DecodeNumbersV2(input);
            Assert.AreEqual(expectedResult, result);
        }

        [TestCase("12", 2)]
        [TestCase("216", 3)]
        [TestCase("0", 0)]
        [TestCase("1", 1)]
        [TestCase("0", 0)]
        [TestCase("1214", 5)]
        public void DecodeNumbersRec(string input, int expectedResult)
        {
            int result = runner.DecodeNumbersRec(input);
            Assert.AreEqual(expectedResult, result);
        }

        [TestCase(4, 7)]
        [TestCase(5, 13)]
        [TestCase(6, 24)]
        public void RunUpstairsRecursive(int numberOfSteps, int expectedResult)
        {
            int result = runner.RunUpstairsRecursive(numberOfSteps);
            Assert.AreEqual(expectedResult, result);
        }

        [TestCase(4, 7)]
        [TestCase(5, 13)]
        [TestCase(6, 24)]
        public void RunUpstairsRecursiveWithMem(int numberOfSteps, int expectedResult)
        {
            int result = runner.RunUpstairsRecursiveWithMem(numberOfSteps);
            Assert.AreEqual(expectedResult, result);
        }

        [TestCase(4, 7)]
        [TestCase(5, 13)]
        [TestCase(6, 24)]
        public void RunUpstairsIterative(int numberOfSteps, int expectedResult)
        {
            int result = runner.RunUpstairsIterative(numberOfSteps);
            Assert.AreEqual(expectedResult, result);
        }

        [TestCase("fastracecar", new[] { "race", "car", "fast" }, true)]
        [TestCase("fastracecar", new[] { "yawar", "car", "fast" }, false)]
        [TestCase("fastracecar", new[] { "race", "car1", "fast" }, false)]
        [TestCase("fastracecar", new[] { "racecar", "race", "fast" }, true)]
        [TestCase("fastracecar", new[] { "astra", "fast", "race" }, false)]
        [TestCase("fastracecar", new[] { "astra", "fast", "race", "car" }, true)]
        [TestCase("fastracecar", new[] { "astra", "race", "car", "fast" }, true)]
        [TestCase("fastracecar", new[] { "rac", "fast", "car", "racecar" }, true)]
        [TestCase("skateboard", new[] { "bo", "rd", "ate", "t", "ska", "sk", "boar" }, false)]
        [TestCase("enterapotentpot", new[] { "a", "p", "ent", "enter", "ot", "o", "t" }, true)]
        [TestCase("abcdef", new[] { "ab", "abc", "cd", "def", "abcd" }, true)]
        [TestCase("eeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeef", new[] { "e", "ee", "eee", "eeee", "eeeee", "eeeeee" }, false)]
        public void WordBreakBottomUp(string s, string[] dictArr, bool expectedResult)
        {
            var x = "yawar".Substring(0, 3);
            bool result = runner.WordBreakBottomUp(s, dictArr);
            Assert.AreEqual(expectedResult, result);
        }

        [TestCase("fastracecar", new[] { "race", "car", "fast" }, true)]
        [TestCase("fastracecar", new[] { "yawar", "car", "fast" }, false)]
        [TestCase("fastracecar", new[] { "race", "car1", "fast" }, false)]
        [TestCase("fastracecar", new[] { "racecar", "race", "fast" }, true)]
        [TestCase("fastracecar", new[] { "astra", "fast", "race" }, false)]
        [TestCase("fastracecar", new[] { "astra", "fast", "race", "car" }, true)]
        [TestCase("fastracecar", new[] { "astra", "race", "car", "fast" }, true)]
        [TestCase("fastracecar", new[] { "rac", "fast", "car", "racecar" }, true)]
        [TestCase("skateboard", new[] { "bo", "rd", "ate", "t", "ska", "sk", "boar" }, false)]
        [TestCase("enterapotentpot", new[] { "a", "p", "ent", "enter", "ot", "o", "t" }, true)]
        [TestCase("abcdef", new[] { "ab", "abc", "cd", "def", "abcd" }, true)]
        [TestCase("eeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeef", new[] { "e", "ee", "eee", "eeee", "eeeee", "eeeeee" }, false)]
        public void WordBreakRecTest(string s, string[] dictArr, bool expectedResult)
        {
            bool result = runner.CanWordBreakRec(s, dictArr);
            Assert.AreEqual(expectedResult, result);
        }

        [TestCase("skateboard", new[] { "bo", "rd", "ate", "t", "ska", "sk", "boar" }, 0)]
        [TestCase("enterapotentpot", new[] { "a", "p", "ent", "enter", "ot", "o", "t" }, 4)]
        [TestCase("purple", new[] { "purp", "p", "ur", "le", "purpl" }, 2)]
        [TestCase("abcdef", new[] { "ab", "abc", "cd", "def", "abcd" }, 1)]
        [TestCase("eeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeef", new[] { "e", "ee", "eee", "eeee", "eeeee", "eeeeee" }, 0)]
        public void CountWordBreak(string target, string[] dictArr, int expectedResult)
        {
            int count = runner.CountWordBreak(target, dictArr);
            Assert.AreEqual(expectedResult, count);
        }

        [Test]
        public void AllConstructWordTest_Abcdef()
        {
            string target = "abcdef";
            var dict = new[] { "ab", "abc", "cd", "def", "abcd", "ef", "c" };
            //var dict = new[] { "ab", "cd", "def", "ef", "c" };
            var result = runner.AllConstructWord(target, dict);

            Assert.AreEqual("def", result[1][0]);
            Assert.AreEqual("c", result[1][1]);
            Assert.AreEqual("ab", result[1][2]);

            Assert.AreEqual("ef", result[0][0]);
            Assert.AreEqual("cd", result[0][1]);
            Assert.AreEqual("ab", result[0][2]);

            Assert.AreEqual("def", result[2][0]);
            Assert.AreEqual("abc", result[2][1]);

            Assert.AreEqual("ef", result[3][0]);
            Assert.AreEqual("abcd", result[3][1]);
        }

        [Test]
        public void HowConstructWordTest_Purple()
        {
            string target = "purple";
            var dict = new[] { "purp", "p", "ur", "le", "purpl" };
            List<List<string>> result = runner.AllConstructWord(target, dict);

            Assert.AreEqual("le", result[0][0]);
            Assert.AreEqual("purp", result[0][1]);

            Assert.AreEqual("le", result[1][0]);
            Assert.AreEqual("p", result[1][1]);
            Assert.AreEqual("ur", result[1][2]);
            Assert.AreEqual("p", result[1][3]);
        }

        [Test]
        public void HowConstructWordTest_Skateboard()
        {
            string target = "skateboard";
            var dict = new[] { "bo", "rd", "ate", "t", "ska", "sk", "boar" };
            List<List<string>> result = runner.AllConstructWord(target, dict);
            Assert.AreEqual(0, result.Count);
        }

        [Test]
        public void HowConstructWordTest_Aaaaaaa()
        {
            string target = "aaaa";
            var dict = new[] { "a", "aa", "aaa", "aaaa", "aaaaa", "aaaaaa", "aaaaaaa" };
            List<List<string>> result = runner.AllConstructWord(target, dict);
            Assert.AreEqual(8, result.Count);
        }

        [Test]
        public void HowConstructWordTest_Aaaaaaaz()
        {
            string target = "Aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaz";
            var dict = new[] { "a", "aa", "aaa", "aaaa", "aaaaa", "aaaaaa", "aaaaaaa" };
            List<List<string>> result = runner.AllConstructWord(target, dict);
            Assert.AreEqual(0, result.Count);
        }

        [TestCase("iamyawarandiamaboy", 3, 5, "iamandiamaboy")]
        [TestCase("yawarisagoodboy", 0, 5, "isagoodboy")]
        [TestCase("iamagoodboyyawar", 11, 5, "iamagoodboy")]

        public void ChopStringTest(string s, int startIndex, int len, string expectedResult)
        {
            string result = runner.ChopString(s, startIndex, len);
            Assert.AreEqual(expectedResult, result);
        }

        [TestCase("iamyawarandiliveindublin", "yawar", 3)]
        [TestCase("iamyawarandiliveindublin", "yawar1", -1)]
        public void GetStartIndex(string s, string word, int expectedStartIndex)
        {
            int result = runner.GetStartIndex(s, word);
            Assert.AreEqual(expectedStartIndex, result);
        }
    }
}
