using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour
{
    [SerializeField] private GameObject parentShip;
    [SerializeField] private float bulletVelocity;
    [SerializeField] private float timeToLive;

    private void OnEnable()
    {
        DestroyBulletAfterDelay();
    }

    private void Update()
    {
        MoveBullet();
    }
    public void MoveBullet()
    {
        transform.position += transform.forward * bulletVelocity * Time.deltaTime;
    }

    public void DestroyBulletAfterDelay()
    {
        StartCoroutine(Delay(timeToLive));
    }

    public IEnumerator Delay(float delayInSeconds)
    {
        yield return new WaitForSeconds(delayInSeconds);
        Destroy(gameObject);
        yield return null;
    }
}
