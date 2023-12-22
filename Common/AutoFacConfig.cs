using Autofac;
using Autofac.Extensions.DependencyInjection;
using FoodDonationAPI.Data;
using FoodDonationAPI.Models;
using FoodDonationAPI.Repositories;
using FoodDonationAPI.Repositories.IRepositories;
using FoodDonationAPI.Services;
using FoodDonationAPI.Services.Services;
using Microsoft.AspNetCore.Identity;
using System.Web.Http.Dependencies;

namespace FoodDonationAPI.Common
{
    public class AutoFacConfig : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            //builder.RegisterType<TestService>().As<ITest>();

            //builder.RegisterType<UserManager<IdentityUser>>().AsSelf().SingleInstance();

            //Repositories
            builder.RegisterAssemblyTypes(typeof(FoodDonationAPI.Repositories.TestRepository).Assembly)
                .Where(t => t.Name.EndsWith("Repository"))
                .AsImplementedInterfaces().InstancePerLifetimeScope();

            // Services
            builder.RegisterAssemblyTypes(typeof(FoodDonationAPI.Services.Services.TestService).Assembly)
               .Where(t => t.Name.EndsWith("Service"))
               .AsImplementedInterfaces().InstancePerLifetimeScope();

            //IContainer container = builder.Build();

            //DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
            //GlobalConfiguration.Configuration.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }
    }
}
