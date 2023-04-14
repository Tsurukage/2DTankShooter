using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class MobileJoystick : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{
    private RectTransform joyStickTransform;
    [SerializeField] private int dragMovementDistance = 50;
    [SerializeField] private int dragOffsetDistance = 100;
    private Vector2 joystickVec;
    private Vector2 joystickTouchPos;

    public event Action<Vector2> OnMove;
    public event Action OnUp;

    private void Awake()
    {
        joyStickTransform = (RectTransform)transform;
    }
    public void OnDrag(PointerEventData eventData)
    {
        Vector2 dragPos = eventData.position;
        joystickVec = (dragPos - joystickTouchPos).normalized;
        OnMove?.Invoke(joystickVec);

        Vector2 offset;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            joyStickTransform,
            eventData.position,
            null,
            out offset);
        offset = Vector2.ClampMagnitude(offset, dragOffsetDistance) / dragOffsetDistance;
        joyStickTransform.anchoredPosition = offset * dragMovementDistance;
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        joyStickTransform.anchoredPosition = Vector2.zero;
        OnUp?.Invoke();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        joystickTouchPos = Input.mousePosition;
    }
    public void SetInteraction(bool isInteractable)
    {
        transform.parent.gameObject.SetActive(isInteractable);
    }
}
