using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class IdleRotator : MonoBehaviour
{
    [SerializeField] private float _rotateSpeed = 50f;

    private void Start()
    {
        float randomStartAngle = Random.Range(0f, 360f);
        transform.Rotate(0f, randomStartAngle, 0f, Space.Self);
    }

    private void Update()
    {
        transform.Rotate(0f, _rotateSpeed * Time.deltaTime, 0f, Space.Self);
    }
}
