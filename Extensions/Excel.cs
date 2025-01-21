using System.Data;
using System.Reflection;
using ClosedXML;
using ClosedXML.Excel;
using DocumentFormat.OpenXml.Office.PowerPoint.Y2021.M06.Main;
using DocumentFormat.OpenXml.Presentation;
namespace smartclinic.Extensions
{
    public static class Excel
    {
        private static XLWorkbook wb = new XLWorkbook();
        #region  SheetsMedtodes

        #region AddSheet
        public static void AddSheet(this XLWorkbook wb){
            wb.Worksheets.Add();
        }
        public static void AddSheet(this XLWorkbook wb, string sheetName){
            wb.Worksheets.Add(sheetName);
        }
        public static void AddSheet(this XLWorkbook wb, string sheetName,int nosheet){
            wb.Worksheets.Add(sheetName,nosheet);
        }
        #endregion
        #endregion
        #region WriteData
        public static void WriteDataByDataTable(this XLWorkbook wb,  DataTable value){
            var sheet = wb.Worksheet(1);
           for (int i = 0; i < value.Columns.Count; i++)
            {
                sheet.Cell(1, i + 1).Value = value.Columns[i].ColumnName;
            }
            sheet.Cell(2,1).InsertData(value);
        }
        public static void WriteDataByList<T>(this XLWorkbook wb,  List<T> value){
            var sheet = wb.Worksheet(1);
            var properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            var i = 0;
            foreach (var prop in properties)
            {
            sheet.Cell(1, i + 1).Value = prop.Name;
            i++;
            }
            sheet.Cell(2,1).InsertData(value);
        }
        public static void WriteDataByList<T>(this XLWorkbook wb, string startcell, List<T> value){
            wb.Worksheet(1).Cell(startcell).InsertData(ToDataTable<T>(value));
        }
        
        #endregion
        #region ToStream
        public static MemoryStream ToFile(this XLWorkbook wb){
            using (var stream = new MemoryStream()){
                wb.SaveAs(stream);
                stream.Seek(0, SeekOrigin.Begin);
                return stream;
            }
            
        }
        #endregion
        #region Service
         public static DataTable ToDataTable<T>(this List<T> data)
         {
        DataTable table = new DataTable();
        var properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
        foreach (var prop in properties)
        {
            table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
        }
        foreach (var item in data)
        {
            var values = new object[properties.Length];
            for (int i = 0; i < properties.Length; i++)
            {
                values[i] = properties[i].GetValue(item, null);
            }
            table.Rows.Add(values);
        }

        return table;
        }
        private static void WriteColumns(DataTable table){
            for (int i = 0; i < table.Columns.Count; i++)
            {
                wb.Worksheet(1).Cell(1, i + 1).Value = table.Columns[i].ColumnName;
            }
        }
        #endregion
    }

}
