using Application.Repositries;
using Application.Services;
using Autofac;

namespace Application.Extensions
{
    public static class DependencyRegistration
    {
        // Register all dependencies here:
        public static void Register(this ContainerBuilder builder)
        {
            builder.RegisterType<NodeRepository>().As<INodeRepository>();
            builder.RegisterType<ReadingRepository>().As<IReadingRepository>();
            builder.RegisterType<NodeService>().As<INodeService>();
        }
    }
}