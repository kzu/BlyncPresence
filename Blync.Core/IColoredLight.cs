using System.Drawing;

namespace Blync
{
	public interface IColoredLight : ILight
	{
        void Flash(byte speed, Color color);

        void TurnOn(Color color);
	}
}
