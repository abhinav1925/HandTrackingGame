﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonController : MonoBehaviour
{
    public float rotationSpeed = 1;
    public float BlastPower = 5;

    public GameObject Cannonball;
    public Transform ShotPoint;

    public GameObject Explosion;

    public GameObject player;

    bool throwedball = false;
    bool actiondone = false;

    Animation_Test AT;

    void Start()
    {
        AT = this.GetComponent<Animation_Test>();
    }

    private void Update()
    {
        //float HorizontalRotation = Input.GetAxis("Horizontal");
        //float VericalRotation = Input.GetAxis("Vertical");

        //  transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + 
        //  new Vector3(0, HorizontalRotation * rotationSpeed, VericalRotation * rotationSpeed));

        this.transform.LookAt(player.transform.position);

        if (AT.attackMode)
        {
            
            if (throwedball)
            {

                 GameObject CreatedCannonball = Instantiate(Cannonball, ShotPoint.position, ShotPoint.rotation);
                 CreatedCannonball.GetComponent<Rigidbody>().velocity = ShotPoint.transform.up * BlastPower;

                // Added explosion for added effect
                Destroy(Instantiate(Explosion, ShotPoint.position, ShotPoint.rotation), 2);
                throwedball = false;
            }

        }
        else
        {

            throwedball = true;
        }
    }




}
