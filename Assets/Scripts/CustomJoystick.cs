using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CustomJoystick : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{
    private Image joystickBG;
    [SerializeField]
    private Image joystick;
    private Vector2 inputVector;

    private Vector2 cachedSizeDelta;


private void Start()
{
    joystickBG = GetComponent<Image>();
    joystick = transform.GetChild(0).GetComponent<Image>();
    cachedSizeDelta = joystickBG.rectTransform.sizeDelta;
}

public virtual void OnPointerDown(PointerEventData eventData)
{
    OnDrag(eventData);
}

public virtual void OnPointerUp(PointerEventData eventData)
{
    inputVector = Vector2.zero;
    joystick.rectTransform.anchoredPosition = Vector2.zero;
}

public virtual void OnDrag(PointerEventData eventData)
{
    // Отримуємо локальні координати пальця відносно joystickBG
    Vector2 localPointerPosition;
    if (RectTransformUtility.ScreenPointToLocalPointInRectangle(joystickBG.rectTransform, eventData.position, eventData.pressEventCamera, out localPointerPosition))
    {
        // Визначаємо нові координати пальця
        inputVector = localPointerPosition * 2f / cachedSizeDelta.y;
        inputVector = (inputVector.magnitude > 1.0f) ? inputVector.normalized : inputVector;

        // Позиція середини джойстика
        joystick.rectTransform.anchoredPosition = new Vector2(inputVector.x * (cachedSizeDelta.y / 2), inputVector.y * (cachedSizeDelta.y / 2));
    }
}

public float Horizontal()
{
    if (inputVector.x != 0) return inputVector.x;
    else return Input.GetAxis("Horizontal");
}

public float Vertical()
{
    if (inputVector.y != 0) return inputVector.y;
    else return Input.GetAxis("Vertical");
}

}
