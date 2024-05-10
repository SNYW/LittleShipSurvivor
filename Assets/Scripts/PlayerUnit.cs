using System;
using UnityEngine;

public class PlayerUnit : Unit
{
    private Rigidbody2D _rb;

    public float baseTurnSpeed;
    public float baseMoveSpeed;

    public WeaponChainDefinition startChain;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        WeaponManager.UpgradeChain(startChain);
    }

    private void FixedUpdate()
    {
        _rb.AddForce(transform.up * baseMoveSpeed, ForceMode2D.Force);
        
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
        _rb.AddTorque(turn);
    }

    private void Boost()
    {
        _rb.AddForce(transform.up * baseMoveSpeed, ForceMode2D.Force);
    }
}
