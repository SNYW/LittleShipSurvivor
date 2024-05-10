using System;
using UnityEngine;

public class PlayerUnit : Unit
{
    public float baseTurnSpeed;
    public float baseMoveSpeed;

    public WeaponChainDefinition startChain;

    private void Awake()
    {
        Rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        WeaponManager.UpgradeChain(startChain);
    }

    private void FixedUpdate()
    {
        Rb.AddForce(transform.up * baseMoveSpeed, ForceMode2D.Force);
        
        if (GameManager.turningLeft && !GameManager.turningRight)
        {
            Turn(-baseTurnSpeed);
        }
        else if (GameManager.turningRight && !GameManager.turningLeft)
        {
            Turn(baseTurnSpeed);
        }
        else if(GameManager.turningLeft && GameManager.turningRight)
        {
            Boost();
        }
    }

    private void Turn(float turn)
    {
        Rb.AddTorque(turn);
    }

    private void Boost()
    {
        Rb.AddForce(transform.up * baseMoveSpeed, ForceMode2D.Force);
    }
}
