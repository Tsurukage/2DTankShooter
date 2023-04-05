using UnityEngine;

public class AmmoDestroyUtl : MonoBehaviour
{
    private SimpleGame game;
    private void Start()
    {
        game = FindObjectOfType<SimpleGame>();
    }
    public void OnDestroyH()
    {
        print("AmmoDestroy");
        Destroy(gameObject);
        game.Count();
    }

    
}
