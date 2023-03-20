using Domain.Entities;
using System;
using System.Text.Json.Serialization;

namespace Aplication.ViewModels
{
    public class HistoricoViewModel
    {
        public int CompetidorId { get; set; }
        public int PistaCorridaId { get; set; }
        public string DataCorrida { get; set; }
        public decimal TempoGasto { get; set; }

        public virtual Competidores Competidor { get; set; }
        public virtual PistaCorrida PistaCorrida { get; set; }
    }
}
