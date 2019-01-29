using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CNA_Assistant
{
	abstract class Unit : ICapabilityPoints
	{
		public int Location { get; private set; }

		public int CapabilityPointsExpended { get; protected set; }

		abstract public int CapabilityPointsAllowance { get; }

		abstract public bool IsMotorised { get; }

		public int BreakdownPoints { get; protected set; }

		public int LightTruckBreakdownPoints { get; protected set; }

		public int Stores { get; private set; }

		public int Ammo { get; private set; }

		public int Fuel { get; private set; }

		public int Water { get; private set; }

		public int LightTrucks { get; private set; }

		public int MediumTrucks { get; private set; }

		public int HeavyTrucks { get; private set; }

		// methods

		abstract public bool CanMove(); // depends whether the Unit is a TruckConvoy or a CombatUnit (or a Collection of ReplacementPoints?)

		public void MoveTo(int location, int cpa, int breakdownpts, int lightbreakdownpts) // would be great if we could pass in a Stack<Hex>, and from that determine CapabilityPoints and BreakdownPoints dynamically.
		{
			if (CanMove())
			{
				Location = location;
				CapabilityPointsExpended += cpa;
			}

			CheckBreakdown(breakdownpts); // easy to do except not for Light Trucks, which need to track their own set of Breakdown Points (different from every other vehicle because why not). 
		}

		protected void CheckLightBreakdown()
		{

		}

		protected void CheckBreakdown(int breakdownpts) // happens after movement I guess. Requires player input to resolve, though.
		{
			// checks every attached truck and every Tank TOE point for breakdown. Not all units have TOE points though...
		}

		internal void EndOpsStage() // Should happen in response to an event at end of operations stage.
		{
			CapabilityPointsExpended = 0;
			BreakdownPoints = 0;
		}

		internal void Evaporate(Game.Evaporation evaporation) // override in CombatUnit?
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
				case Game.Evaporation.HotWeather: // do not count fuel and water in the tanks and radiators
					{
						// not all units have TOEStrengthPoints to get a Fuel Capacity from. 
						// all units may have attached Trucks (CombatUnits have 1st line trucks, TruckConvoys have 2nd/3rd line trucks)

						// each vehicle TOE point has 1 Water in the radiator - but not all units have TOE points. 
						// each Truck point has 1 water in the radiator

						fuelEvaporate -= fuelintanks;
						waterEvaporate -= waterinradiators;

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
