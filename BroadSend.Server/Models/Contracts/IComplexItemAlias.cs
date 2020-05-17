namespace BroadSend.Server.Models.Contracts
{
    public interface IComplexItemAlias
    {
        public int Id { get; set; }
        public string Alias { get; set; }
        public int ParentId { get; set; }
    }
}