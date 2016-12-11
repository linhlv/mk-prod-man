using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StructureMap;
using StructureMap.Configuration.DSL;
using StructureMap.Graph;
using StructureMap.Pipeline;
using StructureMap.TypeRules;

namespace Kenrapid.CRM.Web.Infrastructure.DependencyRegistry
{
    public class ActionFilterRegistry : Registry
    {
        public ActionFilterRegistry(Func<IContainer> containerFactory)
        {
            //Inject for Filter Attributes
            For<IFilterProvider>().Use(new StructureMapFilterProvider(containerFactory));

            SetAllProperties(
                x => x.Matching(p =>
                        p.DeclaringType.CanBeCastTo(typeof(ActionFilterAttribute)) &&
                        p.DeclaringType.Namespace.StartsWith("Kenrapid.CRM.Web") &&
                        !p.PropertyType.IsPrimitive &&
                        p.PropertyType != typeof(string)
                )
            );
        }
    }
}