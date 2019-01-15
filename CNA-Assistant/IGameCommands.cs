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
		 * What should happen if the User sends an invalid command? Either one that doesnt make sense for the current game state, or one that makes sense but has garbage data? */

		void NextPhase(); // the user is finished with the current Phase and wants to move on. Not allowed if the current phase has outstanding decisions to make!

		void DetermineInitiative(DiceRoll userThrow, DiceRoll enemyThrow); // Only makes sense if the current phase is DetermineInitiative. Passes DiceRolls to the model, and the model decides if the user has initiative or not.

		void AssignAircraftToStrategic(Aircraft aircraft, bool isStrategic); // user decides to assign the selected aircraft to Strategic missions this round

		int[] AxisMaltaRaidLevel(int raidLevel, TwoDice twoDice); // Axis user decides what level of raid on malta they want to do (level I to IV). Throws exception if raidLevel is too high (unavailable). Returns an array of two numbers - percentages of planes available for the raid. Throws exception if user is not axis.

		void AxisAssignMaltaRaid(Squadron squadron, /* should this be an enum? */ int maltaAirfield); // Axis user assigns squadrons to a specific target for imminent Malta Raid. Requires aircraft from that Squadron to have been assigned to Malta Raids already. 

		/* In the Strategic Air Planning Phase, the User allocates all aircraft either to Land Support or Strategic command. 
		 * If the User is Axis, they choose a Malta Availability Level (I to IV) and roll a dice to determine the percentage of forces they can use for Raids on Malta. 
		 * The User assigns their Strategic aircraft to a specific strategic role. The Axis User assigns aircraft either to the Malta role, or convoy protection. The CW user assigns aircraft either to Naval Recon, Bombing Reserve or Malta CAP.
		 * The Axis User assigns all Malta Role squadrons to specific airfields, deciding on the Mission to be flown by each squadron at this point. The CW User assigns all Malta CAP aircraft to an airfield. 
		 * Interception, Air-to-Air Combat, and Flak Suppression may occur. AA/Flak fire almost certainly will occur! Bombing missions (against the Air Facilities) are executed following that. 
		 */

		void AssignToStrategicMissionType(Aircraft aircraft, bool isNavalCombat); // user assigns specific aircraft either to Naval recon or Bombing Reserve (naval convoy bombing) (if CW) or Malta Raids or Naval protection (if Axis). True for Bombing Reserve (CW) and Naval CAP (Axis), false for Naval Recon (CW) and Malta Raids (Axis).

		void CWAssignNavalReconToLane(Aircraft aircraft, int NavalConvoyLaneID); // this happens if the CW user assigns an aircraft to Naval Recon - this needs to change!

		// Probably a bunch of decisions are required by the User to resolve Malta Raids. Air to Air combat, Flak, Bombing and Strafing, at least. Those need to be added at some point.

		/* Naval Convoy Scheduling : CW player plans Replacement Point Production arrivals for next month, in the first phase of THIS month. i.e., Jun I, plan Aug I, II, III and IV. 
		 * These can be scheduled to arrive during any particular operations stage in that month.
		 * 
		 * Axis player plans at least two weeks (full game turns) in advance for Replacement Point arrivals. Replacement Point arrivals happen in a given operations stage, and planned in the ops stage also...
		 * The planning happens in the first Ops Stage of the turn, not the naval convoy schedule phase. */

		int AxisGetNextTurnShippingLimit(DiceRoll diceRoll); // the diceroll limits the shipping for next turn, thus altering the game state - making it a command. Returns the shipping limit.

		// The axis player assigns shipping within that shipping limit. They divide the shipping limit into convoys as they like, selecting the contents of that convoy from unlimited Stores, Fuel, and Ammunition.

		void AxisDefineConvoy(/* this should definitely be an enum */ int shippingLaneID, int arrivingOpsStage, int ammo, int fuel, int stores, int[] trucks, Unit[] replacements); // trucks[] is an array containing numbers of light, medium and heavy trucks on board. replacements[] is more or less a placeholder for a list of replacement points of many different types.

		// Convoy Resolution Phase decisions/input

		bool ConvoyRecon(int NavalConvoyLaneID, TwoDice twoDice); // user rolls for whether they successfully recon'ed the given convoy lane. Returns true if recon was successful. 

		// Convoy Lane Assignment Segment - Axis strategic non-malta aircraft are assigned convoy lanes to CAP for. Bombing Reserve aircraft are assigned CAP, Flak Suppression or Convoy Bombing missions in specific convoy lanes.

		void AssignStrategicMission(Aircraft aircraft, int NavalConvoyLaneID, int MissionRole); // yeah, this is going to get refactored/restructed for sure.

		// convoy bombing resolves - air-to-air combat, flak suppression, anti-aircraft fire, and convoy bombing. 
		// The decisions here are going to be the same as in tactical air to air combat mainly, which Ive not yet described. I guess I need to do that, still.

		

		// Stores expenditure stage - this one only requires decisions for hexes with insufficient stores. Everywhere else can be done automatically, I guess - but optionally a player decision could be allowed to make half-rations an option?
		// in that case, the only interaction/decision needs to be one to assign units to half-rations or full rations status, and a way to assign units to get to eat, if there is insufficient food to go around.

		void GoHungry(Unit unit); // marks units to go hungry this stage if there is not enough stores to go around.

		void ConsumeStores(Unit unit); // marks unit to try consume stores this stage

		void HalfRations(Unit unit); // Surely this should be a method of the unit in question. 

		void FullRations(Unit unit); // likewise, surely a method of the unit.

		// First (and second, and third) Operations Stage! So many interactions here!

		void CWRebaseMalta(Aircraft aircraft, int MaltaAirfield); // This should probably be a method of an Aircraft, perhaps? Can happen any time before any missions are flown in that ops stage, or after all missions have been flown that ops stage. 

		void CWRebaseMalta(int FlakPoints, int MaltaAirfield); // same as above, can do 

		// Then finally, the strategic air recovery stage - surviving planes (automatically) try to RTB if possible. Then all RTB strategic aircraft are attempted to be readied for next game turn.

		void Return(Aircraft aircraft);

		void Divert(Aircraft aircraft, AirFacility airFacility); // optional divert if the aircraft doesnt have the range to Return, or if the home base is damaged and cannot accept all its aircraft. Not valid if the home base CAN accept its aircraft, and this aircraft can make it back there!

		void CrashLand(Aircraft aircraft, DiceRoll diceRoll); // if unable to Return or Divert, the aircraft can only crashland. Not really a decision though. requires (diceroll) input from User though.

		// three things in readying, refueling, refitting and rearming.

		void Refuel(Aircraft aircraft); // aircraft have a 'fueled' property which is a simple bool

		void Refit(Aircraft aircraft, Squadron squadron, TwoDice twoDice); // aircraft have a refitted property, simple bool. Not necessarily successful.

		void Rearm(Squadron squadron); // reload guns for all aircraft in squadron

		void Reload(Aircraft aircraft, int LoadID); // load a bomber with a specific loadout (many have only one)

		// and that covers the decisions the user can make in the game.
	}
}
