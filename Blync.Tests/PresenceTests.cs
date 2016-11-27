using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Subjects;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Moq;
using Xunit;

namespace Blync.Tests
{
	public class PresenceTests
	{
		[Fact]
		public async Task it_turns_lights_on_and_off()
		{
			var subject = new Subject<Unit>();
			var light = new Mock<ILight>();
			var presence = new Presence(light.Object, subject, 500);

			await Task.Run(async () =>
			{
				subject.OnNext(Unit.Default);

				await Task.Delay(200);

				subject.OnNext(Unit.Default);

				await Task.Delay(200);

				subject.OnNext(Unit.Default);

				await Task.Delay(600);

				subject.OnNext(Unit.Default);

				await Task.Delay(200);

				subject.OnNext(Unit.Default);

				await Task.Delay(600);
			});

			light.Verify(x => x.TurnOn(), Times.Exactly(5));
			light.Verify(x => x.TurnOff(), Times.Exactly(2));
		}
	}
}
