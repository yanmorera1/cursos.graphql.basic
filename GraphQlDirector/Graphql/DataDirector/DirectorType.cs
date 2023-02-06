using GraphQlDirector.Data;
using GraphQlDirector.Models;

namespace GraphQlDirector.Graphql.DataDirector
{
    public class DirectorType : ObjectType<Director>
    {
        protected override void Configure(IObjectTypeDescriptor<Director> descriptor)
        {
            descriptor.Description("Este modelo representa la data del director");

            descriptor.Field(x => x.Videos)
                .ResolveWith<Resolvers>(x => x.GetVideos(default!, default!))
                .UseDbContext<ApiDbContext>()
                .Description("Representa la coleccion de videos de este director");

            descriptor.Field("nombreCompleto")
                .ResolveWith<Resolvers>(p => p.GetNombreCompleto(default!, default))
                .UseDbContext<ApiDbContext>()
                .Description("El nombre completo del director");

            //descriptor.Ignore(x => x.Id);
        }

        private class Resolvers
        {
            public IQueryable<Video> GetVideos([Parent] Director director, [ScopedService] ApiDbContext context) => context.Videos!.Where(x => x.DirectorId == director.Id);

            public string GetNombreCompleto([Parent] Director director, [ScopedService] ApiDbContext context) => $"{director.Nombre} {director.Apellido}";
        }
    }
}
