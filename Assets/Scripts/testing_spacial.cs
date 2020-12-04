using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testing_spacial : MonoBehaviour
{
    private float distance;
    private float Material;
    [SerializeField] public GameObject player;
    float dir;
    FMOD.Studio.EventInstance test_spacial;
    [SerializeField] public Rigidbody2D rigidBody2D;

    private void Start()
    {
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(test_spacial, gameObject.transform, rigidBody2D);
        test_spacial = FMODUnity.RuntimeManager.CreateInstance("event:/Monster/Shade/ambient");
        test_spacial.start();

    }

    private void FixedUpdate()
    {
        //test_spacial = FMODUnity.RuntimeManager.CreateInstance("event:/Monster/Shade/ambient");
        //FMODUnity.RuntimeManager.AttachInstanceToGameObject(test_spacial, gameObject);
        Vector2 item = transform.position;
        Vector2 playerpos = player.transform.position;

        distance = Mathf.Abs(Vector2.Distance(playerpos, item));

        //dir = Mathf.Atan2(playerpos.y, playerpos.x) * Mathf.Rad2Deg;

        test_spacial.setParameterByName("Distance", distance);
        //test_spacial.setParameterByName("Direction", dir);


        // Debug.Log(dir);
        Debug.Log(distance);
        //Debug.Log(transform.position);

    }



}


