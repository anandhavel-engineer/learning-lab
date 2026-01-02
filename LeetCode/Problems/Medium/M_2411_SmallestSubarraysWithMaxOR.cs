namespace LeetCode.Problems.Medium
{
    /*
Example 1:

Input: nums = [1, 0, 2, 1, 3]
Output: [3, 3, 2, 2, 1]
Explanation:
The maximum possible bitwise OR starting at any index is 3. 
- Starting at index 0, the shortest subarray that yields it is [1, 0, 2].
- Starting at index 1, the shortest subarray that yields the maximum bitwise OR is [0, 2, 1].
- Starting at index 2, the shortest subarray that yields the maximum bitwise OR is [2, 1].
- Starting at index 3, the shortest subarray that yields the maximum bitwise OR is [1, 3].
- Starting at index 4, the shortest subarray that yields the maximum bitwise OR is [3].
Therefore, we return [3, 3, 2, 2, 1]. 
Example 2:

Input: nums = [1, 2]
Output: [2, 1]
Explanation:
Starting at index 0, the shortest subarray that yields the maximum bitwise OR is of length 2.
Starting at index 1, the shortest subarray that yields the maximum bitwise OR is of length 1.
Therefore, we return [2, 1].
*/
    public class M_2411_SmallestSubarraysWithMaxOR
    {
        public M_2411_SmallestSubarraysWithMaxOR()
        {
            int[] nums3 = { 1, 0, 2, 1, 3 };
            int[] nums4 = { 1, 2 };
            int[] nums5 = { 4, 0, 5, 6, 3, 2 };
            var n = SmallestSubarraysBruteForce(nums3); //[3,3,2,2,1]
            Console.WriteLine();
            Console.WriteLine(string.Join(", ", n));

            Console.WriteLine();
            Console.WriteLine("Second Array");
            var m = SmallestSubarraysOptimized(nums4); //[2,1]
            Console.WriteLine(string.Join(", ", m));

            Console.WriteLine();
            Console.WriteLine("Third Array");
            int[] l = SmallestSubarraysOptimized(nums5); //[4,3,2,2,1,1]
            Console.WriteLine(string.Join(", ", l));
        }

        /// <summary>
        /// Original approach – O(n²), preserved for reference.
        /// </summary>
        public int[] SmallestSubarraysBruteForce(int[] nums)
        {
            int[] answer = new int[nums.Length];

            for (int i = 0; i < nums.Length; i++)
            {
                int maxOR = 0;
                for (int k = i; k < nums.Length; k++)
                {
                    maxOR |= nums[k];
                }

                int currentOR = 0, length = 0;
                for (int j = i; j < nums.Length; j++)
                {
                    length++;
                    currentOR |= nums[j];
                    if (currentOR == maxOR)
                    {
                        answer[i] = length;
                        break;
                    }
                }
            }

            return answer;
        }

        /// <summary>
        /// Optimized solution – O(n * 32), using bit tracking from right to left.
        /// </summary>
        public int[] SmallestSubarraysOptimized(int[] nums)
        {
            int n = nums.Length;
            int[] answer = new int[n];
            int[] lastSeen = new int[32]; // Each bit's last index (up to 32 bits for ints)

            for (int i = 0; i < 32; i++)
            {
                lastSeen[i] = -1; // Initialize to -1
            }

            for (int i = n - 1; i >= 0; i--)
            {
                for (int b = 0; b < 32; b++)
                {
                    if (((nums[i] >> b) & 1) == 1)
                    {
                        lastSeen[b] = i;
                    }
                }

                Console.WriteLine(String.Join(", ", lastSeen));

                int maxReach = i;
                for (int b = 0; b < 32; b++)
                {
                    if (lastSeen[b] != -1)
                    {
                        maxReach = Math.Max(maxReach, lastSeen[b]);
                    }
                }
                Console.WriteLine("maxReach : " + maxReach + ", i = " + i + ", answer : " + (maxReach - i + 1));
                answer[i] = maxReach - i + 1;
            }

            return answer;
        }

    }
}
