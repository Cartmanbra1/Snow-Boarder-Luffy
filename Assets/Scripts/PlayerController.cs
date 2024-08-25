using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float torqueAmount = 1f;
    [SerializeField] float boostSpeed = 35f;
    [SerializeField] float normalSpeed = 25f;
    [SerializeField] float timeOfBoost = 4f;
    float timer = 0f;

    Rigidbody2D rb2d;
    SurfaceEffector2D surfaceEffector2D;
    public static bool canMove = true;
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        surfaceEffector2D = FindObjectOfType<SurfaceEffector2D>();
    }

    
    void Update()
    {
        if (canMove)
        {
            RotatePlayer();
            RespondToBoost();
        }
    }

    public static void ChangeControls()
    {
        canMove = !canMove;
    }

    void RespondToBoost()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            while (timer < timeOfBoost)
            {
                timer += Time.deltaTime;
                if (normalSpeed <= boostSpeed)
                {
                    normalSpeed += Time.deltaTime;
                    surfaceEffector2D.speed = normalSpeed;
                }
            }
            timer = 0f;
        } else if (surfaceEffector2D.speed > 25f)
        {
            timer = timeOfBoost;
            while (timer > 0f && surfaceEffector2D.speed >= 25f)
            {
                timer -= Time.deltaTime;
                normalSpeed -= Time.deltaTime;
                surfaceEffector2D.speed = normalSpeed;
            }
        }
    }

    void RotatePlayer()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            rb2d.AddTorque(torqueAmount);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            rb2d.AddTorque(-torqueAmount);
        }
    }
}
