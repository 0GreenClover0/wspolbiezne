namespace Wspolbiezne.Data
{
     public record class CollisionData(
         string Time,
         string Ball1Position,
         string Ball1Velocity,
         float Ball1Mass,
         float Ball1Radius,
         string Ball2Position,
         string Ball2Velocity,
         float Ball2Mass,
         float Ball2Radius
     );
}
