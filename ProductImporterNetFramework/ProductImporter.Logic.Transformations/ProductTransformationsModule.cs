using Autofac;
using Autofac.Extras.DynamicProxy;
using ProductImporter.Logic.Transformations.Util;

namespace ProductImporter.Logic.Transformations
{
    public class ProductTransformationsModule : Module
    {
        private readonly ProductTransformationOptions _options;

        public ProductTransformationsModule(ProductTransformationOptions options)
        {
            _options = options;
        }

        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.Register(_ => new SystemOutLogger());

            builder.RegisterAssemblyTypes(typeof(ProductTransformationsModule).Assembly)
                .Where(type => type.IsAssignableTo<IProductTransformation>())
                .InstancePerLifetimeScope()
                .AsImplementedInterfaces()
                .EnableInterfaceInterceptors()
                .InterceptedBy(typeof(SystemOutLogger));

            builder.RegisterType<ProductTransformationContext>()
                .As<IProductTransformationContext>()
                .InstancePerLifetimeScope()
                .EnableInterfaceInterceptors()
                .InterceptedBy(typeof(SystemOutLogger));

            builder.RegisterType<DateTimeProvider>()
                .As<IDateTimeProvider>()
                .InstancePerLifetimeScope()
                .EnableInterfaceInterceptors()
                .InterceptedBy(typeof(SystemOutLogger));

            builder.RegisterType<ReferenceGeneratorFactory>()
                .As<IReferenceGeneratorFactory>()
                .InstancePerLifetimeScope()
                .EnableInterfaceInterceptors()
                .InterceptedBy(typeof(SystemOutLogger));

            builder.RegisterType<InternalCounter>()
                .As<IInternalCounter>()
                .SingleInstance()
                .EnableInterfaceInterceptors()
                .InterceptedBy(typeof(SystemOutLogger));
        }
    }
}
