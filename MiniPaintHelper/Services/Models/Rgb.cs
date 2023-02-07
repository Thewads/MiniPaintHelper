namespace Services.Models;

public class Rgb
{
    public int R { get; set; }
    public int G { get; set; }
    public int B { get; set; }

    public bool IsValid()
    {
        return R is >= 0 and <= 255 && G is >= 0 and <= 255 && B is >= 0 and <= 255;
    }
}