namespace ControlFlow.Core.UI.Components.SVG;

public class SvgElement
{
    public string? Type { get; set; }
    public Dictionary<string, string> Attributes { get; set; }

    public SvgElement()
    {
        Attributes = new Dictionary<string, string>();
    }
}
