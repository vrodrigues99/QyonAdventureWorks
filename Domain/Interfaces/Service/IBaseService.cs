using Domain.Entities.Base;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Interfaces.Service
{
    public interface IBaseService<TEntity> where TEntity : BaseEntity
    {
        OperationResult<TEntity> Add<TValidator>(TEntity obj) where TValidator : AbstractValidator<TEntity>;

        OperationResult<TEntity> Update<TValidator>(TEntity obj) where TValidator : AbstractValidator<TEntity>;

        TEntity GetById(int id);

        IEnumerable<TEntity> GetAll();

        void Delete(int id);
    }
}
