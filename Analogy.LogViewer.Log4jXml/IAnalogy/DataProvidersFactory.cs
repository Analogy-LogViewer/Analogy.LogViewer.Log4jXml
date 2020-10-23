using System;
using System.Collections.Generic;
using Analogy.Interfaces;

namespace Analogy.LogViewer.Log4jXml.IAnalogy
{
    public class DataProvidersFactory : LogViewer.Template.DataProvidersFactory
    {
        public override Guid FactoryId { get; set; } = PrimaryFactory.Id;
        public override string Title { get; set; } = "Log4jXml Log Parser";
        public override IEnumerable<IAnalogyDataProvider> DataProviders { get; set; } = new List<IAnalogyDataProvider> { new OfflineDataProvider() };
    }
}
