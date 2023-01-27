using GraphQlDirector.Data;
using GraphQlDirector.Models;
using Microsoft.EntityFrameworkCore;

namespace GraphQlDirector.Graphql
{
    public class Query
    {
        [UseDbContext(typeof(ApiDbContext))]
        [UseProjection]
        [UseFiltering]
        [UseSorting]
        public IQueryable<Video> GetVideos([ScopedService] ApiDbContext context)
        {
            return context.Videos!;
        }

        [UseDbContext(typeof(ApiDbContext))]
        [UseProjection]
        [UseFiltering]
        [UseSorting]
        public IQueryable<Director> GetDirectors([ScopedService] ApiDbContext context)
        {
            return context.Directores;
        }
    }
}
