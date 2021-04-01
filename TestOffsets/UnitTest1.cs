using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestOffsets
{
    [TestClass]
    public class UnitTest1
    {
        void TestOffsets(int num, int of20=0, int of21=0, int of22=0)
        {
            int[] offset1_arr;
            int[] offset2_arr;
            int max_threads = num;
            Lib1.TripletFind.MakeOffsets(max_threads, out offset1_arr, out offset2_arr);
            for (int i = 0; i < offset1_arr.Length; i++)
            {
                Assert.AreEqual(offset1_arr[i], i);
            }
            if (max_threads > 3)
            {
                Assert.AreEqual(offset2_arr[0], of20);
                Assert.AreEqual(offset2_arr[1], of21);
                Assert.AreEqual(offset2_arr[2], of22);

            }
        }
        [TestMethod]
        public void TestMethod1()
        {
            TestOffsets(10, 9, 6, 6);
            TestOffsets(2);
            TestOffsets(3);
            TestOffsets(4,3);
            TestOffsets(5,3,0,3);
            TestOffsets(6,3,3,3);
            TestOffsets(7,6,3,3);
            TestOffsets(8,6,3,6);
            TestOffsets(9,6,6,6);
            TestOffsets(11,9,6,9);
            TestOffsets(12,9,9,9);
        }
    }
}
