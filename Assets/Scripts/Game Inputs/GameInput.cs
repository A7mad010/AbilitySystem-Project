using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour
{
    public static GameInput Instance;
    PlayerInputActions m_playerInput;

    //Player
    [HideInInspector] public InputAction move; //Vector;
    [HideInInspector] public InputAction jump; //Button
    [HideInInspector] public InputAction dash; //Button
    [HideInInspector] public InputAction attack1; //Button
    [HideInInspector] public InputAction attack2; //Button
    [HideInInspector] public InputAction attack3; //Button
    [HideInInspector] public InputAction intractTalk; //Button
    [HideInInspector] public InputAction heal; //Button
    [HideInInspector] public InputAction block; //Button
    [HideInInspector] public InputAction switchCharacter; //Button
    [HideInInspector] public InputAction callCharacter; //Button
    [HideInInspector] public InputAction linkCharacter; //Button

    //UI
    [HideInInspector] public InputAction save; //Button
    [HideInInspector] public InputAction navigate; //Button
    [HideInInspector] public InputAction select; //Button
    [HideInInspector] public InputAction inventory; //Button

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(Instance);
        }

        DontDestroyOnLoad(this.gameObject);
    }

    private void OnEnable()
    {
        m_playerInput = new PlayerInputActions();

        //Player 
        move = m_playerInput.Player.Move;
        jump = m_playerInput.Player.Jump;
        dash = m_playerInput.Player.Dash;
        attack1 = m_playerInput.Player.Attack1;
        attack2 = m_playerInput.Player.Attack2;
        attack3 = m_playerInput.Player.Attack3;
        intractTalk = m_playerInput.Player.IntractTalk;
        heal = m_playerInput.Player.Heal;
        block = m_playerInput.Player.Block;
        switchCharacter = m_playerInput.Player.SwitchCharacter;
        callCharacter = m_playerInput.Player.CallCharacter;
        linkCharacter = m_playerInput.Player.LinkCharacters;

        //UI
        save = m_playerInput.Ui.Save;
        select = m_playerInput.Ui.Select;
        navigate = m_playerInput.Ui.Navigate;
        inventory = m_playerInput.Ui.Inventory;

        //Enable
        move.Enable();
        jump.Enable();
        dash.Enable();
        attack1.Enable();
        attack2.Enable();
        attack3.Enable();
        intractTalk.Enable();
        heal.Enable();
        block.Enable();
        switchCharacter.Enable();
        callCharacter.Enable();
        linkCharacter.Enable();

        save.Enable();
        select.Enable();
        navigate.Enable();
        inventory.Enable();
    }
    private void OnDisable()
    {
        move.Disable();
        jump.Disable();
        dash.Disable();
        attack1.Disable();
        attack2.Disable();
        attack3.Disable();
        intractTalk.Enable();
        heal.Disable();
        block.Disable();
        switchCharacter.Disable();
        callCharacter.Disable();

        save.Disable();
        select.Disable();
        navigate.Disable();
        inventory.Disable();
    }

    //PlayerMovement
    public Vector2 MoveMent() 
    {
        return move.ReadValue<Vector2>();
    }
}


