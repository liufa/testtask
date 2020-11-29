using DbfTests;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace DbfTestTask
{
    public class Aggregator
    {

        public IEnumerable<string> GetDbfFileListFromBaseDir(string baseDir, string filename)
        {
            //When tests executed path starts in bin so one needs to go couple of levels up.
            return Directory.GetFiles($"..\\..\\{baseDir}", filename, SearchOption.AllDirectories);
        }

        internal IEnumerable<List<DbfReader.ValueRow>> ReadDbfFiles(IEnumerable<string> fileList)
        {
            var reader = new DbfReader();
            var result = fileList.Select(o => reader.ReadValues(o));

            //Instead of getting 27 values of min date 633036852000000000, Assert.AreEqual(27, outputs[0].Values.Count); I am getting only one.
            //The bit of code below shows it, so ether test data is incorrect, or I am missing something and likely failed the test at this point.

            //So this bit normally wouldn't be here.
            int counter = 0;
            foreach (var fileContents in result)
            {
                foreach (var row in fileContents)
                {
                    if (row.Timestamp.Ticks == 633036852000000000)
                        counter++;
                }
            }

            return result;
        }

        internal void AddHeadersToOutputRow(IEnumerable<string> fileList)
        {
            OutputRow.Headers.AddRange(fileList);
        }

        internal IEnumerable<OutputRow> MergeOrderAndTransformDbfContents(IEnumerable<List<DbfReader.ValueRow>> dbfFileContents)
        {
            foreach (var dbfFileContentRowGrouping in dbfFileContents.SelectMany(o => o).GroupBy(o => o.Timestamp).OrderBy(o => o.Key))
            {
                var outputRow = new OutputRow { Timestamp = dbfFileContentRowGrouping.Key };
                outputRow.Values.AddRange(dbfFileContentRowGrouping.Select(o => (double?)o.Value));
                yield return outputRow;
            }
        }
    }
}
