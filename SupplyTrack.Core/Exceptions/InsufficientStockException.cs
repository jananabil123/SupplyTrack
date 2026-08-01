using System;

namespace SupplyTrack.Core.Exceptions
{
    public class InsufficientStockException : Exception
    {
        public InsufficientStockException(string message)
            : base(message)
        {
        }
    }
}