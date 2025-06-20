# ⚔️ Unity Third-Person Arena Game Prototype

## 🎮 Gameplay Overview

This is a third-person prototype game built in Unity where the player explores an arena, fights enemies, interacts with environmental elements, and reaches a defined goal to win. The player can **lose** by dying and **win** by finding and reaching the hidden goal capsule, all while surviving waves of golem enemies.

---

## 🌍 Environment

- Scene sourced from the Unity Asset Store.
- Scene color may change randomly each session.
- A third-person camera **follows** the player with zoom functionality via scroll wheel.

---

## 🕹️ Player Movement

- Movement via **WASD** or **click-to-move** (on NavMesh).
- Includes **sprint** with **Shift**, and **jump** with **Space**.
- A visual effect is instantiated at the click location.
- Movement is restricted to walkable NavMesh areas.

---

## 🧍 Player Animations

- **Idle**: Standing still
- **Walk**: Basic movement
- **Run**: Sprinting (with Shift)
- **Jump**: Spacebar triggered
- **Attack**: Triggered on enemy click
- **Cast**: Active when casting spells
- **Death**: Plays when health hits 0, with visual effect

---

## ❤️ Health System

- Player has a UI **health bar**.
- Both player and enemies have health values.
- **Boxes** are one-shot destroyable.
- Destroying a **box** restores player health.

---

## 👾 Enemy AI – Patrolling & Aggro

- Enemies patrol using Unity’s **NavMesh**.
- When the player enters their **aggro radius**, they chase and attack.
- If the player escapes, enemies return to patrol.
- On enemy death, another one spawns randomly near the **goal**.

---

## ⚔️ Combat System

- Enemies:
  - Use **animation-based** attack triggers
  - Have attack cooldowns
- Player:
  - Can **cast spells** (1–9)
  - Can **right-click** to use a flamethrower
- **Each spell** has a visual impact effect.
- **Damage** is dealt only via animation events or direct triggers.

---

## 🎯 Objective & Victory System

- The **goal capsule** is hidden inside a destructible **box**.
- On game start, the box and capsule are randomly placed on valid NavMesh areas.
- Reaching the capsule **triggers a win**.

---

## 🔁 Game Restart System

- Win or Lose displays a **UI screen**.
- Restart button resets:
  - Player position & health
  - Enemy & box spawns
  - Game state (like a new round)

---

## 📦 Dynamic Spawning

- Boxes randomly spawn at start using **NavMesh.SamplePosition**
- Enemies now spawn **around the goal**, not the player
- Prevents unfair early attacks & creates a clear goal zone

---

## 🧱 Golem AI & Animation

Enemy Golems behave similarly to the player:

- **Idle / Walk / Run**
- **Attack** (animation-triggered)
- **Death** with animation & visual effect

---

## 🧾 UI Feedback

- Clear “**You Win**” and “**You Lose**” messages.
- Player gets disabled after death.
- Game can be restarted cleanly from UI.

---

## 🎥 Included Video Clips

Paste your demo video links here:

- `Movement_and_Environment.mp4` –  
- `Camera.mp4` –  
- `Enemy.mp4` –  
- `Combat_and_Health.mp4` –  
- `Animations.mp4` –  
- `Win_Restart.mp4` –  
- `Lose_Restart.mp4` –  

---

> Made with 💀 spells, 🔥 fireballs, and Unity