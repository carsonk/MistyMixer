using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MistyMixer.Models
{
    class SoundCue : Cue
    {
        private string _filepath;

        public string Filepath
        {
            get
            {
                return _filepath;
            }
            set
            {
                _filepath = value;
            }
        }

        public override void Stage()
        {
            throw new NotImplementedException();
        }

        public override void Go()
        {
            throw new NotImplementedException();
        }

        public override void Stop()
        {
            throw new NotImplementedException();
        }

        public override void Pause()
        {
            throw new NotImplementedException();
        }
    }
}
