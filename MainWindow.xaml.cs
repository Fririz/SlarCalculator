using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SlarCalculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        int size = 2;
        public MainWindow()
        {
            InitializeComponent();
            ChangeSizeOfSLAR();

        }

        void ChangeSizeOfSLAR()
        {
            Grid myGrid = SLAR;
            myGrid.ShowGridLines = true;
            // Define the Columns
            ColumnDefinition[] ArrayOfColumn = new ColumnDefinition[size];
            for (int i = 0; i < size + 1; i++)
            {
                myGrid.ColumnDefinitions.Add(new ColumnDefinition());
            }
            for (int i = 0; i < size; i++)
            {
                myGrid.RowDefinitions.Add(new RowDefinition());
            }


        }
    }
}