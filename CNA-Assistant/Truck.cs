using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CNA_Assistant
{
	class Truck
	{
		public SupplyType Cargo { get; private set; }

		public int SupplyPoints { get; private set; }

		public int CapabilityPointAllowance { get; } // guess that depends on whether first line or second/third line, and whether Light Medium or Heavy

		public TOEStrengthPointType MotorCargo { get; private set; } // this will be the alternative to carrying supplies, carrying TOE Strength Points of a Unit instead of them walking.


	}
}
