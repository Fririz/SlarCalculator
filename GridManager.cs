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
        public TextBox[] matrixElem;
        public TextBox[] freeMembers;


        public GridManager(Grid grid, TextBlock sizeOfMatrix, int startSize)
        {
            this.grid = grid;
            this.sizeOfMatrix = sizeOfMatrix;

        }

        public void UpdateGrid(int size)
        {
            matrixElem = new TextBox[size * size];
            freeMembers = new TextBox[size];
            int CounterMatrixElem = 0;
            int CounterFreeMembers = 0;
            
            grid.Children.Clear();
            grid.RowDefinitions.Clear();
            grid.ColumnDefinitions.Clear();


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
                        TextBox textbox = new TextBox();
                        AddTextBox(i, j, textbox);
                        matrixElem[CounterMatrixElem] = textbox;
                        CounterMatrixElem++;
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
                        TextBox textbox = new TextBox();
                        AddTextBox(i, j, textbox);
                        freeMembers[CounterFreeMembers] = textbox;
                        CounterFreeMembers++;
                    }

                }
            }

            sizeOfMatrix.Text = $"Size: {size}";
        }

        private void AddTextBox(int row, int column, TextBox textBox)
        {

            var bc = new BrushConverter();
            textBox.Background = (Brush)bc.ConvertFrom("#5483B3");
            textBox.Foreground = (Brush)bc.ConvertFrom("#C1E8FF");
            Grid.SetColumn(textBox, column);
            Grid.SetRow(textBox, row);

            grid.Children.Add(textBox);

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
