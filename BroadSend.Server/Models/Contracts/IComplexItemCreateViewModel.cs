namespace BroadSend.Server.Models.Contracts
{
    public interface IComplexItemCreateViewModel<T1, T2>
        where T1 : class, IComplexItem
        where T2 : class, IComplexItemAlias
    {
        public T1 ComplexItem { get; set; }
        public T2 ComplexItemAlias { get; set; }
    }
}