using System;

namespace ShoppingCart.UnitTests
{
    public class ManualClock : IClock
    {
        public ManualClock(DateTime now)
        {
            Now = now;
        }

        public DateTime Now { get; }
    }
}