using BroadSend.Server.Models;
using BroadSend.Server.Models.Contracts;

namespace BroadSend.Server.ViewModels
{
    public class TitleCreateViewModel : IComplexItemCreateViewModel<Title, TitleAlias>
    {
        public Title ComplexItem { get; set; }

        public TitleAlias ComplexItemAlias { get; set; }
    }
}