using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject objectPrefab;

    private void Update()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.position += new Vector3(0f, 0f, 0.5f);
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.position += new Vector3(0f, 0f, -0.5f);
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.position += new Vector3(-0.5f, 0f, 0f);
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.position += new Vector3(0.5f, 0f, 0f);
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            for (int i = 1; i <= 4; i++)
            {
                Vector3 pos = transform.position + new Vector3(UnityEngine.Random.Range(-20f, 20f),
                UnityEngine.Random.Range(-50f, 50f), 20f);
                Instantiate(objectPrefab, pos, transform.rotation);
            }
        }
    }
}
