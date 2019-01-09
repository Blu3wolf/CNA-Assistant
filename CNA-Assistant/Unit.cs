using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CNA_Assistant
{
    class Unit
    {
        public int ID;

		public int Location;

        public int CharacteristicsCode;

        public string ShortDesignation;

        public string Designation;

        public int CapabilityPointAllowance;

        public int StackingPoints;

        public UnitType UnitType;

        public IDictionary<TOEStrengthPointType, int> TOEStrengthPoints;

        public int BarrageRating;

        public int VulnerabilityRating;

        public int AntiArmorRating;

        public int ArmorProtectionRating;

        public int OffensiveCloseAssaultRating;

        public int DefensiveCloseAssaultRating;

        public int AntiAircraftRating;

        public int MaxTOEStrengthRating;

        public int BasicMoraleRating;

        public int CurrentMoraleRating;

        public int CapabilityPointsExpended;

        public int CohesionLevel;

        public int ActualBarragePoints;

        public int ActualOffensiveCloseAssaultPoints;

        public int ActualDefensiveCloseAssaultPoints;

        public int FuelReserves;

        public int AmmoReserves;

        public int BreakdownPoints;

        public bool IsAssigned;

        public Unit AssignedToParent;

        public bool IsAttached;

        public Unit AttachedToParent;

        public bool HasAssignedUnits;

        public IList<Unit> AssignedUnits;

        public bool HasAttachedUnits;

        public IList<Unit> AttachedUnits;

        public string Notes;


    }
}
