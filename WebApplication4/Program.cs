using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApplication4.EfStuff;
using WebApplication4.EfStuff.DbModel;
using WebApplication4.EfStuff.Repositories;
using WebApplication4.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var connectString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=WebAppl12;Integrated Security=True;";
builder.Services.AddDbContext<WebContext>(x => x.UseSqlServer(connectString));
builder.Services.AddScoped<UserRepository>();
builder.Services.AddScoped<RoleRepository>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

RegisterMapper(builder.Services);

var app = builder.Build();

SeedExtention.Seed(app);

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
else
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
void RegisterMapper(IServiceCollection services)
{
    var provider = new MapperConfigurationExpression();

    provider.CreateMap<User, UserViewModel>()
        .ForMember(nameof(UserViewModel.Roles), opt => opt.MapFrom(db => db.Roles.Select(x => x.Name).ToList()));
    provider.CreateMap<UserViewModel, User>()
        .ForMember(nameof(User.Roles), opt => opt.Ignore());

    provider.CreateMap<Role, RoleViewModel>()
        .ForMember(nameof(RoleViewModel.Users), opt => opt.MapFrom(db => db.RoleUsers.Select(x => x.Name).ToList()));
    provider.CreateMap<RoleViewModel, Role>();

    var mapperConfiguration = new MapperConfiguration(provider);
    var mapper = new Mapper(mapperConfiguration);

    services.AddScoped<IMapper>(x => mapper);
}
