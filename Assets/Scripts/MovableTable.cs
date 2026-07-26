using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(Rigidbody))]
public class MovableTable : MonoBehaviour
{
    private const string HorizontalAxisName = "Horizontal";
    private const string VerticalAxisName = "Vertical";
    
    [Header("Rotate settings")]
    [SerializeField] private float _maxTiltAngle = 15f;
    [SerializeField] private float _tiltSpeed = 5f;

    public bool Enabled { get; private set; }
    
    private Rigidbody _rb;
    private float _currentTiltX;
    private float _currentTiltZ;

    private float _inputHorizontal;
    private float _inputVertical;
    
    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _rb.isKinematic = true;
    }

    private void Update()
    {
        if (Enabled == false)
            return;
        
        _inputHorizontal = Input.GetAxis(HorizontalAxisName);
        _inputVertical = Input.GetAxis(VerticalAxisName);
    }
    private void FixedUpdate()
    {
        float targetTiltX = _inputVertical * _maxTiltAngle;
        float targetTiltZ = -_inputHorizontal * _maxTiltAngle;

        _currentTiltX = Mathf.Lerp(_currentTiltX, targetTiltX, Time.fixedDeltaTime * _tiltSpeed);
        _currentTiltZ = Mathf.Lerp(_currentTiltZ, targetTiltZ, Time.fixedDeltaTime * _tiltSpeed);

        Quaternion targetRotation = Quaternion.Euler(_currentTiltX, 0f, _currentTiltZ);

        _rb.MoveRotation(targetRotation);
    }

    private void OnTriggerEnter(Collider other) => Enabled = true;
    
    private void OnTriggerExit(Collider other) => Enabled = false;
}