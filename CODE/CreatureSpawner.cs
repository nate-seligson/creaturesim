using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureSpawner : MonoBehaviour
{
    void Start()
    {
        CreatureCreator c = GetComponent<CreatureCreator>();
        c.createRandom();
    }
}
