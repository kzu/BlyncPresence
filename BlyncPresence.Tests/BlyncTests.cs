using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace BlyncPresence.Tests
{
    public class BlyncTests
    {
        [Fact]
        public void when_inspecting_then_can_find_device()
        {
            var controller = new Blynclight.BlynclightController();
            var result = controller.InitBlyncDevices();

            controller.TurnOnBlueLight(0);
        }
    }
}
