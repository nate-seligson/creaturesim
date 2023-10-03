using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantSpawner : MonoBehaviour
{
    public GameObject plant;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("loop");
    }
    IEnumerator loop()
    {
        yield return new WaitForSeconds(Random.Range(3f, 5f));
        Instantiate(
            plant,
            transform.position + new Vector3(Random.Range(-5f, 5f), Random.Range(-5f, 5f), 0),
            transform.rotation
            );
        StartCoroutine("loop");
    }
}
