# Save The Grandma

A adventure game where you help Grandma escape from a series of islands by gathering resources, crafting tools, and building bridges.

**[Play on itch.io](https://avellion.itch.io/save-the-grandma)**

## 📖 About

Save The Grandma is a resource management game set across multiple islands. The goal is to guide Grandma to safety by collecting resources, crafting items, and constructing bridges between islands - all while avoiding patrolling enemies.

## ✨ Key Features

### Resource & Crafting System
- **Three resource types**: Wood (from trees), Stone (from rocks), Iron (from ore deposits)
- **Tool-based gathering**: Axe for wood, Pickaxe for stone/iron
- **Crafting bench**: Convert raw materials into usable building components
- **Resource collector tool**: Automated resource gathering system

### Enemy AI System
- **State machine-based AI** with 7 distinct behaviors:
  - Patrol, Idle, Chase, Eat, Stunned states
  - Dynamic pathfinding using Unity NavMesh
  - Randomized enemy sizes and speeds for variety
- **Gun tool** to stun enemies temporarily

### Building & Progression
- **Bridge construction** system to connect islands
- **Rock step** paths for traversing difficult terrain
- **Fence** placement for defensive structures

### Technical Implementation
- **ScriptableObject architecture** for modular tool and resource design
- **Custom inventory system** with UI management
- **Camera controller** with WASD movement and zoom functionality
- **NavMesh-based pathfinding** for character movement
- **DOTween animations** for smooth visual feedback

## 🎮 Controls

| Input | Action |
|-------|--------|
| **1, 2, 3, 4** | Select tools |
| **E** | Use selected tool |
| **Tab** | Open/Close inventory |
| **W, A, S, D** | Move camera |
| **Mouse Scroll** | Zoom camera |

## 🛠️ Built With

- **Unity 2022.3** (Universal Render Pipeline)
- **C#** for all game logic
- **Unity NavMesh** for AI pathfinding
- **DOTween** for animations
- **ScriptableObjects** for data architecture
- **Custom water shader** (third-party)

## 📸 Screenshots

*Screenshots from the itch.io page showcase the game's low-poly art style and island-based puzzle design.*

## 🎯 Development Highlights

This project demonstrates:
- Clean, maintainable code structure with separation of concerns
- State pattern implementation for complex AI behavior
- Modular architecture using ScriptableObjects
- Custom inventory and crafting systems
- NavMesh integration for dynamic pathfinding

## 📝 Notes

All code and systems are original implementations, with the exception of the water shader which uses a third-party asset.

---

**Developer**: Mert Özzencir  
**Portfolio**: [GitHub](https://github.com/MertOzzencir) | [itch.io](https://avellion.itch.io)
