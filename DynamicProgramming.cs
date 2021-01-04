namespace DynamicProgrammingCourse
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary> https://www.youtube.com/watch?v=OQ5jsbhAv_M </summary>
    public class DynamicProgramming
    {
        /// <summary> Time complexity = O(n^m)
        /// Space complexity = O(n^m)
        /// Where n = targetWord.Length and m = words.Length. </summary>
        /// <param name="targetWord"></param>
        /// <param name="words"></param>
        /// <returns></returns>
        public List<List<string>> AllWordBreakTabulation(string targetWord, string[] words)
        {
            List<List<string>>[] result = new List<List<string>>[targetWord.Length + 1];
            result[0] = new List<List<string>>() { new List<string>() };
            for (int i = 0; i <= targetWord.Length; i++)
            {
                foreach (string word in words)
                {
                    if (i + word.Length < result.Length && targetWord.Substring(i, word.Length) == word)
                    {
                        if (result[i + word.Length] == null)
                        {
                            result[i + word.Length] = new List<List<string>>();
                        }

                        if (result[i] != null)
                        {
                            foreach (List<string> list in result[i])
                            {
                                result[i + word.Length].Add(new List<string>(list) {word});
                            }
                        }
                    }
                }
            }

            return result[targetWord.Length];
        }

        // if the targetWord is an empty string, then whatever is in the words array, we can make empty string.
        // Time: O(m^2 * n) where m = targetWord.Length and n = words.Length.
        public int CountWordBreakTabulation(string targetWord, string[] words)
        {
            int[] result = new int[targetWord.Length + 1];
            result[0] = 1; // seeding the value with 1 at 0 index which means we can always make an empty string.

            for (int i = 0; i <= targetWord.Length; i++)
            {
                // we are only interested in counting those that are non-zero because zero means we can construct the target word.
                if (result[i] > 0)
                {
                    foreach (string word in words)
                    {
                        if (i + word.Length <= targetWord.Length &&
                            targetWord.Substring(i, word.Length) == word) // we use 'i' variable to keep moving forward the start index
                                                                          // so that we can inspect all words. 
                        {
                            // add the value to the current word's length making the count of possible combination(s)
                            /*
                             * E.g: targetWord = purple, words[] = "purp", "p", "ur", "le", "purpl"
                             *  0 1 2 3 4 5 6 
                             * |1|0|0|0|0|0|0|
                             * word =  "purp"
                             * will result in
                             *  0 1 2 3 4 5 6 
                             * |1|0|0|0|1||0|
                             *  p u r p-^
                             * the 1 on index 4 is copied from index 0 using following statement:
                            */
                            result[i + word.Length] += result[i];
                        }
                    }
                }
            }

            return result[targetWord.Length];
        }

        // if the targetWord is an empty string, then whatever is in the words array, we can make empty string.
        // Time: O(m^2 * n) where m = targetWord.Length and n = words.Length.
        public bool CanWordBreakTabulation(string targetWord, string[] words)
        {
            bool[] result = new bool[targetWord.Length + 1];
            result[0] = true;

            for (int i = 0; i <= targetWord.Length; i++)
            {
                if (result[i])
                {
                    foreach (string word in words)
                    {
                        if (i + word.Length <= targetWord.Length && targetWord.Substring(i, word.Length) == word)
                        {
                            result[i + word.Length] = true;
                            if (result[targetWord.Length]) return true;
                        }
                    }
                }
            }

            return result[targetWord.Length];
        }


        public int[] BestSumTabulation(int[] arr, int target)
        {
            List<int>[] result = new List<int>[target + 1];
            List<int> bestSumResult = new List<int>();
            result[0] = new List<int>();
            for (int c = 0; c < result.Length; c++)
            {
                if (result[c] != null)
                {
                    foreach (int val in arr)
                    {
                        if (val + c < result.Length)
                        {
                            result[val + c] = new List<int>();
                            result[val + c].Add(val);
                            result[val + c].AddRange(result[c]);

                            if (result[val + c].Sum() == target)
                            {
                                if (bestSumResult.Count > result[val + c].Count)
                                {
                                    bestSumResult = result[val + c];
                                }
                            }

                        }
                    }
                }
            }

            return bestSumResult.ToArray();
        }

        public int[] HowSumTabulationV2(int[] arr, int target)
        {
            List<int>[] result = new List<int>[target + 1];
            result[0] = new List<int>();
            for (int c = 0; c < result.Length; c++)
            {
                if (result[c] != null)
                {
                    foreach (int val in arr)
                    {
                        if (val + c < result.Length)
                        {
                            result[val + c] = new List<int> { val };
                            result[val + c].AddRange(result[c]);

                            if (result[val + c].Sum() == target)
                                return result[val + c].ToArray();
                        }
                    }
                }
            }

            return result[target] != null ? result[target].ToArray() : null;
        }


        /// <summary>
        /// EG:  [TestCase(new[] { 5, 3, 4 }, 7, true)]
        ///  0 1 2 3 4 5 6 7
        /// |T|F|F|F|F|F|F|F|
        /// ------------------------------
        ///  0 1 2 3 4 5 6 7
        /// |T|F|F|T|T|T|F|F|
        ///  |     ^ ^ ^
        ///  |__3__| | |
        ///  |__4____| |
        ///  |__5______| 
        ///  then keep moving by cache[val + c] = true;
        /// </summary>
        /// <param name="arr"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        public bool CanSumTabulation(int[] arr, int target)
        {
            bool[] cache = new bool[target + 1];
            cache[0] = true;

            for (int c = 0; c < target; c++)
            {
                if (cache[c])
                {
                    foreach (int val in arr)
                    {
                        if (val + c < cache.Length)
                            cache[val + c] = true;
                    }
                }
            }

            return cache[target];
        }

        public uint GridTravelerTabulation(int m, int n)
        {
            int[,] grid = new int[m + 1, n + 1];

            // base cases...set the seed values
            grid[0, 0] = 0; // when we have a grid of 0, there is 0 way to navigate.
            grid[1, 1] = 1; // when we have a grid of 1, there is only 1 way to navigate.

            // from above settings / understanding, we can now build the rest of the grid 
            for (int r = 0; r <= m; r++)
            {
                for (int c = 0; c <= n; c++)
                {
                    if (c != n) grid[r, c + 1] += grid[r, c];
                    if (r != m) grid[r + 1, c] += grid[r, c];
                }
            }

            return (uint)grid[m, n];
        }

        public double FibTabulation(int n)
        {
            double[] d = new double[n + 1];

            // base cases...set the seed values
            d[0] = 0;
            d[1] = 1;
            for (int i = 2; i <= n; i++)
            {
                d[i] = d[i - 1] + d[i - 2];
            }

            return d[n];
        }

        public List<List<string>> AllConstructWord(string target, string[] dict)
        {
            var cache = new Dictionary<string, List<List<string>>>();
            var result = AllConstructWordHelper(target, dict, cache);
            return result;
        }

        private List<List<string>> AllConstructWordHelper(string target, string[] dict, IDictionary<string, List<List<string>>> cache)
        {
            if (target == string.Empty) return new List<List<string>> { new List<string>() };

            var result = new List<List<string>>();
            foreach (string word in dict)
            {
                if (target.IndexOf(word, StringComparison.Ordinal) == 0)
                {
                    string remainingWord = target.Substring(word.Length);
                    var suffixWays = AllConstructWordHelper(remainingWord, dict, cache);
                    if (suffixWays.Count > 0)
                    {
                        suffixWays.ForEach(item => item.Add(word));
                        result.AddRange(suffixWays);
                    }
                }
            }

            return result;
        }

        public int CountWordBreak(string target, string[] dict)
        {
            var cache = new Dictionary<string, int>();
            return CountWordBreakRecHelper(target, dict, cache);
        }


        private static int CountWordBreakRecHelper(string target, string[] dict, IDictionary<string, int> cache)
        {
            if (cache.ContainsKey(target)) return cache[target];
            if (target == string.Empty) return 1;
            int count = 0;
            foreach (string word in dict)
            {
                if (target.IndexOf(word, StringComparison.Ordinal) == 0)
                {
                    string suffix = target.Substring(word.Length);
                    int x = CountWordBreakRecHelper(suffix, dict, cache);
                    count += x;
                }
            }

            cache[target] = count;
            return count;
        }

        public bool CanWordBreakRec(string target, string[] dict)
        {
            var cache = new Dictionary<string, bool>();
            return WordBreakRecHelper(target, dict, cache);
        }

        private static bool WordBreakRecHelper(string target, string[] dict, IDictionary<string, bool> cache)
        {
            if (cache.ContainsKey(target)) return cache[target];
            if (string.IsNullOrEmpty(target)) return true;

            foreach (string word in dict)
            {
                if (target.IndexOf(word, StringComparison.Ordinal) == 0)
                {
                    string suffix = target.Substring(word.Length);
                    if (WordBreakRecHelper(suffix, dict, cache))
                    {
                        cache[target] = true;
                        return true;
                    }
                }
            }

            cache[target] = false;
            return false;
        }

        public bool WordBreakBottomUp(string s, string[] wordDict)
        {
            HashSet<string> wordSet = new HashSet<string>(wordDict);
            int maxLenWord = 0;
            foreach (string word in wordSet)
            {
                maxLenWord = Math.Max(word.Length, maxLenWord);
            }
            bool[] dp = new bool[s.Length + 1];
            dp[0] = true;
            for (int i = 0; i <= s.Length; i++)
            {
                for (int j = i - 1; j >= 0; j--)
                {
                    if (i - j > maxLenWord) continue;
                    string tempWord = s.Substring(j, i - j);
                    bool b = dp[j];
                    if (b && wordSet.Contains(tempWord))
                    {
                        dp[i] = true;
                        break;
                    }
                }
            }

            return dp[s.Length];
        }
        public List<int> BestSum(int[] arr, int target)
        {
            var resultColl = new List<List<int>>();
            var cache = new Dictionary<int, List<int>>();
            var result = BestSumHelper(arr, target, cache);
            return result;
        }


        private static List<int> BestSumHelper(int[] arr, int target, IDictionary<int, List<int>> cache)
        {
            if (cache.ContainsKey(target)) return cache[target];
            if (target == 0) return new List<int>();
            if (target < 0) return null;

            List<int> shortestArr = null;

            foreach (int value in arr)
            {
                int remainder = target - value;
                List<int> result = BestSumHelper(arr, remainder, cache);

                if (result != null)
                {
                    List<int> newResult = new List<int>(result);
                    newResult.Add(value);

                    if (shortestArr == null || newResult.Count < shortestArr.Count)
                    {
                        shortestArr = newResult;
                    }

                    // because we are not returning from here, we dont need to cache it
                    //cache[target] = shortestArr;
                }
            }

            cache[target] = shortestArr;
            return shortestArr;
        }

        public List<int> HowSum(int[] arr, int target)
        {
            var cache = new Dictionary<int, List<int>>();
            return HowSumHelper(arr, target, cache);
        }

        private static List<int> HowSumHelper(int[] arr, int target, IDictionary<int, List<int>> cache)
        {
            if (cache.ContainsKey(target)) return cache[target];
            if (target == 0) return new List<int>();
            if (target < 0) return null;

            foreach (int value in arr)
            {
                int remainder = target - value;
                var result = HowSumHelper(arr, remainder, cache);

                if (result != null)
                {
                    List<int> newResult = new List<int>(result);
                    newResult.Add(value);
                    cache[target] = newResult;
                    return newResult;
                }

            }

            cache[target] = null;
            return null;
        }

        public bool CanSum(int[] arr, int target)
        {
            var cache = new Dictionary<int, bool>();
            return CanSumHelper(arr, target, cache);
        }

        //private static bool CanSumHelper(int[] arr, int target, IDictionary<int, bool> cache)
        //{
        //    if (cache.ContainsKey(target)) return cache[target];
        //    if (target == 0) return true;
        //    if (target < 0) return false;

        //    foreach (int value in arr)
        //    {
        //        int remainder = target - value;
        //        if (CanSumHelper(arr, remainder, cache))
        //        {
        //            cache[target] = true;
        //            return true;
        //        }
        //    }
        //    cache[target] = false;
        //    return false;
        //}

        private static bool CanSumHelper(int[] arr, int target, IDictionary<int, bool> cache)
        {
            if (cache.ContainsKey(target)) return cache[target];
            if (target == 0) return true;
            if (target == 1) return false;

            foreach (int value in arr)
            {
                if (target >= value)
                {
                    int remainder = target - value;
                    if (CanSumHelper(arr, remainder, cache))
                    {
                        cache[target] = true;
                        return true;
                    }
                }
            }

            cache[target] = false;
            return false;
        }


        public uint GridTraveler(int rows, int cols)
        {
            var cache = new Dictionary<string, uint>();
            return GridTravelerHelper(rows, cols, cache);
        }

        private uint GridTravelerHelper(int rows, int cols, IDictionary<string, uint> cache)
        {
            var key = rows.ToString() + cols;
            if (cache.ContainsKey(key))
            {
                return cache[key];
            }
            if (rows == 1 && cols == 1) return 1;
            if (rows == 0 || cols == 0) return 0;

            uint value = GridTravelerHelper(rows - 1, cols, cache) + GridTravelerHelper(rows, cols - 1, cache);
            cache[key] = value;
            return value;
        }

        //public int GridTraveler(int[][] grid)
        //{
        //    if (grid.GetLength(0) == 1 && grid.GetLength(1) == 1) return 1;
        //    if (grid.GetLength(0) == 0 && grid.GetLength(0) == 0) return 0;

        //    return GridTraveler(ChopLeftColumn(grid)) + GridTraveler(ChopTopRow(grid));
        //}

        public int[][] ChopTopRow(int[][] grid)
        {
            int[][] newGrid = new int[grid.Length - 1][];
            for (int i = 1; i < grid.Length; i++)
            {
                newGrid[i - 1] = new int[grid[i].Length];
                for (int j = 0; j < grid[i].Length; j++)
                {
                    newGrid[i - 1][j] = grid[i][j];
                }
            }

            return newGrid;
        }

        public int[][] ChopLeftColumn(int[][] grid)
        {
            int[][] newGrid = new int[grid.Length][];
            for (int i = 0; i < grid.Length; i++)
            {
                newGrid[i] = new int[grid[i].Length - 1];
                for (int j = 1; j < grid[i].Length; j++)
                {
                    newGrid[i][j - 1] = grid[i][j];
                }
            }

            return newGrid;
        }

        public int ChangeMaking(int[] coins, int amount)
        {
            var cache = new Dictionary<TargetIndex, int>();
            int result = ChangeMakingHelper(coins, amount, coins.Length - 1, cache);
            if (result == int.MinValue) result = -1;
            return result;
        }

        // this is difficult to understand..
        //https://www.linkedin.com/learning/fundamentals-of-dynamic-programming/what-you-should-know?u=3322
        private int ChangeMakingHelper(int[] allCoins, int targetValue, int coinIndex, Dictionary<TargetIndex, int> cache)
        {
            if (cache.ContainsKey(new TargetIndex(targetValue, coinIndex)))
            {
                return cache[new TargetIndex(targetValue, coinIndex)];
            }

            int choiceTaken = 0;
            int choiceLeft = 0;

            // compute the lowest number of coins we need if choosing to take a coin of current denominations.
            int coinValue = allCoins[coinIndex];
            if (coinValue > targetValue)
            {
                // current coin value is too large - overshoot.
                choiceTaken = int.MaxValue;
            }
            else if (coinValue == targetValue)
            {
                // we have reached the target..
                choiceTaken = 1;
            }
            else
            {
                int newTargetValue = targetValue - coinValue;
                choiceTaken = 1 + ChangeMakingHelper(allCoins, newTargetValue, coinIndex, cache);
            }

            // compute the lowest number of coins we need if not taking anymore coins of current denominations.

            if (coinIndex == 0)
            {
                choiceLeft = int.MaxValue;
            }
            else
            {
                choiceLeft = ChangeMakingHelper(allCoins, targetValue, coinIndex - 1, cache);
            }

            int optimal = Math.Min(choiceLeft, choiceTaken);
            cache[new TargetIndex(coinIndex, targetValue)] = optimal;
            return optimal;
        }

        private class TargetIndex
        {
            public TargetIndex(int t, int i)
            {
                Target = t;
                Index = i;
            }

            private int Target { get; }
            private int Index { get; }

            public override bool Equals(object obj)
            {
                TargetIndex other = (TargetIndex)obj;
                return Target == other.Target && Index == other.Index;
            }

            public override int GetHashCode()
            {
                return Target.GetHashCode() + Index.GetHashCode();
            }
        }

        public int FlowerBox(int[] arr)
        {
            int a = 0;
            int b = 0;

            foreach (int val in arr)
            {
                int temp = a;
                a = b;
                b = Math.Max(temp + val, b);
            }

            return b;
        }

        public double FibWithIteration(int n)
        {
            double x = 0;
            double y = 1;
            double z = 0;
            for (int i = 0; i < n - 1; i++)
            {
                z = x + y;
                x = y;
                y = z;
            }

            return z;
        }


        public double FibWithRecursion(int n)
        {
            if (n <= 2)
            {
                return 1;
            }

            return FibWithMem(n - 1) + FibWithMem(n - 2);
        }


        public double FibWithMem(int n)
        {
            IDictionary<int, double> mem = new Dictionary<int, double>();
            double f = FibHelper(n, mem);
            return f;
        }

        private double FibHelper(int n, IDictionary<int, double> mem)
        {
            // check 1. if we already have value for the n in dict?
            if (mem.ContainsKey(n)) return mem[n];

            // when n is 2 or 1, return 1 because we already know that 2 and 1 has fib value 1.
            if (n <= 2) return 1;

            // compute the fib for n-1 + n-2
            double f = FibHelper(n - 1, mem) + FibHelper(n - 2, mem);

            // put the fib value of n in the dict for check 1.
            mem[n] = f;
            return f;
        }

        // Bottom-Up dynamic programming algorithm!!
        public double FibIterativeMem(int n)
        {
            double f = 0;
            var mem = new Dictionary<int, double>();
            for (int i = 0; i <= n; i++)
            {
                if (i <= 2)
                {
                    f = 1;
                }
                else
                {
                    f = mem[i - 1] + mem[i - 2];
                }

                mem[i] = f;
            }

            return mem[n];
        }

        public int DecodeNumbers(string s)
        {
            int[] waysToDecode = new int[s.Length + 1];
            waysToDecode[0] = 1; // number of ways to decode a string of len 0 is 1
            waysToDecode[1] = s[0] == '0' ? 0 : 1; // number of ways to decode a string of len 1 could be 0 or 1 depending upon the string containing 0 or any from 1 till 9.

            for (int i = 2; i <= s.Length; i++)
            {
                // get the number (int value) of current char 
                int digit = int.Parse(s.Substring(i - 1, 1));

                // get the last 2 numbers because we dont have mapping for 3 numbers. Z = 26!!
                int lastTwoDigits = int.Parse(s.Substring(i - 2, 2));
                if (digit >= 1)
                {
                    // we know the mapping exists from 1 till 9
                    // add to the current sub problem that we are trying to solve the possible ways to decode previous numbers.
                    // that would be the max ways to decode those numbers.
                    // add to the current sub problem the answer to the previous sub problem.
                    waysToDecode[i] += waysToDecode[i - 1];
                }

                if (lastTwoDigits >= 10 && lastTwoDigits <= 26)
                {
                    // since 2 numbers set limit is 10 till 26 (Z is 26)
                    waysToDecode[i] += waysToDecode[i - 2]; // we subtract 2 from i because we have a valid 2 numbers value (between 10 and 26 - inclusive)
                }
            }

            // our array has been filled up to the end
            // our array stores  the maximum ways to decode the input string up till the index, 
            // the index represents the number of chars in the input string.
            return waysToDecode[s.Length];
        }

        // using 2 variables instead of an array...
        public int DecodeNumbersV2(string code)
        {
            int x = 1;
            int y = code[0] == '0' ? 0 : 1;
            int result = y;
            for (int i = 2; i <= code.Length; i++)
            {
                int z = 0;
                // get the number (int value) of current char 
                int digit = int.Parse(code.Substring(i - 1, 1));

                // get the last 2 numbers because we dont have mapping for 3 numbers. Z = 26!!
                int lastTwoDigits = int.Parse(code.Substring(i - 2, 2));
                if (digit >= 1)
                {
                    // we know the mapping exists from 1 till 9
                    // add to the current sub problem that we are trying to solve the possible ways to decode previous numbers.
                    // that would be the max ways to decode those numbers.
                    // add to the current sub problem the answer to the previous sub problem.
                    z = y;
                }

                if (lastTwoDigits >= 10 && lastTwoDigits <= 26)
                {
                    // since 2 numbers set limit is 10 till 26 (Z is 26)
                    // we subtract 2 from i because we have a valid 2 numbers value (between 10 and 26 - inclusive)
                    z = z + x;
                }

                x = y;
                y = z;
                result = z;
            }

            // our array has been filled up to the end
            // our array stores  the maximum ways to decode the input string up till the index, 
            // the index represents the number of chars in the input string.
            //return waysToDecode[s.Length];
            return result;
        }

        public int DecodeNumbersRec(string code)
        {
            var cache = new Dictionary<int, int>();
            return DecodeNumbersRecHelper(code, code.Length, cache);
        }

        private int DecodeNumbersRecHelper(string code, int k, Dictionary<int, int> cache)
        {
            // example here is for code = "1214"
            if (cache.ContainsKey(k))
            {
                return cache[k];
            }
            if (k == 0)
            {
                return 1;
            }

            // code: 1415, code.Length = 4, k = 4, currentIndex = 0: code[currentIndex] = 1
            // code: 1415, code.Length = 4, k = 3, currentIndex = 1: code[currentIndex] = 2
            // code: 1415, code.Length = 4, k = 2, currentIndex = 2: code[currentIndex] = 1
            // code: 1415, code.Length = 4, k = 1, currentIndex = 3: code[currentIndex] = 4

            int currentIndex = code.Length - k;
            char c = code[currentIndex];
            if (c == '0')
            {
                return 0;
            }

            int result = DecodeNumbersRecHelper(code, k - 1, cache);
            if (k >= 2 && int.Parse(code.Substring(currentIndex, 2)) <= 26)
            {
                // currentIndex = 0: code.Substring(currentIndex, 2) = 12
                // currentIndex = 1: code.Substring(currentIndex, 2) = 21
                // currentIndex = 2: code.Substring(currentIndex, 2) = 14
                // currentIndex = 3: k will be 1 for currentIndex to be 3. If k < 2, we wont be here.
                result += DecodeNumbersRecHelper(code, k - 2, cache);
            }

            cache[k] = result;
            return result;
        }

        public int RunUpstairsRecursive(int numberOfSteps)
        {
            if (numberOfSteps == 0 || numberOfSteps == 1) return 1;
            if (numberOfSteps == 2) return 2;

            return RunUpstairsRecursive(numberOfSteps - 3) + RunUpstairsRecursive(numberOfSteps - 2)
                                                  + RunUpstairsRecursive(numberOfSteps - 1);
        }

        public int RunUpstairsRecursiveWithMem(int numberOfSteps)
        {
            var mem = new Dictionary<int, int>();
            return RunUpstairsRecursiveWithMem(numberOfSteps, mem);
        }
        private int RunUpstairsRecursiveWithMem(int numberOfSteps, Dictionary<int, int> mem)
        {
            if (numberOfSteps == 0 || numberOfSteps == 1) return 1;
            if (numberOfSteps == 2) return 2;
            if (mem.ContainsKey(numberOfSteps)) return mem[numberOfSteps];
            int stepCombinations = RunUpstairsRecursiveWithMem(numberOfSteps - 3)
                                   + RunUpstairsRecursiveWithMem(numberOfSteps - 2)
                                   + RunUpstairsRecursiveWithMem(numberOfSteps - 1);
            mem[numberOfSteps] = stepCombinations;
            return stepCombinations;
        }

        public int RunUpstairsIterative(int numberOfSteps)
        {
            int[] arr = new int[numberOfSteps + 1];
            arr[0] = 1;
            arr[1] = 2;
            arr[2] = 4;

            for (int i = 3; i < numberOfSteps; i++)
            {
                arr[i] = arr[i - 1] + arr[i - 2] + arr[i - 3];
            }

            return arr[numberOfSteps - 1];
        }

        public string ChopString(string s, int startIndex, int len)
        {
            string s1 = s.Substring(0, startIndex);
            string s2 = s.Substring(startIndex + len);
            return s1 + s2;
        }

        public int GetStartIndex(string s, string word)
        {
            for (int i = 0; i < s.Length; i++)
            {
                int j = 0;
                while (j < word.Length && i < s.Length && s[i] == word[j])
                {
                    j++;
                    i++;
                }

                if (j == word.Length)
                {
                    return i - j;
                }
            }

            return -1;
        }
    }
}
