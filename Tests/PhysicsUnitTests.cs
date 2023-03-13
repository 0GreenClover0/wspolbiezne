namespace Tests
{
    [TestClass]
    public class PhysicsUnitTests
    {
        [TestMethod]
        public void TestGravitationalForceCalculation()
        {
            double expectedForce = 0.000417144d;
            double actualForce = Wspolbiezne.PhysicsMath.CalculateGravitationalForce(1000d, 25000d, 2d);
            double delta = 0.00000001d;

            Assert.AreEqual(expectedForce, actualForce, delta);

            Console.WriteLine("Testing if " + actualForce + " ~= " + expectedForce + " with max delta of: " + delta);
        }
    }
}