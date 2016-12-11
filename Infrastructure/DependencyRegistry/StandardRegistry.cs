using StructureMap.Configuration.DSL;
using StructureMap.Graph;

namespace Kenrapid.CRM.Web.Infrastructure.DependencyRegistry
{
	public class StandardRegistry : Registry
	{
        public StandardRegistry()
        {
            Scan(scan =>
            {
                scan.TheCallingAssembly();
                scan.WithDefaultConventions();
            });
        }
	}
}