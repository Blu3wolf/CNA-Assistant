using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CNA_Assistant
{
	abstract class Unit : ICapabilityPoints
	{
		public UnitCharacteristics UnitCharacteristics { get; }

		public int Location { get; private set; }

		public string ShortDesignation { get; }

		public string Designation { get; }

		public bool IsShell { get; }

		public int CapabilityPointsExpended { get; private set; }

		public int CapabilityPointsAllowance
		{
			get
			{
				// CPA is either: CPA of its UnitCharacteristics; CPA of its attached TOE Gun or Tank points (if lower, and only if UnitCharacteristics does not force its CPA); 
				// or CPA of an attached unit (if that is lower); or CPA of the Trucks the unit is riding in (if Inf., and Motorised). 

				int CPA = UnitCharacteristics.CapabilityPointAllowance;

				if (true) // all infantry TOE points are motorized by Trucks
				{
					// then CPA = attached Truck's lowest CPA (possibly higher than base Unit Characteristic CPA)
				}

				if (UnitCharacteristics.TOESlowsUnit)
				{
					if (true) // if attached TOE Strength Points (Gun or Tank) have lower CPA
					{
						// then CPA = attached TOE Strength Point lowest CPA
					}
				}

				if (true) // if attached child Unit has lower CPA
				{
					// then CPA = that attached units lower CPA
				}

				if (true) // if attached Trucks have lower CPA
				{
					// then CPA = that attached Truck's lower CPA
				}

				return CPA;
			}
		}
		public int CohesionLevel { get; private set; }

		public int BreakdownPoints { get; private set; }

		public int StackingPoints {	get; }

		public Unit AttachedTo { get; private set; }

		public List<Unit> AssignedUnits { get; private set; }

		public List<Unit> AttachedUnits { get; private set; }

		public int Stores { get; private set; }

		public int Ammo { get; private set; }

		public int Fuel { get; private set; }

		public int Water { get; private set; }

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

			CheckBreakdown();
		}

		public void CheckBreakdown() // happens after movement I guess. Requires player input to resolve, though.
		{

		}

		public void Assign() // assigns this unit to the unit it is attached to, if able
		{

		}

		private void ExpendCapabilityPoints(int points)
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

		internal void EndOpsStage() // Should happen in response to an event at end of operations stage (both sides).
		{
			Rest();

			CapabilityPointsExpended = 0;

			BreakdownPoints = 0;
		}

		public bool CanAttach(Unit unit) // returns true if able to attach the specified unit to this unit
		{
			// This is a complicated chapter. 

			/* So you can attach a unit if there is space in the formation organisation chart, or if there is not space in the org chart, then if there is space in the max attachment chart.
			 * The problem there is that there are lots and lots of different formation org charts for different types of units. So its not going to be that simple a function to figure out
			 * whether or not a given unit can be attached. 
			 * 
			 * The immediate solution is to only track whether or not a unit can be attached over and above existing assignments - 
			 * i.e., assume for now that units being attached to have already got a full org chart
			 * and could not therefore accept any further assignments.
			 * 
			 * For now, just return true - implement later.
			 */

			return true;
		}

		public void Attach(Unit unit)
		{
			if (CanAttach(unit))
			{
				AttachedUnits.Add(unit);

				if (AssignedUnits.Contains(unit)) // then both units expend 1 CP
				{

				}
				else // both units expend 2 CP, and it must currently be a Reorganisation segment
				{

				}
			}
		}

		public void Detach(Unit unit)
		{
			if (IsAttached(unit))
			{
				AttachedUnits.Remove(unit);

				if (AssignedUnits.Contains(unit)) // then both units expend 1 CP
				{

				}
				else // both units expend 2 CP, and it must currently be a Reorganisation segment or movement segment
				{

				}
			}
		}

		public bool IsAttached(Unit unit)
		{
			if (AttachedUnits.Contains(unit))
			{
				return true;
			}
			return false;
		}

		public void Assign(Unit unit)
		{
			throw new NotImplementedException();
		}

		public bool IsAssignable(Unit unit)
		{
			throw new NotImplementedException();
		}

		internal void Evaporate(Game.Evaporation evaporation)
		{
			int fuelEvaporate = Fuel;
			int waterEvaporate = Water;

			switch (evaporation)
			{
				case Game.Evaporation.Flimsies:
					{
						fuelEvaporate *= 9;
						waterEvaporate *= 9;
					}
					break;
				case Game.Evaporation.Jerrycans:
					{
						fuelEvaporate *= 6;
						waterEvaporate *= 6;
					}
					break;
				case Game.Evaporation.HotWeather:
					{
						fuelEvaporate *= 5;
						waterEvaporate *= 5;
					}
					break;
				default: throw new ArgumentException("evaporation case not handled in Unit.Evaporate()");
			}

			fuelEvaporate /= 100;
			waterEvaporate /= 100;

			Fuel -= fuelEvaporate;
			Water -= waterEvaporate;
		}
	}
}
