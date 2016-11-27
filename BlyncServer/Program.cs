using System;
using System.Reactive.Linq;
using System.Net.Mqtt;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BlyncServer
{
    class Program
    {
        ManualResetEventSlim signal = new ManualResetEventSlim();
        IMqttClient client;
        IMqttServer server;

        static void Main(string[] args)
        {
            new Program().Run();
        }

        public Program()
        {
            server = MqttServer.Create(new MqttConfiguration());

            Console.WriteLine("Starting Server...");

            server.Stopped += (sender, e) => Console.WriteLine("Server stopped. Finishing app...");
            server.ClientConnected += (sender, e) => Console.WriteLine($"New client connected: {e}");
            server.ClientDisconnected += (sender, e) => Console.WriteLine($"Client disconnected: {e}");

            server.Start();

            Console.WriteLine("Server started...");
            Console.WriteLine("Listening for new clients...");
			Console.WriteLine("Type [topic]:[payload] to publish or [Enter] to quit.");

		}

		void Run()
        {
			Task.Run(Loop);
            signal.Wait();
			server.Dispose();
		}

		async Task Loop()
        {
            client = await server.CreateClientAsync();

			await client.SubscribeAsync("#", MqttQualityOfService.AtMostOnce);

			// Trace everything we receive
			client.MessageStream.Subscribe(msg =>
				Console.WriteLine($"{msg.Topic}:{Encoding.UTF8.GetString(msg.Payload)}"));

			// Publish whatever is entered in the console
			string line;
            while ((line = Console.ReadLine()) != "")
            {
                var parts = line.Split(new[] { ':' }, StringSplitOptions.RemoveEmptyEntries);
                var topic = parts[0].Trim();
				var payload = parts.Length > 1 ?
					Encoding.UTF8.GetBytes(parts[1].Trim()) :
					new byte[0];

				await client.PublishAsync(new MqttApplicationMessage(topic, payload), MqttQualityOfService.AtMostOnce);
            }

            signal.Set();
        }
    }
}