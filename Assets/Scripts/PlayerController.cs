using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _maxHealth = 100f;  
    private float _currentHealth;  

    [SerializeField] private float _moveSpeed = 5f;  
    [SerializeField] private float _jumpForce = 5f;  
    [SerializeField] private bool _isGrounded = false;  

    private Rigidbody _rb;  
    private Vector3 _movementInput;  
    private bool _jumpInput;  

    [SerializeField] private Transform _groundCheck;  
    [SerializeField] private float _groundCheckRadius = 0.2f;  
    [SerializeField] private LayerMask _groundMask;

    public float GetMoveSpeed()
    {
        return _moveSpeed;
    }
    public float GetHealth()
    {
        return _currentHealth;
    }

    public void SetMoveSpeed(float newSpeed)
    {
        _moveSpeed = newSpeed;
    }

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _rb.freezeRotation = true;  
        _currentHealth = _maxHealth;  
    }

    private void Update()
    {
        _movementInput = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")).normalized;

        _jumpInput = Input.GetButtonDown("Jump");

        _isGrounded = Physics.CheckSphere(_groundCheck.position, _groundCheckRadius, _groundMask);

        if (_jumpInput && _isGrounded)
        {
            Jump();
        }
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void MovePlayer()
    {
        Vector3 move = transform.right * _movementInput.x + transform.forward * _movementInput.z;
        _rb.MovePosition(_rb.position + move * _moveSpeed * Time.fixedDeltaTime);
    }

    private void Jump()
    {
        _rb.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
    }

    public void TakeDamage(float damage)
    {
        _currentHealth -= damage;
        Debug.Log("Player took damage: " + damage);
        if (_currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("Player died!");
    }

    public void ApplyWindForce(Vector3 force)
    {
        _rb.AddForce(force, ForceMode.Force);  
    }

    
}
