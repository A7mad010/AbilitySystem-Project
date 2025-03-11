# 🎮 Ability System for Unity

🚀 **Ability System** is a flexible and extensible system for managing abilities in **Unity Engine** using **C# and ScriptableObjects**.

---

## ✨ Features
✅ **ScriptableObject-based** for storing abilities outside of code.  
✅ **Supports multiple abilities** and triggers them using `InputSystem`.  
✅ **Separation of ability logic and player control** for better reusability.  

---

## 🎮 Controls
- **Q** → Switch Ability  
- **Ctrl** → Use Ability
  
---
## 🔧 Extending the System (Adding New Abilities)  

### 1️⃣ Create a New Ability Script  
- Create a new `C# Script` that **inherits from `AbilityBase`** and **implements `UseAbility()`**.  

Example:  
```csharp
[CreateAssetMenu(fileName = "NewExampleAbility", menuName = "Ability/Example")]
public class ExampleAbility : AbilityBase
{
    public override void UseAbility()
    {
        Debug.Log("you use ability!");
    }
}
```

### 2️⃣ Create an Ability Asset
- Right-click in the Project window → Create > Ability > Jump. 
- Drag it into the abilities[] list in AbilityController.



