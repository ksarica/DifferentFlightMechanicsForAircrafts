using UnityEngine;
using Assets.Common.ObjectPooling;
using System.Collections;

public class Bullet : MonoBehaviour, IPoolableObject
{
    [SerializeField] private GameObject parentShip;
    [SerializeField] private float bulletVelocity;
    [SerializeField] private float timeToLive;

    private void OnEnable()
    {
        DisableBulletWhenTimeout();
    }

    private void Update()
    {
        MoveBullet();
    }

    // private void OnDisable()
    // {
    //     CancelInvoke();
    // }

    public void MoveBullet()
    {
        transform.position += transform.forward * bulletVelocity * Time.deltaTime;
    }

    public void DisableBulletWhenTimeout()
    {
        // Invoke("DisableBullet", timeToLive);
        StartCoroutine(Delay(timeToLive));
    }

    public void DisableBullet()
    {
        this.gameObject.SetActive(false);
    }

    public IEnumerator Delay(float delayInSeconds)
    {
        yield return new WaitForSeconds(delayInSeconds);
        this.gameObject.SetActive(false);
        yield return null;
    }

    public void ResetPoolObject(Vector3 position, Quaternion rotation)
    {
        this.transform.position = position;
        this.transform.rotation = rotation;
        // Debug.Log("Resetted position: " + this.transform.position + " and rotation: " + parentShip.transform.rotation.eulerAngles);
        this.gameObject.SetActive(true);
        // TODO: SoundEffect Resetting
    }
}
