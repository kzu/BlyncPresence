using System.Drawing;

namespace Blync
{
    public interface ILight
    {
        bool IsDimmed { get; set; }

        bool IsOn { get; }

        void Flash(byte speed);

        void TurnOn();

        void TurnOff();
    }
}