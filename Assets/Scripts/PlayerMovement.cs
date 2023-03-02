using System;
using UnityEngine;
using UnityEngine.Serialization;


public class PlayerMovement : MonoBehaviour
{
    public CharacterController2D Controller;
    
    public float RunSpeed = 40f;
    
    float _horizontalMove = 0f;
    bool _jump = false;

    void Update()
    {
        _horizontalMove = Input.GetAxis("Horizontal") * RunSpeed;
        
        if (Input.GetButtonDown("Jump"))
        {
            _jump = true;
        }
    }
    
    void FixedUpdate()
    {
        Controller.Move(_horizontalMove * Time.fixedDeltaTime, _jump);
        _jump = false;
    }
}