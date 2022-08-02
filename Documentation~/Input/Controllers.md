# Controllers

This module contains scripts that are used to track the user input. Along with this module, [Listeners](Listeners.md) should be used to manage the received input.

## `InputController`

Base class for controllers that require a data structure to store and manage the listeners, updating them when necessary.

## Input Types

### Keys

There are 3 scripts that can be used to track the interaction between the user and a key

|Class|Description|
|---|---|
|`KeyDownInputController`|Controller that notifies the listeners when the user presses down the specified key.|
|`KeyHoldInputController`|Controller that notifies the listeners while the user holds down the specified key.|
|`KeyUpInputController`|Controller that notifies the listeners when the user releases the specified key.|
