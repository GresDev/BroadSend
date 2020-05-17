namespace BroadSend.Server.Models.Contracts
{
    public interface IComplexItemAliasCreateViewModel
    {
        public string Alias { get; set; }
        public int ParentId { get; set; }
    }
}