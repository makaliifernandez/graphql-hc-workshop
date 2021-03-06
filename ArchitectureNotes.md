# Architecture Notes

The directory structure so far:

- `Common`
- `Data` - Schema of the db, used by EF to define dbSet for dbContext
- `DataLoader` - Injected into Resolvers Configured in ObjectType classes to reduce the number of queries when requesting bulk data.
- `Extensions`
- `Migrations` - Autogenerated EF Core db schema migrations
- `Types` - GraphQL ObjectType Configurations and Resolvers

The GraphQL Schema is defined in these directories:

- `Speakers`
- `Sessions`
- `Tracks`

with these type of files:

- `*Queries` `class`
- `*PayloadBase` `class`
- `Add*Payload` `class`
- `*Mutations` `class`
- `Add*Input` `record`


## `DataLoader`
 
Data requested in bulk uses the DataLoader to reduce the number of queries made to get the data.

- See: [Data Loader and the problem it solves in GraphQL](https://www.youtube.com/watch?v=ld2_AS4l19g)
