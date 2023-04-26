using UnityEngine;
using System.Collections;

public class AttackSystem : MonoBehaviour
{

    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform[] bulletPoints;

    [SerializeField] private float _fireDelay;

    private int bulletPointCount;
    private int nextFirePointIndex;

    private bool canBeFired = true;
    private void Start()
    {
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
        }
    }

    public void Fire()
    {
        for (int i = 0; i < bulletPointCount; i++)
        {
            nextFirePointIndex = nextFirePointIndex % bulletPointCount;
            Instantiate(bulletPrefab, bulletPoints[nextFirePointIndex].position, this.transform.rotation);
            nextFirePointIndex++;
        }
        StartCoroutine(WaitAndLetShooting(_fireDelay));
    }

    public IEnumerator WaitAndLetShooting(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        canBeFired = true;
        yield return null;
    }

}
