# Structures

This module contains utility data structures.

## Priority Queue

Data Structure that behaves lika a priority queue, being implemented with a `List`. Element with lower priority number are obtained first.

Its element must implement the `IComparable` interface.

## Select Implementation

`Interface` that allows the implementation of classes that have a collection of abstract classes with the goal of having different inherited types.

## Pausable Interface

Any class that can change between two action states (pause and unpause) should implement this `Interface`.

## RPEST Custom Coroutine

To expand the functionalities of Unity coroutines, the class `RPESTCoroutine` was created, featuring methods to `Start()`, `Stop()`, `Pause()` and `Play()`, along with its `Status` and and an event handler that is called at the end of the coroutine's execution.

This class can also be used with the `yield` expression, that keeps waiting while its status is **Running**.

## Utility Models

|Class|Description|
|---|---|
|`Direction`|Model used to store and manage two dimensional directions.|
|`Position`|Model used to store and manage two dimensional positions. Default sub-classes are `IntPosition` and `FloatPosition`.|
|`Matrix`|Model used to store and manage a two dimensional array of elements of the given type.|

