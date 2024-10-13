using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Параметры игрока
    [SerializeField] private float maxHealth = 100f;  // Максимальное здоровье игрока
    private float currentHealth;  // Текущее здоровье игрока

    [SerializeField] private float moveSpeed = 5f;  // Скорость перемещения
    [SerializeField] private float jumpForce = 5f;  // Сила прыжка
    [SerializeField] private float gravityMultiplier = 2f;  // Усиление силы тяжести
    [SerializeField] private bool isGrounded = false;  // Флаг, находится ли игрок на земле

    private Rigidbody rb;  // Ссылка на Rigidbody игрока
    private Vector3 movementInput;  // Направление перемещения
    private bool jumpInput;  // Ввод для прыжка

    public Transform groundCheck;  // Точка проверки касания земли
    [SerializeField] private float groundCheckRadius = 0.2f;  // Радиус проверки касания земли
    public LayerMask groundMask;  // Слой для земли

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;  // Отключаем вращение физики для стабильности управления
        currentHealth = maxHealth;  // Инициализируем здоровье на старте
    }

    private void Update()
    {
        // Получаем ввод от пользователя для движения
        movementInput = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")).normalized;

        // Проверяем ввод для прыжка
        jumpInput = Input.GetButtonDown("Jump");

        // Проверка, находится ли игрок на земле
        isGrounded = Physics.CheckSphere(groundCheck.position, groundCheckRadius, groundMask);
    }

    private void FixedUpdate()
    {
        // Реализуем движение
        MovePlayer();

        // Реализуем прыжок
        if (jumpInput && isGrounded)
        {
            Jump();
        }

        // Применяем усиленную гравитацию
        ApplyExtraGravity();
    }

    // Метод для движения игрока
    private void MovePlayer()
    {
        // Перемещение игрока в зависимости от ввода
        Vector3 move = transform.right * movementInput.x + transform.forward * movementInput.z;
        rb.MovePosition(rb.position + move * moveSpeed * Time.fixedDeltaTime);
    }

    // Метод для прыжка
    private void Jump()
    {
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }

    // Метод для усиления гравитации
    private void ApplyExtraGravity()
    {
        if (!isGrounded)
        {
            rb.AddForce(Physics.gravity * (gravityMultiplier - 1), ForceMode.Acceleration);
        }
    }

    // Метод для получения урона от ловушек
    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        Debug.Log("Player took damage: " + damage);
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    // Метод для "смерти" игрока
    private void Die()
    {
        Debug.Log("Player died!");
        // Логика для смерти игрока, например, перезапуск уровня
        // Destroy(gameObject);  // Временно уничтожаем объект игрока для простоты
    }

    // Метод для воздействия ветра
    public void ApplyWindForce(Vector3 force)
    {
        rb.AddForce(force, ForceMode.Force);  // Применяем силу к Rigidbody
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Дополнительные обработки для других взаимодействий
    }
}
