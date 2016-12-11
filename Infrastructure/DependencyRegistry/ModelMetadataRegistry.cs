using System.Web.Mvc;
using StructureMap;
using StructureMap.Graph;
using StructureMap.Configuration.DSL;
using Kenrapid.CRM.Web.Infrastructure.ModelMetadata;

namespace Kenrapid.CRM.Web.Infrastructure.DependencyRegistry
{
    public class ModelMetadataRegistry :Registry
    {
        public ModelMetadataRegistry(){
            For<ModelMetadataProvider>().Use<ExtensibleModelMetadataProvider>();
            Scan(scan =>
            {
                scan.TheCallingAssembly();
                scan.AddAllTypesOf<IModelMetadataFilter>();
            });
        }
       
    }
}