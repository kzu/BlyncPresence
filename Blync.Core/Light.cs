using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blynclight;

namespace Blync
{
	public class Light
	{
		public static IColoredLight Default => Create(0);

		/// <summary>
		/// Creates a light that can be turned on with arbitrary 
		/// colors.
		/// </summary>
		public static IColoredLight Create(int deviceId = 0)
		{
			return new LightImpl(deviceId);
		}

		/// <summary>
		/// Creates a light that has a fixed color and can only 
		/// be turned on or off.
		/// </summary>
		public static ILight Create(Color color, int deviceId = 0)
		{
			return new LightImpl(color, deviceId);
		}

		class LightImpl : IColoredLight
		{
			Color color;
            bool isOn;
            bool isDimmed;
			int deviceIndex;
			BlynclightController controller;

			public LightImpl(int deviceIndex)
				: this(Color.White, deviceIndex)
			{
			}

			public LightImpl(Color color, int deviceIndex)
			{
				this.color = color;
				this.deviceIndex = deviceIndex;

				controller = new BlynclightController();
				controller.InitBlyncDevices();
			}

            public bool IsDimmed
            {
                get => isDimmed;
                set 
                {
                    if (value)
                        isDimmed = controller.SetLightDim(deviceIndex);
                    else
                        isDimmed = controller.ClearLightDim(deviceIndex);
                }
            }

            public bool IsOn => isOn;

            public void Flash(byte speed)
            {
                TurnOn(color);
                controller.SelectLightFlashSpeed(deviceIndex, speed);
                controller.StartLightFlash(deviceIndex);
            }

            public void Flash(byte speed, Color color)
            {
                TurnOn(color);
                Flash(speed);
            }

            public void TurnOn() => TurnOn(color);

            public void TurnOn(Color color)
            {
                this.color = color;
                controller.StopLightFlash(deviceIndex);
                isOn = controller.TurnOnRGBLights(deviceIndex, color.R, color.G, color.B);
            }

            public void TurnOff()
            {
                controller.ResetLight(deviceIndex);
                controller.StopLightFlash(deviceIndex);
                isOn = false;
            }
		}
	}
}
