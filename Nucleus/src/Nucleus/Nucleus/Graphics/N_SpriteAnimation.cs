namespace Nucleus
{
    public class Nucleus_Animation
    {
        public string Name { get; private set; }
        public int[] Frames { get; private set; }
        public float FrameDuration { get; private set; }
        public bool IsLooping { get; set; }
        public bool IsFinished { get; set; }

        //*---------------------------------------------------------------------------------
        //* METHODS
        //*---------------------------------------------------------------------------------

        // 1f/12f = 12 frames per second
        public Nucleus_Animation(string pName, int[] pFrames, float pFrameDuration = 1f/12f, bool pIsLooping = true)
        {
            Name = pName;
            Frames = pFrames;
            FrameDuration = pFrameDuration;
            IsLooping = pIsLooping;
            IsFinished = false;
        }
    }
}