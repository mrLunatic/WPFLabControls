using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabControls.Common
{
    /// <summary>
    /// Отметка на шкале
    /// </summary>
    public class ScaleLabel
    {
        /// <summary>
        /// Значение, которому соответсвует отметка
        /// </summary>
        public double Value { get; set; }
        /// <summary>
        /// Текст отметки
        /// </summary>
        public string String { get; set; }

        /// <summary>
        /// Создание новой отметки
        /// </summary>
        /// <param name="Value"></param>
        /// <param name="String"></param>
        public ScaleLabel(double Value, string String)
        {
            this.Value = Value;
            this.String = String;
        }

    }
}
