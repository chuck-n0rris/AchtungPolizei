using System;

namespace AchtungPolizei.Plugins.Impl
{
    /// ѕришлось писать наследника дл€ того чтобы пон€ть когда дошли до конца файла
    public class TrackableWaveChannel : WaveChannel32
    {
        public TrackableWaveChannel(WaveStream sourceStream, float volume, float pan)
            : base(sourceStream, volume, pan)
        {
        }

        public TrackableWaveChannel(WaveStream sourceStream)
            : base(sourceStream)
        {
        }

        public override int Read(byte[] destBuffer, int offset, int numBytes)
        {
            var result = base.Read(destBuffer, offset, numBytes);

            if (Position >= Length && Finished != null)
            {
                Finished(this, new EventArgs());
            }

            return result;
        }

        public event EventHandler Finished;
    }
}