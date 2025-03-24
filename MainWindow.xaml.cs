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
            gridManager = new GridManager(SLAR, sizeOfMatrix, size);
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

            if (Validation() == false)
            {
                MessageBox.Show("ERROR");
                return;
            }
            
            decimal[] matrixElements = new decimal[size * size];
            decimal[] freeMembers = new decimal[size];

            for (int i = 0; i < size * size; i++) 
            { 
                matrixElements[i] = Convert.ToDecimal(gridManager.matrixElem[i].Text);
            }
            for (int i = 0; i < size; i++)
            {
                freeMembers[i] = Convert.ToDecimal(gridManager.freeMembers[i].Text);
            }

            //TODO
            if (Choose.SelectedIndex == 0)
            {
                MessageBox.Show("Метод Гауса");
            }
            else if (Choose.SelectedIndex == 1)
            {
                MessageBox.Show("Метод Обертання");
            }
            else if (Choose.SelectedIndex == 2)
            {
                MessageBox.Show("Метод Гауса-Холецького");
            }

        }
        bool Validation()
        {
            if (gridManager == null || gridManager.matrixElem == null || gridManager.freeMembers == null)
            {
                return false;
            }
            for (int i = 0; i < size * size; i++)
            {

                if (!Decimal.TryParse(gridManager.matrixElem[i].Text, out decimal n))
                {
                    return false;
                }

            }

            for (int i = 0; i < size; i++)
            {
                if (!Decimal.TryParse(gridManager.freeMembers[i].Text, out decimal n))
                {
                    return false;
                }

            }
            return true;
        }
    

        void ChangeSizeOfSLAR()
        {
            gridManager.UpdateGrid(size);
        }
    }
}