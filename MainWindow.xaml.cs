using SlarCalculator.Classes;
using SlarCalculator.Solvers;
using System.Diagnostics;
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
        void SaveToFile(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
            dlg.FileName = "result";
            dlg.DefaultExt = ".txt";
            dlg.Filter = "Text documents (.txt)|*.txt";

            bool? result = dlg.ShowDialog();

            if (result == true)
            {
                string filename = dlg.FileName;
                string textToSave = AnswerTextBlock.Text;

                try
                {
                    System.IO.File.WriteAllText(filename, textToSave, Encoding.UTF8);
                    MessageBox.Show("Результат успешно сохранён!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка при сохранении: " + ex.Message);
                }
            }
        }

        void Calculate(object sender, RoutedEventArgs e)
        {

            if (Validation() == false)
            {
                MessageBox.Show("ERROR");
                return;
            }
            
            decimal[,] matrixElements = new decimal[size, size];
            decimal[] freeMembers = new decimal[size];

            for (int i = 0; i < size; i++) 
            {
                for (int j = 0; j < size; j++)
                {
                    matrixElements[i, j] = Convert.ToDecimal(gridManager.matrixElem[i * size + j].Text);
                }
            }

            for (int i = 0; i < size; i++)
            {
                freeMembers[i] = Convert.ToDecimal(gridManager.freeMembers[i].Text);
            }

            if (Choose.SelectedIndex == 0)
            {

                Solvers.GaussSolver gaussSolver = new Solvers.GaussSolver();
                decimal[] result = gaussSolver.Solve(matrixElements, freeMembers);
                AnswerTextBlock.Text = string.Join("\n", result);
            }

            else if (Choose.SelectedIndex == 1)
            {
                Solvers.RotationSolver Rotation = new Solvers.RotationSolver();
                try
                {
                    decimal[] result = Rotation.Solve(matrixElements, freeMembers);
                    if (result.Length == 0)
                    {
                        MessageBox.Show("Метод Обертання: решение не найдено.");
                    }
                    else
                    {
                       
                        AnswerTextBlock.Text = string.Join("\n", result);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

            }
            else if (Choose.SelectedIndex == 2)
            {
                Solvers.KholetskySolver Kholetsky = new Solvers.KholetskySolver();

                try
                {
                    double[,] matrixElementsDouble = new double[size, size];
                    for (int i = 0; i < size; i++)
                    {
                        for (int j = 0; j < size; j++)
                        {
                            matrixElementsDouble[i, j] = (double)matrixElements[i, j];
                        }
                    }

                    double[] freeMembersDouble = new double[size];
                    for (int i = 0; i < size; i++)
                    {
                        freeMembersDouble[i] = (double)freeMembers[i];
                    }

                    double[] result = Kholetsky.Solve(matrixElementsDouble, freeMembersDouble);

                    AnswerTextBlock.Text = string.Join("\n", result);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("error: "+ ex);
                }
            }
            else if (Choose.SelectedIndex == 3)
            {
                if(size != 2)
                {
                    MessageBox.Show("Matrix should be 2x2");
                    return;
                }

                string pythonPath = @"C:\Users\Friri\AppData\Local\Programs\Python\Python313\python.exe";

                string scriptPath = @"D:\Other\VisualStudioProj\Kursovaya\SlarCalculator\python\solve_graphically.py";

                //a1 b1 c1 a2 b2 c2
                List<string> argsList = new List<string>();

                for (int i = 0; i < size; i++)
                {
                    for (int j = 0; j < size; j++)
                    {
                        argsList.Add(matrixElements[i, j].ToString());
                    }
                    argsList.Add(freeMembers[i].ToString());
                }

                string arguments = $"\"{scriptPath}\" " + string.Join(" ", argsList);


                ProcessStartInfo start = new ProcessStartInfo();
                start.FileName = pythonPath;
                start.Arguments = arguments;
                start.UseShellExecute = false;
                start.RedirectStandardOutput = true;
                start.RedirectStandardError = true;
                start.CreateNoWindow = true;

                using (Process process = Process.Start(start))
                {
                    string output = process.StandardOutput.ReadToEnd();
                    string error = process.StandardError.ReadToEnd();
                    process.WaitForExit();
                    System.Console.WriteLine("OUTPUT:\n" + output);
                    System.Console.WriteLine("ERROR:\n" + error);
                }
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