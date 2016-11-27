using System.Drawing;

namespace Blync
{
	public interface IColoredLight : ILight
	{
		void TurnOn(Color color);
	}
}
