﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace AChildsCourage.Game
{
    public class StartCutScene : MonoBehaviour
    {
        private void OnStartCutSceneEnded() =>
            SceneManager.LoadScene(SceneNames.Game);
    }
}