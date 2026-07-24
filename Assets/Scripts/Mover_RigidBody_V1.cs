using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(Rigidbody))]
public class Mover_RigidBody_V1 : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float _baseForce = 10f;
    [SerializeField] private float _boostMultiplier = 2f;
    [SerializeField] private float _jumpForce = 10f;
    
    [Header("Ground Check (Collision)")]
    [Tooltip("Max slop angle to jump (0 — plane, 90 — wall)")]
    [Range(0f, 89f)] 
    [SerializeField] private float _maxSlopeAngle = 50f;
    
    private float _minGroundNormalY = 0.6f;

    private Rigidbody _rbMover;
    private Vector3 _jumpDirection = Vector3.up;
    private bool _jumpRequest = false;
    private bool _isGrounded = false;
    
    public Vector3 MoveDirection { get; private set; }

    void Start()
    {
        _rbMover = GetComponent<Rigidbody>();
        _minGroundNormalY = Mathf.Cos(_maxSlopeAngle * Mathf.Deg2Rad);
    }

    public void SetMovementCommand(Vector3 direction, bool isBoosting = false, bool jump = false)
    {
        if (jump && _isGrounded)
            _jumpRequest = true;

        if (direction.sqrMagnitude < 0.001f)
        {
            MoveDirection = new Vector3();
            return;
        }
        
        float currentBoost = isBoosting ? _boostMultiplier : 1f;
        if (_isGrounded)
            MoveDirection = direction * (_baseForce * currentBoost);
    }

    private void FixedUpdate()
    {
        if (_jumpRequest)
        {
            _rbMover.AddForce(_jumpDirection * _jumpForce, ForceMode.Impulse);
            _jumpRequest = false;
            _isGrounded = false;
        }
        
        _rbMover.AddForce(MoveDirection, ForceMode.Force);
        
        _isGrounded = false;
    }
    
    private void OnCollisionStay(Collision collision)
    {
        for (int i = 0; i < collision.contactCount; i++)
        {
            ContactPoint contact = collision.GetContact(i);
            
            if (contact.normal.y >= _minGroundNormalY)
            {
                _isGrounded = true;
            }
        }
    }
}
