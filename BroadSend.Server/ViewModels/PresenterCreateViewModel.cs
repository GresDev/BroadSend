using BroadSend.Server.Models;
using BroadSend.Server.Models.Contracts;

namespace BroadSend.Server.ViewModels
{
    public class PresenterCreateViewModel : IComplexItemCreateViewModel<Presenter, PresenterAlias>
    {
        public Presenter ComplexItem { get; set; }

        public PresenterAlias ComplexItemAlias { get; set; }
    }
}