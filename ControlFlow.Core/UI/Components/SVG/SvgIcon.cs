namespace ControlFlow.Core.UI.Components.SVG;

public class SvgIcon
{
    public int Width { get; set; }
    public int Height { get; set; }
    public string? ViewBox { get; set; }
    public string Fill { get; set; }
    public string Stroke { get; set; }
    public double StrokeWidth { get; set; }
    public List<SvgElement> Elements { get; set; }

    // Constructor to make initialization easier
    public SvgIcon()
    {
        Elements = new List<SvgElement>();
        Fill = "none";  // Common default
        Stroke = "currentColor";  // Common default
        StrokeWidth = 2;  // Common default
    }
}
