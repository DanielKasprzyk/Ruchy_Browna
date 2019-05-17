using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;

namespace Ruchy_Browna
{
    public partial class MainWindow : Window
    {
        static readonly string directoryPath = Environment.CurrentDirectory + @"\Output";
        static readonly string output = directoryPath + @"\brown.xlsx";
        public List<Point> points;
        public MainWindow()
        {
            InitializeComponent();
            CreateDirectory();
            
        }
        public void Start(object sender, RoutedEventArgs e)
        {
            points = new List<Point>();
            points.Add(new Point());
            Random rnd = new Random();
            double x = 0;
            double y = 0;
            double s, fi;
            int i;
            for (i = 0; i < int.Parse(n.Text); i++)
            {
                fi = RandomGen.RNGGenerate();
                x += Math.Cos(fi);
                y += Math.Sin(fi);
                if (roundCheckBox.IsChecked == true)
                {
                    x = Round(x);
                    y = Round(y);
                }  
                points.Add(new Point(x, y));
            }
            RandomGen.RNGDispose();
            s = Math.Sqrt(Math.Pow(points[points.Count-1].X, 2) + Math.Pow(points[points.Count - 1].Y, 2));
            SaveIntoFile(s);
            
        }
        public void SaveIntoFile(double distance)
        {
            Createfile(output);
            Excel excel = new Excel(output, 1);
            for (int i = 0; i < points.Count; i++)
            {
                excel.WriteToCell(i, 1, points[i].X);
                excel.WriteToCell(i, 2, points[i].Y);
                x.Content = points[i].X;
                y.Content = points[i].Y;

            }
            distanceLabel.Content = distance;
            excel.WriteToCell(0, 0, "x:");
            excel.WriteToCell(0, 3, ":y");
            excel.WriteToCell(0, 4, "Cząsteczka przemiesciła się na odległość: ");
            excel.WriteToCell(0, 9, distance);
            excel.Save();
            excel.Close();
            points = null;
        }
        public void Createfile(string path)
        {
            if (!File.Exists(output))
            {
                Excel ex = new Excel();
                ex.CreateNewFile();
                ex.SaveAs(path);
                ex.Close();
            }
        }
        public void CreateDirectory()
        {
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }
        }
        public double Round(double value)
        {
            return Math.Round(value, int.Parse(placesAfterComa.Text), MidpointRounding.ToEven);
        }

        private void N_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}

