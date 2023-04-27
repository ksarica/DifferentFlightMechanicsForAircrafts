using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject objectPrefab;

    private void Start()
    {

    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.position += new Vector3(0f, 0f, 1f);
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.position += new Vector3(0f, 0f, -1f);
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.position += new Vector3(-1f, 0f, 0f);
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.position += new Vector3(1f, 0f, 0f);
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            for (int i = 1; i <= 8; i++)
            {
                Vector3 pos = transform.position + new Vector3(UnityEngine.Random.Range(-10f, 10f),
                UnityEngine.Random.Range(-50f, 50f), 10f);
                Instantiate(objectPrefab, pos, transform.rotation);
            }
        }
    }
}
