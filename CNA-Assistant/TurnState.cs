﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CNA_Assistant
{
	class TurnState
	{
		/* The TurnState object is responsible for tracking the current step in the turn. When the user is finished taking actions in the current step, it
		 * moves the game to the next step, and provides the tasking for the new step. Possibly this could be done by having classes that take control of the 
		 * PlayerSide object, providing methods for functions unique to their step? So there would be a MaltaRaid class, for example, with methods that when called,
		 * would make the necessary alterations to the PlayerSide object that represents the user's paperwork.  */

		TurnState()
		{

		}

		// Properties

		public TurnPhase GetTurnPhase
		{
			get
			{
				return CurrentTurnPhase;
			}
		}

		// Fields

		TurnPhase CurrentTurnPhase;

		// Methods

		public void NextPhase() // Called when the user has finished all actions to complete in the current phase.
		{
			CurrentTurnPhase += 1;
		}
	}

	enum TurnPhase
	{
		InitiativeDetermination,
		StrategicAirPlanningDesignation,
		AxisMaltaAvailabilityDetermination,
		StrategicMissionAssignment,
		MaltaRaid,
		NavalConvoySchedule,
		NavalConvoyReconnaissance,
		ConvoyLaneAssignment,
		ConvoyBombing, 
		StoresExpenditure, 
		InitiativeDeclaration,    // This is the start of the Ops Stage I, II and III
		WeatherDetermination,
		Organisation,             // This has multiple Segments which can be completed in any order - but each Segment must be completed before moving onto the next?
		NavalConvoyArrival,
		CommonwealthFleet,
		LandSupportAirMissionAssignment,
		LandSupportAirMissionDeployment, 
		AirToAirCombatResolution,
		FlakResolution,
		LandSupportAirMissionCompletion,
		LandSupportAirReturnToBase,
		LandSupportAirMaintenance,
		ReserveDesignation,       // This is the start of the phasing / non-phasing user distinction - play returns to here at the end of the Ops Stage for Player A, for Player B to repeat
		MovementAndCombat,        // This has multiple Segments which must be completed in order, but can be repeated at the phasing player's discretion
		TruckConvoyMovement,
		CommonwealthRailMovement,
		VehicleRepair,            // Has two steps in the rules, but I dont see a need to complete one before the other, or even all of one before any of the other... may as well be one step.
		Patrol,
		CompleteOpsStage,         // Patrol is the last step of the Ops Stage. If Player A is phasing, play returns to ReserveDesignation and Player B is now phasing...
		StrategicAirReturnToBase, // ... If Player B, start the next Ops Stage. If Ops Stage is 3 and Player B, continue to Strategic Air Recovery instead.
		StrategicAirMaintenance   // CompleteOpsStage covers almost all resets - such as resetting Capability Points expended, for instance.
	}

	enum OrganisationSegment
	{
		WaterDistribution,
		Reorganisation,
		Attrition,
		Construction,
		Training,
		SupplyDistribution,
		TacticalShipping
	}

	enum MovementAndCombatSegment
	{
		Movement,
		BreakdownDetermination,
		Combat,
		ReserveRelease
	}
}