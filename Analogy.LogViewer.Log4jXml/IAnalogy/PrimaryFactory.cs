using System;
using System.Collections.Generic;
using System.Drawing;
using Analogy.Interfaces;
using Analogy.LogViewer.Log4jXml.Properties;

namespace Analogy.LogViewer.Log4jXml.IAnalogy
{
    public class PrimaryFactory : Analogy.LogViewer.Template.PrimaryFactory
    {
        internal static Guid Id { get; }= new Guid("22c1be23-1198-41af-a67c-5b636ad619d6");
        public override Guid FactoryId { get; set; } = Id;
        public override string Title { get; set; } = "Log4jXml";
        public override IEnumerable<IAnalogyChangeLog> ChangeLog { get; set; } = ChangeLogList.GetChangeLog();
        public override IEnumerable<string> Contributors { get; set; } = new List<string> { "Lior Banai" };
        public override string About { get; set; } = "Analogy Log Parser for Log4jXml";
        public override Image? SmallImage { get; set; } = Resources.Logazmic16x16;
        public override Image? LargeImage { get; set; } = Resources.Logazmic32x32;


    }
}
