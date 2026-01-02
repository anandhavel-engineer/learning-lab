namespace LeetCode.Problems.Easy
{
    /// <summary>
    /// Provides multiple implementations for solving the Two Sum problem.
    /// </summary>
    public class TwoSum_Easy
    {
        public TwoSum_Easy()
        {
            int[] nums1 = { 2, 7, 11, 15 };
            int target1 = 9;
            int[] result1 = RunWithStrategy(new TwoSum_Easy.HashMapTwoSumStrategy(), nums1, target1);

            int[] nums2 = { 3, 2, 4 };
            int target2 = 6;
            int[] result2 = RunWithStrategy(new TwoSum_Easy.BruteForceTwoSumStrategy(), nums2, target2);

            int[] result3 = TwoSumBruteForce(new int[] { 3, 3 }, 6);
        }

        /// <summary>
        /// Brute-force O(n²) solution — for comparison only.
        /// </summary>
        public int[] TwoSumBruteForce(int[] nums, int target)
        {
            for (int i = 0; i < nums.Length; i++)
            {
                for (int j = i + 1; j < nums.Length; j++)
                {
                    if (nums[i] + nums[j] == target)
                    {
                        return new int[] { i, j };
                    }
                }
            }
            return Array.Empty<int>();
        }

        /// <summary>
        /// Optimized solution using HashMap (Dictionary) — O(n) time.
        /// </summary>
        public int[] TwoSumHashMap(int[] nums, int target)
        {
            Dictionary<int, int> map = new();

            for (int i = 0; i < nums.Length; i++)
            {
                int complement = target - nums[i];
                if (map.TryGetValue(complement, out int index))
                {
                    return new int[] { index, i };
                }
                if (!map.ContainsKey(nums[i]))
                {
                    map[nums[i]] = i; // Avoid overwriting earlier index
                }
            }

            return Array.Empty<int>();
        }

        /// <summary>
        /// Strategy pattern for extensibility — plug and play different strategies.
        /// </summary>
        public interface ITwoSumStrategy
        {
            int[] FindTwoSum(int[] nums, int target);
        }

        public class HashMapTwoSumStrategy : ITwoSumStrategy
        {
            public int[] FindTwoSum(int[] nums, int target)
            {
                Dictionary<int, int> map = new();

                for (int i = 0; i < nums.Length; i++)
                {
                    int complement = target - nums[i];
                    if (map.TryGetValue(complement, out int index))
                    {
                        return new int[] { index, i };
                    }
                    if (!map.ContainsKey(nums[i]))
                    {
                        map[nums[i]] = i;
                    }
                }

                return Array.Empty<int>();
            }
        }

        public class BruteForceTwoSumStrategy : ITwoSumStrategy
        {
            public int[] FindTwoSum(int[] nums, int target)
            {
                for (int i = 0; i < nums.Length; i++)
                {
                    for (int j = i + 1; j < nums.Length; j++)
                    {
                        if (nums[i] + nums[j] == target)
                        {
                            return new int[] { i, j };
                        }
                    }
                }
                return Array.Empty<int>();
            }
        }

        /// <summary>
        /// A flexible runner that takes a strategy as input.
        /// </summary>
        public int[] RunWithStrategy(ITwoSumStrategy strategy, int[] nums, int target)
        {
            return strategy.FindTwoSum(nums, target);
        }
    }
}
