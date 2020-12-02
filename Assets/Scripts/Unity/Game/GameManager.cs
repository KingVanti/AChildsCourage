﻿using AChildsCourage.Game;
using Ninject.Extensions.Unity;
using UnityEngine.Events;

namespace AChildsCourage.Unity
{

    public class GameManager : SceneManager
    {

        public UnityEvent onNightPrepared;

        [AutoInject] public INightManager NightManager { private get; set; }


        public void PrepareGame()
        {
            NightManager.PrepareNight();
            onNightPrepared.Invoke();
        }


        public void OnLose()
        {
            LoadSceneWith(SceneNames.GameScene);
        }

    }

}