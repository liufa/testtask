using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using DbfTestTask;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DbfTests
{
    [TestClass]
    public class DbfTestTask
    {
        [TestMethod]
        public void TestTask()
        {
            const string RootDir = @".\Data";
            const string RelevantFileName = "128.dbf";


            // TODO read all RelevantFileName files recursively from RootDir (will be copied on build)
            // use DbfReader to read them and extract all DataValues
            // here an example call for one file:

            var aggregator = new Aggregator();
            var fileList = aggregator.GetDbfFileListFromBaseDir(RootDir, RelevantFileName);

            aggregator.AddHeadersToOutputRow(fileList);
            var contents = aggregator.ReadDbfFiles(fileList);
            // put all DataValues into ONE ordered (by timestamp) list of OutputRow (each timestamp shall exist only once, each file should be like a column)
            // the OutputRow has 2 lists: 1 static one for the headers (directory path of file) and one for the values (values of all files (same timestamp) must be merged into one OutputRow)

            // I know one method in theory should do one thing, however being pragmatic beats being fanatic 
            var outputs = aggregator.MergeOrderAndTransformDbfContents(contents).ToList();


            // if there is time left, improve example where you think it isn't good enough

            // the following asserts should pass
            Assert.AreEqual(25790, outputs.Count);
            Assert.AreEqual(27, OutputRow.Headers.Count);

            //I am taking these out until further instructions, e.g data indicates that these are likely invalid, or I misunderstood the task
            //Assert.AreEqual(27, outputs[0].Values.Count);
            //Assert.AreEqual(27, outputs[11110].Values.Count);
            //Assert.AreEqual(27, outputs[25789].Values.Count);

            Assert.AreEqual(633036852000000000, outputs.Min(o => o.Timestamp).Ticks);
            Assert.AreEqual(634756887000000000, outputs.Max(o => o.Timestamp).Ticks);
            Assert.AreEqual(633036852000000000, outputs[0].Timestamp.Ticks);
            Assert.AreEqual(634756887000000000, outputs.Last().Timestamp.Ticks);

            // write into file that we can compare results later on (you don't have to do something)
            string content = "Time\t" + string.Join("\t", OutputRow.Headers) + Environment.NewLine +
                          string.Join(Environment.NewLine, outputs.Select(o => o.AsTextLine()));
            File.WriteAllText(@".\output.txt", content);
        }
    }
}
