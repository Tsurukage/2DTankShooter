using System;
using UnityEngine;
using UnityEngine.UI;

public class WindowQTE : MonoBehaviour
{
    public static WindowQTE instance;
    [SerializeField] private Text text_cd;
    [SerializeField] private Button btn_qte;
    [SerializeField] private float value_cd;
    private static Text Text_cd { get; set; }
    private static Button Btn_qte { get; set; }
    private static float Value_cd { get; set; }
    private static event Action<bool> CallbackAction;
    // Start is called before the first frame update
    public void Display(bool display)
    {
        gameObject.SetActive(display);
    }
    void Start()
    {
        instance = this;
        Display(false);
        Text_cd = text_cd;
        Value_cd = value_cd;
        Text_cd.text = Value_cd.ToString();
        Btn_qte = btn_qte;
        Btn_qte.onClick.RemoveAllListeners();
        Btn_qte.onClick.AddListener(() =>
        {
            CallbackAction(true);
            instance.Display(false);
        });
    }

    void Update()
    {
        if(Value_cd > 0)
        {
            Value_cd -= Time.deltaTime;
        }
        if(Value_cd < 0 )
        {
            CallbackAction(false);
            Display(false);
            Value_cd = 0;
        }
        Text_cd.text = Value_cd.ToString("00.00");
    }
    public static void TriggerQTE(Action<bool> callbackAction)
    {
        instance.Display(true);
        Value_cd = instance.value_cd;
        CallbackAction = callbackAction;
    }
}
