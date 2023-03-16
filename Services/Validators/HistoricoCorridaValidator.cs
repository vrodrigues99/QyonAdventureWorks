using Domain.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Validators
{
    public class HistoricoCorridaValidator : AbstractValidator<HistoricoCorrida>
    {
        public HistoricoCorridaValidator()
        {
            RuleFor(x => x.DataCorrida)
                .LessThan(DateTime.Now);
        }
    }
}
