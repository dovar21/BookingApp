namespace Booking.API.Infrastructure.AutofacModules;

using Autofac;
using Booking.API.Application.Queries;
using Booking.Domain.AggregatesModel.ServiceObjectAggregate;
using Booking.Infrastructure.Repositories;

public class ApplicationModule : Module
{
    public string QueriesConnectionString { get; }

    public ApplicationModule(string qconstr)
    {
        QueriesConnectionString = qconstr;
    }
    protected override void Load(ContainerBuilder builder)
    {

        builder.RegisterType<ServiceObjectQueries>()
        .As<IServiceObjectQueries>()
        .InstancePerLifetimeScope();

        builder.RegisterType<ServiceObjectRepository>()
        .As<IServiceObjectRepository>()
        .InstancePerLifetimeScope();
    }
}
