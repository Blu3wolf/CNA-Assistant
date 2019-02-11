using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CNA_Assistant
{
	internal class StoresExpenditureStage : TurnState
	{
		internal StoresExpenditureStage(Game game) : base(game)
		{
			HungryHexes = new List<HungryHex>();
		}

		protected override void Entry()
		{
			// handle evaporation/spillage of fuel and water

			// try to consume stores for Prisoners

			// build list of hexes that contain units
			List<int> Hexes = new List<int>();
			foreach (Unit unit in game.Units)
			{
				if (!Hexes.Contains(unit.Location))
				{
					Hexes.Add(unit.Location);
				}
			}
			// for each hex, check if enough stores are in that hex
			// hexes that are oases automatically have enough stores
			foreach (int location in Hexes)
			{
				HungryHexes.Add(new HungryHex(game, location));
			}
			foreach (HungryHex hex in HungryHexes.ToList())
			{
				if (hex.CanFeed)
				{
					hex.Feed();
					HungryHexes.Remove(hex);
				}
			}
			// if insufficient stores, generate a decision, else deduct required stores from that hex, starting with those units attached stores
			// each decision then represents a hex with not enough stores - some units will have to go hungry

			// hexes with PresentStores == HalfRationsRequired will need to go on half rations
			// hexes with PresentStores > HalfRationsRequired will need some or all units to go on half rations
			// hexes with PresentStores < HalfRationsRequired will need all units to go on half rations, and some or all units to go hungry

			throw new NotImplementedException();
		}

		internal override void Execute(Command command)
		{
			// handle valid Commands to resolve decisions
			throw new NotImplementedException();
		}

		internal override void Next()
		{
			if (HungryHexes.Count == 0)
			{
				game.OpStage = 1;
				game.TurnState = new InitiativeDeclarationPhase(game);
			}
		}

		private List<HungryHex> HungryHexes;

		public class HungryHex
		{
			public HungryHex(Game game, int location)
			{
				gameturn = game.GameTurn;
				Location = location;
				if (game.SupplyDumps.TryGetValue(Location, out SupplyDump supplyDump))
				{
					SupplyDump = supplyDump;
				}
				units = new List<Unit>();
				foreach (Unit unit in game.Units)
				{
					if (unit.Location == this.Location)
					{
						units.Add(unit);
						PresentStores += unit.Stores;
						unit.HalfRations(false);
					}
				}
				if (SupplyDump != null)
				{
					PresentStores += SupplyDump.Stores;
				}

			}

			public int Location { get; }

			public ReadOnlyCollection<Unit> Units { get => units.AsReadOnly(); }

			public int RequiredStores
			{
				get
				{
					int rations = 0;
					foreach (Unit unit in Units)
					{
						rations += unit.RequiredStores;
					}
					return rations;
				}
			}

			public int HalfRationsRequired
			{
				get
				{
					if (HalfRationsRequired == 0)
					{
						int rations = 0;
						foreach (Unit unit in Units)
						{
							unit.HalfRations(true);
							rations += unit.RequiredStores;
							unit.HalfRations(false);
						}
						HalfRationsRequired = rations;
					}
					return HalfRationsRequired;
				}
				private set
				{
					HalfRationsRequired = value;
				}
			}

			public int PresentStores { get; }

			public SupplyDump SupplyDump { get; }

			private List<Unit> units;

			private int gameturn;

			public bool CanFeed 
			{
				get => RequiredStores >= PresentStores;
			}

			public void Feed()
			{
				if (CanFeed)
				{
					foreach (Unit unit in Units)
					{
						if (unit.Stores >= unit.RequiredStores)
						{
							unit.ConsumeStores(gameturn);
						}
						else // another source (unit, DOGs, SupplyDump) needs to supply this unit
						{
							throw new NotImplementedException();
						}
					}
				}
			}

			public void Resolve()
			{

			}
		}
	}
}
