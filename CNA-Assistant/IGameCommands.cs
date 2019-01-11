﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CNA_Assistant
{
	interface IGameCommands
	{
		/* This would represent all the interactions the User has with the game model - any decision the User makes that changes the model, is part of this interface.
		 * This would be implemented by the PlayerSide object, or perhaps a Game object if that changes. Essentially, any non-UI related decision goes through this interface. 
		 * Any change the model does without requiring player input doesnt need to be here, as this is about defining the game related decisions the player can make. 
		 *
		 * What should happen if the User sends an invalid command? Either one that doesnt make sense for the current game state, or one that makes sense but has garbage data? */

		void NextPhase(); // the user is finished with the current Phase and wants to move on.

		DiceRoll GetDiceRoll(); // the user wants to roll dice instead of rolling physical dice - used to automatically input results rather than have the player type in results of actual dice throw.

		void DetermineInitiative(DiceRoll userThrow, DiceRoll enemyThrow); // Only makes sense if the current phase is DetermineInitiative. Passes DiceRolls to the model, and the model decides if the user has initiative or not.

		void AssignAircraftStrategic(Aircraft aircraft, bool isStrategic); // user decides to assign the selected aircraft to Strategic missions this round

		int[] AxisMaltaRaidLevel(int raidLevel, DiceRoll dice1, DiceRoll dice2); // Axis user decides what level of raid on malta they want to do (level I to IV). Throws exception if raidLevel is too high (unavailable). Returns an array of two numbers - percentages of planes available for the raid. Throws exception if user is not axis.

		void AxisAssignMaltaRaid(Squadron squadron, /* should this be an enum? */ int maltaTargetID); // Axis user assigns squadrons to a specific target for imminent Malta Raid. Requires aircraft from that Squadron to have been assigned to Malta Raids already. 

		void AssignStrategicMission(Aircraft aircraft, bool isNavalCombat); // user assigns specific aircraft either to Naval recon or Naval bombing (if CW) or Malta Raids or Naval protection (if Axis). True for Bombing Reserve (CW) and Naval CAP (Axis), false for Naval Recon (CW) and Malta Raids (Axis).

		// Probably a bunch of decisions are required by the User to resolve Malta Raids. Air to Air combat, Flak, Bombing and Strafing, at least. Those need to be added at some point.

		/* Naval Convoy Scheduling : CW player plans Replacement Point Production arrivals for next month, in the first phase of THIS month. i.e., Jun I, plan Aug I, II, III and IV. 
		 * These can be scheduled to arrive during any particular operations stage in that month.
		 * 
		 * Axis player plans at least two weeks (full game turns) in advance for Replacement Point arrivals. Replacement Point arrivals happen in a given operations stage, and planned in the ops stage also...
		 * The planning happens in the first Ops Stage of the turn, not the naval convoy schedule phase. */

		int AxisGetNextShippingLimit(DiceRoll diceRoll); // the diceroll limits the shipping for next turn, thus altering the game state - making it a command. Returns the shipping limit.

		// The axis player assigns shipping within that shipping limit. They divide the shipping limit into convoys as they like, selecting the contents of that convoy from unlimited Stores, Fuel, and Ammunition.

		void AxisDefineConvoy(/* this should definitely be an enum */ int shippingLaneID, int arrivingOpsStage, int ammo, int fuel, int stores, int[] trucks, Unit[] replacements); // trucks[] is an array containing numbers of light, medium and heavy trucks on board. replacements[] is more or less a placeholder for a list of replacement points of many different types.

		// Convoy Resolution Phase decisions/input


	}
}
