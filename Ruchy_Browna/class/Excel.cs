using Microsoft.Office.Interop.Excel;
using _Excel = Microsoft.Office.Interop.Excel;

namespace Ruchy_Browna
{
    class Excel
    {
        string path = "";
        _Application excel = new _Excel.Application();
        Workbook wb;
        Worksheet ws;
        public Excel()
        {
        }
        public Excel(string path, int sheet)        //  konstruktor klasy
        {
            this.path = path;
            wb = excel.Workbooks.Open(path);
            ws = wb.Worksheets[sheet];
        }
        public void CreateNewFile()     //  tworzenie nowego pliku
        {
            wb = excel.Workbooks.Add(XlWBATemplate.xlWBATWorksheet);
            ws = wb.Worksheets[1];
            ws.Name = "Wykresy";
        }
        public void CreateNewsheet(string name, int sheet)      //  tworzenie nowego arkusza
        {
            Worksheet tempSheet = wb.Worksheets.Add(After: ws);
            ws = wb.Worksheets[sheet];
            ws.Name = name;
        }
        public void WriteToCell(long i, long j, double d)       //  metoda do wpisawania liczb
        {
            i++;
            j++;
            ws.Cells[i, j].Value2 = d;
        }
        public void WriteToCell(int i, int j, string s)     //  metoda do wpisywania ciągów znaków
        {
            i++;
            j++;
            ws.Cells[i, j].Value2 = s;
        }
        public void Save()      //  metoda zapisująca plik
        {
            wb.Save();
        }
        public void SaveAs(string path)     //  metoda zapisz jako
        {
            wb.SaveAs(path);
        }
        public void Close()     //  metoda do zamykania pliku, bardzo ważna
        {
            wb.Close();
        }
    }
}
