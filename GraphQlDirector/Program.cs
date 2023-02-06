using GraphQL.Server.Ui.Voyager;
using GraphQlDirector.Data;
using GraphQlDirector.Graphql;
using HotChocolate.AspNetCore.Playground;
using HotChocolate.AspNetCore;
using Microsoft.EntityFrameworkCore;
using GraphQlDirector.Graphql.DataVideo;
using GraphQlDirector.Graphql.DataDirector;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

//DbContext added into a pooled factory
builder.Services.AddPooledDbContextFactory<ApiDbContext>
    (options => options.UseSqlServer(builder.Configuration.GetConnectionString("ConnectionString")));

Type[] types =
{
    typeof(VideoType),
    typeof(DirectorType)
};

builder.Services.AddGraphQLServer()
    .AddQueryType<Query>()
    .AddTypes(types)
    .AddProjections()
    .AddMutationType<Mutation>()
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

//Configuring voyager (graphql interface)
app.UseGraphQLVoyager("graphql-ui", new VoyagerOptions
{
    GraphQLEndPoint = "/graphql"
});


app.MapControllers();

app.Run();
