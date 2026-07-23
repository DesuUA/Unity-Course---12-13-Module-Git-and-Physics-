using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputMovement_V1 : MonoBehaviour
{
    private const string HorizontalAxisName = "Horizontal";
    private const string VerticalAxisName = "Vertical";
    
    [SerializeField] private Mover_RigidBody_V1 _mover;
    [SerializeField] private KeyCode _boostKey = KeyCode.LeftShift;
    [SerializeField] private KeyCode _jumpKey = KeyCode.Space;

    private void Update()
    {
        Vector3 input = new Vector3(Input.GetAxisRaw(HorizontalAxisName), 0, Input.GetAxisRaw(VerticalAxisName));
        
        bool jump = Input.GetKeyDown(_jumpKey) ? true : false;
        
        Vector3 inputDirection = new Vector3(input.x, 0, input.z).normalized;
        bool isBoosting = Input.GetKey(_boostKey);
        
        _mover.SetMovementCommand(inputDirection, isBoosting, jump);
    }
}
