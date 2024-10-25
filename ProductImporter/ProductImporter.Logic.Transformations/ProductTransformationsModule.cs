using Autofac;
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

            builder.RegisterType<ProductTransformationContext>()
                .As<IProductTransformationContext>()
                .InstancePerLifetimeScope();

            builder.RegisterType<NameDecapitaliser>()
                .As<IProductTransformation>()
                .InstancePerLifetimeScope();

            builder.RegisterType<ProductTransformationContext>()
                .As<IProductTransformationContext>()
                .InstancePerLifetimeScope();

            if (_options.EnableCurrencyNormalizer)
            {
                builder.RegisterType<NameDecapitaliser>()
                    .As<IProductTransformation>()
                    .InstancePerLifetimeScope();
            }

            builder.RegisterType<DateTimeProvider>()
                .As<IDateTimeProvider>()
                .InstancePerLifetimeScope();

            builder.RegisterType<ReferenceAdder>()
                .As<IProductTransformation>()
                .InstancePerLifetimeScope();

            builder.RegisterType<ReferenceGeneratorFactory>()
                .As<IReferenceGeneratorFactory>()
                .InstancePerLifetimeScope();

            builder.RegisterType<InternalCounter>()
                .As<IInternalCounter>()
                .SingleInstance();
        }
    }
}
