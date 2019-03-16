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
			abstract class OrganisationSegment : TurnState
			{
				public OrganisationSegment(Game game, OrganisationPhase orgPhase) : base(game)
				{
					OrganisationPhase = orgPhase;
				}

				protected OrganisationPhase OrganisationPhase { get; }
			}
		}
	}

	
	
}
