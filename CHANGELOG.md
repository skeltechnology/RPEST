# Changelog
All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/)
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## Unreleased

- Character Objects
- Inventory System
- Cutscene System
- Quest System
- Dialogue System
- Tutorials
- Loading Scene System
- Save / Load System

## [0.1.1] - 2022-08-09
### Changed
- Enhancements to the `InteractorObject`.
- The Input System (more specifically `KeyHoldInputController`) now tracks the order the keys were pressed, returning them in that order.
- Code optimizations.
- The `Pathfinder` has now an asynchronous method `FindShortestPathAsync()`.
- The `KeyPathMovementInputListener` can now interrupt the movement, in order to start a new path movement.
- The initial direction of a `WalkableObject` can now be specified in the inspector.

### Fixed
- Fixed missing documentation images.

## [0.1.0] - 2022-08-02
### Added
- Initial Documentation: `README.md`, `CHANGELOG.md` and `Documentation~`.
- World Representation
    - Databases
        - Collider Objects
        - Interactables
        - Triggers
        - Walkable Tilemaps
        - World (Objects)
    - Objects
        - World Object
        - Collider Object
        - Walkable Object
        - Interactable Object
        - Interactor Object
        - Trigger Object
- Pathfinding System
- Movement System
- Object Chasers
- World Obstacles
- Input System
- Interaction and Trigger Systems
- Sprite Animation

[0.1.1]: https://github.com/skeltechnology/RPEST/releases/tag/v0.1.1
[0.1.0]: https://github.com/skeltechnology/RPEST/releases/tag/v0.1.0
