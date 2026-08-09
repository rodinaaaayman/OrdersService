using FluentValidation;
using FluentValidation.AspNetCore;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using OrdersService.Api.ExceptionHandlers;
using OrdersService.Application.Abstractions;
using OrdersService.Application.Behaviors;
using OrdersService.Application.FluentValidation;
using OrdersService.Application.Interfaces;
using OrdersService.Application.Services.orders;
using OrdersService.Application.Services.orders.Commands.CancelOrder;
using OrdersService.Application.Services.orders.Commands.PlaceOrder;
using OrdersService.Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddValidatorsFromAssemblyContaining<OrdersFluentValidation>();
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddScoped<IApplicationDbContext>(provider =>
    provider.GetRequiredService<AppDbContext>());
builder.Services.AddProblemDetails();
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddMemoryCache();
builder.Services.AddAuthorization();
builder.Services.AddControllers();
builder.Services.AddHttpContextAccessor();
builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(typeof(Program).Assembly);
    cfg.RegisterServicesFromAssembly(typeof(PlaceOrderCommand).Assembly);
    cfg.RegisterServicesFromAssembly(typeof(CancelOrderCommand).Assembly);
    cfg.RegisterServicesFromAssembly(typeof(GetOrderByIdQueryHandler).Assembly);
    cfg.AddOpenBehavior(typeof(TransactionBehavior<,>));
});
builder.Services.AddSingleton<IClientVerificationService, RabbitMqClientVerificationService>();
builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<OrderFilledConsumer>();

    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host("localhost", "/", h =>
        {
            h.Username("admin");
            h.Password("admin123");
        });

        cfg.ConfigureEndpoints(context);
    });
});
var app = builder.Build();
app.UseExceptionHandler();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
;

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();



app.Run();
public partial class Program { }

