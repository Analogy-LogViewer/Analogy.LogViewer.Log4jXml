using Analogy.Interfaces;
using Analogy.Interfaces.WinForms;
using System;
using System.Collections.Generic;

namespace Analogy.LogViewer.Log4jXml.IAnalogy
{
    public class DataProvidersFactory : LogViewer.Template.DataProvidersFactoryWinForms
    {
        public override Guid FactoryId { get; set; } = PrimaryFactory.Id;
        public override string Title { get; set; } = "Log4jXml Log Parser";
        public override IEnumerable<IAnalogyDataProviderWinForms> DataProviders { get; set; } = new List<IAnalogyDataProviderWinForms> { new OfflineDataProvider() };
    }
}