# Learning Hot Chocolate (C#) flavored GraphQL

I'm following the official ChilliCream graphql workshop, to learn more about hot chocolate.

https://github.com/ChilliCream/graphql-workshop

[SESSION 1 NOTES](https://github.com/ChilliCream/graphql-workshop/blob/master/docs/1-creating-a-graphql-server-project.md):

- Created a new Net core 5 web project
- added GraphQL and EF Core 5 packages
- added a model for efcore `dbContext` to test query against
- setup a sqlite db using ef core migrations
- added a graphQL `QueryType` using `dbContext` configured model
- tested query using banana cake pop
- added a graphql `MutationType`
- tested mutation

[SESSION 2 NOTES](https://github.com/ChilliCream/graphql-workshop/blob/master/docs/2-controlling-nullability.md):

- learned more about the GraphQL Type system and nullability
- gql type system guarantees the consumer an expected value

[SESSION 3 NOTES](https://github.com/ChilliCream/graphql-workshop/blob/master/docs/3-understanding-dataLoader.md):

- efcore's `dbContext` is not thread safe, and gql will always attempt to execute fields in parallel, this session addresses this issue using resource pooling.
- this approach uses attributes to apply middleware that uses a pool of (by default) 128 `dbContext` instances to mitigate concurrency issues related to ef core connections.
  - dbContext pooling is super important and useful!
- added and configured more models to `dbContext` using a Fluent API.
- introduced and added `DataLoader` class to make data fetching more efficient (bulk requests)
	- I'm still learning more about DataLoader, but I think its used to address N+1 fetching problems, by reducing the amount of sql queries made using a batch query.
		- [DataLoader N+1 solution Video explanation](https://www.youtube.com/watch?v=ld2_AS4l19g)

[SESSION 4 NOTES](https://github.com/ChilliCream/graphql-workshop/blob/master/docs/4-schema-design.md)

... In Progress ...