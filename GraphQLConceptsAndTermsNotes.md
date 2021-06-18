# GraphQL Concepts And Terms Notes

Being a specification, there are a lot of GraphQL Concepts that should be identified and understood at an abstract level, separate from the implementation.

## Edges and Nodes on a Graph

The Graph data structure

- Nodes - Objects in a Graph with a standardized `id`, used on request to "hydrate" the value using a resolver.
  - https://graphql.org/learn/global-object-identification/
- Edges
  - https://graphql.org/learn/pagination/#pagination-and-edges

## Pagination and GraphQL Connections

Pagination (limiting the requested data set) is a common and complex problem to solve.

GraphQL Connections is an approach towards implementing pagination.

With Hot Chocolate, [Connections are returned](https://chillicream.com/docs/hotchocolate/fetching-data/pagination/#connections) instead of a list of entries, giving the client access to Pagination capabilities.

Related Terms:

- Cursor - Points to a location in an ordered list.
  - Hot Chocolate implements a similar capability using `UseOffsetPaging`

References:

- [Apollo Blog - Explaining GraphQL Connections](https://www.apollographql.com/blog/graphql/explaining-graphql-connections/)
- [Hot Chocolate Docs v11 - Pagination](https://chillicream.com/docs/hotchocolate/fetching-data/pagination/)
- [GraphQL - Learn - Pagination](https://graphql.org/learn/pagination/)
- [Relay GraphQL Cursor Connections Specification](https://relay.dev/graphql/connections.htm)