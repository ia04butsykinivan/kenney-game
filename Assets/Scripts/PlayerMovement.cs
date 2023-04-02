using System;
using UnityEngine;
using UnityEngine.Serialization;


public class PlayerMovement : MonoBehaviour
{
    public CharacterController2D Controller;
    public Animator Animator;

    public Joystick Joystick;
    
    public float RunSpeed = 40f;
    
    float _horizontalMove = 0f;
    bool _jump = false;

    void Update()
    {
        if (Joystick.Horizontal >= .2f || Input.GetAxis("Horizontal") > 0)
        {
            _horizontalMove = RunSpeed;
        }
        else if (Joystick.Horizontal <= -.2f || Input.GetAxis("Horizontal") < 0)
        {
            _horizontalMove = -RunSpeed;
        }
        else
        {
            _horizontalMove = 0;
        }

        Animator.SetFloat("Speed", Mathf.Abs(_horizontalMove));

        if (Input.GetButtonDown("Jump") || Joystick.Vertical >= .2f)
        {
            _jump = true;

            Animator.SetBool("IsJumping", true);
        }
    }
    
    public void OnLanding()
    {
        Animator.SetBool("IsJumping", false);
    }
    
    void FixedUpdate()
    {
        Controller.Move(_horizontalMove * Time.fixedDeltaTime, _jump);
        _jump = false;
    }
}