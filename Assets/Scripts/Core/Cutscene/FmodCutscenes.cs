using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FmodCutscenes : MonoBehaviour
{
    public void startAudio() => RuntimeManager.PlayOneShot("event:/cutScenes/startAudio");

    public void endAudio() => RuntimeManager.PlayOneShot("event:/cutScenes/endAudio");


}
