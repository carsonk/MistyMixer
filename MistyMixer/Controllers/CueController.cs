using MistyMixer.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MistyMixer.Controllers
{
    class CueController
    {
        private ObservableCollection<Cue> _cueList = new ObservableCollection<Cue>();

        public Cue currentCue;
        public int currentCueIndex = 0;

        public ObservableCollection<Cue> CueList
        {
            get
            {
                return _cueList;
            }
        }

        public CueController()
        {

        }

        public void AddSoundCue()
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Audio Files|*.mp3;*.wav;*.aiff";

            if(ofd.ShowDialog() == DialogResult.OK)
            {
                SoundCue cue = new SoundCue();
                cue.FileName = ofd.FileName;
                cue.Title = Path.GetFileName(ofd.FileName);

                _cueList.Add(cue);
            }
        }

        public void RemoveCue(SoundCue cue)
        {
            _cueList.Remove(cue);
        }

        public void StageCue(Cue cue)
        {
            int index = _cueList.IndexOf(cue);

            if(index > -1)
            {
                currentCue = cue;
                currentCueIndex = index;
                currentCue.Stage();
            }
        }

        public void StageCue(int index)
        {
            Cue cue = _cueList.ElementAt(index);

            if(cue != null)
            {
                currentCue = cue;
                currentCueIndex = index;
                currentCue.Stage();
            }
        }

        public void PlayCurrentCue()
        {
            currentCue.Go();
        }

        public void PauseCurrentCue()
        {
            currentCue.Pause();
        }
    }
}
