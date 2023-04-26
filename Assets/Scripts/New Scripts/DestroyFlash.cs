using UnityEngine;
using UnityEngine.UI;

public class DestroyFlash : MonoBehaviour
{
    private Image _flashImage;
    public float alphaValue = 0;
    private void Start()
    {
        _flashImage = GetComponent<Image>();
        var color = _flashImage.color;
        color.a = alphaValue;
        _flashImage.color = color;
    }
    public static void OnDestroyFlashEffect(float alpha)
    {
        var instance = FindObjectOfType<DestroyFlash>();
        instance.alphaValue = alpha;
        var color = instance._flashImage.color;
        color.a = instance.alphaValue;
        instance._flashImage.color = color;
        instance.Flash();
    }
    public void Flash()
    {
        
    }
    void Update()
    {
        if(alphaValue > 0)
        {
            alphaValue -= 0.007f;
            var color = _flashImage.color;
            color.a = alphaValue;
            _flashImage.color = color;
        }
    }
}
