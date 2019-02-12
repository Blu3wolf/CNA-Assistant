using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CNA_Assistant
{
	public partial class Game
	{
		internal class StoresExpenditureStage : TurnState
		{
			internal StoresExpenditureStage(Game game) : base(game)
			{
				game.hungryHexes = new List<HungryHex>();
			}

			protected override void Entry()
			{
				if (game.SideIs == Side.Commonwealth && game.GameTurn < 47)
				{
					game.Evaporate(Evaporation.Flimsies);
				}
				else
				{
					game.Evaporate(Evaporation.Jerrycans);
				}
				// try to consume stores for Prisoners

				List<int> Hexes = new List<int>();
				foreach (Unit unit in game.Units)
				{
					if (!Hexes.Contains(unit.Location))
					{
						Hexes.Add(unit.Location);
					}
				}
				// hexes that are oases automatically have enough stores!
				foreach (int location in Hexes)
				{
					game.hungryHexes.Add(new HungryHex(game, location));
				}
				Next();

				// hexes with PresentStores == HalfRationsRequired will need to go on half rations
				// hexes with PresentStores > HalfRationsRequired will need some or all units to go on half rations
				// hexes with PresentStores < HalfRationsRequired will need all units to go on half rations, and some or all units to go hungry

			}

			internal override void Execute(Command command)
			{

			}

			private void RemoveFedHexes()
			{
				foreach (HungryHex hex in game.HungryHexes.ToList())
				{
					if (hex.IsFed)
					{
						game.hungryHexes.Remove(hex);
					}
				}
			}

			internal override void Next()
			{
				RemoveFedHexes();
				if (game.hungryHexes.Count == 0)
				{
					game.OpStage = 1;
					game.TurnState = new InitiativeDeclarationPhase(game);
				}
			}
		}
	}
}
