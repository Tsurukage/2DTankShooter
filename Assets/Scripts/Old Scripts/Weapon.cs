using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class Weapon : MonoBehaviour
{
    [Range(1, 5)]
    [SerializeField] private int _maxIterations = 3;

    [SerializeField] private float _maxDistance = 10f;

    public int _count;
    public LineRenderer _line;

    public Transform Firepoint;
    public GameObject BulletPrefab;
    public GameObject FirePrefab;

    private void Start()
    {
        _line = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }

        _count = 0;
        _line.positionCount = 1;
        _line.SetPosition(0, transform.position);
        _line.enabled = true;
        RayCast(transform.position, transform.up);
    }

    private void Shoot()
    {
        //shooting logic
        var destroyBullet = Instantiate(BulletPrefab, Firepoint.position, Firepoint.rotation);
        Destroy(destroyBullet, 10);
        var destroyFire = Instantiate(FirePrefab, Firepoint.position, Firepoint.rotation);
        Destroy(destroyFire, 0.3f);
    }

    private bool RayCast(Vector2 position, Vector2 direction)
    {
        RaycastHit2D hit = Physics2D.Raycast(position, direction, _maxDistance);
        if (hit && _count <= _maxIterations - 1)
        {
            _count++;
            var reflectAngle = Vector2.Reflect(direction, hit.normal);
            _line.positionCount = _count + 1;
            _line.SetPosition(_count, hit.point);
            RayCast(hit.point + reflectAngle, reflectAngle);
            return true;
        }

        if (hit == false)
        {
            _line.positionCount = _count + 2;
            _line.SetPosition(_count + 1, position + direction * _maxDistance);
        }
        return false;
    }
}