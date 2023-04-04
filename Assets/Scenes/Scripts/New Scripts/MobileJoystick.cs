using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class MobileJoystick : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{
    private RectTransform joyStickTransform;
    [SerializeField] private float dragThreshold = 0.6f;
    [SerializeField] private int dragMovementDistance = 50;
    [SerializeField] private int dragOffsetDistance = 100;

    public event Action<Vector2> OnMove;
    public event Action OnUp;

    private void Awake()
    {
        joyStickTransform = (RectTransform)transform;
    }
    public void OnDrag(PointerEventData eventData)
    {
        Vector2 offset;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            joyStickTransform,
            eventData.position,
            null,
            out offset);
        offset = Vector2.ClampMagnitude(offset, dragOffsetDistance) / dragOffsetDistance;
        joyStickTransform.anchoredPosition = offset * dragMovementDistance;

        Vector2 inputVector = CalculateMovementInput(offset);
        OnMove?.Invoke(inputVector);
    }

    private Vector2 CalculateMovementInput(Vector2 offset)
    {
        float x = Mathf.Abs(offset.x) > dragThreshold ? offset.x : 0;
        float y = Mathf.Abs(offset.y) > dragThreshold ? offset.y : 0;
        return new Vector2(x, y);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        joyStickTransform.anchoredPosition = Vector2.zero;
        OnMove?.Invoke(Vector2.zero);
        OnUp?.Invoke();
    }

    public void OnPointerDown(PointerEventData eventData)
    {

    }
}
