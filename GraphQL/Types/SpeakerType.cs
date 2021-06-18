using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ConferencePlanner.GraphQL.Data;
using ConferencePlanner.GraphQL.DataLoader;
using HotChocolate;
using HotChocolate.Types;
using HotChocolate.Resolvers;


// Configuring/Resolving to Types are sort of like mapping to a ViewModel
namespace ConferencePlanner.GraphQL.Types
{
    public class SpeakerType : ObjectType<Speaker>
    {
        // In the type configuration we are giving SessionSpeakers a new name sessions. Also, we are binding a new resolver to this field which also rewrites the result type. The new field sessions now returns [Session].
        protected override void Configure(
            IObjectTypeDescriptor<Speaker> descriptor
            )
        {
            // Configure node to implement the Node interface
            descriptor
                .ImplementsNode()
                .IdField(t => t.Id) // The id specified to be written to a global object identifier, that will be used to find and retrieve the data on the graph should it need to be resolved
                .ResolveNode((ctx, id) => ctx.DataLoader<SpeakerByIdDataLoader>()
                    .LoadAsync(id, ctx.RequestAborted)); // The Node Resolver

            // This is the Code-first approach to creating a Resolver
            descriptor
                // For the field SessionsSpeakers, we will be Resolving With the SpeakerResolvers
                .Field(t => t.SessionSpeakers)
                .ResolveWith<SpeakerResolvers>(t => t.GetSessionsAsync(default!, default!, default!, default)) // binds the SpeakerResolvers to the SessionSpeakers field
                .UseDbContext<ApplicationDbContext>()
                .Name("sessions"); // configures a new name for the SessionSpeakers field
        }

        private class SpeakerResolvers
        {
            // When using this as a Resolver, we are injecting the dbContext using a ScopedService, to get access to the Speakers dbSet
            //  and then using the DataLoader to load the data asnychronously
            //  Should the field that uses this resolver be requested by the client,
            //  the DataLoader will then retrieve the data requested in bulk using the sent list of sessionIds
            public async Task<IEnumerable<Session>> GetSessionsAsync(
                Speaker speaker,
                [ScopedService] ApplicationDbContext dbContext,
                SessionByIdDataLoader sessionById,
                CancellationToken cancellationToken)
            {
                int[] sessionIds = await dbContext.Speakers
                    .Where(s => s.Id == speaker.Id)
                    .Include(s => s.SessionSpeakers)
                    .SelectMany(s => s.SessionSpeakers.Select(t => t.SessionId))
                    .ToArrayAsync();

                return await sessionById
                    .LoadAsync(sessionIds, cancellationToken);
            }
        }
    }
}
