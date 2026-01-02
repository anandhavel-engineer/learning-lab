namespace LeetCode.Problems.Easy
{
    public class RepeatedNTimes
    {
        // ============================
        // 1. YOUR ORIGINAL METHOD
        // ============================
        public int RepeatedNTimes_2DArray(int[] nums)
        {
            int n = nums.Length / 2;
            int[,] unique = new int[2, n + 1];

            foreach (int num in nums)
            {
                for (int i = 0; i <= n; i++)
                {
                    if (unique[0, i] == num)
                    {
                        unique[1, i]++;
                        break;
                    }
                    else if (unique[1, i] == 0)
                    {
                        unique[0, i] = num;
                        unique[1, i]++;
                        break;
                    }
                }
            }

            for (int i = 0; i <= n; i++)
            {
                if (unique[1, i] == n)
                {
                    return unique[0, i];
                }
            }

            return -1;
        }

        // ============================
        // 2. HASHMAP / DICTIONARY
        // ============================
        public int RepeatedNTimes_Dictionary(int[] nums)
        {
            int n = nums.Length / 2;
            var freq = new Dictionary<int, int>();

            foreach (int num in nums)
            {
                if (!freq.ContainsKey(num))
                    freq[num] = 0;

                freq[num]++;

                if (freq[num] == n)
                    return num;
            }

            return -1;
        }

        // ============================
        // 3. HASHSET (EARLY EXIT)
        // ============================
        public int RepeatedNTimes_HashSet(int[] nums)
        {
            var seen = new HashSet<int>();

            foreach (int num in nums)
            {
                if (!seen.Add(num))
                {
                    return num; // repeated found
                }
            }

            return -1;
        }

        // ============================
        // 4. SORTING BASED
        // ============================
        public int RepeatedNTimes_Sorting(int[] nums)
        {
            Array.Sort(nums);

            for (int i = 1; i < nums.Length; i++)
            {
                if (nums[i] == nums[i - 1])
                    return nums[i];
            }

            return -1;
        }

        // ============================
        // 5. PAIR CHECKING (SMART TRICK)
        // ============================
        public int RepeatedNTimes_PairCheck(int[] nums)
        {
            for (int i = 2; i < nums.Length; i++)
            {
                if (nums[i] == nums[i - 1] || nums[i] == nums[i - 2])
                {
                    return nums[i];
                }
            }

            return nums[0]; // fallback
        }
    }
}

//| Method | Time | Space | Interview Quality |
//| ------------- | ---------- | ----- | ----------------- |
//| Your 2D array | O(n²) | O(n) | ❌                 |
//| Dictionary | O(n) | O(n) | ✅                 |
//| HashSet | O(n) | O(n) | ⭐⭐⭐               |
//| Sorting | O(n log n) | O(1) | ⚠️                |
//| Pair Check | O(n) | O(1) | ⭐⭐⭐⭐              |
