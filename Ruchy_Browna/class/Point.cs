namespace Ruchy_Browna
{
    public class Point
    {
        public double X { get; set; }
        public double Y { get; set; }
        // jeśli nie podamy nic do konstruktora klasy domyślnie otrzymamy punkt [0,0]
        public Point(double x = 0, double y = 0)
        {
            X = x;
            Y = y;
        }
    }
}
