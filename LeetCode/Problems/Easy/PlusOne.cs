namespace LeetCode.Problems.Easy
{
    public class PlusOne
    {
        public PlusOne()
        {
            Console.WriteLine("Hello, World!");

            var nums1 = new int[] { 1, 2, 3 };
            var nums2 = new int[] { 4, 3, 2, 2 };
            var nums3 = new int[] { 9, 9 };

            var s1 = PlusOnemethod(nums1);
            var s2 = PlusOnemethod(nums2);
            var s3 = PlusOnemethod(nums3);

            Console.WriteLine(string.Join(", ", s1));
            Console.WriteLine(string.Join(", ", s2));
            Console.WriteLine(string.Join(", ", s3));
        }

        public int[] PlusOnemethod(int[] digits)
        {
            for (int i = digits.Length - 1; i >= 0; i--)
            {
                digits[i]++;
                if (digits[i] < 10)
                {
                    return digits;
                }
                digits[i] = 0;
            }

            int[] result = new int[digits.Length + 1];
            result[0] = 1;
            return result;
        }
    }
}
