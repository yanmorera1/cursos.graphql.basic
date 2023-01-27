using GraphQL.Server.Ui.Voyager;
using GraphQlDirector.Data;
using GraphQlDirector.Graphql;
using HotChocolate.AspNetCore.Playground;
using HotChocolate.AspNetCore;
using Microsoft.EntityFrameworkCore;
using GraphQlDirector.Graphql.DataVideo;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

builder.Services.AddPooledDbContextFactory<ApiDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("ConnectionString")));

builder.Services.AddGraphQLServer()
    .AddQueryType<Query>()
    .AddType<VideoType>()
    .AddProjections()
    .AddFiltering()
    .AddSorting();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UsePlayground(new PlaygroundOptions
    {
        QueryPath = "/graphql",
        Path = "/playground"
    });
}

app.UseRouting();

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapGraphQL();
});

app.UseGraphQLVoyager("graphql-ui", new VoyagerOptions
{
    GraphQLEndPoint = "/graphql"
});


app.MapControllers();

app.Run();
