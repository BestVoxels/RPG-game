# 🎯 RPG System Project — Unity (C#)

A fully modular, architecturally driven RPG system built in Unity over several months.  
This project showcases clean code, scalable design, and production-oriented gameplay systems.

---

## 🏛️ Architecture Overview

This RPG system is designed using modular, loosely coupled, high-cohesion principles, and much more!

### **Key Principles Used**

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

## 🧩 System Architecture Diagram

> This architecture map visualizes the high-level relationships between the systems, modules, and gameplay logic.

![RPG Architecture Diagram](Images/RPG_Architecture.png)

---

## ⚔️ Core Features Included

### **✔ Character System**
- Character stats, level, progression  
- Health, mana, stamina  
- Status effects / buffs & debuffs  
- Attribute modifiers  
- Event-driven updates  

### **✔ Ability / Skill System**
- Interface-driven ability modules  
- Cooldowns, costs, conditions  
- Targeting logic  
- Logic separated from visuals  

### **✔ Inventory & Item System**
- Item definitions  
- Equipment slots  
- Consumables  
- ScriptableObject-based data  
- Modular item behaviors  

### **✔ Combat System**
- Melee & ranged logic  
- Hit detection  
- Damage events  
- Attack patterns  
- Animation-driven events  

### **✔ NPC System**
- State machines  
- Behavior modules  
- Threat evaluation  

### **✔ Input / Controller System**
- Input abstraction layer  
- Player controller using composition  
- Animation layers & movement logic  

### **✔ UI / UX System**
- Data-binding style UI  
- Health bars, inventory UI, skill UI  
- Modular UI components  

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

## 🧠 Technical Highlights

### **🧹 Clean Code**
- Interfaces used everywhere  
- Minimal direct references  
- Event-driven communication  

### **🚀 Performance**
- Object pooling  
- No GC spikes  
- Lightweight runtime structures  

### **🧩 Modularity**
- Each subsystem replaceable independently  
- Logic fully separated from visuals  
- Data-driven behaviors  

### **📈 Scalability**
- Designed for production-scale expansion  
- Easy to add new abilities, items, enemies, stats  

---

## 👤 Author

**Thanitsak Leuangsupornpong**  
Software Developer / Game Developer  

---
