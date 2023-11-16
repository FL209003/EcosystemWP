using AccessLogic.Repositories;
using AppLogic.UCInterfaces;
using AppLogic.UseCases;
using Domain.RepositoryInterfaces;
using Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddSession(options => {
    options.IdleTimeout = TimeSpan.FromMinutes(120);
    options.Cookie.IsEssential = true;
    options.Cookie.HttpOnly = true;
    options.Cookie.Name = "EcosystemApp";
});

// Users
//builder.Services.AddScoped<IRepositoryUsers, UsersRepository>();
//builder.Services.AddScoped<IAddUser, AddUserUC>();
//builder.Services.AddScoped<IFindUser, FindUserUC>();

//// Ecosystems
//builder.Services.AddScoped<IRepositoryEcosystems, EcosystemsRepository>();
//builder.Services.AddScoped<IAddEcosystem, AddEcoUC>();
//builder.Services.AddScoped<IRemoveEcosystem, RemoveEcoUC>();
//builder.Services.AddScoped<IListEcosystem, ListEcosUC>();
//builder.Services.AddScoped<IFindEcosystem, FindEcoUC>();

//// Species
//builder.Services.AddScoped<IRepositorySpecies, SpeciesRepository>();
//builder.Services.AddScoped<IAddSpecies, AddSpeciesUC>();
//builder.Services.AddScoped<IListSpecies, ListSpeciesUC>();
//builder.Services.AddScoped<IRemoveSpecies, RemoveSpeciesUC>();
//builder.Services.AddScoped<IFindSpecies, FindSpeciesUC>();
//builder.Services.AddScoped<IUpdateSpecies, UpdateSpeciesUC>();

//// Threats
//builder.Services.AddScoped<IRepositoryThreats, ThreatsRepository>();
//builder.Services.AddScoped<IAddThreat, AddThreatUC>();
//builder.Services.AddScoped<IListThreats, ListThreatsUC>();
//builder.Services.AddScoped<IFindThreat, FindThreatUC>();

////Countries
//builder.Services.AddScoped<IRepositoryCountries, CountriesRepository>();
//builder.Services.AddScoped<IListCountries, ListCountriesUC>();
//builder.Services.AddScoped<IFindCountry, FindCountryUC>();

////Conservation
//builder.Services.AddScoped<IRepositoryConservations, ConservationsRepository>();
//builder.Services.AddScoped<IFindConservation, FindConservationUC>();

// DB config
ConfigurationBuilder configurationBuilder = new();
configurationBuilder.AddJsonFile("appsettings.json", false, true);
var config = configurationBuilder.Build();
string connectionString = config.GetConnectionString("Connection1");
builder.Services.AddDbContextPool<EcosystemContext>(Options => Options.UseSqlServer(connectionString));

// Params
builder.Services.AddScoped<IRepositoryParams, ParamsRepository>();
builder.Services.AddScoped<IModifyLengthParam, ModifyLengthParamUC>();
DbContextOptionsBuilder<EcosystemContext> b = new();
b.UseSqlServer(connectionString);
var options = b.Options;
EcosystemContext context = new(options);
ParamsRepository repo = new(context);

Name.MinNameLength = int.Parse(repo.FindValue("MinNameLength"));
Name.MaxNameLength = int.Parse(repo.FindValue("MaxNameLength"));
Description.MinDescLength = int.Parse(repo.FindValue("MinDescLength"));
Description.MaxDescLength = int.Parse(repo.FindValue("MaxDescLength"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();
app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
