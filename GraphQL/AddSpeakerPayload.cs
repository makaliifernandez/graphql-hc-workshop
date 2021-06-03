using ConferencePlanner.GraphQL.Data;

namespace ConferencePlanner.GraphQL
{
    // represents the output of our GraphQL mutation 
    public class AddSpeakerPayload
    {
        public AddSpeakerPayload(Speaker speaker)
        {
            Speaker = speaker;
        }

        public Speaker Speaker { get; }
    }
}