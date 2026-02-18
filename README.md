# 🎯 RPG System Project — Unity (C#)

A fully modular, architecturally driven RPG system built in Unity over several months.  
This project showcases clean code, scalable design, and production-oriented gameplay systems.

---

## 🏛️ Architecture Overview

This RPG system is designed using modular, loosely coupled, high-cohesion principles to support long-term scalability.

### 🔑 Key Principles Used

- **Composition over Inheritance**
- **SOLID Principles** for clean, maintainable, and extensible architecture
- **High Scalability & Maintainability** as core design goals

### **Design Patterns**
- **Strategy Pattern**
- **Decorator Pattern**
- **Composite Pattern**
- **Finite State Machine (FSM) / State Pattern**
- **Object Pooling**
- **Singleton** (applied cautiously and only where appropriate)

### **Architectural Approaches**
- **Interface-Driven Architecture** for loose coupling and testability
- **MVC, MVP, MVVM, and MVPR**
  - *MVPR is a refined adaptation of MVP that I designed to improve clarity, modularity, and separation of concerns in Unity workflows.*
- **Feature-Based Folder Organization** for a clean, discoverable project structure

---

## 🧩 System Architecture Diagrams

### **Classes Dependency Map**
This diagram visualizes the relationships between core gameplay classes and how different systems communicate with one another.  
It highlights modularity, decoupling, and the overall structure of the RPG codebase.

[![Classes Dependencies](https://i.postimg.cc/5tKt6bwK/Screenshot-2568-12-03-at-7-49-01-PM.png)](https://postimg.cc/rdWTH6Z5)

---

### **Module Overview**
This image shows the top-level module organization used throughout the project.  
Every color represents a different system category (Abilities, Inventory, Combat, Traits, Dialogue, etc.), helping visualize how each subsystem fits into the whole architecture.

![RPG Architecture Diagram](https://i.postimg.cc/Mp095BhT/Modules.png)

---

## 🧠 Technical Highlights

### **🧹 Clean Code**
- Interfaces used where possible
- Minimal direct references  
- Event-driven communication  

### **🚀 Performance**
- Object pooling  
- No GC spikes  

### **🧩 Modularity**
- Replaceable independently  
- Logic fully separated from visuals  

### **📈 Scalability**
- Designed for production-scale expansion  
- Easy to add new abilities, items, enemies, stats  

---

## ⚔️ Core Features Included

### **✔ Character System**
- Character stats, level, progression  
- Health, mana, stamina  
- Status effects / buffs & debuffs  
- Attribute modifiers  
- Event-driven updates  

### **✔ Ability / Skill System**
- Trinity pattern ability modules  
- Cooldowns, costs, conditions  
- Targeting logic  
- Ability unlocking and progression  

### **✔ Inventory & Item System**
- Item definitions & metadata  
- Equipment slots  
- Consumables & usable items  
- ScriptableObject-based item configuration  
- Modular, extensible item behaviors  
- Drag-and-drop inventory interactions  

### **✔ Combat System**
- Melee & ranged combat logic  
- Hit detection & collision checks  
- Damage events & modifiers  
- Attack patterns and combos  
- Animation-driven combat triggers  

### **✔ NPC System**
- Finite State Machine (FSM) behavior  
- Patrol, idle, interact, and combat states  
- Threat evaluation logic  
- NPC interaction hooks for quests & dialogue  

### **✔ Quest System**
- Multi-step objectives  
- Conditional branching (based on player choices or item possession)  
- Progress tracking
- Integration with dialogue, combat, and inventory events  

### **✔ Dialogue System**
- Branching conversation trees  
- Conditional dialogue nodes  
- Quest-aware dialogue paths  
- Player choices and multiple outcomes  

### **✔ Trait / Attribute System**
- Strength, Dexterity, Resistance, Intelligence, Luck  
- Level-up system with unallocated points  
- Real-time stat updates affecting combat & abilities  
- UI integrated with player progression  

### **✔ Shop System**
- Category filtering (Weapons, Armour, Potions, Abilities, etc.)  
- Buy & Sell modes  
- Availability, pricing, and quantity logic  
- Currency updates & transaction validation  
- Clean decoupling between UI and backend shop logic  

### **✔ Input / Controller System**
- Input abstraction layer  
- Player controller using composition  
- Animation layers & movement logic  
- Attack, ability, and interaction mapping  

### **✔ UI / UX System**
- Data-binding style UI updates  
- Health, mana, inventory UI, skill UI  
- Quest tracker UI  
- Dialogue UI with choice buttons  
- Modular & reusable UI components  

### **✔ Progression & World Systems**
- XP gain & level progression  
- Save/load-friendly data structures  
- Modular time/day tracking (if used)  
- Event-driven world interactions  

---

## 📁 Project Structure
```
/Scripts
  /Character
  /Abilities
  /Inventory
  /Combat
  /AI
  /UI
  /Events
  /Utilities
  /Data
```
This structure keeps systems modular, testable, and scalable.

---

## 🎮 Gameplay & System UI Showcase

Below are screenshots of several core gameplay systems, each fully implemented with modular and scalable architecture.

---

### **🧭 Quest System**
![Quest System](https://i.postimg.cc/ZKpTwm6J/Quest-System.png)

The quest system supports:
- Multi-step objectives  
- Conditional logic and branching  
- UI synchronization and automatic updates  

---

### **🎒 Inventory & Equipment System**
![Inventory System](https://i.postimg.cc/Wbg25V01/Inventory-System.png)

Supports:
- Items with behavior modules  
- Equipment slots  
- Drag-and-drop logic  
- Stackable items  
- ScriptableObject-based item definitions  

---

### **🏪 Shop System**
![Shop System](https://i.postimg.cc/65n9MKCW/Shop-System.png)

Features:
- Category filtering (Weapons, Armour, Potions, etc.)  
- Item availability  
- Buy/Sell modes  
- Price & quantity logic  
- Clean separation between UI and backend shop logic  

---

### **📊 Trait / Attribute System**
![Trait System](https://i.postimg.cc/cLWdjzcN/Trait-System.png)

Includes:
- Customizable player attributes  
- Leveling and stat progression  
- Point allocation  
- UI binding to underlying data models  

---

## 💬 Dialogue System

### **Dialogue UI**
![Dialogue System 1](https://i.postimg.cc/sgQGFTbX/Screenshot-2568-12-03-at-7-56-23-PM.png)  
![Dialogue System 2](https://i.postimg.cc/5NLxsJ8N/Dialogue-System-2.png)

The dialogue system supports:
- Branching dialogue  
- Conditional nodes  
- Quest-aware conversation paths  
- Unique speaker templates  
- Player choice options  

---

## 🛠️ Custom Dialogue Editor Window (Unity Editor Tool)

One of the most advanced and important parts of this entire project:

### **⭐ A Fully Custom-Built Dialogue Editor Window (Not from Unity, not third-party)**

This editor tool was **designed and programmed by me** to streamline the creation of RPG branching dialogue.  
It is **not** based on any Unity package, third-party asset, or external framework.  
Every component — the nodes, layout, transitions, conditions, and actions — is built from scratch as a Unity Editor extension.

![My Dialogue Editor 1](https://i.postimg.cc/fRKpNvX9/My-Dialogue-Window-ex-1.png)
![My Dialogue Editor 2](https://i.postimg.cc/q7vYMCX6/My-Dialogue-Window-ex-2.png)

### ✨ Key Capabilities
- Custom **node-based visual graph editor**  
- **Enter/Exit node actions** fully customizable  
- **Condition nodes** for checking quests, items, stats, or gameplay state  
- Designed specifically for **RPG branching dialogue** needs  

### 🎯 Why This Matters
Building a custom Editor tool like this demonstrates:
- Deep understanding of Unity’s Editor API  
- Ability to build **production tools** used by designers/writers  
- Strong architecture thinking beyond runtime gameplay  
- Capability to extend the Unity Engine itself  

This tool showcases engineering skills far beyond typical gameplay scripting and highlights the ability to create **studio-level tooling**.

---

## 👤 Author
**Thanitsak Leuangsupornpong**
Software Engineer (Independent)
