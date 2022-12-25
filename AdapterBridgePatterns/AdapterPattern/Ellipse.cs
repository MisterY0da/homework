namespace AdapterPattern
{
    public class Ellipse
    {
        public double BigSemiAxis { get; private set; }
        public double SmallSemiAxis { get; private set; }
        private double Perimeter { get; set; }
        private double Area { get; set; }

        public Ellipse(double bigSemiAxis, double smallSemiAxis)
        {
            BigSemiAxis = bigSemiAxis;
            SmallSemiAxis = smallSemiAxis;
        }

        public void Calculate()
        {
            Perimeter = Math.Round(4 * (3.14 * BigSemiAxis * SmallSemiAxis + (BigSemiAxis - SmallSemiAxis)) / (BigSemiAxis + SmallSemiAxis));
            Area = Math.Round(3.14 * BigSemiAxis * SmallSemiAxis);
        }

        public void Apply()
        {
            Console.WriteLine("I'm an ellips! " + " with area:" + Area + " and perimeter:" + Perimeter);
        }
    }
}
