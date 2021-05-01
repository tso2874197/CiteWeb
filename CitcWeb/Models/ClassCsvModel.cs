using CitcWeb.Models.Csv;

namespace CitcWeb.Models
{
    public class ClassCsvModel:CsvableBase
    {
        public string ClassName { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
    }
}