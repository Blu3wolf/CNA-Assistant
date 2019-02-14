using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CNA_Assistant
{
	public class Schedule
	{
		public Schedule(Convoy[] convoys)
		{
			this.convoys = convoys;
		}



		private readonly Convoy[] convoys;

		public int Tonnage
		{
			get
			{
				int tonnage = 0;
				foreach (Convoy convoy in convoys)
				{
					tonnage += convoy.Tonnage;
				}
				return tonnage;
			}
		}

		public Convoy GetConvoy(ShippingLanes shippingLane)
		{
			return convoys[(int)shippingLane];
		}

		public Schedule SetConvoy(Convoy convoy, ShippingLanes lane)
		{
			Convoy[] newConvoys = convoys;
			newConvoys[(int)lane] = convoy;
			return new Schedule(newConvoys);
		}
	}
}
