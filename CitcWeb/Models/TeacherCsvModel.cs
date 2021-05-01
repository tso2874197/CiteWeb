using CitcWeb.Models.Csv;

namespace CitcWeb.Models
{
    public class TeacherCsvModel:CsvableBase
    {
        public string IdNum { get; set; }
        public string Name { get; set; }
        public string County { get; set; }
        public string PhoneNum { get; set; }
        public string MilNum { get; set; }
        public string Email { get; set; }
        public string PayBureauNum { get; set; }
        public string PayAccount { get; set; }
    }
}