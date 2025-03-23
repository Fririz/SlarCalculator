using SlarCalculator.Classes;
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
        Classes.GridManager gridManager;
        public MainWindow()
        {
            InitializeComponent();
            gridManager = new GridManager(SLAR, sizeOfMatrix);
            ChangeSizeOfSLAR();

        }
        void PlusSize(object sender, RoutedEventArgs e)
        {
            if (size == 10)
            {
                MessageBox.Show("Too Big");
            }
            else
            {
                size += 1;
                ChangeSizeOfSLAR();
            }

        }

        void MinusSize(object sender, RoutedEventArgs e)
        {
            if (size == 1)
            {
                MessageBox.Show("Too Small");
            }
            else
            {
                size -= 1;
                ChangeSizeOfSLAR();
            }

        }
        void Calculate(object sender, RoutedEventArgs e)
        {
            int[,] matrix = new int[size,size];
            int[] freeMembers = new int[size];
            //TextBox[] textboxes = new TextBox[];
            //TODO
        }

        void ChangeSizeOfSLAR()
        {
            gridManager.UpdateGrid(size);
        }
    }
}