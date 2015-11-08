using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace MistyMixer.Models
{
    public abstract class Cue
    {
        public enum Status
        {
            Inactive,
            Staged,
            Playing,
            Paused,
            Stopped
        }

        protected string _title;

        public string Title
        {
            get { return _title; }
            set { _title = value; }
        }

        protected Status _currentStatus;

        public Status CurrentStatus
        {
            get { return _currentStatus; }
        }

        abstract public void Stage();
        abstract public void Go();
        abstract public void Stop();
        abstract public void Pause();
    }
}
