using GraphQlDirector.Data;
using GraphQlDirector.Graphql.Streamers;
using GraphQlDirector.Models;

namespace GraphQlDirector.Graphql
{
    public class Mutation
    {
        [UseDbContext(typeof(ApiDbContext))]
        public async Task<AddStreamerPayload> AddStreamerAsync(AddStreamerInput input, 
            [ScopedService] ApiDbContext context)
        {
            var streamer = new Streamer
            {
                Nombre = input.nombre,
                Url = input.url,
            };

            context.Streamers!.Add(streamer);
            await context.SaveChangesAsync();
            return new AddStreamerPayload(streamer);
        }
    }
}
