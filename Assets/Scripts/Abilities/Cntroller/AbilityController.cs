using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class AbilityController : MonoBehaviour
{
    [SerializeField] PlayerController player;
    [SerializeField] AbilityBase[] ability;

    [Header("Ability UI")]
    [SerializeField] Image imageAbility;
    [SerializeField] TextMeshProUGUI abilityName;
    [SerializeField] TextMeshProUGUI abilityLevel;

    int m_abilityIndex = 0;

    private void Start()
    {
        GameInput.Instance.dash.performed += UseAbility;
        GameInput.Instance.switchCharacter.performed += AbilitySwitch;

        SetUp();
    }

    public void UseAbility(InputAction.CallbackContext context)
    {
        ability[m_abilityIndex].UseAbility(player.gameObject);
    }

    private void AbilitySwitch(InputAction.CallbackContext context)
    {
        if (m_abilityIndex < ability.Length - 1)
        {
            m_abilityIndex++;
        }
        else
        {
            m_abilityIndex = 0;
        }

        imageAbility.sprite = ability[m_abilityIndex].abilityImage;
        abilityName.text = ability[m_abilityIndex].abilityName;
        abilityLevel.text = "Level : " + ability[m_abilityIndex].abilityLevel;
    }
    
    private void SetUp()
    {
        m_abilityIndex = 0;
        imageAbility.sprite = ability[m_abilityIndex].abilityImage;

        abilityName.text = ability[m_abilityIndex].abilityName;
        abilityLevel.text = "Level : " + ability[m_abilityIndex].abilityLevel;
    }


}
