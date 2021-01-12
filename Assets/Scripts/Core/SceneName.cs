namespace AChildsCourage
{

    internal readonly struct SceneName
    {
        
        public static readonly SceneName game = new SceneName("Game");
        public static readonly SceneName menu = new SceneName("MainMenu");
        public static readonly SceneName startCutscene = new SceneName("StartCutscene");
        public static readonly SceneName endCutscene = new SceneName("EndCutscene");


        private readonly string value;


        private SceneName(string value) =>
            this.value = value;


        public static implicit operator string(SceneName sceneName) =>
            sceneName.value;

    }

}