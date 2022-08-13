# Sprite Animators

## `SpriteAnimator`

*Abstract* class that is reponsible for animating a specific `SpriteRenderer` with the given collection of sprites.

## `WorldObjectAnimator`

*Specialization* of the `SpriteAnimator` class, that contains a list of `WorldObjectAnimatorComponent`, which are components that will be responsible for animating the correspondent sprite.

### Components

There are **3** default components, but the programmer can create one by *extending* the `WorldObjectAnimatorComponent` class. The custom editor will automatically detect any instance of that class.

The available components are:

|Component|Required (Super-)Class|Description|
|---|---|---|
|`WalkableAnimatorComponent`|`WalkableObject`|Component responsible for handling walking animations.|
|`InteractorAnimatorComponent`|`InteractorObject`|Component responsible for displaying an animation for a certain time when the object interacts with something.|
|`StandingAnimatorComponent`|`WalkableObject`|Component responsible for displaying infinitely an animation the object is not walking.|
