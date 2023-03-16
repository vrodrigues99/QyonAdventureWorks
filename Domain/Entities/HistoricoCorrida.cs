using Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class HistoricoCorrida : BaseEntity
    {
        public int CompetidorId { get; set; }
        public int PistaCorridaId { get; set; }
        public DateTime DataCorrida { get; set; }
        public decimal TempoGasto { get; set; }

        public virtual Competidores Competidor { get; set; }
        public virtual PistaCorrida PistaCorrida { get; set; }

        public HistoricoCorrida()
        {

        }
    }
}
