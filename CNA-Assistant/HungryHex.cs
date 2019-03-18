using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CNA_Assistant
{
	public class HungryHex
	{
		public HungryHex(Game game, int location)
		{
			this.game = game;
			gameturn = game.GameTurn;
			Location = location;
			if (game.SupplyDumps.TryGetValue(Location, out LocationSupplies supplyDump))
			{
				SupplyDump = supplyDump;
				PresentStores = SupplyDump.Stores;
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

			if (HalfRationsRequired >= PresentStores && !game.Oases.Contains(this.Location))
			{
				HalfRations();
			}
			FeedHex();
			if (!IsFed)
			{
				starvingUnits = new List<Unit>();
			}
		}

		public Game game;

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

		public LocationSupplies SupplyDump { get; }

		private List<Unit> units;

		private List<Unit> starvingUnits;

		private int gameturn;

		public bool CanFeed
		{
			get => game.Oases.Contains(this.Location) || PresentStores >= RequiredStores;
		}

		public bool IsFed {	get; private set; }

		// methods

		public void FeedHex()
		{
			if (CanFeed && !IsFed)
			{
				foreach (Unit unit in Units)
				{
					FeedUnit(unit);
				}
				IsFed = true;
			}
		}

		private void FeedUnit(Unit unit)
		{
			if (unit.Stores >= unit.RequiredStores || game.Oases.Contains(this.Location))
			{
				unit.ConsumeOwnStores();
			}
			else // another source (another unit, DOGs, SupplyDump) needs to supply this unit
			{
				if (SupplyDump.Stores >= unit.RequiredStores - unit.Stores)
				{
					SupplyDump.WithdrawStores(unit.RequiredStores - unit.Stores);
					unit.ConsumeStores(unit.RequiredStores - unit.Stores);
				}

				if (unit.TurnStoresLastConsumed != gameturn) // still hungry!
				{
					// start looking...
					int dumpstores = SupplyDump.Stores;
					SupplyDump.WithdrawStores(dumpstores);
					unit.ConsumeStores(dumpstores);

					foreach (Unit otherUnit in Units)
					{
						int deficit = unit.RequiredStores - unit.Stores;
						if (otherUnit.Stores >= deficit)
						{
							otherUnit.SupplyStores(deficit);
							unit.ConsumeStores(deficit);
						}
						if (unit.TurnStoresLastConsumed == gameturn)
						{
							continue;
						}
					}

					if (unit.TurnStoresLastConsumed != gameturn) // still hungry...
					{
						foreach (Unit otherUnit in Units)
						{
							int xferstores = otherUnit.Stores;
							otherUnit.SupplyStores(xferstores);
							unit.ConsumeStores(xferstores);

							if (unit.TurnStoresLastConsumed == gameturn)
							{
								continue;
							}
						}
					}

					// possibly still hungry..?
				}
			}
		}

		private void HalfRations()
		{
			foreach (Unit unit in Units)
			{
				unit.HalfRations(true);
			}
		}

		public void HalfRations(Unit unit, bool halfrations)
		{
			if (!halfrations && PresentStores <= HalfRationsRequired)
			{
				return;
			}
			if (Units.Contains(unit))
			{
				unit.HalfRations(halfrations);
			}
		}

		public void NoRations(Unit unit, bool starve)
		{
			if (PresentStores < HalfRationsRequired)
			{
				if (starve)
				{
					if (!starvingUnits.Contains(unit))
					{
						starvingUnits.Add(unit);
					}
				}
				else
				{
					if(starvingUnits.Contains(unit))
					{
						starvingUnits.Remove(unit);
					}
				}
			}
		}

		public void Resolve() // refactor should be possible...
		{
			if (PresentStores < HalfRationsRequired)
			{
				IEnumerable<Unit> feedingUnits = from unit in Units
												 where !starvingUnits.Contains(unit)
												 select unit;
				int rationsrequired = 0;
				foreach (Unit unit in feedingUnits)
				{
					rationsrequired += unit.RequiredStores;
				}
				if (rationsrequired <= PresentStores)
				{
					foreach (Unit unit in feedingUnits)
					{
						FeedUnit(unit);
					}

					foreach (Unit unit in starvingUnits)
					{
						unit.AttritStores();
					}
					IsFed = true;
				}
			}
			else
			{
				FeedHex();
			}
		}
	}
}
