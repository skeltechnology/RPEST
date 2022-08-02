# Search

This module contains utility classes that can be used to solve `state exploration` problems, like pathfinding.

In order to use it, it is necessary to implement two classes and extend the generic class `SearchSolver`.

## State

The implementation of this class is completly up to the user, as it does not need to extend any class or implement any interface. It must however store the necessary information that the solver needs.

## Solver

The solver must extend the generic abstract class `SearchSolver` and implement the following methods:

- **`Cost`**: Gets the cumulative cost of the given search state, being called after Previous is updated.
- **`Heuristic`**: Gets the heuristic cost (prediction of the remaining cost to reach the final state), being called after Previous is updated.
- **`Neighbors`**: Gets the neighbor states of the given state.
- **`IsFinal`**: Indicates of the given state is final.

Optionally, the method **`solve`** can be overwritten.
