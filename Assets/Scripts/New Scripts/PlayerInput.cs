using System;
using UnityEngine;
using UnityEngine.Events;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] private MobileJoystick joystick;
    [SerializeField] private Turret turret;
    public UnityEvent OnShoot = new UnityEvent();
    public UnityEvent<Vector2> OnRotateTurret = new UnityEvent<Vector2>();
    // Start is called before the first frame update

    void Start()
    {
        joystick = FindObjectOfType<MobileJoystick>();
        turret = FindObjectOfType<Turret>();
        joystick.OnMove += Move;
        joystick.OnUp += GetShottingInput;
        turret.OnCountDown += SetInteraction;
    }

    private void SetInteraction(bool isInteractable)
    {
        joystick.SetInteraction(isInteractable);
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
