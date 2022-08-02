# Databases

## Description

The **RPEST** `World` must acknowledge some of its elements, in order to facilitate some operations, such as finding obstacles in a certain position or check if a certain object activated a trigger.

In order to achieve this, the world contains **databases** that store and manage its elements in a modular way.

## Access

Any script that has a reference to a `World` can access its databases, as they are a `public` reference.

## Initialization

When a world is initialized, empty databases are created, expecting them to be populated when the `WorldElements` are initialized.

Note that database elements can be added and removed at any time and not only upon its initialization.

## Consistency

It is the responsibility of the database elements to notify their removal of the respective databases when the element is destroyed or does not belong to the database anymore.

## Performance

The larger the number of elements a database is, the lower its performance (especially with searching operations) will be, so feel free to create, when possible, more than one world, to increase the efficiency of your game.

## Classes

There are 4 world databases:

|Database|Element Type|
|---|---|
|`ColliderObjectDatabase`|`ColliderObject`|
|`InteractableDatabase`|`Interactable`|
|`TriggerDatabase`|`Trigger`|
|`WalkableTilemapDatabase`|`WalkableTilemap`|
