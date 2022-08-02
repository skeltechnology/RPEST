# World Elements

## Description

Every class that belongs to a `World` component and needs to acknowledge it must extend the `WorldElement` class.

## Classes Diagram

Currently, there are 2 classes that extend it.

![World Elements Diagram](../images/WorldElementsDiagram.png)

### World Objects

A `WorldObject` represents a game object that belongs to the `Grid` component and can be placed inside a specific cell.

More details can be found in the [World Objects](./WorldObjects.md) section.

### Walkable Tilemaps

The `WalkableTilemap` mono behaviour must be attached to a `Tilemap` that contains the walkable tiles, so that the correspondent tilemap can be used to indicate which cells are valid.
