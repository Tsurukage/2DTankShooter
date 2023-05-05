using UnityEngine;

public class DestroyUtl : MonoBehaviour
{
    private SpawnButtonManager _spawnButtonManager;
    private void Start()
    {
        _spawnButtonManager = FindObjectOfType<SpawnButtonManager>();
    }
    public void DestroyHelper()
    {
        Destroy(gameObject);
    }
    public void UpdateTankCount()
    {
        SimpleGame.Instance.UpdateTankCount();
    }
    public void UpdateShootingCount()
    {
        SimpleGame.Instance.UpdateShootingCount();
    }
    public void RemoveBullet()
    {
        _spawnButtonManager.Remove();
    }
    public void OnAnimalHitFail()
    {
        SimpleGame.Instance.UpdateAnimalChanceCount();
    }
    public void TankDestroyEffect()
    {
        CameraEffects.ShakeOnce(1f, 10f, Vector3.one);
        DestroyFlash.OnDestroyFlashEffect(0.8f);
    }
}
