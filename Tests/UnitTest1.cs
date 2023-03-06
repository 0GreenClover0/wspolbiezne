namespace Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            Console.WriteLine("Testing 1 == 1");
            Assert.AreEqual(1, 1);
        }
    }
}