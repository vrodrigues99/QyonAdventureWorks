using Domain.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Validators
{
    public class PistaValidator : AbstractValidator<PistaCorrida>
    {
        public PistaValidator()
        {
            RuleFor(x => x.Descricao)
                .NotNull()
                .WithMessage("Insira a descrição da pista.");
        }
    }
}
