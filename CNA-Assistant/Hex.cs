using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CNA_Assistant
{
	struct Hex
	{


		// properties

		public int Q { get; }

		public int R { get; }

		public TerrainType Terrain { get; }

		// methods



		public ReadOnlyCollection<SideTerrainType> SideTerrain(Hex hex)
		{
			throw new NotImplementedException();
			// should return the collection of SideTerrain to move into the given (adjacent) hex
		}

		// enums

		public enum TerrainType
		{
			Clear,
			Gravel,
			SaltMarsh,
			HeavyVegetation,
			Rough,
			Mountain,
			Delta,
			Desert,
			Swamp,
			Sea,
			MajorCity
		}

		public enum SideTerrainType
		{
			Road,
			Track,
			MajorRiver,
			MinorRiver,
			Wadi,
			UpSlope,
			DownSlope,
			UpEscarpment,
			DownEscarpment
		}
	}
}
