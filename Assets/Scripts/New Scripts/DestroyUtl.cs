using UnityEngine;

public class DestroyUtl : MonoBehaviour
{
    private SimpleGame _game;
    private void Start()
    {
        _game = FindObjectOfType<SimpleGame>();
    }
    public void DestroyHelper()
    {
        Destroy(gameObject);
    }
    public void UpdateTankCount()
    {
        _game.UpdateTankCount();
    }
    public void UpdateShootingCount()
    {
        _game.UpdateShootingCount();
    }
}
