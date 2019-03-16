using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CNA_Assistant
{
	public partial class Game
	{
		partial class OrganisationPhase
		{
			private class WaterDistributionSegment : OrganisationSegment
			{
				public WaterDistributionSegment(Game game, OrganisationPhase orgPhase) : base(game, orgPhase)
				{

				}

				protected override void Entry()
				{
					// water is distributed to units
					OrganisationPhase.WaterDistribution = true;
				}

				internal override void Execute(Command command)
				{
					throw new NotImplementedException();
				}

				internal override void Next()
				{
					if (true)
					{

					}
				}
			}
		}
	}

}
