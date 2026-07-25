using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    [SerializeField] private float _speed = 1f;
    private Vector3 _targetPosition;

    private void Start()
    {
        _targetPosition = transform.position;
    }

    public void SetTargetCommand(Vector3 target)
    {
        _targetPosition = target;
    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, _targetPosition, _speed * Time.deltaTime);
    }
}
