using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Way_to_save_your_agendas.Models;

namespace Way_to_save_your_agendas.Controller
{
    public class AimsController : BaseController
    {
        private const string FilePath = @"Bin\Aims.bin";
        public List<Aim> Aims { get; set; }
        public Aim CurrentAim { get; set; }



        public bool IsNew { get; set; } = false;

        public AimsController()
        {
            Aims = Load();
        }

        public AimsController(int id)
        {
            Aims = Load();
            CurrentAim = Aims.SingleOrDefault(a => a.ID == id);
        }


        private List<Aim> Load()
        {
            Directory.CreateDirectory("Bin");
            return base.Load<List<Aim>>(FilePath) ?? new List<Aim>();
        }

        private void Save()
        {
            Directory.CreateDirectory("Bin");
            base.Save(FilePath, Aims);
        }

        public void CreateAim(string name, DateTime finalTime)
        {
            var  currentAim = Aims.SingleOrDefault(a => a.Name == name);

            if (currentAim == null)
            {
                IsNew = true;
                currentAim = new Aim(name,finalTime);

                if (Aims.Count == 0)
                {
                    currentAim.ID = 1;
                }
                else
                {
                    currentAim.ID = Aims.Last().ID + 1;
                }
            }
            else
            {
                IsNew = false;
            }

            CurrentAim = currentAim;
            Aims.Add(CurrentAim);
            Save();
        }


        public void AddNote(string text, DateTime final)
        {
            var newNote = new Note(text,final,CurrentAim);

            if (CurrentAim.Notes == null)
            {
                CurrentAim.Notes = new List<Note>();
                newNote.ID = 1;
            }
            else
            {
                newNote.ID = CurrentAim.Notes.Last().ID + 1;
            }
            CurrentAim.Notes.Add(newNote);
            Save();
        }

        public Aim Choose(int id)
        {
            if (id < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(id), "ID не может быть NULL");
            }

            var currAim = Aims.SingleOrDefault(a => a.ID == id);

            if (currAim == null)
            {
                CurrentAim = null;
                return null;
            }
            else
            {
                CurrentAim = currAim;
                return CurrentAim;
            }
        }

        public List<Note> GetAimsNotes()
        {
            return CurrentAim.Notes ?? null;
        }

        ~AimsController()
        {
            Save();
        }
    }
}
