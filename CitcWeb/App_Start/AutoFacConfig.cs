using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;

namespace CitcWeb.App_Start
{
    public class AutoFacConfig
    {
        public static void Register()
        {
            var builder = new ContainerBuilder();
            RegisterMvcComponent(builder);
            SetMvcResolver(builder);
        }

        private static void SetMvcResolver(ContainerBuilder builder)
        {
            var container = builder.Build();
            var mvcResolver = new AutofacDependencyResolver(container);
            DependencyResolver.SetResolver(mvcResolver);
        }

        private static void RegisterMvcComponent(ContainerBuilder builder)
        {
            builder.RegisterControllers(typeof(MvcApplication).Assembly);
            builder.RegisterModelBinders(typeof(MvcApplication).Assembly);
            builder.RegisterModelBinderProvider();
            builder.RegisterFilterProvider();
        }
    }
}