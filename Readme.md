# Learning Hot Chocolate (C#) flavored GraphQL

I'm working through the official ChilliCream graphql workshop, to learn more about using hot chocolate to implement a GraphQL API.

https://github.com/ChilliCream/graphql-workshop

...

[GraphQL Concepts and Terms Notes](./GraphQLConceptsAndTermsNotes.md)

...

SESSION 1 NOTES - Creating a GraphQL Server Project [(Link)](https://github.com/ChilliCream/graphql-workshop/blob/master/docs/1-creating-a-graphql-server-project.md):

- Created a new Net core 5 web project
- added GraphQL and EF Core 5 packages
- added a model for efcore `dbContext` to test query against
- setup a sqlite db using ef core migrations
- added a graphQL `QueryType` using `dbContext` configured model
- tested query using banana cake pop
- added a graphql `MutationType`
- tested mutation

SESSION 2 NOTES - Controlling Nullability [(Link)](https://github.com/ChilliCream/graphql-workshop/blob/master/docs/2-controlling-nullability.md):

- learned more about the GraphQL Type system and nullability
- gql type system guarantees the consumer an expected value

SESSION 3 NOTES - Understanding the DataLoader [(Link)](https://github.com/ChilliCream/graphql-workshop/blob/master/docs/3-understanding-dataLoader.md):

- efcore's `dbContext` is not thread safe, and gql will always attempt to execute fields in parallel, this session addresses this issue using resource pooling.
- this approach uses attributes to apply middleware that uses a pool of (by default) 128 `dbContext` instances to mitigate concurrency issues related to ef core connections.
  - dbContext pooling is super important and useful!
- added and configured more models to `dbContext` using a Fluent API.
- introduced and added `DataLoader` class to make data fetching more efficient (bulk requests)
	- Addresses the N+1 fetching problems by reducing the amount of sql queries made using a batch query.

SESSION 4 NOTES - Schema Design [(Link)](https://github.com/ChilliCream/graphql-workshop/blob/master/docs/4-schema-design.md):

- refactored dir structure to be scalable (see [Architecture Notes](./ArchitectureNotes.md))
- enabled relay support to implement Global Object Identification implemented with the Node interface.

SESSION 5 NOTES - Understanding Middleware [(Link)](https://github.com/ChilliCream/graphql-workshop/blob/master/docs/5-understanding-middleware.md):

- ...In Progress

...