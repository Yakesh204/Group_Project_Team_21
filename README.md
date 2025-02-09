# Shoot-It
&emsp;A third person RPG shooter. Fight your way through enemies! Shoot your way to the end!

## Table of Contents
> - [About](#about)
> - [Development](#development)
>   - [Team Taks](#team-tasks)
>   - [File Structure](#file-structure)
>   - [File Descriptions](#file-descriptions)
>   - [Style Sheet](#style-sheet)
> - [Branches](#branches)

## About
&emsp;_Shoot It!_ is a third person shooter (TPS) in which you fight your way through progrsively harder levels, fighting your way through ever more difficult enemies and bosses, all while trying to collect weapon upgrades and most importantly, reach the exfil point.

## Development

### Team Tasks
> - [ ] __Arham__
>     - [X] Create hud
>     - [x] Interface hud with player class
>     - [x] Interface hud with weapons class
>     - [ ] Interface quest/objective system with hud
> - [ ] __Harveer__
>     - [x] Create character controller
>     - [x] Create weapons manager
>     - [x] Interface weapons and player
>     - [ ] Program triggers to switch between scenes (exfil points)
>     - [ ] Create character select screen
> - [ ] __Omar__
>     - [x] Create pickup and upgrade system
>     - [x] Interface upgrade system with weapon system
>     - [x] Interface pickups with player class
>     - [ ] Work with Yakesh to create quest/objective system
> - [ ] __Yakesh__
>     - [x] Create enemy A.I. using Unity navmesh  
>     - [x] Integrate enemies  
>     - [x] Integrate enemies with game  
>     - [x] Work with Omar to create quest/objective system
>     - [ ] Create start menu
> - [ ] __Shared Tasks__  
>     - [ ] Create at least 3 levels  
>     - [ ] Add finishing touches  
### File Structure
&emsp;Please note, this file structure only includes important files and folders and does not represent the full file structure of the unity project. \*\*Generated in VSCode\*\*
```
└── 📁Assets
    └── 📁InputSystem
        └── Input.cs
        └── Input.inputactions
    └── 📁Resources
        └── 📁Prefabs
            └── 📁Entities
                └── 📁Enemies
                └── 📁Managers
                    └── CharacterSelecManager.prefab
                └── 📁Players
                    └── 📁CharacterModels
                        └── Heavy.prefab
                        └── Infantry.prefab
                        └── Recon.prefab
                    └── Heavy.prefab
                    └── Infantry.prefab
                    └── Recon.prefab
            └── 📁Pickups
                └── 📁PickUps
                    └── Ammo Pickup.prefab
                    └── Health Pickup.prefab
                    └── Stamina Pickup.prefab
                    └── Weapon Pickup.prefab
                └── Ammo Pickup.fbx
                └── Health Pickup.fbx
                └── Stamina Pickup.fbx
                └── Weapon Pickup.fbx
            └── 📁Projectiles
                └── Bullet01.prefab
                └── Bullet02.prefab
                └── Bullet03.prefab
                └── Bullet04.prefab
                └── Bullet05.prefab
            └── Projectiles.meta
            └── 📁Weapons
                └── M107.prefab
                └── M1911.prefab
                └── M249.prefab
                └── M4.prefab
                └── M4Shot.prefab
                └── Uzi.prefab
    └── 📁Scenes
        └── Staging Area.unity
    └── 📁Scripts
        └── CollectibleSpin.cs
        └── 📁PlayerScripts
            └── AimManager.cs
            └── IEntity.cs
            └── MovementManager.cs
            └── Player.cs
        └── 📁WeaponScripts
            └── FirearmManager.cs
            └── ProjectileController.cs
            └── WeaponSwitcher.cs
    └── 📁UI Elements
        └── 📁Crosshairs
            └── 📁Black
                └── crosshairCrossBlack.png
                └── crosshairDotBlack.png
            └── 📁White
                └── crosshairCrossWhite.png
                └── crosshairDotWhite.png
        └── 📁Sprites
            └── medKit.png
            └── pistolBullet.png
            └── rifleBullet.png
            └── WhiteSquaree.png
```

### File Descriptions
| File Name | File Decription |
| :---: | :--- |
| MovementManager.cs | Controls WASD movement |
| AimManager.cs | Controls aiming and free look  |
| IEntity.cs | Interface which described all entities in _Shoot It!_ |
| Player.cs | Describes the player, inherits from IEntity |
| FirearmManager.cs | Handles shooting and describes all weapons in _Shoot It!_ |
| WeaponSwitcher.cs | Handles switching between weapons |

###  Style Sheet
Please follow the rules laid out in [Google's C# Style Sheet](https://google.github.io/styleguide/csharp-style.html) for this project

**Short Recap of Style Sheet**
- Names of files and directories/folders: **`PascalCase`**
- Names of classes, methods, enumerations, public fields, public properties, namespaces: **`PascalCase`**
- Names of local variables, parameters: **`camelCase`**
- Names of private, protected, internal and protected internal fields and properties: **`_camelCase`**
- For casing, a “word” is anything written without internal spaces, including acronyms. For example, **`MyRpc`** instead of **`MyRPC`**
- Names of interfaces start with **`I`**, e.g. **`IInterface`**
- Modifiers occur in the following order **`public protected internal private new abstract virtual override sealed static readonly extern unsafe volatile async`**
- Class member ordering:
  - Group class members in the following order:
    1. Nested classes, enums, delegates and events
    2. Static, const and readonly fields
    3. Fields and properties
    4. Constructors and finalizers
    5. Methods
  - Within each group, elements should be in the following order:
    1. Public
    2. Internal
    3. Protected internal
    4. Protected
    5. Private

## Branches
1. **`stage`** branch for staging changes to main

2. **`main`** current most clean branch

3. **`temp`** branch for implementing new features

**Temporary Branches**  
Temporary branches are for adding new features which should not be developed in the regular staging branch. To create a temporary branch, create a branch with the format: `temp-feature`. Where `temp` is the prefix for all temporary branches and `feature` is the feature this branch is implementing. For example `temp-bullet-time` would be the branch name for a bullet time feature.

**Pull Requests for Temporary Branches**
1. Make a PR to `stage` branch.
2. Comply with the best practices and guidelines
3. Pass reviews and merge with stage.

After this, changes will be merged.
