using System.Collections.Specialized;
using UnityEngine;
using Assets.Common.ObjectPooling;
using System.Collections;

public class AttackSystem : MonoBehaviour
{

    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform[] bulletPoints;

    [SerializeField] private float _fireDelay;

    private PoolController _poolController;
    //private ObjectPool bulletPool;

    private int bulletPointCount;
    private int nextFirePointIndex;

    private bool canBeFired = true;
    private void Awake()
    {
        _poolController = PoolController.Instance;
    }

    private void Start()
    {
        //bulletPool = new ObjectPool(bulletPrefab);
        //bulletPool = new ObjectPool(bulletPrefab, 500);
        nextFirePointIndex = 0;
        bulletPointCount = bulletPoints.Length;
    }

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            if (canBeFired)
            {
                Fire();
                StartCoroutine(WaitAndLetShooting(_fireDelay));
                canBeFired = false;
            }
            //Debug.Log("Pool size for the " + gameObject.name + ": " + bulletPool.PoolLength);
        }
        if (Input.GetMouseButton(1))
        {
            if (canBeFired)
            {
                FireWithoutPool();
                StartCoroutine(WaitAndLetShooting(_fireDelay));
                canBeFired = false;
            }
            //Debug.Log("Pool size for the " + gameObject.name + ": " + bulletPool.PoolLength);
        }
    }

    // TODO: Implement Object Pooling
    public void Fire()
    {
        nextFirePointIndex = nextFirePointIndex % bulletPointCount;
        // Debug.Log(bulletPoints[nextFirePointIndex].position  + " NextFirePointIndex: " + nextFirePointIndex +  " BulletPointsCount: " + bulletPointCount);
        // Debug.Log("bulletCreated at position " + bulletPoints[nextFirePointIndex].position + " and rotation: " +  this.transform.rotation.eulerAngles);
        //bulletPool.CreateAtPosition(bulletPoints[nextFirePointIndex].position, this.transform.rotation);
        _poolController.InstantiateFromPool(bulletPoints[nextFirePointIndex].position, this.transform.rotation);
        StartCoroutine(WaitAndLetShooting(_fireDelay));
        nextFirePointIndex++;
    }

    public void FireWithoutPool()
    {
        nextFirePointIndex = nextFirePointIndex % bulletPointCount;
        Instantiate(bulletPrefab, bulletPoints[nextFirePointIndex].position, this.transform.rotation);
        StartCoroutine(WaitAndLetShooting(_fireDelay));
        nextFirePointIndex++;
    }

    public IEnumerator WaitAndLetShooting(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        canBeFired = true;
        yield return null;
    }


}
