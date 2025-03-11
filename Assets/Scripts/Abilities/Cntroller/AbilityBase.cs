using UnityEngine;

public class AbilityBase : ScriptableObject, IAbilitable
{
    [Header("Ability Settings")]
    public string abilityName;
    public Sprite abilityImage;
    public int abilityLevel = 1;

    public virtual void UseAbility(GameObject player) { }
}
