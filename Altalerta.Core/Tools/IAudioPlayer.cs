namespace Altalerta.Core.Tools
{
    public interface IAudioPlayer
    {
        void Play(string fileName);
        void Stop();
    }
}