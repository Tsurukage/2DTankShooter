using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MovementJoystick : MonoBehaviour
{
    public GameObject joystick;
    public GameObject joystickBG;
    public Vector2 joystickVec;
    private Vector2 joystickTouchPos;
    private Vector2 joystickOriPos;
    private float joystickRadius;
    private PlayerInputController playerController;

    void Start()
    {
        playerController = FindObjectOfType<PlayerInputController>();
        joystickOriPos = joystickBG.transform.position;
        joystickRadius = joystickBG.GetComponent<RectTransform>().sizeDelta.y / 4;
    }

    public void PointerDown()
    {
        joystick.transform.position = Input.mousePosition;
        joystickTouchPos = Input.mousePosition;
    }
    public void Drag(BaseEventData baseEventData)
    {
        PointerEventData pointerEventData = baseEventData as PointerEventData;
        Vector2 dragPos = pointerEventData.position;
        joystickVec = (dragPos - joystickTouchPos).normalized;

        float joystickDist = Vector2.Distance(dragPos, joystickTouchPos);

        if (joystickDist < joystickRadius)
            joystick.transform.position = joystickTouchPos + joystickVec * joystickDist;
        else
            joystick.transform.position = joystickTouchPos + joystickVec * joystickRadius;

        playerController.Aim(joystickVec.y, joystickVec.x);
        print($"{joystickVec.x}, {joystickVec.y}");
    }

    public void PointerUp()
    {
        joystickVec = Vector2.zero;
        joystick.transform.position = joystickOriPos;
        playerController.Shoot();
    }
    public void MovementBool(bool isMovable)
    {
        joystickBG.GetComponent<Image>().raycastTarget = isMovable;
        joystick.GetComponent<Image>().raycastTarget = isMovable;
    }
}
