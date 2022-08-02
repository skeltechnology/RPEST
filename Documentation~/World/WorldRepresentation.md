# World Representation

## Description

The **RPEST** word is created by adding the `World` mono behaviour to a Unity object. Then, every object or element associated to that world has to be a child or sub-child of that object.

## Initialization

When the Unity scene is initialized and the `Awake()` method is called, the `World` performs a depth-first search to initialize every `WorldElement`, which are responsible for updating the correspondent databases (more details in the [Databases](./Databases.md) section.)

## Warnings

Make sure to read the **Consistency** and **Performance** sections in the [Databases](./Databases.md) section.
