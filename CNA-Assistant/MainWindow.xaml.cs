using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CNA_Assistant
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

			Game playerSide = new Game();

			int turn = 1;
			int stage = 1;
			Console.WriteLine("The date the full campaign starts will be: ");
			playerSide.TestGetDateAtTurn(turn, stage);
			Console.WriteLine("This should be the third week (15th) of Sep, 1940.");

			turn = 4;
			stage = 2;
			Console.WriteLine("On turn 4, stage 2, the date will be:");
			playerSide.TestGetDateAtTurn(turn, stage);

			turn = 26;
			stage = 3;
			Console.WriteLine("The Desert Fox campaign starts on turn 26, stage 3.");
			Console.WriteLine("The date will be: ");
			playerSide.TestGetDateAtTurn(turn, stage);
			Console.WriteLine("This 'should' be the fourth week of Mar, 1941.");

			turn = 111;
			Console.WriteLine("The date for turn 111, OpStage 1, the end of the campaign, will be: ");
			playerSide.TestGetDateAtTurn(turn, 1);
			Console.WriteLine("This 'should' be the first week of Jan, 1943.");

			turn = 111;
			Console.WriteLine("The date for turn 111, OpStage 3, the final date of the campaign, will be: ");
			playerSide.TestGetDateAtTurn(turn, 3);
			Console.WriteLine("This 'should' be in the first week of Jan, 1943.");
		}
    }
}
