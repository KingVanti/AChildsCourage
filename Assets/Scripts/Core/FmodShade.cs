using System.Collections;
using AChildsCourage.Game.Shade;
using FMODUnity;
using UnityEngine;

public class FmodShade : MonoBehaviour
{

    [SerializeField] public GameObject @char;

    private float distance;
    private StudioEventEmitter emitter;

    private void Update()
    {
        distance = Mathf.Abs(Vector2.Distance(transform.position, @char.transform.position));
        emitter.SetParameter("Distance", distance);
    }

    private void OnEnable()
    {
        var target = GameObject.Find("Shade");
        emitter = target.GetComponent<StudioEventEmitter>();
    }

    public void Shade_getBanished() => RuntimeManager.PlayOneShot(ShadeBanished_path, GetComponent<Transform>().position);

    public void Shade_spawn() => RuntimeManager.PlayOneShot(ShadeSpawn_path, GetComponent<Transform>().position);

    public void AwarenessCues(AwarenessLevel awareness)
    {
        if (!shade_awarness_is_playing)
        {
            StartCoroutine(ShadeTimer());
            switch (awareness)
            {
                case AwarenessLevel.Oblivious: //0
                    if (old_awarness == AwarenessLevel.Suspicious) //0-1
                        RuntimeManager.PlayOneShot(awarenessCues_10, GetComponent<Transform>().position);
                    break;

                case AwarenessLevel.Suspicious: //1
                    if (old_awarness == AwarenessLevel.Oblivious) //1-0
                        RuntimeManager.PlayOneShot(awarenessCues_01, GetComponent<Transform>().position);
                    break;

                case AwarenessLevel.Hunting: //2
                    if (old_awarness == AwarenessLevel.Suspicious) //2-1
                        RuntimeManager.PlayOneShot(awarenessCues_12, GetComponent<Transform>().position);
                    break;
            }

            old_awarness = awareness;
        }
    }

    private IEnumerator ShadeTimer()
    {
        shade_awarness_is_playing = true;

        yield return new WaitForSeconds(waitTime);
        shade_awarness_is_playing = false;
    }

    #region eventpaths

    private const string ShadeBanished_path = "event:/Monster/getBanished";
    private const string ShadeSpawn_path = "event:/Monster/Spawn";
    private const string awarenessCues_01 = "event:/Monster/Shade/SoundCues/0-1 notice";
    private const string awarenessCues_10 = "event:/Monster/Shade/SoundCues/1-0 lost";
    private const string awarenessCues_12 = "event:/Monster/Shade/SoundCues/1-2 detected";
    private bool shade_awarness_is_playing;
    private readonly float waitTime = 0.6f;
    private AwarenessLevel old_awarness;

    #endregion

}