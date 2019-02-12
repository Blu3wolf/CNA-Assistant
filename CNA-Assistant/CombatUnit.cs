﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CNA_Assistant
{
	public class CombatUnit : Unit
	{
		// properties

		public UnitCharacteristics UnitCharacteristics { get; }

		public int StackingPoints { get; }

		public string ShortDesignation { get; }

		public string Designation { get; }

		public bool IsShell { get; }

		public bool IsArmor
		{
			get
			{
				foreach (TOEStrengthPoint toe in TOEStrengthPoints)
				{
					if (toe.Vulnerability == 0)
					{
						return true;
					}
				}
				return false;
			}
		}

		public bool HasBarrageRating
		{
			get
			{
				if (UnitCharacteristics.BarrageRating != 0)
				{
					return true;
				}
				foreach (TOEStrengthPoint toe in TOEStrengthPoints)
				{
					if (toe.BarrageRating != 0)
					{
						return true;
					}
				}
				return false;
			}
		}

		public override bool IsMotorised { get; }

		public bool IsPinned { get; private set; }

		public List<BarrageEffects> BarrageEffects { get; }

		public Unit AttachedTo { get; private set; }

		public List<Unit> AssignedUnits { get; private set; }

		public List<Unit> AttachedUnits { get; private set; }

		public int CohesionLevel { get; private set; }

		public override int CapabilityPointsAllowance
		{
			get
			{
				// CPA is either: CPA of its UnitCharacteristics; CPA of its attached TOE Gun or Tank points (if lower, and only if UnitCharacteristics does not force its CPA); 
				// or CPA of an attached unit (if that is lower); or CPA of the Trucks the unit is riding in (if Inf., and Motorised). 

				int CPA = UnitCharacteristics.CapabilityPointAllowance;

				if (IsMotorised) // all infantry TOE points are motorized by Trucks
				{
					// then CPA = attached Truck's lowest CPA (possibly higher than base Unit Characteristic CPA)
					// at most this is 25 (light trucks carrying infantry)
					CPA = 25;
				}
				else if (UnitCharacteristics.TOESlowsUnit)
				{
					foreach (TOEStrengthPoint point in TOEStrengthPoints)
					{
						if (point.CapabilityPointAllowance < CPA)
						{
							CPA = point.CapabilityPointAllowance;
						}
					}
				}

				if (true) // if attached child Unit has lower CPA
				{
					// then CPA = that attached units lower CPA
				}

				if (true) // if attached Trucks have lower CPA
				{
					// then CPA = that attached Truck's lower CPA
					// trucks CPA depends on its cargo - highest for light trucks carrying inf, less for medium and heavy trucks, less if motorising guns
				}

				return CPA;
			}
		}


		

		public bool OutstandingDecisions { get => throw new NotImplementedException(); } // returns true if the Unit requires decisions to be made before moving onto next step, for instance barrage outstanding

		// methods

		override public bool CanMove() // or attack, or defend...
		{
			if (CohesionLevel > -26) // check if in valid movement phase? Movement (and Reaction), and Retreat Before Assault. 
			{
				return true;
			}
			return false;
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

		public bool IsAttached(Unit unit)
		{
			if (AttachedUnits.Contains(unit))
			{
				return true;
			}
			return false;
		}

		public void Assign() // assigns this unit to the unit it is attached to, if able
		{

		}

		public void Barrage(int TOEpts) // unit barraged, takes TOEpts damage. Resolved at end of barrage step. 
		{
			BarrageEffects.Add(new BarrageEffects(TOEpts));
		}

		public int BarrageOutstandingTOEs()
		{
			int togo = 0;
			foreach (BarrageEffects effect in BarrageEffects)
			{
				togo += effect.TOEDestroyed;
			}
			return togo;
		}

		public void ResolveBarrage() // attempt to automatically resolve the barrage effects
		{
			// only works if there is only one type of TOE strength point in the unit, OR if there are more TOE strength points destroyed than the unit has remaining. 

		}

		public void ResolveBarrage(TOEStrengthPoint strengthPoint) // destroy the given (tank or gun) strengthPoint to resolve barrage effects
		{
			
			if (TOEStrengthPoints.Contains(strengthPoint) && BarrageOutstandingTOEs() > 0)
			{
				strengthPoints.Remove(strengthPoint);
				IsPinned = true;

				BarrageEffects first = BarrageEffects.First();
				BarrageEffects.Remove(first);
				if (first.TOEDestroyed > 1)
				{
					BarrageEffects.Add(new BarrageEffects(first.TOEDestroyed - 1));
				}
			}
		}

		public void ResolveBarrage(int infantrypts) // destroy the given number of infantry TOE pts to resolve barrage effects
		{

			if (InfantryTOE > 0 && BarrageOutstandingTOEs() >= infantrypts)
			{
				InfantryTOE -= infantrypts;
			}
			IsPinned = true;

			int removed = 0;
			while (removed < infantrypts)
			{
				BarrageEffects first = BarrageEffects.First();
				BarrageEffects.Remove(first);
				removed += first.TOEDestroyed;
			}
			if (removed > infantrypts)
			{
				BarrageEffects.Add(new BarrageEffects(removed - infantrypts));
			}
		}


		public void Victory() // called when enemy is pushed out of hex due to combat (not due to reaction, retreat before assault, etc).
		{
			CohesionLevel += 3;
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

		public void Assign(Unit unit)
		{
			throw new NotImplementedException();
		}

		public bool IsAssignable(Unit unit)
		{
			throw new NotImplementedException();
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

		internal void Defeat() // called internally by assault class. If unit takes 30% losses from that assault, or if the unit is forced to retreat due to not assigning any points to defend against an assault.
		{
			CohesionLevel -= 3;
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
	}
}
