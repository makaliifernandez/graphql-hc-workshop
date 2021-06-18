namespace ConferencePlanner.GraphQL.Speakers
{
    // The input and output (payload) both contain a client mutation identifier used to reconcile requests and responses in some client frameworks
    public record AddSpeakerInput(
        string Name,
        string? Bio,
        string? WebSite);
}
