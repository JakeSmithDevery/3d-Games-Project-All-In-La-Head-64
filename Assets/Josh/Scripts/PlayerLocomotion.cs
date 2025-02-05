using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerLocomotion : MonoBehaviour
{
    [SerializeField] private float gravity = -9.81f;
    [SerializeField] private float walkSpeed = 10;
    //[SerializeField] private float jumpHeight = 7;
    [SerializeField] private float lookSensitivity = 0.5f;
    [SerializeField] private float verticalLookLimit = 75;

    private CharacterController controller;
    private Vector2 movementInput;
    private Vector3 currentVelocity;
    private bool isOnGround;
    public PlayerMeleeAttack meleeAttack;
    public PlayerGunAttack gunAttack;
    public PlayerStats stats;
    public double MaxHealth = 100;
    public double Health;
    public GameManager GameManager;


    private Animator animator;

    private Camera playerCamera;
    private Vector2 lookInput;
    private float xRotation;
    private float yRotation;

    
    // Start is called before the first frame update
    void Start()
    {
       controller = GetComponent<CharacterController>();
       playerCamera = GetComponentInChildren<Camera>();
        meleeAttack = GetComponent<PlayerMeleeAttack>();
        gunAttack = GetComponent<PlayerGunAttack>();
        stats = FindAnyObjectByType<PlayerStats>();
        animator = GetComponent<Animator>();
        GameManager = FindAnyObjectByType<GameManager>();
        if (stats != null)
        {
            GameManager.instance.SetPlayerStats(stats);
        }
        else
        {
            Debug.LogError("PlayerStats component not found on the player GameObject.");
        }

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        Health = MaxHealth * ((stats.HealthPoints * 0.05) + 1);
        MaxHealth = MaxHealth * ((stats.HealthPoints * 0.05) + 1);
        gunAttack.maxAmmo *= (stats.AmmoPoints * 0.1) +1;
        walkSpeed *= (float)((stats.SpeedPoints * 0.1) + 1);
    }

    // Update is called once per frame
    void Update()
    {
        isOnGround = controller.isGrounded;

        if (isOnGround && currentVelocity.y <0)
        {
            currentVelocity.y = 0f;
        
        }

        

        controller.Move((
            transform.forward * movementInput.y +
            transform.right * movementInput.x) *
            walkSpeed * Time.deltaTime);

        currentVelocity.y += gravity * Time.deltaTime;

        controller.Move(currentVelocity *  Time.deltaTime);
    }

    //public void OnJump()
    //{ 
    //    if (isOnGround)
    //    {
    //        currentVelocity.y += jumpHeight;
    //    }
    //}

    public void OnMove(InputValue input)
    {
        movementInput = input.Get<Vector2>();
    }

    public void OnLook(InputValue input) 
    {

        lookInput = input.Get<Vector2>() * lookSensitivity;

        xRotation += lookInput.y;
        xRotation = Mathf.Clamp(xRotation, -verticalLookLimit, verticalLookLimit);

        yRotation += lookInput.x;

        playerCamera.transform.localRotation = Quaternion.Euler(xRotation, -180, 0);
        transform.rotation = Quaternion.Euler(0, yRotation, 0);
    
    }

    public void OnAttack(InputValue input) 
    {
        animator.Play("Swing");
        meleeAttack.PerformMeleeAttack();
        animator.Play("GunIdle");
    }

    public void OnFire(InputValue input) 
    {
        animator.Play("pew");
        gunAttack.CheckAmmo();
        animator.Play("GunIdle");

    }

    public void TakeDamage(int amount)
    {
        Health -= amount;
        if (Health <= 0)
        {
            GameManager.SaveGame();
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
            SceneManager.LoadScene("MenuTest");
        }
    }

    



}
