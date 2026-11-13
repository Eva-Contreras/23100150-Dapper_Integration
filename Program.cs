using Microsoft.AspNetCore.Mvc;
using Orders.Api.context;
using Orders.Api.helpers;
using Orders.Api.models.dtos;
using Orders.Api.Models;
using Orders.Api.repositories;
using Orders.Api.services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddOptions<StudentInfo>()
    .BindConfiguration(StudentInfo.SectionName);
builder.Services.AddSingleton<SqlDapperContext>();
builder.Services.AddScoped<IDapperService, DapperService>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IOrderService,OrderService>();
builder.Services.AddScoped<IStudentService, StudentService>();
builder.Services.AddControllers()
    .ConfigureApiBehaviorOptions(options =>
        {
            options.InvalidModelStateResponseFactory = context =>
                new BadRequestObjectResult(new ApiErrorResponse(
                    StatusCodes.Status400BadRequest,
                    "The request contains invalid data.",
                    ValidationErrors.FromModelState(context.ModelState)));
        });
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/openapi/v1.json", "Orders.Api");
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
