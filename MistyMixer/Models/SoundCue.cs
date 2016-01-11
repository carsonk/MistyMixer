using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MistyMixer.Utilities;
using NAudio.Wave;

namespace MistyMixer.Models
{
    class SoundCue : Cue
    {
        private string _fileName;

        private IWavePlayer wavePlayer;
        private AudioFileReader file;

        public string FileName
        {
            get
            {
                return _fileName;
            }
            set
            {
                _fileName = value;
            }
        }

        public SoundCue()
        {
        }

        ~SoundCue()
        {
            Stop();
        }

        public override void Stage()
        {
            this.wavePlayer = new WaveOutEvent();

            this.file = new AudioFileReader(_fileName);
            this.file.Volume = 1;

            this.wavePlayer.Init(this.file);
            this.wavePlayer.PlaybackStopped += new EventHandler<StoppedEventArgs>(PlaybackEnded);

            this._currentStatus = Status.Staged;
        }

        public override void Go()
        {
            if(this.wavePlayer == null)
            {
                throw new InvalidOperationException("Tried to play cue without staging.");
            }

            this.wavePlayer.Play();

            this._currentStatus = Status.Playing;
        }

        public override void Stop()
        {
            if (this.wavePlayer == null)
                throw new InvalidOperationException("Tried to stop cue without staging.");

            this.wavePlayer.Stop();

            this.wavePlayer.PlaybackStopped -= new EventHandler<StoppedEventArgs>(PlaybackEnded);

            this.wavePlayer.Dispose();
            this.wavePlayer = null;

            this.CurrentStatus = Status.Inactive;
        }

        public override void Pause()
        {
            if (this.CurrentStatus != Status.Playing)
                throw new InvalidOperationException("Tried to pause cue without staging.");

            this.wavePlayer.Pause();

            this._currentStatus = Status.Paused;
        }

        private void PlaybackEnded(object sender, StoppedEventArgs args)
        {
            Stop();
        }
    }
}
