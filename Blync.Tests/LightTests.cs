using System;
using System.Drawing;
using System.Linq;
using System.Net.Mqtt;
using System.Reactive;
using System.Reactive.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace Blync.Tests
{
	public class LightTests
	{
		ITestOutputHelper output;

		public LightTests(ITestOutputHelper output)
		{
			this.output = output;
		}

		[Fact]
		public void it_can_have_fixed_color()
		{
			var light = Light.Create(Color.Blue);

			light.TurnOn();
			Thread.Sleep(200);
			light.TurnOff();
		}

		[InlineData(KnownColor.Red)]
		[InlineData(KnownColor.DeepPink)]
		[InlineData(KnownColor.BlueViolet)]
		[InlineData(KnownColor.SpringGreen)]
		[InlineData(KnownColor.DarkGreen)]
		[Theory]
		public void it_can_use_known_colors(KnownColor color)
		{
			var light = Light.Create();

			light.TurnOn(color);

			Thread.Sleep(500);

			light.TurnOff();
			
			Thread.Sleep(500);
		}

		[Fact]
		public void when_no_light_connected_then_turning_on_off_does_not_fail()
		{
			var light = Light.Create(9);

			light.TurnOn(Color.Blue);
			light.TurnOff();
		}

		[Fact]
		public void it_can_turn_on_twice()
		{
			var light = Light.Create();

			light.TurnOn(Color.Blue);
			Thread.Sleep(200);
			light.TurnOn(Color.Blue);
			Thread.Sleep(200);
			light.TurnOff();
		}
	}
}