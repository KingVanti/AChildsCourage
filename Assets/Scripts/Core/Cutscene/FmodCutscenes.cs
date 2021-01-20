using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FmodCutscenes : MonoBehaviour
{
    public void ShadeGrunt() => RuntimeManager.PlayOneShot("event:/cutScenes/Start/shade");

    public void ClosetClosing() => RuntimeManager.PlayOneShot("event:/cutScenes/End/ClosetClosing");

    public void ShadeGrunt2() => RuntimeManager.PlayOneShot("event:/cutScenes/Start/shade");

    public void ShadeGrunt3() => RuntimeManager.PlayOneShot("event:/cutScenes/Start/shade");

    public void ShadeGrunt4() => RuntimeManager.PlayOneShot("event:/cutScenes/Start/shade");

}
