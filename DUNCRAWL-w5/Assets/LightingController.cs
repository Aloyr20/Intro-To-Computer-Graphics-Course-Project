using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public class LightingController : MonoBehaviour
{
    public List<Material> materials = new List<Material>();
    public GameObject sphere;

    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            sphere.GetComponent<Renderer>().material = materials[0];
        }

        if (Input.GetKeyDown(KeyCode.I))
        {
            sphere.GetComponent<Renderer>().material = materials[1];
        }

        if (Input.GetKeyDown(KeyCode.O))
        {
            sphere.GetComponent<Renderer>().material = materials[2];
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            sphere.GetComponent<Renderer>().material = materials[3];
        }
    }
}
