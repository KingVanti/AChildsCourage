using UnityEditor;
using UnityEngine;

namespace AChildsCourage.Game.Char
{

    [CustomEditor(typeof(TensionMeterEntity))]
    public class TensionMeterEntityEditor : Editor
    {

        private TensionMeterEntity TensionMeterEntity => (TensionMeterEntity) target;

        private Tension CurrentTension => TensionMeterEntity.TensionMeter.Tension;

        private Vector3 CharPosition => TensionMeterEntity.transform.position;


        public void OnSceneGUI()
        {
            var angleProgression = CurrentTension * 360;

            Handles.color = new Color(0.53f, 0.11f, 0.13f, 0.74f);
            Handles.DrawSolidArc(CharPosition + new Vector3(0, 1.5f, 0), Vector3.forward, Vector3.up, angleProgression, 0.5f);
        }

    }

}