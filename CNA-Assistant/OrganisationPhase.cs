using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CNA_Assistant
{
	public partial class Game
	{
		partial class OrganisationPhase : TurnState
		{
			internal OrganisationPhase(Game game) : base(game)
			{
				WaterDistribution = false;
				Reorganisation = false;
				Attrition = false;
				//Construction = false;
				//Training = false;
				SupplyDistribution = false;
				TacticalShipping = false;
			}

			protected override void Entry()
			{
				// generate decisions
				throw new NotImplementedException();
			}

			internal override void Execute(Command command)
			{
				// handle commands relevant to organisation phase
				throw new NotImplementedException();
			}

			private OrganisationSegment CurrentOrganisationSegment;

			private bool WaterDistribution;

			private bool Reorganisation;

			private bool Attrition;

			//private bool Construction;

			//private bool Training;

			private bool SupplyDistribution;

			private bool TacticalShipping;

			internal override void Next()
			{
				if (WaterDistribution && Reorganisation && Attrition && /*Construction && Training &&*/ SupplyDistribution && TacticalShipping)
				{
					game.TurnState = new NavalConvoyArrivalPhase(game);
				}
				
			}
		}
	}
	
}
