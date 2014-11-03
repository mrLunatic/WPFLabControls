using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabControls.Common
{
    /// <summary>
    /// Атрибут события обновления текстовых меток
    /// </summary>
    public class LabelsUpdateArgs
    {
        /// <summary>
        /// Массив отметок
        /// </summary>
        public ScaleLabel[] Labels { get; private set; }

        /// <summary>
        /// Создание нового 
        /// </summary>
        /// <param name="Labels">Массив новых отметок</param>
        public LabelsUpdateArgs(ScaleLabel[] Labels)
        {
            this.Labels = Labels;
        }
    }
}
