using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "NewAbility",menuName = "Ability/Dash")]
public class DashAbility : AbilityBase
{
    [Header("Dash Settings")]
    public float dashSpeed = 10;
    public float dashDuration = 1;
    public float dashCoolDown = 3;
    bool m_canDash = true;

    public override void UseAbility(GameObject player)
    {
        Rigidbody2D rg = player.GetComponent<Rigidbody2D>();
        MonoBehaviour playerBehv = player.GetComponent<MonoBehaviour>();

        playerBehv.StartCoroutine(Dash(rg));
    }

    IEnumerator Dash(Rigidbody2D rg)
    {
        //setup Dash
        m_canDash = false;

        //save gravity
        float originalGravity = rg.gravityScale;
        rg.gravityScale = 0;

        //add force for dash
        rg.linearVelocity = new Vector2(rg.transform.localScale.x * dashSpeed, 0);

        //Wait before stop
        yield return new WaitForSeconds(dashDuration);

        //Stop
        rg.linearVelocity = new Vector2(GameInput.Instance.MoveMent().x, rg.linearVelocity.y);

        //reset gravity
        rg.gravityScale = originalGravity;

        //wait
        yield return new WaitForSeconds(dashCoolDown);
        m_canDash = true;
    }
}
