namespace Services.Models;

public class Paint
{
    public string Brand { get; set; } = "";
    public string Name { get; set; } = "";
    
    public Rgb Rgb { get; set; } = new();
    public Lab Lab { get; set; } = new();
}