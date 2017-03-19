using System.Drawing;
using System;

namespace GuiLabs.Utils
{
	public static class Colors
	{
		public static Color Interpolate(Color sourceColor, Color destColor, double lambda)
		{
            float sr = sourceColor.R;
            float sg = sourceColor.G;
            float sb = sourceColor.B;

            float dr = destColor.R - sr;
            float dg = destColor.G - sg;
            float db = destColor.B - sb;

            int r = (int)(sr + lambda * dr);
            int g = (int)(sg + lambda * dg);
            int b = (int)(sb + lambda * db);

            if (r < 0) r = 0;
            if (g < 0) g = 0;
            if (b < 0) b = 0;

            if (r > 255) r = 255;
            if (g > 255) g = 255;
            if (b > 255) b = 255;

            return Color.FromArgb((int)r, g, b);
		}

        public static Color ScaleColor(System.Drawing.Color sourceColor, float scaleFactor)
        {
            float r = sourceColor.R * scaleFactor;
            float g = sourceColor.G * scaleFactor;
            float b = sourceColor.B * scaleFactor;

            if (r < 0) r = 0;
            if (g < 0) g = 0;
            if (b < 0) b = 0;

            if (r > 255) r = 255;
            if (g > 255) g = 255;
            if (b > 255) b = 255;

            return System.Drawing.Color.FromArgb((int)r, (int)g, (int)b);
        }

		public static int ScaleColor(int SourceColor, float ScaleFactor)
		{
			float r = R(SourceColor) * ScaleFactor;
			float g = G(SourceColor) * ScaleFactor;
			float b = B(SourceColor) * ScaleFactor;

			if (r < 0) r = 0;
			if (g < 0) g = 0;
			if (b < 0) b = 0;

			if (r > 255) r = 255;
			if (g > 255) g = 255;
			if (b > 255) b = 255;

			return RGB((int)r, (int)g, (int)b);
		}

		public static int R(int SourceColor)
		{
			return SourceColor & 0xFF;
		}

		public static int G(int SourceColor)
		{
			return (SourceColor & 0xFF00) >> 8;
		}

		public static int B(int SourceColor)
		{
			return (SourceColor & 0xFF0000) >> 16;
		}

		public static int RGB(int R, int G, int B)
		{
			return (B << 16) | (G << 8) | R;
		}

		public static Color GetRandom()
		{
			return Color.FromArgb(
				GetRandomComponent(), 
				GetRandomComponent(), 
				GetRandomComponent());
		}

		private static Random Rnd = new Random();

		public static int GetRandomComponent()
		{
			return Rnd.Next(256);
		}
	}
}