using UnityEngine;

public class UiFollowTarget : MonoBehaviour
{
    public Transform objectFollow;
    RectTransform rectTranform;

    private void Awake()
    {
        rectTranform = GetComponent<RectTransform>();
    }
    private void Update()
    {
        if(objectFollow != null)
        {
            rectTranform.anchoredPosition = objectFollow.localPosition;
        }
    }
}
