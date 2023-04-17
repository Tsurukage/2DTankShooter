using System;
using UnityEngine;
using UnityEngine.Events;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] private MobileJoystick joystick;
    [SerializeField] private Turret turret;
    public UnityEvent OnShoot = new UnityEvent();
    public UnityEvent<Vector2> OnRotateTurret = new UnityEvent<Vector2>();
    public UnityEvent OnCasting = new UnityEvent();
    // Start is called before the first frame update

    void Start()
    {
        joystick = FindObjectOfType<MobileJoystick>();
        turret = FindObjectOfType<Turret>();
        joystick.OnMove += Move;
        joystick.OnUp += GetShottingInput;
        turret.OnCountDown += SetInteraction;
        joystick.OnDown += GetInputDown;
    }

    private void GetInputDown()
    {
        OnCasting?.Invoke();
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
