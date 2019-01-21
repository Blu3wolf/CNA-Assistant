using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CNA_Assistant
{
	class UnitCharacteristics // immutable, I think. every unit with the same characteristics holds a reference to the same UnitCharacteristics object. 
	{
		public int CapabilityPointAllowance { get; }

		public bool TOESlowsUnit { get; }

		public int AntiAirRating { get; }

		public int BarrageRating { get; }

		public int AntiArmorRating { get; }

		public int Vulnerability { get; }

		public int ArmorProtection { get; }

		public int CloseAssaultOffence { get; }

		public int CloseAssaultDefence { get; }

		public int MaxInfantryTOE { get; }

		public int MaxTankTOE { get; }

		public int MaxArtyTOE { get; }

		public int MaxAntiTankTOE { get; }

		public int MaxLightAntiAirTOE { get; }

		public int MaxHeavyAntiAirTOE { get; }
	}
}
