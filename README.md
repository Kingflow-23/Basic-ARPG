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

- V0 ... 😂

https://github.com/user-attachments/assets/764a4e1e-2555-4eff-813c-bc03a5030f9f

- `Movement_and_Environment.mp4` 

https://github.com/user-attachments/assets/0393ca57-7b51-4870-9d89-dc3ec5564a11

- `Camera.mp4`

https://github.com/user-attachments/assets/48b6b704-48cc-467d-81be-a91b1a30bf59

- `Enemy.mp4` 

https://github.com/user-attachments/assets/4b8989f9-7df6-4341-84b3-66593e584987
  
- `Combat_and_Health.mp4` 

https://github.com/user-attachments/assets/bc2c192d-b0b3-418c-8b6c-25cb6d488f85
  
- `Animations.mp4`

https://github.com/user-attachments/assets/d5486cd9-ebde-444a-8ac2-9cb7590bde1e
  
- `Win_Restart.mp4`

https://github.com/user-attachments/assets/f1ce7a66-5ca4-4648-af4d-ae8a9f177c8a

- `Lose_Restart.mp4`

https://github.com/user-attachments/assets/c8f67c77-df8f-4ba1-817a-28ef3556a553

---

> Made with 💀 spells, 🔥 fireballs, and Unity
