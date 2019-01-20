using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CNA_Assistant
{
	class SupplyPoint
	{
		public int CapabilityPointAllowance;

		private int capabilityPointsExpended;

		public SupplyType Type;

		public int CapabilityPointsExpended { get => capabilityPointsExpended; }

		internal void EndTurn()
		{
			capabilityPointsExpended = 0;
		}

		internal void ExpendCapabilityPoints(int cp)
		{
			capabilityPointsExpended -= cp;
		}
	}

	enum SupplyType
	{
		Ammo, Fuel, Water, Stores
	}
}
