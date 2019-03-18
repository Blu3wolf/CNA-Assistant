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
				attrition = false;
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

				switch (command.CommandType)
				{
					case Command.Type.SelectPhase:
						if (command.Params[0] is OrganisationSegmentType)
						{
							switch (command.Params[0])
							{
								case OrganisationSegmentType.WaterDistribution:
									if (!WaterDistribution)
									{
										WaterDistribution = true;
										game.TurnState = new WaterDistributionSegment(game, this);
									}
									break;
								case OrganisationSegmentType.Reorganisation:
									if (!Reorganisation)
									{
										Reorganisation = true;
										game.TurnState = new ReorganisationSegment(game, this);
									}
									break;
								case OrganisationSegmentType.Attrition:
									// there are no options as to what happens here - no decisions. So this is effectively a method, not a Segment. 
									if (!attrition)
									{
										Attrition();
										attrition = true;
									}
									break;
								case OrganisationSegmentType.SupplyDistribution:
									if (!SupplyDistribution)
									{
										SupplyDistribution = true;
										game.TurnState = new SupplyDistributionSegment(game, this);
									}
									break;
								case OrganisationSegmentType.TacticalShipping:
									if (!TacticalShipping)
									{
										TacticalShipping = true;
										game.TurnState = new TacticalShippingSegment(game, this);
									}
									break;
								default:
									break;
							}
						}
						break;
					default:
						break;
				}
				throw new NotImplementedException();
			}

			private bool WaterDistribution;

			private bool Reorganisation;

			private bool attrition;

			//private bool Construction;

			//private bool Training;

			private bool SupplyDistribution;

			private bool TacticalShipping;

			private void Attrition()
			{

			}

			internal override void Next()
			{
				if (WaterDistribution && Reorganisation && attrition && /*Construction && Training &&*/ SupplyDistribution && TacticalShipping)
				{
					game.TurnState = new NavalConvoyArrivalPhase(game);
				}
				
			}
		}

		public enum OrganisationSegmentType
		{
			WaterDistribution,
			Reorganisation,
			Attrition,
			SupplyDistribution,
			TacticalShipping
		}
	}
	
}
