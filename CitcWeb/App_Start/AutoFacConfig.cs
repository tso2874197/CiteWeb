using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;
using CitcWeb.Repository;
using CitcWeb.Repository.Base;
using CitcWeb.Repository.Interface;
using CitcWeb.Services;
using CitcWeb.Services.Interface;

namespace CitcWeb.App_Start
{
    public class AutoFacConfig
    {
        public static void Register()
        {
            var builder = new ContainerBuilder();
            RegisterMvcComponent(builder);
            RegisterGeneralComponent(builder);
            RegisterRepositories(builder);
            RegisterServices(builder);
            SetMvcResolver(builder);
        }

        private static void RegisterServices(ContainerBuilder builder)
        {
            builder.RegisterType<LifePictureService>().As<ILifePictureService>().InstancePerRequest();
            builder.RegisterType<ClassInfoService>().As<IClassInfoService>().InstancePerRequest();
            builder.RegisterType<CourseService>().As<ICourseService>().InstancePerRequest();
            builder.RegisterType<TeacherService>().As<ITeacherService>().InstancePerRequest();


        }

        private static void RegisterRepositories(ContainerBuilder builder)
        {
            builder.RegisterType<LifePictureRepository>().As<ILifePictureRepository>().InstancePerRequest();
            builder.RegisterType<StudentReportRepository>().As<IStudentTopicRepository>().InstancePerRequest();
            builder.RegisterType<ClassInfoRepository>().As<IClassInfoRepository>().InstancePerRequest();
            builder.RegisterType<TeacherRepository>().As<ITeacherRepository>().InstancePerRequest();
            builder.RegisterType<PayRankRepository>().As<IPayRankRepository>().InstancePerRequest();
            builder.RegisterType<CourseRepository>().As<ICourseRepository>().InstancePerRequest();
            builder.RegisterType<AnnualCourseRepository>().As<IAnnualCourseRepository>().InstancePerRequest();
        }

        private static void RegisterGeneralComponent(ContainerBuilder builder)
        {
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerRequest();
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