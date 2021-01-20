using static AChildsCourage.Rng;

namespace AChildsCourage.Game
{

    public readonly struct GameSeed
    {

        public static GameSeed CreateRandom() =>
            new GameSeed(RandomRng().Map(GetValueBetween, int.MinValue, int.MaxValue));


        private readonly int value;


        private GameSeed(int value) => this.value = value;


        public static implicit operator int(GameSeed seed) =>
            seed.value;

        public static explicit operator GameSeed(int value) =>
            new GameSeed(value);

    }

}