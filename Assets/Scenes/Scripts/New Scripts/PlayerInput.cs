using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] private MobileJoystick joystick;
    public UnityEvent OnShoot = new UnityEvent();
    public UnityEvent<Vector2> OnRotateTurret = new UnityEvent<Vector2>();
    // Start is called before the first frame update

    void Start()
    {
        joystick.OnMove += Move;
        joystick.OnUp += GetShottingInput;
    }
    private void GetShottingInput()
    {
        OnShoot?.Invoke();
    }
    private void Move(Vector2 input)
    {
        OnRotateTurret?.Invoke(input);
    }
}
