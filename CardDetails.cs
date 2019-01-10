using System;
using System.Collections.Generic;
using System.Text;

namespace VirtualCashCard.ServiceProviders
{
    public class CardDetails
    {
        public long CardNumber { get; set; }
        public int CardPin { get; set; }
        public double Balance { get; set; }
    }
}
