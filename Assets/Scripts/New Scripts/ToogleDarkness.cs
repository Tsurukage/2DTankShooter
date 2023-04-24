using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToogleDarkness : MonoBehaviour
{
    bool isPressed;
    float alphaValue;
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
            alphaValue += 0.01f;
            if(alphaValue > 0.5f)
            {
                alphaValue = 0.5f;
            }
        }
        else
        {
            alphaValue = 0;
            //alphaValue -= 0.01f;            if (alphaValue < 0) { }
        }
        var sprite = GetComponent<SpriteRenderer>();
        var color = sprite.color;
        color.a = alphaValue;
        sprite.color = color;
    }
}
