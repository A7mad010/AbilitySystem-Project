using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "NewAbility", menuName = "Ability/Shield")]
public class ShieldAbility : AbilityBase
{
    [Header("Shield Setting")]
    [SerializeField] Color shieldColor;
    [SerializeField] float shildTime = 10;
    [SerializeField] float duration;

    SpriteRenderer m_sprite;
    Color m_orgnaleColor;

    public override void UseAbility(GameObject player)
    {
        MonoBehaviour behv = player.GetComponent<MonoBehaviour>();
        m_sprite = player.GetComponent<SpriteRenderer>();

        m_orgnaleColor = m_sprite.color;

        behv.StartCoroutine(useShield());
    }

    IEnumerator useShield()
    {
        //Shield On
        m_sprite.color = shieldColor;

        yield return new WaitForSeconds(shildTime);

        //Shield Off
        m_sprite.color = m_orgnaleColor;
    }
}
