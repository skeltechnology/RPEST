# Tilemaps

This module contains classes that are used to animate tilemaps, tiles or any interaction with them.

## `TileHighlighter`

### Description

`MonoBehaviour` that is responsible for highlighting tiles, according to the given parameters, when the user hovers the mouse over the tile. Only `Tile`s that are contained in the `Tilemap` that the `WalkableObject` is on can be highlighted.

### Parameters

|Field|Type|Value|Description|
|---|---|---|---|
|camera|`Camera`|Reference|Reference to the camera component.|
|walkableObject|`WalkableObject`|Reference|Reference to the walkable object.|
|highlightTile|`Tile`|Reference|Reference to the highlighted tile prefab.|

### Usage

This component must be placed in a `Tilemap` that will be modified to display the highlighted tiles. This implies that this highlightable tilemap must be only used for the this purpose and must be above (greater Z index) all `WalkableTilemap`s.
