using System;
using System.Collections.Generic;
using System.Windows;
using System.IO;

namespace Ruchy_Browna
{
    public partial class MainWindow : Window
    {
        static readonly string directoryPath = Environment.CurrentDirectory + @"\Output";
        static readonly string filePath = directoryPath + @"\brown.xlsx";
        public List<Point> points;
        public int sheet;
        public MainWindow()
        {
            InitializeComponent();
            CreateDirectory();
            Createfile(filePath);
            sheet = 1;
        }
        public void Start(object sender, RoutedEventArgs e)
        //  funkcja głowna zajmująca się losowaniem liczb przy pomocy klasy RandomGen
        {
            long N = long.Parse(n.Text);
            points = new List<Point>(); //  lista punktów
            points.Add(new Point());    //  punkt [0,0]
            double x = points[0].X;     //  inicjalizujemy zmienne wartościami punktu startowego
            double y = points[0].Y;
            double s;                   //  s - długość wektora przesunięcia cząstki po n ruchach
            double fi;                  //  fi - kąt z przedziału <0;2PI>
            long on = 0;
            CreateSheet(filePath, N.ToString());

            for (long i = 0; i < N; i++)     //  pętla wykona się n razy
            {
                if (i % 10000 == 0 && i != 0) //  co 10000 zmiennych wykonujemy zapis i zwalniamy pamięć
                {
                    SaveIntoFile(0, on);     //  zapisujemy wyniki do pliku
                    on = i;
                    points = new List<Point>();     //  dla optymalizacji tworzymy nową listę
                }
                fi = RandomGen.RNGGenerate();
                //  przy użyciu klasy RandomGen generujemu kąt z zadanego przedziału
                x += Math.Cos(fi);      //  ustawiamy nowy x na bazie poprzedniego i cosinusa konta fi
                y += Math.Sin(fi);      //  ustawiamy nowy y na bazie poprzedniego i sinusa konta fi
                points.Add(new Point(x, y));        //  dodajemy do listy punktów
            }
            RandomGen.RNGDispose();     // po wykonanych obliczeniach czyścimy zmienne losowe z pamięci
            s = Math.Sqrt(Math.Pow(points[points.Count - 1].X, 2) + Math.Pow(points[points.Count - 1].Y, 2));
            //  obliczamy długość wektora s
            SaveIntoFile(s, on);        //  zapisujemy wyniki do pliku     
        }

        public void SaveIntoFile(double distance, long startOn)
        //  funkcja zapisująca listę punktów do pliku przy użyciu klasy Excel
        {

            Excel excel = new Excel(filePath, sheet);       // podajemy scieżke do pliku
            for (int i = 0; i < points.Count; i++)
            {
                excel.WriteToCell(i + startOn, 1, points[i].X);   // wpisujemy do konkretnych komórek
                excel.WriteToCell(i + startOn, 2, points[i].Y);
            }
            excel.WriteToCell(0, 0, "x:");
            excel.WriteToCell(0, 3, ":y");
            excel.WriteToCell(0, 4, "Cząsteczka przemiesciła się na odległość: ");
            excel.WriteToCell(0, 9, distance);
            excel.WriteToCell(0, 10, "od punktu (0,0).");
            excel.WriteToCell(1, 4, "n:");
            excel.WriteToCell(1, 5, n.Text);
            x.Content = points[points.Count - 1].X;     //   wyswietlanie wartosci w aplikacji
            y.Content = points[points.Count - 1].Y;
            distanceLabel.Content = distance;
            excel.Save();       //  zapisujemy
            excel.Close();      //  zamykamy plik
            points = null;      //  czyscimy liste
        }
        public void Createfile(string path)     //  funkcja tworząca plik
        {
            if (!File.Exists(filePath))
            {
                Excel ex = new Excel();
                ex.CreateNewFile();
                ex.SaveAs(path);
                ex.Close();
                sheet = 1;
            }
        }
        public void CreateSheet(string path, string name)     //  funkcja tworząca arkusz
        {
            bool done = false;
            do
            {
                if (File.Exists(filePath))
                {
                    Excel ex = new Excel(filePath, sheet);
                    sheet++;
                    ex.CreateNewsheet(name, sheet);
                    ex.Save();
                    ex.Close();
                    done = true;
                }
                else Createfile(path);
            } while (!done);
        }
        public void CreateDirectory()       //  funkcja tworząca folder
        {
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }
        }
    }
}
