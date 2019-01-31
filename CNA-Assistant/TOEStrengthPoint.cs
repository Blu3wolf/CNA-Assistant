using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CNA_Assistant
{
	class TOEStrengthPoint : CombatStats // technically, Tank and Gun points. Infantry points remain integer properties of CombatUnits. 
	{
		public int FuelRate { get; }

		public int BreakdownAdjustmentRating { get; }
	}
}
