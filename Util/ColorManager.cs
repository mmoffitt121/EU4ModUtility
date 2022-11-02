using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using EU4ModUtil.Models.Data.Map;

namespace EU4ModUtil.Util
{
    internal static class ColorManager
    {
        public static (int, int) seaHRange = (170, 210);
        public static (double, double) seaSRange = (0.5, 1);
        public static (double, double) seaLRange = (0.25, 0.75);
        public static (int, int) landHRange = (210, 510);
        public static (double, double) landSRange = (0.5, 1);
        public static (double, double) landLRange = (0.25, 0.75);
        public enum ColorMode
        {
            Land,
            Sea
        }
        public static (int, int, int) GetUniqueProvinceColor(this List<Province> provinces, ColorMode cMode = ColorMode.Land)
        {
            switch (cMode)
            {
                case ColorMode.Land:
                    return provinces.RandomLandColor();
                case ColorMode.Sea:
                    return provinces.RandomSeaColor();
                default:
                    return provinces.RandomLandColor();
            }
            
        }

        private static (int, int, int) RandomSeaColor(this List<Province> provinces)
        {
            Random rnd = new Random();

            int r;
            int g;
            int b;

            (r, g, b) = GetRandomColorFromHSLRange(seaHRange, seaSRange, seaLRange);

            while (provinces.Where(p => p.R == r && p.G == g && p.B == b).Count() > 0 && !(r == 0 && g == 0 && b == 0))
            {
                (r, g, b) = GetRandomColorFromHSLRange(seaHRange, seaSRange, seaLRange);
            }

            return (r, g, b);
        }

        private static (int, int, int) RandomLandColor(this List<Province> provinces)
        {
            Random rnd = new Random();

            int r;
            int g;
            int b;

            (r, g, b) = GetRandomColorFromHSLRange(landHRange, landSRange, landLRange);

            while (provinces.Where(p => p.R == r && p.G == g && p.B == b).Count() > 0 && !(r == 0 && g == 0 && b == 0))
            {
                (r, g, b) = GetRandomColorFromHSLRange(landHRange, landSRange, landLRange);
            }

            return (r, g, b);
        }

        private static (int, int, int) GetRandomColorFromHSLRange((int, int) h, (double, double) s, (double, double) l)
        {
            Random rand = new Random();

            int hue = rand.Next(h.Item1, h.Item2) % 360;
            
            double saturation = rand.NextDouble() * Math.Abs(s.Item1 - s.Item2) + Math.Min(s.Item1, s.Item2);
            double lightness = rand.NextDouble() * Math.Abs(l.Item1 - l.Item2) + Math.Min(l.Item1, l.Item2);

            return FromHSL(hue, saturation, lightness);
        }

        private static (int, int, int) FromHSL(int h, double s, double l)
        {
            int r = 0;
            int g = 0;
            int b = 0;

            if (s == 0)
            {
                r = (int)(l * 255);
                g = (int)(l * 255);
                b = (int)(l * 255);
                return (r, g, b);
            }

            double v1, v2;
            double hue = ((double)h) / 360;

            v2 = (l < 0.5) ? (l * (1 + s)) : (l + s) - (l * s);
            v1 = 2 * l - v2;

            r = (int)(255 * HueToRGB(v1, v2, hue + (1.0 / 3)));
            g = (int)(255 * HueToRGB(v1, v2, hue));
            b = (int)(255 * HueToRGB(v1, v2, hue - (1.0 / 3)));

            return (r, g, b);
        }

        public static double HueToRGB(double v1, double v2, double vh)
        {
            if (vh < 0)
                vh += 1;

            if (vh > 1)
                vh -= 1;

            if ((6 * vh) < 1)
                return (v1 + (v2 - v1) * 6 * vh);

            if ((2 * vh) < 1)
                return v2;

            if ((3 * vh) < 2)
                return (v1 + (v2 - v1) * ((2.0f / 3) - vh) * 6);

            return v1;
        }
    }
}
