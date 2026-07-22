using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputMovement : MonoBehaviour
{
    private const string HorizontalAxisName = "Horizontal";
    private const string VerticalAxisName = "Vertical";
    
    [SerializeField] private Mover _mover;
    [SerializeField] private KeyCode _boostKey = KeyCode.LeftShift;

    private void Update()
    {
        Vector3 input = new Vector3(Input.GetAxisRaw(HorizontalAxisName), 0, Input.GetAxisRaw(VerticalAxisName));
        
        Vector3 inputDirection = new Vector3(input.x, 0, input.z).normalized;
        bool isBoosting = Input.GetKey(_boostKey);
        
        _mover.SetMovementCommand(inputDirection, isBoosting);
    }
}
