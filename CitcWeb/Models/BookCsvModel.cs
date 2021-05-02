using CitcWeb.Models.Csv;

namespace CitcWeb.Models
{
    public class BookCsvModel:CsvableBase
    {
        public string BookName { get; set; }
        public string BookNumber { get; set; }
    }
}