using WEB_API.Model;

namespace WEB_API.Repository
{
    public interface IExchangeRepository
    {
        List<Exchange> GetAllBranches();
        List<Exchange> GetFindBranches(string date);
        List<Exchange> PostExchangeBranches(List<Exchange> exchange);
    }
}
