using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ConferencePlanner.GraphQL.Data;
using HotChocolate;
using ConferencePlanner.GraphQL.DataLoader;
using System.Threading;

namespace ConferencePlanner.GraphQL
{
    public class Query
    {
        // By annotating UseApplicationDbContext we are essentially applying a Middleware to the field resolver pipeline.
        [UseApplicationDbContext]
        // Important: Note, that we no longer are returning the IQueryable but are executing the IQueryable by using ToListAsync (for reasons related to middleware and filtering)
        public Task<List<Speaker>> GetSpeakers(
            [ScopedService] ApplicationDbContext context
        ) =>
            context.Speakers.ToListAsync()
        ;

        public Task<Speaker> GetSpeakerAsync(
            int id,
            SpeakerByIdDataLoader dataLoader,
            CancellationToken cancellationToken) =>
                dataLoader.LoadAsync(id, cancellationToken)
        ;
    }
}
