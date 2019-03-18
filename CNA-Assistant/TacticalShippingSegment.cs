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
			private class TacticalShippingSegment : OrganisationSegment
			{
				public TacticalShippingSegment(Game game, OrganisationPhase organisationPhase) : base(game, organisationPhase)
				{

				}

				protected override void Entry()
				{
					throw new NotImplementedException();
				}

				internal override bool CanContinue()
				{
					throw new NotImplementedException();
				}

				internal override void Execute(Command command)
				{
					throw new NotImplementedException();
				}
			}
		}
	}
}
