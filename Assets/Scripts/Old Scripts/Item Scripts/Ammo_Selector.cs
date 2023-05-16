using UnityEngine;
using UnityEngine.UI;

public class Ammo_Selector : MonoBehaviour          //UI view
{
    public Button[] launcherMode;
    public AmmoScriptableObject[] ammo;
    public GameObject bullet;
    public float ammoSpeed;
    public float ammoDamage;
    public Vector2 lastVelocity;
    public new Rigidbody2D rigidbody2D;
    public float impactField;
    public AmmoType ammoType;

    private PlayerInputController playerController;

    // Start is called before the first frame update
    void Start()
    {
        playerController = FindObjectOfType<PlayerInputController>();
        for (int i = 0; i < launcherMode.Length; i++)
        {
            Button btn = launcherMode[i];

            int buttonIndex = i;
            btn.onClick.AddListener(() => ChangeLauncherMode(buttonIndex));
            btn.GetComponent<Image>().color = ammo[i].ammoColor;
        }
        ChangeLauncherMode(0);
    }

    private void ChangeLauncherMode(int index)
    {
        bullet = ammo[index].modelPrefab;
        bullet.GetComponent<SpriteRenderer>().color = ammo[index].ammoColor;
        ammoSpeed = ammo[index].ammoSpeed;
        lastVelocity = ammo[index].lastVelocity;
        rigidbody2D = ammo[index].rb;
        ammoDamage = ammo[index].ammoDamage;
        ammoType = ammo[index].type;
        impactField = ammo[index].impactField;

        playerController.GetAmmo(bullet, ammoSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Shoot(int index)
    {
        Vector2 spawn = Vector2.zero;
        Instantiate(bullet, spawn, bullet.transform.rotation);
        bullet.GetComponent<Rigidbody2D>().AddForce(spawn * ammo[index].ammoSpeed, ForceMode2D.Impulse);
    }
}
