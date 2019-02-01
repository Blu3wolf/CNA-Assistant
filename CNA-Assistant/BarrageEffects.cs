using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CNA_Assistant
{
	class BarrageEffects
	{
		public BarrageEffects(int TOED)
		{
			TOEDestroyed = TOED;
		}

		public int TOEDestroyed { get; }
	}
}
