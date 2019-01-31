using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CNA_Assistant
{
	class UnitCharacteristics : CombatStats // immutable, I think. every unit with the same characteristics holds a reference to the same UnitCharacteristics object. 
	{
		public bool TOESlowsUnit { get; }

		public int MaxInfantryTOE { get; }

		public int MaxTankTOE { get; }

		public int MaxArtyTOE { get; }

		public int MaxAntiTankTOE { get; }

		public int MaxLightAntiAirTOE { get; }

		public int MaxHeavyAntiAirTOE { get; }
	}
}
