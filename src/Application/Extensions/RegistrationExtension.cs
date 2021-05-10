using Autofac;
using MediatR;

namespace Application.Extensions
{
    public static class DependencyRegistration
    {
        // Register all dependencies here:
        public static void Register(this ContainerBuilder builder)
        {
            builder.RegisterType<Mediator>()
                .As<IMediator>()
                .InstancePerLifetimeScope();

            builder.Register<ServiceFactory>(context =>
            {
                var c = context.Resolve<IComponentContext>();
                return t => c.Resolve(t);
            });

            // builder.RegisterAssemblyTypes(typeof(Application).GetTypeInfo().Assembly);
        }
    }
}