using System;
using UnityEngine;
using UnityEngine.Serialization;


public class PlayerMovement : MonoBehaviour
{
    public CharacterController2D Controller;
    public Animator Animator;
    
    public float RunSpeed = 40f;
    
    float _horizontalMove = 0f;
    bool _jump = false;

    void Update()
    {
        _horizontalMove = Input.GetAxis("Horizontal") * RunSpeed;
        
        Animator.SetFloat("Speed", Mathf.Abs(_horizontalMove));
        
        if (Input.GetButtonDown("Jump"))
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