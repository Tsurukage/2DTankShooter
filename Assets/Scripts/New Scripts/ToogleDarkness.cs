using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToogleDarkness : MonoBehaviour
{
    public Material _material;
    bool isPressed;
    float alphaValue = 1;
    public void ToggleToDark()
    {
        isPressed = true;
        alphaValue = 1;
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
            alphaValue = 1;
            //alphaValue -= 0.01f;            if (alphaValue < 0) { }
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
