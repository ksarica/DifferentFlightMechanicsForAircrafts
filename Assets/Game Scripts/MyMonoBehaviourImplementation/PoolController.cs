using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolController : MonoBehaviour
{
    // TODO: Transform parent folder ekle.
    // TODO: Array bitti, List implementasyonu da yap bu arada array de limit bitince list'e geçmesini sağla

    public static PoolController Instance { get; private set; }
    private GameObject _gameObjectReference;

    private GameObject[] _mainPool;
    private List<GameObject> _additionalListPool;

    [SerializeField] private GameObject prefab;
    [SerializeField] private int initialLength; // cant be bigger than maxPoolCapacity
    [SerializeField] private int maxPoolCapacity;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        _mainPool = new GameObject[maxPoolCapacity];
        _additionalListPool = new List<GameObject>();
        InitializeInitialPoolForArray();
        InitializeInitialPoolForList();

        if (initialLength > maxPoolCapacity)
        {
            initialLength = maxPoolCapacity;
        }
    }

    private void InitializeInitialPoolForArray()
    {
        Debug.Log(initialLength + " elemanlı initial pool oluşturulacak. Maksimum kapasite: " + _mainPool.Length);
        for (int i = 0; i < initialLength; i++)
        {
            _gameObjectReference = Instantiate(prefab);
            _gameObjectReference.SetActive(false);
            _mainPool[i] = _gameObjectReference;
            Debug.Log("1 eleman array pool'a eklendi !");
        }
    }

    private GameObject GetAvailableObjectFromArrayPool()
    {
        for (int i = 0; i < initialLength; i++)
        {
            if (!_mainPool[i].activeSelf)
            {
                Debug.Log("Array indeks: " + i + ", pool aktif değil kullanılabilir...");
                return _mainPool[i];
            }
        }
        Debug.Log("Hiçbir arrayde kullanılabilir object yok. Önce maksimum kapasiteyi dolduracak. Sonra list kullanmaya başlayacak.");
        return null;
    }

    public void InstantiateFromPool(Vector3 position, Quaternion rotation)
    {
        _gameObjectReference = GetAvailableObjectFromArrayPool();
        if (_gameObjectReference != null) // get from pool
        {
            _gameObjectReference.transform.position = position;
            _gameObjectReference.SetActive(true);
        }
        else // enlarge pool 
        {
            if (initialLength < maxPoolCapacity)
            {
                Debug.Log("Array kapasitesi daha dolmadı. Boş olan yerlere yeni object eklenip return edilecek.");
                _mainPool[initialLength++] = Instantiate(prefab, position, rotation);
            }
            else
            {
                Debug.Log("Array kapasitesi doldu. Artık ekstra ihtiyaçlarda list'e ekleme yapılacak.");
                InstantiateFromList(position, rotation);
            }
        }
    }

    private void InitializeInitialPoolForList()
    {
        Debug.Log(initialLength + " elemanlı initial pool oluşturulacak. Maksimum kapasite: " + _mainPool.Length);
        for (int i = 0; i < (initialLength / 2); i++)
        {
            _gameObjectReference = Instantiate(prefab);
            _gameObjectReference.SetActive(false);
            _additionalListPool.Add(_gameObjectReference);
            Debug.Log("1 eleman list pool'a eklendi !");
        }
    }
    private GameObject GetAvailableObjectFromListPool()
    {
        for (int i = 0; i < _additionalListPool.Count; i++)
        {
            if (!_additionalListPool[i].activeSelf)
            {
                Debug.Log("List indeks: " + i + ", pool aktif değil kullanılabilir...");
                return _additionalListPool[i];
            }
        }
        Debug.Log("Listde kapasite doldu. Arttırılacak...");
        return null;
    }
    private void InstantiateFromList(Vector3 position, Quaternion rotation)
    {
        _gameObjectReference = GetAvailableObjectFromListPool();
        if (_gameObjectReference != null) // get from pool
        {
            _gameObjectReference.transform.position = position;
            _gameObjectReference.SetActive(true);
        }
        else // enlarge pool 
        {
            Debug.Log("List kapasitesi dolu. Genişleticek...");
            _additionalListPool.Add(Instantiate(prefab, position, rotation));
        }
    }


}
