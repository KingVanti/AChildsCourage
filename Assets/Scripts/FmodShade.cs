using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FmodShade : MonoBehaviour
{
    FMODUnity.StudioEventEmitter emitter;
    [SerializeField] public GameObject player;
    float distance;
    private void OnEnable()
    {
        var target = GameObject.Find("Shade");
        emitter = target.GetComponent<FMODUnity.StudioEventEmitter>();
    }
    void Update()
    {

        distance = Mathf.Abs(Vector2.Distance(transform.position, player.transform.position));
        emitter.SetParameter("Distance", distance);
    }
}

