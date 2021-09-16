using ESourcing.UI.Core.Entities;
using ESourcing.UI.Core.Repositories;
using ESourcing.UI.Infrastructure.Data;
using ESourcing.UI.Infrastructure.Repositories.Base;

namespace ESourcing.UI.Infrastructure.Repositories
{
    public class UserRepository : Repository<AppUser>, IUserRepository
    {
        #region Fields
        private readonly WebAppContext _webAppContext;
        #endregion

        #region Ctor
        public UserRepository(WebAppContext webAppContext) : base(webAppContext)
        {
        }
        #endregion
    }
}
