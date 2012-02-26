namespace Altalerta.Core
{
    public interface IAudioPlayer
    {
        void Play(string fileName);
        void Stop();
    }
}