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
            return fileList.Select(o => reader.ReadValues(o));
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
