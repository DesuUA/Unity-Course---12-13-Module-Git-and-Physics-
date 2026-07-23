using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(Rigidbody))]
public class Mover_RigidBody_V1 : MonoBehaviour
{
    [SerializeField] private float _baseForce = 5f;
    [SerializeField] private float _boostMultiplier = 2f;
    [SerializeField] private float _jumpForce = 5f;

    private Vector3 _jumpDirection = Vector3.up;
    private Rigidbody _rbMover;

    private bool _isJumping = false;
    
    public Vector3 MoveDirection { get; private set; }

    void Start()
    {
        _rbMover = GetComponent<Rigidbody>();
    }

    public void SetMovementCommand(Vector3 direction, bool isBoosting = false, bool jump = false)
    {
        if (jump)
            _isJumping = true;
        
        if (direction.sqrMagnitude < 0.001f)
        {
            MoveDirection = new Vector3();
            return;
        }
        
        float currentBoost = _baseForce * (isBoosting ? _boostMultiplier : 1f);
        
        MoveDirection = direction * (_baseForce * currentBoost);
    }

    private void FixedUpdate()
    {
        if (_isJumping)
        {
            _rbMover.AddForce(_jumpDirection * _jumpForce, ForceMode.Impulse);
            _isJumping = false;
        }
        
        _rbMover.AddForce(MoveDirection, ForceMode.Impulse);
    }
}
