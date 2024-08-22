namespace Sound
{
    public interface ISoundSontroller
    {
        bool IsMuteSound { get;  set; }
        bool IsMuteMusic { get;  set; }
        void MuteSound();   
        void MuteMusic();
    }
}