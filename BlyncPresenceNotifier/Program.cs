using System;
using System.Net.Mqtt;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BlyncClient
{
	class Program
	{
		ManualResetEventSlim signal = new ManualResetEventSlim();
		IMqttClient client;

		static void Main(string[] args)
		{
			Console.WriteLine("Type [Enter] to connect");
			Console.ReadLine();

			new Program().Run();
		}

		void Run()
		{
			Task.Run(Setup).ContinueWith(async t => await Loop());

			Console.ReadLine();
			signal.Set();
			if (client != null)
				client.Dispose();
		}

		async Task Setup()
		{
			client = await MqttClient.CreateAsync("127.0.0.0", new MqttConfiguration());
			await client.ConnectAsync(new MqttClientCredentials("blync"));

			Console.WriteLine($"Connected as {client.Id}");
			await client.SubscribeAsync("#", MqttQualityOfService.AtMostOnce);

			client.MessageStream.Subscribe(msg =>
				Console.WriteLine($"{msg.Topic}:{Encoding.UTF8.GetString(msg.Payload)}"));
		}

		async Task Loop()
		{
			// Take screenshots, call webservice, send status
			await Task.Delay(0);
		}
	}
}
