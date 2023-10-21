using UnityEngine;
using Unity.Burst;

public class CharacterControllerScript : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5.0f;
    [SerializeField] private float gravity; // Гравітація
    [SerializeField] private CustomJoystick mobileJoystick;
    [SerializeField] private Camera mainCamera;

    private CharacterController controller;
    private Vector3 moveDirection;
    private float cameraRotationY;
    private float cameraRotationZ;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        mainCamera = Camera.main;
        cameraRotationY = mainCamera.transform.rotation.eulerAngles.y;
        cameraRotationZ = mainCamera.transform.rotation.eulerAngles.z;
    }

    private void FixedUpdate()
    {
        HandleMovement();
    }

   [BurstCompile]
    private void HandleMovement()
    {
        float horizontalInput = mobileJoystick.Horizontal();
        float verticalInput = mobileJoystick.Vertical();

        // Визначаємо напрямок руху на площині XZ
        Vector3 moveInput = new Vector3(horizontalInput, 0, verticalInput);

        // Обчислюємо кватерніон, який представляє нахил камери
        Quaternion cameraRotation = Quaternion.Euler(0, cameraRotationY, cameraRotationZ);

        // Повертаємо вектор руху на основі нахилу камери
        moveDirection = cameraRotation * moveInput;

        // Нормалізуємо вектор руху, якщо його довжина перевищує 1
        moveDirection = Vector3.ClampMagnitude(moveDirection, 1);

        if (!controller.isGrounded)
        {
            moveDirection.y += gravity;
        }

        // Виконуємо рух персонажа
        controller.Move(moveDirection * moveSpeed * Time.fixedDeltaTime);
    }

}
