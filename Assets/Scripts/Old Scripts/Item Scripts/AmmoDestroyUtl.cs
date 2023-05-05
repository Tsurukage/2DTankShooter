using UnityEngine;

public class AmmoDestroyUtl : MonoBehaviour
{
    public void OnDestroyH()
    {
        print("AmmoDestroy");
        Destroy(gameObject);
        //game.Count();
    }

    
}
