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
            wavePlayer = new WaveOutEvent();

            file = new AudioFileReader(_fileName);
            file.Volume = 1;

            wavePlayer.Init(file);
            wavePlayer.PlaybackStopped += new EventHandler<StoppedEventArgs>(PlaybackEnded);

            _currentStatus = Status.Staged;
        }

        public override void Go()
        {
            if(wavePlayer == null)
            {
                throw new InvalidOperationException("Tried to play cue without staging.");
            }

            wavePlayer.Play();

            _currentStatus = Status.Playing;
        }

        public override void Stop()
        {
            if (wavePlayer == null)
                throw new InvalidOperationException("Tried to stop cue without staging.");

            wavePlayer.Stop();

            wavePlayer.PlaybackStopped -= new EventHandler<StoppedEventArgs>(PlaybackEnded);

            wavePlayer.Dispose();
            wavePlayer = null;

            CurrentStatus = Status.Inactive;
        }

        public override void Pause()
        {
            if (CurrentStatus != Status.Playing)
                throw new InvalidOperationException("Tried to pause cue without staging.");

            wavePlayer.Pause();

            _currentStatus = Status.Paused;
        }

        private void PlaybackEnded(object sender, StoppedEventArgs args)
        {
            Stop();
        }
    }
}
