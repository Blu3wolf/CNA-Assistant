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
			gameturn = game.GameTurn;
			Location = location;
			if (game.SupplyDumps.TryGetValue(Location, out SupplyDump supplyDump))
			{
				SupplyDump = supplyDump;
				PresentStores += SupplyDump.Stores;
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

			
			if (HalfRationsRequired >= PresentStores)
			{
				HalfRations();
			}
			Feed();
			if (!IsFed)
			{
				starvingUnits = new List<Unit>();
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

		private List<Unit> starvingUnits;

		private int gameturn;

		public bool CanFeed
		{
			get => PresentStores >= RequiredStores;
		}

		public bool IsFed {	get; private set; }

		public void Feed()
		{
			if (CanFeed && !IsFed)
			{
				foreach (Unit unit in Units)
				{
					if (unit.Stores >= unit.RequiredStores)
					{
						unit.ConsumeStores(gameturn);
					}
					else // another source (another unit, DOGs, SupplyDump) needs to supply this unit
					{
						unit.ConsumeStores(gameturn, SupplyDump.SupplyStores(unit.RequiredStores - unit.Stores));
						if (unit.TurnStoresLastConsumed != gameturn) // still hungry!
						{
							// start looking...
							foreach (Unit otherUnit in Units)
							{
								unit.ConsumeStores(gameturn, otherUnit.SupplyStores(unit.RequiredStores - unit.Stores));
								if (unit.TurnStoresLastConsumed == gameturn)
								{
									continue;
								}
							}
						}
					}
				}
				IsFed = true;
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
						if (unit.Stores >= unit.RequiredStores)
						{
							unit.ConsumeStores(gameturn);
						}
						else // another source (another unit, DOGs, SupplyDump) needs to supply this unit
						{
							unit.ConsumeStores(gameturn, SupplyDump.SupplyStores(unit.RequiredStores - unit.Stores));
							if (unit.TurnStoresLastConsumed != gameturn) // still hungry!
							{
								// start looking...
								foreach (Unit otherUnit in Units)
								{
									unit.ConsumeStores(gameturn, otherUnit.SupplyStores(unit.RequiredStores - unit.Stores));
									if (unit.TurnStoresLastConsumed == gameturn)
									{
										continue;
									}
								}
							}
						}
					}

					foreach (Unit unit in starvingUnits)
					{
						unit.AttritStores(gameturn);
					}
					IsFed = true;
				}
			}
			else
			{
				Feed();
			}
		}
	}
}
