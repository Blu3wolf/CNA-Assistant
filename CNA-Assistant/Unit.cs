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

		public UnitCharacteristics UnitCharacteristics { get; }

		public int Location { get; private set; }

        public string ShortDesignation { get; }

        public string Designation { get; }

        public int StackingPoints;

		public int CapabilityPointsExpended	{ get; private set;	}
		public int CapabilityPointsAllowance
		{
			get
			{
				// CPA is either: CPA of its UnitCharacteristics; CPA of its attached TOE Gun or Tank points (if lower, and only if UnitCharacteristics does not force its CPA); 
				// or CPA of an attached unit (if that is lower); or CPA of the Trucks the unit is riding in (if Inf., and Motorised). 

				int CPA = UnitCharacteristics.CapabilityPointAllowance;

				if (UnitCharacteristics.TOESlowsUnit)
				{
					if (true) // if attached TOE Strength Points (Gun or Tank) have lower CPA
					{
						// then CPA = attached TOE Strength Point lowest CPA
					}
				}

				if (true) // if attached unit has lower CPA
				{
					// then CPA = that attached units lower CPA
				}

				return CPA;
			}
		}
		public int CohesionLevel { get; private set; }

		// methods

		public bool CanMove() // or attack, or defend...
		{
			if (CohesionLevel > -26) // check if in valid movement phase? Movement (and Reaction), and Retreat Before Assault. 
			{
				return true;
			}
			return false;
		}

		public void MoveTo(int location, int cpa) // should only happen for Movement (and Reaction), and Retreat Before Assault. 
		{
			if (CanMove())
			{
				Location = location;
				CapabilityPointsExpended += cpa;
			}
			

		}

		abstract public bool AttachTo(); // attach to the specified unit

		public void ExpendCapabilityPoints(int points)
		{
			CapabilityPointsExpended += points;
			if (CapabilityPointsExpended > CapabilityPointsAllowance)
			{
				int diff = CapabilityPointsExpended - CapabilityPointsAllowance;
				if (diff >= points)
				{
					CohesionLevel -= points;
				}
				else
				{
					CohesionLevel -= diff;
				}
			}
		}

		public void Surrender()
		{
			// really just need to delete the unit - perhaps return information about what TOE points it contained.

			// This probably is going to call a method of Game, say Game.Surrender(Unit unit) which would remove this Unit from the list of Units the user owns. So Game.Surrender(this) basically.
		}

		public void Victory() // called when enemy is pushed out of hex due to combat (not due to reaction, retreat before assault, etc).
		{
			CohesionLevel += 3;
		}

		internal void Defeat() // called internally by assault class. If unit takes 30% losses from that assault, or if the unit is forced to retreat due to not assigning any points to defend against an assault.
		{
			CohesionLevel -= 3;
		}

		private void Rest() // recover up to 5 cohesion if negative and you didnt work this turn - and not undergoing training!
		{
			if (CapabilityPointsExpended == 0)
			{
				if (CohesionLevel < 0)
				{
					CohesionLevel += 5;
					if (CohesionLevel > 0)
					{
						CohesionLevel = 0;
					}
				}
			}
		}

		internal void EndTurn() // called by Game, I guess. At end of turn. 
		{
			Rest();

			CapabilityPointsExpended = 0; 
		}
    }
}
