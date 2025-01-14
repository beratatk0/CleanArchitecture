﻿using Autofac;
using AutoMapper;
using Clean.Core.Repositories;
using Clean.Core.Services;
using Clean.Core.UnitOfWorks;
using Clean.Repo;
using Clean.Repo.Repositories;
using Clean.Repo.UnitOfWork;
using Clean.Service.Mapper;
using Clean.Service.Services;
using System.Reflection;
using Module = Autofac.Module;
namespace Clean.Web.Modules
{
    public class RepoServiceModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterGeneric(typeof(GenericRepository<>)).As(typeof(IGenericRepository<>));
            builder.RegisterGeneric(typeof(Service<>)).As(typeof(IService<>));
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>();
            builder.RegisterType<ProductServiceWithNoCaching>().As<IProductService>();

            var mvcAssembly = Assembly.GetExecutingAssembly();
            var repoAssembly = Assembly.GetAssembly(typeof(AppDbContext));
            var serviceAssembly = Assembly.GetAssembly(typeof(MapProfile));
            builder.RegisterAssemblyTypes(mvcAssembly, repoAssembly, serviceAssembly).Where(x => x.Name.EndsWith("Repository")).AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.RegisterAssemblyTypes(mvcAssembly, repoAssembly, serviceAssembly).Where(x => x.Name.EndsWith("Service")).AsImplementedInterfaces().InstancePerLifetimeScope();
        }
    }
}
