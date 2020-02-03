using System;
using System.Collections.Generic;
using System.Linq;

namespace LC4Sum
{
    class Program
    {
        static void Main(string[] args)
        {
            //target 4
            //-10, -1, 5, 10
            //-10, 1, 3, 10

            //-10, -5, -3, -1, 0, 1, 3, 5, 10

            int[] nums = new int[] { -3, -2, -1, 0, 0, 1, 2, 3 };
            int target = 0;

            var fourSums = FourSum(nums, target);

            foreach (var fourSum in fourSums)
            {
                foreach (var num in fourSum)
                {
                    Console.Write($"{num}, ");
                }
                Console.WriteLine(" SET");
            }
            
            IList<IList<int>> FourSum(int[] nums, int target)
            {
                var fourSums = new List<IList<int>>();

                Array.Sort(nums);

                for (int edgeLeft = 0; edgeLeft < nums.Length - 3; edgeLeft++)
                {
                    for (int innerLeft = edgeLeft + 1; innerLeft < nums.Length - 2; innerLeft++)
                    {
                        var pointerLeft = innerLeft + 1;
                        var pointerRight = nums.Length - 1;

                        while (pointerLeft < pointerRight)
                        {
                            var setSum = nums[edgeLeft] + nums[innerLeft] + nums[pointerLeft] + nums[pointerRight];

                            if (setSum == target)
                            {
                                fourSums.Add(new List<int>() { nums[edgeLeft], nums[innerLeft], nums[pointerLeft], nums[pointerRight] });

                                while (pointerLeft < pointerRight && nums[pointerLeft] == nums[pointerLeft + 1]) { pointerLeft++; }
                                while (pointerLeft < pointerRight && nums[pointerRight] == nums[pointerRight - 1]) { pointerRight--; }

                                pointerLeft++;
                                pointerRight--;
                            }
                            else if (setSum > target)
                            {
                                pointerRight--;
                            }
                            else
                            {
                                pointerLeft++;
                            }                           
                        }

                        while (innerLeft < nums.Length - 2 && nums[innerLeft] == nums[innerLeft + 1]) { innerLeft++; }
                    }

                    while (edgeLeft < nums.Length - 3 && nums[edgeLeft] == nums[edgeLeft + 1]) { edgeLeft++; }
                }

                return fourSums;
            }            
        }
    }
}
