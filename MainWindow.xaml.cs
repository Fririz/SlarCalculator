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

        void PlusSize(object sender, RoutedEventArgs e)
        {
            if (size == 9)
            {
                //TODO сделать вывод ошибки
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
                //TODO сделать вывод ошибки
            }
            else
            {
                size -= 1;
                ChangeSizeOfSLAR();
            }

        }

        void ChangeSizeOfSLAR()
        {
            SLAR.Children.Clear();
            SLAR.RowDefinitions.Clear();
            SLAR.ColumnDefinitions.Clear();
            Grid myGrid = SLAR;
            //myGrid.ShowGridLines = true;

            // Определяем колонки
            /*
            for (int i = 0; i <= size * 2 + 2; i++)
            {
                myGrid.ColumnDefinitions.Add(new ColumnDefinition());
            }

            // Определяем строки
            for (int i = 0; i < size; i++)
            {
                myGrid.RowDefinitions.Add(new RowDefinition());
            }
            */

            for (int i = 0; i < 20; i++)
            {
                myGrid.ColumnDefinitions.Add(new ColumnDefinition());
            }
            for (int i = 0; i < 10; i++)
            {
                myGrid.RowDefinitions.Add(new RowDefinition());
            }

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j <= size * 2 + 2; j++)
                {
                    if (j % 2 == 0 && j < size * 2)
                    {
                        TextBox textbox = new TextBox();
                        Grid.SetColumn(textbox, j);
                        Grid.SetRow(textbox, i);
                        myGrid.Children.Add(textbox);
                    }
                    else if (j % 2 != 0 && j < size * 2)
                    {
                        TextBlock myTextBlock = new TextBlock();
                        myTextBlock.Text = $"x{(j + 1) / 2}";
                        myTextBlock.VerticalAlignment = VerticalAlignment.Center;
                        myTextBlock.HorizontalAlignment = HorizontalAlignment.Center;
                        Grid.SetColumn(myTextBlock, j);
                        Grid.SetRow(myTextBlock, i);
                        myGrid.Children.Add(myTextBlock);
                    }
                    else if (j == size * 2)
                    {
                        TextBlock myTextBlock = new TextBlock();
                        myTextBlock.Text = "=";
                        myTextBlock.VerticalAlignment = VerticalAlignment.Center;
                        myTextBlock.HorizontalAlignment = HorizontalAlignment.Center;
                        Grid.SetColumn(myTextBlock, j);
                        Grid.SetRow(myTextBlock, i);
                        myGrid.Children.Add(myTextBlock);
                    }
                    else if (j == size * 2 + 1)
                    {
                        TextBox textbox = new TextBox();
                        Grid.SetColumn(textbox, j);
                        Grid.SetRow(textbox, i);
                        myGrid.Children.Add(textbox);
                    }
                }
            }

            sizeOfMatrix.Text = $"Size: {size}";

    }
    }
}