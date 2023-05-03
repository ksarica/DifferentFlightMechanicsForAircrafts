using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float bulletVelocity;
    [SerializeField] private float timeToLive;

    private void OnEnable()
    {
        StartCoroutine(DestroyBulletAfterDelay());
    }

    private void Update()
    {
        MoveBullet();
    }

    public void MoveBullet()
    {
        transform.position += transform.forward * bulletVelocity * Time.deltaTime;
    }

    private IEnumerator DestroyBulletAfterDelay()
    {
        yield return new WaitForSeconds(timeToLive);
        gameObject.SetActive(false);
    }
}
