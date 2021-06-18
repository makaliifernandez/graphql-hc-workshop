using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ConferencePlanner.GraphQL.Data;
using HotChocolate;
using ConferencePlanner.GraphQL.DataLoader;
using System.Threading;
using HotChocolate.Types.Relay;
using HotChocolate.Types;

namespace ConferencePlanner.GraphQL.Speakers
{
    [ExtendObjectType(Name = "Query")]
    public class SpeakerQueries
    {
        // By annotating UseApplicationDbContext we are essentially applying a Middleware to the field resolver pipeline.
        [UseApplicationDbContext]
        // Important: Note, that we no longer are returning the IQueryable but are executing the IQueryable by using ToListAsync (for reasons related to middleware and filtering)
        public Task<List<Speaker>> GetSpeakersAsync(
            [ScopedService] ApplicationDbContext context
        ) =>
            context.Speakers.ToListAsync()
        ;

        public Task<Speaker> GetSpeakerByIdAsync(
            [ID(nameof(Speaker))] int id, // Annotate the id to tell the execution engine what type depends on this value as it's ID,
                                          // this could also be done as a fluent api descriptor method
                                          //    EX:     descriptor.Field(t => t.FooId).ID("FOO");
            SpeakerByIdDataLoader dataLoader,
            CancellationToken cancellationToken) =>
                dataLoader.LoadAsync(id, cancellationToken)
        ;

        public async Task<IEnumerable<Speaker>> GetSpeakersByIdAsync(
            [ID(nameof(Speaker))] int[] ids,
            SpeakerByIdDataLoader dataLoader,
            CancellationToken cancellationToken) =>
                await dataLoader.LoadAsync(ids, cancellationToken)
        ;
    }
}
