using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using System.Drawing;
using System.Windows.Media;
using SlarCalculator.Solvers;

namespace SlarCalculator.Classes
{
    class GridManager
    {
        private Grid grid;
        private TextBlock sizeOfMatrix;
        /*
        //сделать
        int CounterMatrixElem = 0;
        int CounterFreeMembers = 0;
        public TextBox[] matrixElem {  get; }
        public TextBox[] freeMembers { get; }
        */
        public GridManager(Grid grid, TextBlock sizeOfMatrix)
        {
            this.grid = grid;
            this.sizeOfMatrix = sizeOfMatrix;
        }

        public void UpdateGrid(int size)
        {
            grid.Children.Clear();
            grid.RowDefinitions.Clear();
            grid.ColumnDefinitions.Clear();
            grid.ShowGridLines = true;

            for (int i = 0; i < 22; i++)
            {
                grid.ColumnDefinitions.Add(new ColumnDefinition());
            }
            for (int i = 0; i < 10; i++)
            {
                grid.RowDefinitions.Add(new RowDefinition());
            }

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j <= size * 2 + 2; j++)
                {
                    if (j % 2 == 0 && j < size * 2)
                    {
                        AddTextBox(i, j, "matrixElem");
                    }
                    else if (j % 2 != 0 && j < size * 2)
                    {
                        AddTextBlock(i, j, $"x{(j + 1) / 2}");
                    }
                    else if (j == size * 2)
                    {
                        AddTextBlock(i, j, "=");
                    }
                    else if (j == size * 2 + 1)
                    {
                        AddTextBox(i, j, "freeMembers");
                    }
                }
            }

            sizeOfMatrix.Text = $"Size: {size}";
        }

        private void AddTextBox(int row, int column, string name)
        {
            TextBox textbox = new TextBox
            {
                Foreground = Brushes.White,
                Background = Brushes.MidnightBlue,
                Name = name
            };
            Grid.SetColumn(textbox, column);
            Grid.SetRow(textbox, row);
            grid.Children.Add(textbox);

        }
        private void AddTextBlock(int row, int column, string text)
        {
            TextBlock textBlock = new TextBlock
            {
                Text = text,
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Center
            };
            Grid.SetColumn(textBlock, column);
            Grid.SetRow(textBlock, row);
            grid.Children.Add(textBlock);
        }
    }
}
