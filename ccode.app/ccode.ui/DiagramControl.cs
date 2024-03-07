using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;
using DynamicData;
using Newtonsoft.Json;

namespace ccode.ui;

public class Node
{
    public int X { get; set; }
    public int Y { get; set; }
    public string Id { get; set; }
    public string Title { get; set; }
}

public class DiagramControl : Grid
{
    private List<Node> nodes;
    public List<Node> Nodes
    {
        get { return nodes;}
        set { nodes = value;
            OnNodesChanged();
        }
    }

    public DiagramControl()
    {
        var jsonString = @"[
  { ""x"" : 14, ""y"" : 20, ""id"" : ""42342-42342-42424-4266"", ""title"":  ""wezel 1"" },
  { ""x"" : 100, ""y"" : 200, ""id"" : ""42342-53453-42424-35353"", ""title"":  ""wezel 2"" }
]";

        List<Node> nodes = JsonConvert.DeserializeObject<List<Node>>(jsonString);

        Nodes = new List<Node>(nodes);
    }

    private void OnNodesChanged()
    {
        foreach (var node in Nodes)
        {
            var textBlock = new TextBlock
            {
                Text = node.Title,
                Foreground = Brushes.White,
                FontFamily = new FontFamily("Arial"),
                FontSize = 12,
                TextAlignment = TextAlignment.Center,
                Margin = new Thickness(node.X, node.Y, 0, 0),
                Width = 100,
                Height = 50,
                HorizontalAlignment = Avalonia.Layout.HorizontalAlignment.Left,
                VerticalAlignment = Avalonia.Layout.VerticalAlignment.Top
            };

            this.Children.Add(textBlock);
        }
    }
}
