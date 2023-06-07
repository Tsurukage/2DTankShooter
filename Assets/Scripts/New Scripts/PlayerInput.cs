using System;
using UnityEngine;
using UnityEngine.Events;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] private MobileJoystick joystick;
    public UnityEvent OnShoot = new UnityEvent();
    public UnityEvent<Vector2> OnRotateTurret = new UnityEvent<Vector2>();
    public UnityEvent OnCasting = new UnityEvent();
    // Start is called before the first frame update

    void Start()
    {
        joystick = FindObjectOfType<MobileJoystick>();
        joystick.OnMove += Move;
        joystick.OnUp += GetShottingInput;
        joystick.OnDown += GetInputDown;
    }

    private void GetInputDown()
    {
        OnCasting?.Invoke();
    }

    private void GetShottingInput()
    {
        OnShoot?.Invoke();
        CameraEffects.ShakeOnce(1f, 10f, new Vector3(0.5f, 0.5f));//·¢ÉäÅÚµ¯ÆÁÄ»Õð¶¯
    }
    private void Move(Vector2 input)
    {
        OnRotateTurret?.Invoke(input);
    }
}
