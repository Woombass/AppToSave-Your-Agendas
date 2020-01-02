using System;
using System.Collections.Generic;

namespace Way_to_save_your_agendas.Models
{
    [Serializable]
    public class Aim
    {
        public int ID { get; set; }
        /// <summary>
        /// Список заметок цели.
        /// </summary>
        public List<Note> Notes { get; set; }
        /// <summary>
        /// Имя цели.
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Предполагаемая дата завершения цели.
        /// </summary>
        public DateTime FinalizationTime { get; set; }

        public Aim(string name, DateTime finalization)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException(nameof(name),"Имя цели не может быть NULL!");
            }

            if (finalization < DateTime.Now)
            {
                throw new ArgumentOutOfRangeException(nameof(finalization), "Дата завершения цели не может быть меньше текущей!");
            }


            Name = name;
            FinalizationTime = finalization;
        }


        public override string ToString()
        {
            return "ID: " + ID + " Цель: " + Name + " Дата предполагаемого завершения: " +
                   FinalizationTime.ToShortDateString();
        }
    }
}
