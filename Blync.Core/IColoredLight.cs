using System.Drawing;

namespace Blync
{
	public interface IColoredLight : ILight
	{
        Color Color { get; set; }

        void Flash(byte speed, Color color);

        void TurnOn(Color color);
	}
}
