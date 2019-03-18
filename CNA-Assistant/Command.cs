using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CNA_Assistant
{
	class Command
	{
		public Command(Type type, params object[] arr)
		{
			CommandType = type;
			Params = arr;
		}

		public Type CommandType { get; }

		public Object[] Params { get; }



		public enum Type
		{
			DetermineInitiative,
			DeclareInitiative,
			DetermineWeather,

			SelectPhase,

			AssignFleet,
			AssignReserve,
			Move,

			ReleaseReserve,
			NewMovementCombat,
			RailMovement,
			TowVehicle,
			RepairVehicle,
			AssignPatrol,
			ReconPatrol,
			PatrolObjective
		}
	}
}
