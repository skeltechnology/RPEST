# Inventory System

This module contains classes that can be used to implement or expand and inventory system.

An inventory system is composed by **3** basic parts:

1. **Item Data**: `ItemData` classes are `ScriptableObjects` that the programmer should create to store the necessary information of an item. There are currently **3** classes that can be used or expanded.
2. **Item**: `Item` classes are wrappers for `ItemData` that contain essential information for the `Inventory`.
3. **Inventory**: Is basically a model and controller for the inventory, implementing the necessary mechanisms to interact with it.

There are currently **3** different types of inventories, but the programmer can create its own based on this parts.

## Classic Inventory

![Classic Inventory Example](../images/ClassicInventoryExample.jpg)

The design paradigm behind this system is exceedingly simple: players can stash an innumerable amount of items in their inventory, but only a fixed number of each (can be a very large number to simulate infinity). The programmer can also choose the maximum amount of unique items allowed.

To create one, the programmer needs to extend the class `ClassicInventory` and create or expand the desired `ClassicItemData` objects.

## Weighted Inventory

![Weighted Inventory Example](../images/WeightedInventoryExample.jpg)

This inventory system is similar to the `Classic Inventory`, but, in this system, each item is assigned a numerical value that represents its weight, along with a maximum weight capacity to the inventory.

To create one, the programmer needs to extend the class `WeightedInventory` and create or expand the desired `WeightedItemData` objects.

## Grid Inventory

![Grid Inventory Example](../images/GridInventoryExample.jpg)

The visual grid inventory system is just that: players have a specific number of slots in which to store items, represented visually by a rectangular grid.

Instead of being assigned a weighted value, items are sized accordingly. This inventory does not support non-rectangular item compositions.

To create one, the programmer needs to extend the class `GridInventory` and create or expand the desired `GridItemData` objects.
