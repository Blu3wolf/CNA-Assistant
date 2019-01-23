using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CNA_Assistant
{
	public interface ICapabilityPoints
	{
		int CapabilityPointsAllowance { get; }

		int CapabilityPointsExpended { get; }

		void ExpendCapabilityPoints(int points);
	}
}
