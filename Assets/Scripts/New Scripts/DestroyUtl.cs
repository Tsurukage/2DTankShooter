using UnityEngine;

public class DestroyUtl : MonoBehaviour
{
    private SimpleGame _game;
    private SpawnButtonManager _spawnButtonManager;
    private void Start()
    {
        _game = FindObjectOfType<SimpleGame>();
        _spawnButtonManager = FindObjectOfType<SpawnButtonManager>();
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
    public void RemoveBullet()
    {
        _spawnButtonManager.Remove();
    }
    public void OnAnimalHitFail()
    {
        _game.UpdateAnimalChanceCount();
    }
    public void TankDestroyEffect()
    {
        CameraEffects.ShakeOnce(1f, 10f, Vector3.one);
        DestroyFlash.OnDestroyFlashEffect(0.8f);
    }
}
