using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CNA_Assistant
{
    class Unit
    {
		public Unit()
		{

		}

		public UnitCharacteristics UnitCharacteristics;

		public int Location;

        public string ShortDesignation;

        public string Designation;

        public int StackingPoints;

		private int capabilityPointsAllowance;

		private int capabilityPointsExpended;

		private int cohesionLevel;

		public int CapabilityPointsExpended
		{
			get => capabilityPointsExpended;
			set
			{
				capabilityPointsExpended = value;
				if (capabilityPointsExpended > capabilityPointsAllowance)
				{
					int diff = capabilityPointsExpended - capabilityPointsAllowance;
					cohesionLevel -= diff;
				}
			}
		}
		public int CapabilityPointsAllowance { get => capabilityPointsAllowance; }
		public int CohesionLevel { get => cohesionLevel; set => cohesionLevel = value; }

		// methods

		public void MoveTo(int location, int cpa) // should only happen for Movement (and Reaction), and Retreat Before Assault. 
		{
			if (cohesionLevel > -26)
			{
				Location = location;
				CapabilityPointsExpended += cpa;
			}
			

		}

		public void Surrender()
		{
			// really just need to delete the unit - perhaps return information about what TOE points it contained.

			// This probably is going to call a method of Game, say Game.Surrender(Unit unit) which would remove this Unit from the list of Units the user owns. So Game.Surrender(this) basically.
		}

		public void Victory()
		{
			cohesionLevel += 3;
		}

		private void Rest() // recover 5 cohesion if negative and you didnt work this turn - and not undergoing training!
		{
			if (capabilityPointsExpended == 0)
			{
				if (cohesionLevel < 0)
				{
					cohesionLevel += 5;
					if (cohesionLevel > 0)
					{
						cohesionLevel = 0;
					}
				}
			}
		}

		internal void EndTurn()
		{
			Rest();

			capabilityPointsExpended = 0; 
		}
    }
}
