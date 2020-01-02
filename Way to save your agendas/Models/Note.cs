using System;

namespace Way_to_save_your_agendas.Models
{
    [Serializable]
    /// <summary>
    /// Класс заметки цели.
    /// </summary>
    public class Note
    {
        public int ID { get; set; }
        /// <summary>
        /// Дата создания
        /// </summary>
        public DateTime CreatingTime { get; set; }
        /// <summary>
        /// Текст заметки в цели 
        /// </summary>
        public string NotesText { get; set; }
        /// <summary>
        /// Предполагаемая дата выполнения промежуточнной цели
        /// </summary>
        public DateTime FinlizationTime { get; set; }
        public Aim CurrentAim { get; set; }
        public Note(string aim, DateTime finalTime, Aim currentAim)
        {
            if (finalTime < DateTime.Now)
            {
                throw new ArgumentOutOfRangeException("Время выполнения задачи не может быть меньше текущего!");
            }

            if (string.IsNullOrWhiteSpace(aim))
            {
                throw new ArgumentNullException(nameof(aim), "Текст цели не может быть NULL!");
            }

            if (currentAim == null)
            {
                throw new ArgumentNullException(nameof(currentAim),"Текущая цель не может быть NULL!");
            }
            CurrentAim = currentAim;
            NotesText = aim;
            FinlizationTime = finalTime;
            CreatingTime = DateTime.Now;
        }

        public override string ToString()
        {
            return "ID: " + ID +" Задачи подцели: " + NotesText + " Предполагаемое время завершения: " + FinlizationTime.ToShortDateString();
        }
    }
}
