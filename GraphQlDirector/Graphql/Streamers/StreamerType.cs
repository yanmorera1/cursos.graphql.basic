using GraphQlDirector.Models;

namespace GraphQlDirector.Graphql.Streamers
{
    public class StreamerType : ObjectType<Streamer>
    {
        protected override void Configure(IObjectTypeDescriptor<Streamer> descriptor)
        {
            descriptor.Description("Este modelo representa la compañía de streamer");
        }
    }
}
