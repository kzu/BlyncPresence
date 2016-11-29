using Blynclight;
using System;
using System.Drawing;
using System.Linq;
using System.Net.Mqtt;
using System.Reactive;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace Blync.Tests
{
	public class BlyncTests
	{
		ITestOutputHelper output;

		public BlyncTests(ITestOutputHelper output)
		{
			this.output = output;
		}

        [Fact]
        public void when_dimming_then_dims()
        {
            var controller = new BlynclightController();
            controller.InitBlyncDevices();

            controller.Display(BlynclightController.Color.Blue);
            controller.TurnOnBlueLight(0);
            controller.SetLightDim(0);
            Thread.Sleep(1000);
            controller.SetLightDim(0);
            Thread.Sleep(1000);
            controller.SetLightDim(0);
            Thread.Sleep(1000);
            controller.ClearLightDim(0);
            Thread.Sleep(1000);
            controller.ResetLight(0);
        }

        //public BlyncTests() { }

        [Fact]
		public async Task when_connecting_then_succeeds()
		{
			var client = await MqttClient.CreateAsync("localhost", new MqttConfiguration());
			await client.ConnectAsync(new MqttClientCredentials(Guid.NewGuid().ToString("n")));

			await client.PublishAsync(new MqttApplicationMessage("foo", Encoding.UTF8.GetBytes("bar")), MqttQualityOfService.AtLeastOnce);

			//var signal = new ManualResetEventSlim();

			//await client.SubscribeAsync("#", MqttQualityOfService.AtMostOnce);

			//client.MessageStream.Subscribe(msg =>
			//{
			//	if (msg.Topic == "$SYS/stop")
			//		signal.Set();

			//	output.WriteLine($"{msg.Topic}:{Encoding.UTF8.GetString(msg.Payload)}");
			//});

			//signal.Wait();
		}
	}
}