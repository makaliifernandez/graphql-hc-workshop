using System.Threading.Tasks;
using ConferencePlanner.GraphQL.Data;
using HotChocolate;
using HotChocolate.Types;

namespace ConferencePlanner.GraphQL.Speakers
{
    // For mutations we are using the relay mutation pattern which is commonly used in GraphQL.
    // A mutation consists of three components, the input, the payload and the mutation itself.
    // In our case we want to create a mutation called addSpeaker, by convention,
    // mutations are named as verbs, their inputs are the name with "Input" appended at the end,
    // and they return an object that is the name with "Payload" appended.
    // for our addSpeaker mutation, we create two types: AddSpeakerInput and AddSpeakerPayload.
    [ExtendObjectType(Name = "Mutation")]
    public class SpeakerMutations
    {
        // By annotating UseApplicationDbContext we are essentially applying a Middleware to the field resolver pipeline.
        [UseApplicationDbContext]
        public async Task<AddSpeakerPayload> AddSpeakerAsync(
            AddSpeakerInput input,
            [ScopedService] ApplicationDbContext context)
        {
            var speaker = new Speaker
            {
                Name = input.Name,
                Bio = input.Bio,
                WebSite = input.WebSite
            };

            context.Speakers.Add(speaker);
            await context.SaveChangesAsync();

            return new AddSpeakerPayload(speaker);
        }
    }
}