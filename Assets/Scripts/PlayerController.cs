using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // ��������� ������
    [SerializeField] private float maxHealth = 100f;  // ������������ �������� ������
    private float currentHealth;  // ������� �������� ������

    [SerializeField] private float moveSpeed = 5f;  // �������� �����������
    [SerializeField] private float jumpForce = 5f;  // ���� ������
    [SerializeField] private float gravityMultiplier = 2f;  // �������� ���� �������
    [SerializeField] private bool isGrounded = false;  // ����, ��������� �� ����� �� �����

    private Rigidbody rb;  // ������ �� Rigidbody ������
    private Vector3 movementInput;  // ����������� �����������
    private bool jumpInput;  // ���� ��� ������

    public Transform groundCheck;  // ����� �������� ������� �����
    [SerializeField] private float groundCheckRadius = 0.2f;  // ������ �������� ������� �����
    public LayerMask groundMask;  // ���� ��� �����

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;  // ��������� �������� ������ ��� ������������ ����������
        currentHealth = maxHealth;  // �������������� �������� �� ������
    }

    private void Update()
    {
        // �������� ���� �� ������������ ��� ��������
        movementInput = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")).normalized;

        // ��������� ���� ��� ������
        jumpInput = Input.GetButtonDown("Jump");

        // ��������, ��������� �� ����� �� �����
        isGrounded = Physics.CheckSphere(groundCheck.position, groundCheckRadius, groundMask);
    }

    private void FixedUpdate()
    {
        // ��������� ��������
        MovePlayer();

        // ��������� ������
        if (jumpInput && isGrounded)
        {
            Jump();
        }

        // ��������� ��������� ����������
        ApplyExtraGravity();
    }

    // ����� ��� �������� ������
    private void MovePlayer()
    {
        // ����������� ������ � ����������� �� �����
        Vector3 move = transform.right * movementInput.x + transform.forward * movementInput.z;
        rb.MovePosition(rb.position + move * moveSpeed * Time.fixedDeltaTime);
    }

    // ����� ��� ������
    private void Jump()
    {
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }

    // ����� ��� �������� ����������
    private void ApplyExtraGravity()
    {
        if (!isGrounded)
        {
            rb.AddForce(Physics.gravity * (gravityMultiplier - 1), ForceMode.Acceleration);
        }
    }

    // ����� ��� ��������� ����� �� �������
    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        Debug.Log("Player took damage: " + damage);
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    // ����� ��� "������" ������
    private void Die()
    {
        Debug.Log("Player died!");
        // ������ ��� ������ ������, ��������, ���������� ������
        // Destroy(gameObject);  // �������� ���������� ������ ������ ��� ��������
    }

    // ����� ��� ����������� �����
    public void ApplyWindForce(Vector3 force)
    {
        rb.AddForce(force, ForceMode.Force);  // ��������� ���� � Rigidbody
    }

    private void OnCollisionEnter(Collision collision)
    {
        // �������������� ��������� ��� ������ ��������������
    }
}
