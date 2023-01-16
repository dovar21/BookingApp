using Autofac;
using Autofac.Extensions.DependencyInjection;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Booking.API.Application.AutoMapper;
using Booking.API.Controllers;
using Booking.API.Infrastructure.AutofacModules;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers()
    .AddApplicationPart(typeof(ServicesController).Assembly)
            .AddJsonOptions(options => options.JsonSerializerOptions.WriteIndented = true);

builder.Services.AddDbContext<Booking.Infrastructure.BookingDbContext>(
    options =>
    {
        options.UseSqlite(builder.Configuration.GetConnectionString("BookingDb"), b => b.MigrationsAssembly("Booking.API"));
    },
    ServiceLifetime.Scoped 
);

var mappingConfig = AutoMapperConfig.RegisterMappings();

IMapper mapper = mappingConfig.CreateMapper();
builder.Services.AddSingleton(mapper);

builder.Services.AddControllers().AddJsonOptions(opts => {
    opts.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Host.ConfigureContainer<ContainerBuilder>(cb=> cb.RegisterModule(new MediatorModule()));
builder.Host.ConfigureContainer<ContainerBuilder>(cb=> cb.RegisterModule(new ApplicationModule(builder.Configuration.GetConnectionString("BookingDb"))));

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapControllers();

app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
    options.RoutePrefix = string.Empty;
});

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.Run();
