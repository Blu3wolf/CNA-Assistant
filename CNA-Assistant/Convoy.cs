using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CNA_Assistant
{
	class Convoy
	{
		public int Tonnage
		{
			get
			{
				return Ammo * 4 + Stores + (Fuel + 7) / 8;
			}
		}

		public int Stores { get; }

		public int Fuel { get; }

		public int Ammo { get; }


	}
}
