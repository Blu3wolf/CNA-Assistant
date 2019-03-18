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

				List<int> Hexes = new List<int>();
				foreach (Unit unit in game.Units)
				{
					if (!Hexes.Contains(unit.Location))
					{
						Hexes.Add(unit.Location);
					}
				}

				// Guards consume 2 stores points each (automatically, from nearest source). 
				// Prisoners consume 3 stores points per 5 prisoners (or fraction thereof)(automatically, from nearest source). 

				foreach (int location in Hexes)
				{
					game.hungryHexes.Add(new HungryHex(game, location));
				}

				Next();
				// if able skip straight to next phase. Else, there is a list of hungryhexes to resolve
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
