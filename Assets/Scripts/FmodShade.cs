using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FmodShade : MonoBehaviour
{
    FMODUnity.StudioEventEmitter emitter;
    [SerializeField] public GameObject player;

    #region eventpaths
    private const string ShadeBanished_path = "event:/Monster/getBanished";
    private const string ShadeSpawn_path = "event:/Monster/Spawn";


    #endregion

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

    public void Shade_getBanished()
    {
        RuntimeManager.PlayOneShot(ShadeBanished_path, GetComponent<Transform>().position);
    }

    public void Shade_spawn()
    {
        RuntimeManager.PlayOneShot(ShadeSpawn_path, GetComponent<Transform>().position);
    }

}

