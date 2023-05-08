using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToogleDarkness : MonoBehaviour
{
    public static ToogleDarkness Instance;
    public Material _material;
    bool isPressed;
    float alphaValue = 1;
    void Awake()
    {
        Instance = this;
    }
    public void ToggleToDark()
    {
        isPressed = true;
    }
    public void ToggleToLight()
    {
        isPressed = false;
    }
    void Update()
    {
        if(isPressed)
        {
            alphaValue -= 0.01f;
            if(alphaValue < 0.5f)
            {
                alphaValue = 0.5f;
            }
        }
        else
        {
            //alphaValue = 1;
            alphaValue += 0.01f;            
            if (alphaValue > 1) 
            { 
                alphaValue = 1;
            }
        }
        var mat = _material.color;
        mat = new Color(alphaValue, alphaValue, alphaValue);
        _material.color = mat;
        //var sprite = GetComponent<SpriteRenderer>();
        //var color = sprite.color;
        //color.a = alphaValue;
        //sprite.color = color;
    }
}
