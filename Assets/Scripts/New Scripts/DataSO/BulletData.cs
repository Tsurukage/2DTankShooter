using UnityEngine;

[CreateAssetMenu(fileName = "NewBulletData", menuName = "Data/BulletData")]
public class BulletData : ScriptableObject
{
    public float speed = 100;
    public int damage = 5;
    public float maxDistance = 10;
    public float splashRange = 1;
    public Sprite bulletSprite;
    public Sprite bulletIcon;
    public Sprite bulletGradeBase;
}
