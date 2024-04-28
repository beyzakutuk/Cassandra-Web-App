using Cassandra;
using CassandraWebExam.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSingleton(opt =>
{
    var cluster = Cluster.Builder()
                .AddContactPoint("localhost")
                .WithPort(1453).Build();
    var session = cluster.Connect();
    session.Execute("CREATE KEYSPACE IF NOT EXISTS my_keyspace WITH replication = {'class': 'SimpleStrategy', 'replication_factor': '1'}");

    session.Execute("CREATE TABLE IF NOT EXISTS my_keyspace.users (" +
                        "id UUID PRIMARY KEY," +
                        "username TEXT," +
                        "password TEXT," +
                        "age INT," +
                        "email TEXT," +
                        "address TEXT)");


    return session;

});
builder.Services.AddSingleton<IDbContext, DBContext>();
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

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

