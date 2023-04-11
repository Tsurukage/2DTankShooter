using UnityEngine;

public class DestroyUtl : MonoBehaviour
{
    private SimpleGame game;
    private void Start()
    {
        game = FindObjectOfType<SimpleGame>();
    }
    public void DestroyHelper()
    {
        Destroy(gameObject);
        //game.ShootingCD();
    }
}
