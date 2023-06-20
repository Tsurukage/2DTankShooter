using UnityEngine;

[CreateAssetMenu(fileName = "NewBulletData", menuName = "Data/BulletData")]
public class BulletData : ScriptableObject
{
    public string bulletName;
    public float speed = 100;
    public int damage = 5;
    public float maxDistance = 10;
    public Sprite bulletSprite;
    public Sprite bulletIcon;
    public Sprite bulletGradeBase;
    public GameObject bulletTrail;
    public float splashRange = 1;
    public BulletType bulletType;
    public Sprite bullet_npc;

    public BulletData(string bulletName, float speed, int damage, float maxDistance, float splashRange, Sprite bulletSprite, Sprite bulletIcon, Sprite bulletGradeBase)
    {
        this.bulletName = bulletName;
        this.speed = speed;
        this.damage = damage;
        this.maxDistance = maxDistance;
        this.splashRange = splashRange;
        this.bulletSprite = bulletSprite;
        this.bulletIcon = bulletIcon;
        this.bulletGradeBase = bulletGradeBase;
    }
}
public enum BulletType
{
    SingleHit,      //单发
    Explosion,      //爆炸
    Penetrate,      //穿透
    ReflectBullet,  //弹射
    StopTank,       //定住坦克
    SlowTank,       //减速坦克
    TankOnly        //坦克而已
}