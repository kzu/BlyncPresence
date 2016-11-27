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

			public void TurnOn() => TurnOn(color);

			public void TurnOn(Color color) => controller.TurnOnRGBLights(deviceIndex, color.R, color.G, color.B);

			public void TurnOff() => controller.ResetLight(deviceIndex);
		}
	}
}
