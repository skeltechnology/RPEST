# Chasers

## Description

`ObjectChaser`s are `MonoBehaviour`s that follow a specific target (`Transform`).

For example, they can be used to make the `Camera` chase a specific player or game object.

The abstraction level of the base class allows the programmer to easily implement new classes that attends the neccessary requirements.

## Base Class Parameters

The base class contains the following parameters:

|Field|Type|Value|Description|
|---|---|---|---|
|target|`Transform`|Reference|Target that will be followed by the chaser.|
|smoothness|`float`|$\ge$ 0|Smoothness of the chaser movement. The greater the value, the longer it will take the chaser to keep up.|
|xAxisFrozen, yAxisFrozen, zAxisFrozen,|`bool`|`true` / `false`|Boolean indicating if the respective axis is frozen.|
|offset|`Vector3`|Reference (the vector can contain any values)|Offset of the chaser relatively to the target.|
|border **\***|`Renderer`|Reference|Optional parameter. It is used to calculate the area that the chaser is allowed to move in.|

**\*** *Optional parameters*

## Classes

|Class|Required Component|
|---|---|
|ChaserCamera|Camera|
|ChaserRectTransform|RectTransform|
|ChaserRenderer|Renderer|
