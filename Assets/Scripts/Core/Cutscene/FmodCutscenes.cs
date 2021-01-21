using FMODUnity;
using UnityEngine;

public class FmodCutscenes : MonoBehaviour
{

    public void startAudio() => RuntimeManager.PlayOneShot("event:/cutScenes/startAudio");

    public void endAudio() => RuntimeManager.PlayOneShot("event:/cutScenes/endAudio");

}