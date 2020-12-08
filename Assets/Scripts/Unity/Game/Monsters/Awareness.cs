using System;

namespace AChildsCourage.Game.Monsters
{

    public static class MAwareness
    {

        private const float MinAwareness = 0;
        private const float MaxAwareness = 1;

        
        public static Func<Awareness, float, Awareness> LooseAwareness =>
            (awareness, amount) =>
                new Awareness(awareness.Value - amount);
        
        public static Func<Awareness, float, Awareness> GainAwareness =>
            (awareness, amount) =>
                new Awareness(awareness.Value + amount);

        
        public readonly struct Awareness
        {

            public float Value { get; }


            public Awareness(float value) => Value = value.Clamp(MinAwareness, MaxAwareness);

        }

    }

}