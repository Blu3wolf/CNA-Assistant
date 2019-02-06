using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CNA_Assistant
{
	class Barrage
	{
		public ReadOnlyCollection<CombatUnit> FiringUnits { get; }

		public int ActualBarragePoints { get; }
	}
}
