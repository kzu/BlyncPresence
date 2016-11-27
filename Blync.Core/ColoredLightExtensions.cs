using System.Drawing;

namespace Blync
{
	public static class ColoredLightExtensions
	{
		public static void TurnOn(this IColoredLight light, string color) => 
			light.TurnOn((Color)new ColorConverter().ConvertFromString(color));

		public static void TurnOn(this IColoredLight light, KnownColor color) => light.TurnOn(color.ToString());
	}
}