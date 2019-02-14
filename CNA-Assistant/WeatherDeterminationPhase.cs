using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CNA_Assistant
{
	public partial class Game
	{
		class WeatherDeterminationPhase : TurnState
		{
			internal WeatherDeterminationPhase(Game game) : base(game)
			{

			}

			protected override void Entry()
			{
				WeatherSet = false;
				RoughWeather = false;
			}

			internal override void Execute(Command command)
			{
				switch (command.CommandType)
				{
					case Command.Type.DetermineWeather:
						if (command.Params[0] is TwoDice)
						{
							RollWeather((TwoDice)command.Params[0]);
						}
						else if (command.Params[0] is DiceRoll)
						{
							RollWeatherLocations((DiceRoll)command.Params[0]);
						}
						break;
					default:
						break;
				}
			}

			private bool WeatherSet;

			private bool RoughWeather;

			private void RollWeather(TwoDice diceRoll)
			{
				if (WeatherSet)
				{
					return;
				}
				int minHot = 0;
				int minSand = 0;
				int minRain = 0;
				switch (game.CurrentSeason) // extract SeasonalWeather class here?
				{
					case Season.Spring:
						{
							minHot = 43;
							minSand = 56;
							minRain = 65;
						}
						break;
					case Season.Summer:
						{
							minHot = 24;
							minSand = 56;
							minRain = 67;
						}
						break;
					case Season.Autumn:
						{
							minHot = 36;
							minSand = 55;
							minRain = 62;
						}
						break;
					case Season.Winter:
						{
							minHot = 67;
							minSand = 67;
							minRain = 53;
						}
						break;
					default:
						break;
				}

				int roll = diceRoll.LargeResults;
				Weather weather = Weather.Normal;
				if (roll >= minRain)
				{
					weather = Weather.Rainstorm;
				}
				else if (roll >= minSand)
				{
					weather = Weather.Sandstorm;
				}
				else if (roll >= minHot)
				{
					weather = Weather.Hot;
					game.Evaporate(Evaporation.HotWeather);
				}

				SetWeather(weather);
			}

			private void SetWeather(Weather weather)
			{
				game.CurrentWeather = weather;
				if (weather == Weather.Normal || weather == Weather.Hot)
				{
					WeatherSet = true;
					Next();
				}
				else
				{
					RoughWeather = true;
				}
			}

			private void RollWeatherLocations(DiceRoll diceRoll)
			{
				if (RoughWeather)
				{
					switch (diceRoll.Result)
					{
						case 1:
							game.WeatherLocations = new int[] { 1, 2 };
							WeatherSet = true;
							break;
						case 2:
							game.WeatherLocations = new int[] { 3, 4 };
							WeatherSet = true;
							break;
						case 3:
							game.WeatherLocations = new int[] { 4, 5 };
							WeatherSet = true;
							break;
						case 4:
							game.WeatherLocations = new int[] { 2, 3 };
							WeatherSet = true;
							break;
						case 5:
							game.WeatherLocations = new int[] { 2, 4 };
							WeatherSet = true;
							break;
						case 6:
							game.WeatherLocations = new int[] { 2, 3, 4 };
							WeatherSet = true;
							break;
						default:
							break;
					}
					Next();
				}
			}

			internal override void Next()
			{
				if (WeatherSet)
				{
					game.TurnState = new OrganisationPhase(game);
				}
			}
		}
	}
}
