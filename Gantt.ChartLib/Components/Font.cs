using System.Linq;
using System.Windows;
using System.Windows.Media;

namespace Gantt.ChartLib.Components
{
    public class Font
    {
        public FontFamily Family { get; set; }
        public float Size { get; set; } = 11;
        public FontWeight Weight { get; set; }
        public FontStyle Style { get; set; }
        public FontStretch Stretch { get; set; }
        public SolidColorBrush Foreground { get; set; }

        public Font()
        {
            Family = Fonts.SystemFontFamilies.FirstOrDefault(x => x.Source == "Segoe UI");
            Weight = FontWeights.Normal;
            Style = FontStyles.Normal;
            Stretch = FontStretches.Normal;
        }

        public Typeface GetTypeface()
        {
            return new Typeface(Family, Style, Weight, Stretch);
        }

        public static Font Normal(Color color)
        {
            var foreground = new SolidColorBrush(color);
            foreground.Freeze();
            return new Font()
            {
                Weight = FontWeights.Normal,
                Foreground = foreground
            };
        }

        public static Font Weighted(Color color)
        {
            var foreground = new SolidColorBrush(color);
            foreground.Freeze();
            return new Font()
            {
                Weight = FontWeights.Medium,
                Foreground = foreground
            };
        }
    }
}
