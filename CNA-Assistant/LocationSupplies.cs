using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CNA_Assistant
{
	public class LocationSupplies
	{
		public LocationSupplies(int location, int ammo, int stores, int fuel, int water)
		{
			Location = location;
			Ammo = ammo;
			Stores = stores;
			Fuel = fuel;
			Water = water;
		}

		// properties

		public int Location { get; }

		public int Ammo { get; private set; }

		public int Stores { get; private set; }

		public int Fuel { get; private set; }

		public int Water { get; private set; }

		// methods

		internal void WithdrawAmmo(int supply)
		{
			if (Ammo >= supply)
			{
				Ammo -= supply;
			}
			else
			{
				throw new ArgumentOutOfRangeException("supply", "Insufficient Ammo available");
			}
		}

		internal void WithdrawStores(int supply)
		{
			if (Stores >= supply)
			{
				Stores -= supply;
			}
			else
			{
				throw new ArgumentOutOfRangeException("supply", "Insufficient Stores available");
			}
		}

		internal void WithdrawFuel(int supply)
		{
			if (Fuel >= supply)
			{
				Fuel -= supply;
			}
			else
			{
				throw new ArgumentOutOfRangeException("supply", "Insufficient Fuel Available");
			}
		}

		internal void WithdrawWater(int supply)
		{
			if (Water >= supply)
			{
				Water -= supply;
			}
			else
			{
				throw new ArgumentOutOfRangeException("supply", "Insufficient Water Available");
			}
		}

		internal void DepositAmmo(int supply)
		{
			Ammo += supply;
		}

		internal void DepositStores(int supply)
		{
			Stores += supply;
		}

		internal void DepositFuel(int supply)
		{
			Fuel += supply;
		}

		internal void DepositWater(int supply)
		{
			Water += supply;
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
				default: throw new ArgumentException("evaporation case not handled in SupplyDump.Evaporate()");
			}

			fuelEvaporate /= 100;
			waterEvaporate /= 100;

			Fuel -= fuelEvaporate;
			Water -= waterEvaporate;
		}
	}
}
