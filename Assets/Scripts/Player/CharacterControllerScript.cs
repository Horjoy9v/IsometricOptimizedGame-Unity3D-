using UnityEngine;
using UnityEngine.UI;

public class CharacterControllerScript : MonoBehaviour
{
    [Header("Настройки Зброї")]
    public Slider bulletCountSlider;
    public byte bulletCount;
    private byte spreadAngle = 15;
    public Transform firePoint;
    private FasterFireRateDecorator fireRateDecorator;
    public CanvasController canvasController;

    [Header("Настройки физики")]
    public byte moveSpeed = 4;
    private byte rotationSpeed = 10;
    private float gravity = -1.25f;
    
    private CharacterController controller;
    private Quaternion characterRotation;
    private Transform characterTransform;
    [Header("Джойстик")]
    public CustomJoystick mobileJoystick;
    
    private void Awake()
    {
        mobileJoystick = GameObject.FindWithTag("Joystick").GetComponent<CustomJoystick>();
        canvasController = GameObject.FindWithTag("Hellper").GetComponent<CanvasController>();
        bulletCountSlider = GameObject.FindWithTag("Slider(Test)").GetComponent<Slider>();
        bulletCountSlider.onValueChanged.AddListener(AdjustVariableFromSlider);
        controller = GetComponent<CharacterController>();
        characterTransform = transform;
        characterRotation = Quaternion.Euler(0, 45, 0);
        fireRateDecorator = new FasterFireRateDecorator(new BasicWeapon());
        canvasController.fasterFireRateDecorator = fireRateDecorator;
    }

    private void FixedUpdate()
    {
        HandleMovement();
        fireRateDecorator.Shoot(firePoint, bulletCount, spreadAngle);
    }
    private void HandleMovement()
    {
        float horizontalInput = mobileJoystick.Horizontal();
        float verticalInput = mobileJoystick.Vertical();
        Vector3 moveInput = new Vector3(horizontalInput, 0, verticalInput);

        moveInput.y = gravity;

        if (moveInput.sqrMagnitude > 1)
        {
            moveInput.Normalize();
        }

        controller.Move(characterRotation * moveInput * moveSpeed * Time.fixedDeltaTime);

        Vector3 joystickDir = new Vector3(horizontalInput, 0, verticalInput).normalized;
        if (joystickDir != Vector3.zero)
        {
            float targetAngle = Mathf.Atan2(joystickDir.x, joystickDir.z) * Mathf.Rad2Deg;
            Quaternion targetRotation = Quaternion.Euler(0, targetAngle - 45, 0);
            characterTransform.rotation = Quaternion.Slerp(characterTransform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
        }
    }

    private void AdjustVariableFromSlider(float sliderValue)
    {
        bulletCount = (byte)Mathf.RoundToInt(sliderValue);
    }
}
