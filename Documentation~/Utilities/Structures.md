# Structures

This module contains utility data structures.

## Priority Queue

Data Structure that behaves lika a priority queue, being implemented with a `List`. Element with lower priority number are obtained first.

Its element must implement the `IComparable` interface.

## Select Implementation

`Interface` that allows the implementation of classes that have a collection of abstract classes with the goal of having different inherited types.

## Pausable Interface

Any class that can change between two action states (play and stop) should implement this `Interface`.

## Utility Models

|Class|Description|
|---|---|
|`Direction`|Model used to store and manage two dimensional directions.|
|`Position`|Model used to store and manage two dimensional positions. Default sub-classes are `IntPosition` and `FloatPosition`.|
|`Matrix`|Model used to store and manage a two dimensional array of elements of the given type.|

