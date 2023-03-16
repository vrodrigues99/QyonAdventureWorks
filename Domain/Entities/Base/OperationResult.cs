namespace Domain.Entities.Base
{
    public class OperationResult<TEntity> where TEntity : BaseEntity
    {
        public OperationResult(bool isSuccess, TEntity result)
        {
            this.isSuccess = isSuccess;
            this.result = result;
        }

        public bool isSuccess { get; private set; }
        public TEntity result { get; private set; }
    }
}
