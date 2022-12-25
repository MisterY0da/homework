namespace AdapterPattern
{
    public class EllipseAdapter : IShape
    {
        Ellipse Ellipse { get; set; }
        public EllipseAdapter(Ellipse ellipse)
        {
            Ellipse = ellipse;
        }
        public void Draw()
        {
            Ellipse.Calculate();
            Ellipse.Apply();
        }
    }
}
