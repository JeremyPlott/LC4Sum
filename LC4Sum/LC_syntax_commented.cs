public class Solution
{
	public IList<IList<int>> FourSum(int[] nums, int target)
	{

		//all of our solutions will be stored in this collection and returned at the end
		var fourSums = new List<IList<int>>();

		//sorting the numbers is very important for this logic to work.
		//it lets us skip over repeated values, and use the double pointers to search efficiently
		Array.Sort(nums);

		//this is our slowly iterating value that I'm going to call an anchor. I am calling it that for
		//purposes of explanation because it only moves after the double pointers have done their search,
		//and the next for loop has finished its full cycle.
		//it is going to the array length - 3 because we are considering four indexes, and this one
		//is always going to be the leftmost index.

		for (int edgeLeft = 0; edgeLeft < nums.Length - 3; edgeLeft++)
		{
			//this is the same thing for the second leftmost anchor. We want to increase it after the
			//double pointers have finished their search, and then increase the outer anchor after this one 
			//has finished its loop. That way we get every combination.
			//it runs to length - 2 since this will be the furthest index it can consider without overlap.

			for (int innerLeft = edgeLeft + 1; innerLeft < nums.Length - 2; innerLeft++)
			{
				//here are the two pointers we will use to home in on the value we want.
				//one starts to the left, one to the right. This way we can ++/-- them efficiently
				//instead of iterating through every combination

				var pointerLeft = innerLeft + 1;
				var pointerRight = nums.Length - 1;

				//continue doing this until the two pointers would cross each other.
				while (pointerLeft < pointerRight)
				{
					//this is just a clean way to track the sum of the current set.
					//maybe I have too many variable names using 'left'... :)
					var setSum = nums[edgeLeft] + nums[innerLeft] + nums[pointerLeft] + nums[pointerRight];

					//when we find a set that matches the value we were asked to find...
					if (setSum == target)
					{
						//add it to the solution list
						fourSums.Add(new List<int>() { nums[edgeLeft], nums[innerLeft], nums[pointerLeft], nums[pointerRight] });

						//these two while loops will skip over duplicate solutions.
						//++/-- the right or left side as long as they are not crossing, and the next value
						//is the same as the current value. This means it will stop right before a new value
						while (pointerLeft < pointerRight && nums[pointerLeft] == nums[pointerLeft + 1]) { pointerLeft++; }
						while (pointerLeft < pointerRight && nums[pointerRight] == nums[pointerRight - 1]) { pointerRight--; }

						//they are stopped before new values now and both have to move to new values
						//to be able to find another potential solution, so we can iterate both sides.
						pointerLeft++;
						pointerRight--;
					}
					//if the sum of our four numbers is larger than our target number, we want the sum
					//to become smaller. To get a smaller number, we have to move the right pointer
					//towards the center. Sorting the list lets us use this logic.
					else if (setSum > target)
					{
						pointerRight--;
					}
					//if we didn't find a matching set, and the sum needs to get larger
					else
					{
						pointerLeft++;
					}
				}

				//the double pointers have finished their work finding all possible sets for this combination
				//of outer left and inner-left anchors. Now we want to move our inner anchor up
				//to the next new value. This lets us skip over all repeats.
				while (innerLeft < nums.Length - 2 && nums[innerLeft] == nums[innerLeft + 1]) { innerLeft++; }
			}

			//our for loop for the inner-anchor has fully cycled now, so we can do the same thing
			//to the outer anchor to skip previously used values.
			while (edgeLeft < nums.Length - 3 && nums[edgeLeft] == nums[edgeLeft + 1]) { edgeLeft++; }
		}

		//the for loop for the outer anchor is finished, so we have all unique solutions now
		return fourSums;
	}
}