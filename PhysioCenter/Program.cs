using PhysioCenter.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDatabase(builder.Configuration)
                .AddDatabaseDeveloperPageExceptionFilter()
                .AddIdentity()
                .AddApplicationControllers()
                .AddAutoMapper(typeof(Program))
                .AddCloudinary(builder.Configuration)
                .AddCookiePolicyOptions()
                .AddApplicationServices()
                .AddEndpointsApiExplorer()
                .AddSwaggerGen();

var app = builder.Build();

app.SeedData();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage()
       .UseMigrationsEndPoint()
       .UseSwagger()
       .UseSwaggerUI();
}
else
{
    app.UseStatusCodePagesWithRedirects("/Home/Error?statusCode={0}")
       .UseExceptionHandler("/Home/Error")
       .UseHsts();
}

app.UseHttpsRedirection()
   .UseStaticFiles()
   .UseCookiePolicy()
   .UseRouting()
   .UseAuthentication()
   .UseAuthorization()
   .UseMyEndPoints();

app.Run();