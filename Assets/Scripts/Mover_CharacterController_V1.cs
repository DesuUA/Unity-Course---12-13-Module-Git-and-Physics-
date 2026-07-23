using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class Mover_CharacterController_V1 : MonoBehaviour
{
    [SerializeField] private float _baseSpeed = 5f;
    [SerializeField] private float _boostMultiplier = 2f;
    [SerializeField] private float _rotationSpeed = 180f;
    
    private CharacterController _characterController;

    private void Start()
    {
        _characterController = GetComponent<CharacterController>();
    }

    public void SetMovementCommand(Vector3 direction, bool isBoosting = false)
    {
        if (direction.sqrMagnitude < 0.001f) return;
        
        Vector3 directionXZ = new Vector3(direction.x, 0f, direction.z);
        
        ProcessMoveTo(directionXZ.normalized, isBoosting);
        
        ProcessRotateTo(directionXZ);
    }

    private void ProcessMoveTo(Vector3 direction, bool isBoosting)
    {
        float speed;
        if (isBoosting) speed = _boostMultiplier * _baseSpeed;
        else speed = _baseSpeed;
        
        _characterController.Move(direction * (speed * Time.deltaTime));
    }
    
    private void ProcessRotateTo(Vector3 direction)
    {
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        
        float step = Time.deltaTime * _rotationSpeed;
        
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, step);
    }
}
