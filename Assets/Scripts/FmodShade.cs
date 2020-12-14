using AChildsCourage.Game.Shade;
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
    private const string awarenessCues_01 = "event:/Monster/Shade/SoundCues/0-1 notice";
    private const string awarenessCues_10 = "event:/Monster/Shade/SoundCues/1-0 lost";
    private const string awarenessCues_12 = "event:/Monster/Shade/SoundCues/1-2 detected";


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

    public void AwarenessCues(AwarenessLevel awareness)
    {
        switch (awareness)
        {
            case AwarenessLevel.Oblivious:
                RuntimeManager.PlayOneShot(awarenessCues_10, GetComponent<Transform>().position);
                break;

            case AwarenessLevel.Suspicious:
                RuntimeManager.PlayOneShot(awarenessCues_01, GetComponent<Transform>().position);
                break;

            case AwarenessLevel.Hunting:
                RuntimeManager.PlayOneShot(awarenessCues_12, GetComponent<Transform>().position);
                break;

            default:
                break;

        }

    }

}

