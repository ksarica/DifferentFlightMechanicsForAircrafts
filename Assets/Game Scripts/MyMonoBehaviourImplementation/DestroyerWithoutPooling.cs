using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyerWithoutPooling : MonoBehaviour, IPoolDestroyBehaviour
{

    [SerializeField] private float _lifetime;
    WaitForSeconds bulletLifeTime;

    private void OnEnable()
    {
        DestroyWhenDesired();
    }

    void Start()
    {
        bulletLifeTime = new WaitForSeconds(_lifetime);
    }

    public IEnumerator WaitForDelay()
    {
        //Debug.Log("1 WaitForDelay(): " + _lifetime + " saniye bekleniyor...");
        yield return new WaitForSeconds(_lifetime);
        //Debug.Log("2 WaitForDelay(): " + _lifetime + " saniye süresi doldu...");
        Destroy(this.gameObject);
        yield return null;
    }
    public void DestroyWhenDesired()
    {
        StartCoroutine(WaitForDelay());
    }

}
