using System;
using UnityEngine;
using UnityEngine.UI;

public class WindowQTE : MonoBehaviour
{
    public static WindowQTE instance;
    [SerializeField] private Text text_cd;
    [SerializeField] private Button btn_qte;
    [SerializeField] private float value_cd;
    [SerializeField] private float currentTime;
    [SerializeField] private Image img_circle;
    [SerializeField] private RectTransform rt_spawnArea;
    private static Text Text_cd { get; set; }
    private static Button Btn_qte { get; set; }
    private static float Value_cd { get; set; }
    private static Image Img_Circle { get; set; }
    private static RectTransform Rt_spawnArea { get; set; }
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
        Img_Circle = img_circle;
        currentTime = value_cd;
        Rt_spawnArea = rt_spawnArea;
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
        currentTime -= Time.deltaTime;
        float filled = currentTime / value_cd;
        img_circle.fillAmount = filled;
        if(Value_cd > 0)
        {
            Value_cd -= Time.deltaTime;
        }
        if(Value_cd < 0 )
        {
            currentTime = value_cd;
            Time.timeScale = 1.0f;
            CallbackAction(false);
            Display(false);
            Value_cd = 0;
        }
        Text_cd.text = Value_cd.ToString("00.00");
    }
    public static void TriggerQTE(Action<bool> callbackAction)
    {
        Img_Circle.rectTransform.position = instance.GetRandomPosition();
        instance.currentTime = instance.value_cd;
        instance.Display(true);
        Time.timeScale = 0.1f;
        Value_cd = instance.value_cd;
        CallbackAction = callbackAction;
    }
    public Vector3 GetRandomPosition()
    {
        float minX = Rt_spawnArea.position.x - Rt_spawnArea.rect.width / 2f;
        float maxX = Rt_spawnArea.position.x + Rt_spawnArea.rect.width / 2f;
        float minY = Rt_spawnArea.position.y - Rt_spawnArea.rect.height / 2f;
        float maxY = Rt_spawnArea.position.y + Rt_spawnArea.rect.height / 2f;

        float randomX = UnityEngine.Random.Range(minX, maxX);
        float randomY = UnityEngine.Random.Range(minY, maxY);
        Vector3 randomPos = new Vector3(randomX, randomY, 0f);

        return randomPos;
    }
}
