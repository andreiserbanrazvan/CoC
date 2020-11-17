using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class building : MonoBehaviour
{
    public int health = 100;


    public bool destroyed;
    public Transform house, house_des;
    public ParticleSystem particle;
    

    void Start()
    {
        house.gameObject.SetActive(true);
        house_des.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (destroyed)
        {
            Instantiate(particle, house_des.transform);
            house.gameObject.SetActive(false);
            house_des.gameObject.SetActive(true);
        }

        destroyed = false;
       
    }
}
