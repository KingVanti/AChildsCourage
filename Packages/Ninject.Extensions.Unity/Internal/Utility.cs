using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Ninject.Extensions.Unity
{

    internal static class Utility
    {

        #region Methods

        internal static IEnumerable<MonoBehaviour> GetMonoBehavioursOfType(Type type)
        {
            return UnityEngine.Object.FindObjectsOfType<MonoBehaviour>().Where(m => m.GetType() == type);
        }

        #endregion

    }

}