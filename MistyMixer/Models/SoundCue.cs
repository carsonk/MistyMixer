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

        public override void Stage()
        {
            this.wavePlayer = new WaveOutEvent();
            this.file = new AudioFileReader(_fileName);
            this.file.Volume = 1;

            this.wavePlayer.Init(this.file);

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
            this.wavePlayer.Stop();

            this._currentStatus = Status.Stopped;
        }

        public override void Pause()
        {
            this.wavePlayer.Pause();

            this._currentStatus = Status.Paused;
        }
    }
}
