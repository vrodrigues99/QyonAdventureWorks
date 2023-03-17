using Domain.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Services.Validators
{
    public class CompetidorValidator : AbstractValidator<Competidores>
    {
        public CompetidorValidator()
        {
            RuleFor(x => x.TemperaturaMediaCorpo)
                .ExclusiveBetween(36, 38)
                .WithMessage("A temperatura do corpo do competidor não é valida. Somente valores entre 36 e 38 graus serão aceitos.");

            RuleFor(x => x.Peso)
                .GreaterThan(0)
                .WithMessage("O peso do competidor deve ser maior que 0.");

            RuleFor(x => x.Altura)
                .GreaterThan(0)
                .WithMessage("A altura do competidor deve ser maior que 0.");

            RuleFor(x => x.Sexo)
                .Must(x => new[] { 'M', 'F', 'O' }.Contains(x))
                .WithMessage("Insira uma opção de sexo válida (M, F ou O [outros])");
        }
    }
}
