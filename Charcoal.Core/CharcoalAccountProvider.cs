using System.Collections.Generic;
using System.Web.Security;
using Charcoal.Core.Entities;
using Charcoal.DataLayer;
using System.Linq;

namespace Charcoal.Core
{
    public class CharcoalAccountProvider : IAccountProvider
    {
        private IUserRepository m_userRepository;
        public CharcoalAccountProvider(IUserRepository userRepository)
        {
            m_userRepository = userRepository;
        }

        public string Authenticate(string username, string password)
        {
            return Membership.ValidateUser(username, password)
                ? Membership.Provider.GetPassword(username, password)
                : string.Empty;
        }

        public OperationResponse CreateUser(User user)
        {
            MembershipCreateStatus createStatus;
            Membership.CreateUser(user.UserName, user.Password, user.Email, user.FirstName, user.LastName, false, null, out createStatus);
            return new OperationResponse(createStatus == MembershipCreateStatus.Success);
        }

        public List<User> GetAllUsers(string apiKey)
        {
            var allUsers = m_userRepository.GetAllUsers(apiKey);
            return allUsers.Count>0 ? allUsers.ConvertAll(e => (User) e):new List<User>();
        }
    }
}
