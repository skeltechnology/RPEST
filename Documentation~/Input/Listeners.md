# Listeners

Module that contains scripts that are notified when the respective controller receiveis an input.

## `InputListener`

Base class for listening to input actions.

Every class that extends the abstract class `InputListener` should implement the methods `SetListeners()` and `RemoveListeners()`.

## Listener Types

### Movement

This namespace contains scripts that can be used to manipulate and manage the user input and using it to move the correspondent object.

|Class|Description|
|---|---|
|`KeyDirectionMovementInputListener`|Listener that moves an object in any direction when the user interacts the correspondent key.|
|`KeyInteractionInputListener`|Listener that make the selected object interact when the user interacts the correspondent key.|
|`KeyPathMovementInputListener`|Listener that moves an object to the position below the mouse when the user interacts the correspondent key.|
|`KeyRunningMovementInputListener`|Listener that make the selected object run when the user interacts the correspondent key.|
