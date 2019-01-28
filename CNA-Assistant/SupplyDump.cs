using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CNA_Assistant
{
	public class SupplyDump
	{
		public SupplyDump()
		{

		}

		// properties

		public int Location { get; }

		public int Ammo { get; private set; }

		public int Stores { get; private set; }

		public int Fuel { get; private set; }

		public int Water { get; private set; }

		// methods

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
