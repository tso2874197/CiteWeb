using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CitcWeb.Models.Csv
{
	public class CsvWriter<T> where T : CsvableBase
    {
        public void Write(IEnumerable<T> objects, string destination)
        {
            var objs = objects as IList<T> ?? objects.ToList();
            if (objs.Any())
            {
                using (var sw = new StreamWriter(destination))
                {
                    foreach (var obj in objs)
                    {
                        sw.WriteLine(obj.ToCsv());
                    }
                }
            }
        }

        public void Write(IEnumerable<T> objects, string destination,
            string[] propertyNames, bool isIgnore)
        {
            var objs = objects as IList<T> ?? objects.ToList();
            if (objs.Any())
            {
                using (var sw = new StreamWriter(destination))
                {
                    foreach (var obj in objs)
                    {
                        sw.WriteLine(obj.ToCsv(propertyNames, isIgnore));
                    }
                }
            }
        }

        public void Write(IEnumerable<T> objects, string destination,
            int[] propertyIndexes, bool isIgnore)
        {
            var objs = objects as IList<T> ?? objects.ToList();
            if (objs.Any())
            {
                using (var sw = new StreamWriter(destination))
                {
                    foreach (var obj in objs)
                    {
                        sw.WriteLine(obj.ToCsv(propertyIndexes, isIgnore));
                    }
                }
            }
        }
    }
}