using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] private Camera mainCam;
    public UnityEvent OnShoot = new UnityEvent();
    public UnityEvent<Vector2> OnRotateTurret = new UnityEvent<Vector2>();
    // Start is called before the first frame update

    void Start()
    {
        if(mainCam != null)
            mainCam = Camera.main;
    }
    // Update is called once per frame
    void Update()
    {
        GetTurretRotate();
        GetShottingInput();
    }

    private void GetShottingInput()
    {
        if (Input.GetMouseButtonUp(0))
        {
            OnShoot?.Invoke();
            print("Clicked");
        }
    }

    private void GetTurretRotate()
    {
        if(Input.GetMouseButtonDown(0))
        {
            OnRotateTurret?.Invoke(GetTurretRotation());
            print(GetTurretRotation());
        }
    }

    private Vector2 GetTurretRotation()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = 0;
        Vector2 mouseWorldPos = mainCam.WorldToScreenPoint(mousePosition);
        return mouseWorldPos;
    }
}
