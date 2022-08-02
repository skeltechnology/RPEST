# Pathfinding

## `Path`

Model used to store information of a path, allowing operations such as adding a position and returning a collection of positions or directions.

## `Pathfinder`

Class that manages the pathfinding of a tilemap, being responsible for finding a path between the given positions.

## Search Algorithms

### Shortest Path

The `ShortestPathSolver` is a `SearchSolver` that travels through `Cell` states using the `A*` algorithm, in order to obtain the minimum cost until reaching the final state (position).