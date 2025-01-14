using ControlFlow.Core.UI.Components.SVG;

namespace ControlFlow.Core;

public static class SvgLibrary
{
    public static readonly SvgIcon Plus = new SvgIcon
    {
        Width = 18,
        Height = 18,
        ViewBox = "0 0 24 24",
        Fill = "none",
        Stroke = "currentColor",
        StrokeWidth = 2,
        Elements = new List<SvgElement>
        {
            new SvgElement
            {
                Type = "path",
                Attributes = new Dictionary<string, string>
                {
                    { "d", "M12 20v-8m0 0V4m0 8h8m-8 0H4" }
                }
            }
        }
    };
    public static readonly SvgIcon InProgress = new SvgIcon
    {
        Width = 18,
        Height = 18,
        ViewBox = "0 0 24 24",
        Fill = "none",
        Stroke = "currentColor",
        StrokeWidth = 2,
        Elements = new List<SvgElement>
        {
            new SvgElement
            {
                Type = "path",
                Attributes = new Dictionary<string, string>
                {
                    { "d", "M20 12a8 8 0 0 1-8 8m8-8a8 8 0 0 0-8-8m8 8h2-2z" }
                }
            },
            new SvgElement
            {
                Type = "path",
                Attributes = new Dictionary<string, string>
                {
                    { "d", "M4 12a8 8 0 0 0 8 8m-8-8a8 8 0 0 1 8-8m-8 8h2-2z" }
                }
            }
        }
    };
    public static readonly SvgIcon Checkmark = new SvgIcon
    {
        Width = 18,
        Height = 18,
        ViewBox = "0 0 24 24",
        Fill = "none",
        Stroke = "currentColor",
        StrokeWidth = 2,
        Elements = new List<SvgElement>
        {
            new SvgElement
            {
                Type = "path",
                Attributes = new Dictionary<string, string>
                {
                    { "d", "M20 6L9 17l-5-5" }
                }
            }
        }
    };

    public static readonly SvgIcon Calendar = new SvgIcon
    {
        Width = 12,
        Height = 12,
        ViewBox = "0 0 24 24",
        Fill = "none",
        Stroke = "currentColor",
        StrokeWidth = 2,
        Elements = new List<SvgElement>
        {
            new SvgElement
            {
                Type = "rect",
                Attributes = new Dictionary<string, string>
                {
                    { "x", "3" },
                    { "y", "4" },
                    { "width", "18" },
                    { "height", "18" },
                    { "rx", "2" },
                    { "ry", "2" }
                }
            },
            new SvgElement
            {
                Type = "line",
                Attributes = new Dictionary<string, string>
                {
                    { "x1", "16" },
                    { "y1", "2" },
                    { "x2", "16" },
                    { "y2", "6" }
                }
            },
            new SvgElement
            {
                Type = "line",
                Attributes = new Dictionary<string, string>
                {
                    { "x1", "8" },
                    { "y1", "2" },
                    { "x2", "8" },
                    { "y2", "6" }
                }
            },
            new SvgElement
            {
                Type = "line",
                Attributes = new Dictionary<string, string>
                {
                    { "x1", "3" },
                    { "y1", "10" },
                    { "x2", "21" },
                    { "y2", "10" }
                }
            }
        }
    };

    public static string RenderSvgElement(SvgElement element)
    {
        var attributes = string.Join(" ",
            element.Attributes.Select(a => $"{a.Key}=\"{a.Value}\""));
        return $"<{element.Type} {attributes} />";
    }
}
