using DbfTests;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace DbfTestTask
{
    public class Aggregator
    {
        internal class HeaderAndRecord
        {
            internal HeaderAndRecord(string header, List<DbfReader.ValueRow> record)
            {
                this.Header = header;
                this.Record = record;
            }

            internal string Header { get; }

            internal List<DbfReader.ValueRow> Record { get; }
        }

        internal IEnumerable<string> GetDbfFileListFromBaseDir(string baseDir, string filename)
        {
            //When tests executed path starts in bin so one needs to go couple of levels up.
            return Directory.GetFiles($"..\\..\\{baseDir}", filename, SearchOption.AllDirectories);
        }

        internal IEnumerable<HeaderAndRecord> ReadDbfFiles(IEnumerable<string> fileList)
        {
            var reader = new DbfReader();
            return fileList.Select(o => new HeaderAndRecord(o, reader.ReadValues(o)));
        }

        internal IEnumerable<OutputRow> MergeOrderAndTransformDbfContents(
            IEnumerable<HeaderAndRecord> dbfFileContents)
        {
            var result = new ConcurrentQueue<OutputRow>();
            var dbfFileContentsAsList = dbfFileContents.ToList();
            for (int i = 0; i < dbfFileContentsAsList.Count; i++)
            {
                OutputRow.Headers.Add(dbfFileContentsAsList[i].Header);
                Parallel.ForEach(dbfFileContentsAsList[i].Record, record =>
                {
                    if (result.Any(o => o.Timestamp == record.Timestamp))
                    {
                        var dateRecord = result.Single(o => o.Timestamp == record.Timestamp);

                        dateRecord.Values.AddRange(Enumerable.Repeat<double?>(null,
                            Math.Max(0, i - dateRecord.Values.Count)));
                        dateRecord.Values.Add(record.Value);
                    }
                    else
                    {
                        result.Enqueue(new OutputRow
                        {
                            Timestamp = record.Timestamp,
                            Values = new List<double?>(Enumerable.Repeat<double?>(null, Math.Max(0, i))) {record.Value}
                        });
                    }
                });
            }

            Parallel.ForEach(result, outputRow =>
            {
                if (outputRow.Values.Count < 27)
                {
                    outputRow.Values.AddRange(Enumerable.Repeat<double?>(null, 27 - outputRow.Values.Count));
                }
            });

            return result.OrderBy(o => o.Timestamp);
        }
    }
}
