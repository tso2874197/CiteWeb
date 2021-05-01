using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CitcWeb.Models.Csv
{
    public class CsvReader<T> where T : CsvableBase, new()
    {
        public IEnumerable<T> Read(string filePath, bool hasHeaders)
        {
            var objects = new List<T>();
            using (var sr = new StreamReader(filePath))
            {
                bool headersRead = false;
                string line;
                do
                {
                    line = sr.ReadLine();

                    if (line != null && headersRead)
                    {
                        var obj = new T();
                        var propertyValues = line.Split(',');
                        obj.AssignValuesFromCsv(propertyValues);
                        objects.Add(obj);
                    }
                    if (!headersRead)
                    {
                        headersRead = true;
                    }
                } while (line != null);
            }

            return objects;
        }

        public IEnumerable<T> Read(Stream stream, bool hasHeaders)
        {
            var objects = new List<T>();
            using (var sr = new StreamReader(stream,Encoding.Default))
            {
                bool headersRead = false;
                string line;
                do
                {
                    line = sr.ReadLine();
                    if (line != null && headersRead)
                    {
                        var obj = new T();
                        var propertyValues = line.Split(',');
                        obj.AssignValuesFromCsv(propertyValues);
                        objects.Add(obj);
                    }
                    if (!headersRead)
                    {
                        headersRead = true;
                    }
                } while (line != null);
            }
            return objects;
        }
    }
}