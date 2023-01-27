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
        }

        private class Resolvers
        {
            public IQueryable<Video> GetVideos(Director director, [ScopedService] ApiDbContext context)
            {
                return context.Videos.Where(x => x.DirectorId == director.Id);
            }
        }
    }
}
