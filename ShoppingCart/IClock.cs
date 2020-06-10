#nullable enable

using System;

namespace ShoppingCart
{
    public interface IClock
    {
        DateTime Now { get; }
    }
}
