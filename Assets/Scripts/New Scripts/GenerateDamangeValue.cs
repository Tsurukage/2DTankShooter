using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateDamangeValue : MonoBehaviour
{
    [SerializeField] GameObject _damageValue;
    private Vector3 offset = new Vector3(0, 0.5f);
    public void ShowFloatingText(Damagable damagevalue)
    {
        var text = _damageValue.GetComponent<TextMesh>();
        text.text = damagevalue.DamageValue.ToString();
        text.color = Color.red;
        var obj = Instantiate(_damageValue);
        obj.transform.position = transform.position + offset;
    }
}
