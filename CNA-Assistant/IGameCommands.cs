using System;
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
		 * It is seeming that this PlayerSide object is probably far too large, based on a cursory read of Fowler's 'Refactoring'. It should probably be broken down into smaller objects. 
		 *
		 * What should happen if the User sends an invalid command? Either one that doesnt make sense for the current game state, or one that makes sense but has garbage data?
		 * Ignore it! */

		void Next(); // the user is finished with the current Step and wants to move on. Not allowed if the current phase has outstanding decisions to make!

		void DetermineInitiative(DiceRoll userThrow, DiceRoll enemyThrow); // Only makes sense if the current phase is DetermineInitiative. Passes DiceRolls to the model, and the model decides if the user has initiative or not.

		void DetermineInitiative(bool HasInitiative); // user advises software whether they have intiative or not directly. 

		// planning for implementing the Land and Logistics Games only at this point! Air Game will be added later. 

		/* Naval Convoy Scheduling : CW player plans Replacement Point Production arrivals for next month, in the first phase of THIS month. i.e., Jun I, plan Aug I, II, III and IV. 
		 * These can be scheduled to arrive during any particular operations stage in that month.
		 * 
		 * Axis player plans at least two weeks (full game turns) in advance for Replacement Point arrivals. Replacement Point arrivals happen in a given operations stage, and planned in the ops stage also...
		 * The planning happens in the first Ops Stage of the turn, not the naval convoy schedule phase. */

		int AxisGetNextTurnShippingLimit(DiceRoll diceRoll); // the diceroll limits the shipping for next turn, thus altering the game state - making it a command. Returns the shipping limit.

		// The axis player assigns shipping within that shipping limit. They divide the shipping limit into convoys as they like, selecting the contents of that convoy from unlimited Stores, Fuel, and Ammunition.

		void AxisDefineConvoy(ShippingLanes shippingLanes, int arrivingOpsStage, int ammo, int fuel, int stores, int[] trucks, Unit[] replacements); // trucks[] is an array containing numbers of light, medium and heavy trucks on board. replacements[] is more or less a placeholder for a list of replacement points of many different types.

		// Stores expenditure stage - this one only requires decisions for hexes with insufficient stores. Everywhere else can be done automatically, I guess - but optionally a player decision could be allowed to make half-rations an option?
		// in that case, the only interaction/decision needs to be one to assign units to half-rations or full rations status, and a way to assign units to get to eat, if there is insufficient food to go around.

		void GoHungry(Unit unit); // marks units to go hungry this stage if there is not enough stores to go around.

		void ConsumeStores(Unit unit); // marks unit to try consume stores this stage

		void HalfRations(Unit unit); // Surely this should be a method of the unit in question. 

		void FullRations(Unit unit); // likewise, surely a method of the unit.

		// First (and second, and third) Operations Stage! So many interactions here!

		void DeclareInitiative(bool IsPlayerA); // user indicates whether they are player A or player B for this Ops Stage.

		void DeclareWeather(); // user without initiative advises software what weather is in effect.

		void DetermineWeather(); // user with initiative rolls for weather. User without initiative could use this if they had the dice rolls too I guess. 

		// Organisation Phase - Segments undertaken in any order desired

		void DistributeWater(); // user denotes a unit to consume water (from the same hex)

		void Detach(); // when you detach a unit, you can reorganise the unit pretty freely. 

		void Attach(); // attach only to unit in the same hex

		void Assign(); // assign only to attached unit

		void AttritLackOfWater(); // all units without water distributed already undergo attrition

		void Construct(); // units start or continue construction projects

		void PerformTraining(); // units start or continue training

		void DistributeSupplies(); // Supplies in the same hex as land units may be redistributed, trucks loaded/unloaded, etc

		void TacticalShipping(); // user performs tactical shipping, moving cargo between ports

		// Naval Convoy Arrival Phase - reinforcements, replacements and supplies arrive at ports as scheduled. 

		void PlanReplacements(); // user plans replacements to arrive in future turns

		// Commonwealth Fleet Phase

		void AssignFleet(); // CW player assigns ship to hexes for bombardment

		void RepairFleet(); // CW player performs fleet repair

		// Reserve Designation Phase - this is the start of the PlayerA/PlayerB repetition. No interaction unless user is phasing!

		void AssignReserve(); // user assigns unit to Reserve status

		// Movement and Combat Phase - 4 segments in order, repeated as long as phasing user desires.

		// movement segment

		void Move(); // phasing user moves units according to Continual Movement rules.

		void React(); // non phasing user may move some units per reaction rules

		// Breakdown Determination segment

		// any decisions to make here?

		// Combat Segment - so many decisions to make here!

		

		// Reserve Release Segment

		void ReleaseReserve(); // user releases reserves

		void RestartMovementCombat(); // User decides to start a new movement and combat phase - happens automatically if they release reserves

		// Truck Convoy Movement Phase

		void Move(); // phasing user moves Truck Convoys, POWs and Guards

		// Commonwealth Rail Movement Phase

		void MoveSupplies(); // CW phasing user moves supplies along rail

		// Repair Phase

		void TowVehicle(); // phasing user tows brokendown and recovered vehicles

		void RepairVehicle(); // phasing user attempts to repair a broken or destroyed vehicle (that was not towed this phase)

		// Patrol Phase - this is the end of the repetition - after this, go either to next op stage, next turn, or playerB reserve designation phase.

		void Patrol(); // phasing user performs Patrols if no combat occurred

		// and that covers the decisions the user can make in the Land/Logistics Game.
	}
}
