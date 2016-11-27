using System;
using System.Reactive;
using System.Threading;

namespace Blync
{
	/// <summary>
	/// Turns on the light as long as pings keep coming.
	/// </summary>
	public class Presence : IDisposable
	{
		ILight light;
		int keepAlive;
		Timer timer;
		IDisposable subscription;

		public Presence(ILight light, IObservable<Unit> ping, int keepAlive)
		{
			this.light = light;
			this.keepAlive = keepAlive;

			timer = new Timer(_ => light.TurnOff());
			subscription = ping.Subscribe(_ =>
			{
				this.light.TurnOn();
				timer.Change(TimeSpan.FromMilliseconds(this.keepAlive), TimeSpan.FromMilliseconds(-1));
			});
		}

		public void Dispose()
		{
			subscription.Dispose();
		}
	}
}
