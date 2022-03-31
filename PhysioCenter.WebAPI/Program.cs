using Microsoft.EntityFrameworkCore;

using PhysioCenter.Core.Contracts;
using PhysioCenter.Core.Services;
using PhysioCenter.Core.Services.Appointments;
using PhysioCenter.Infrastructure.Data;
using PhysioCenter.Infrastructure.Data.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
;
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddControllers();

builder.Services.AddScoped<IApplicationDbRepository, ApplicationDbRepository>();
builder.Services.AddTransient<IAppointmentsService, AppointmentsService>();
builder.Services.AddTransient<ITherapistsServicesService, TherapistsServicesService>();
builder.Services.AddScoped<IApplicationDbRepository, ApplicationDbRepository>();

builder.Services.AddCors(o => o.AddPolicy("Default", builder =>
{
    builder.WithOrigins("*")
           .AllowAnyMethod()
           .AllowAnyHeader();
}));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("Default");
app.UseAuthorization();

app.MapControllers();

app.Run();