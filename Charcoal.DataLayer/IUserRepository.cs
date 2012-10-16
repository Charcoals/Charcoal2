using System.Collections.Generic;

namespace Charcoal.DataLayer
{
    public interface IUserRepository:IRepository
    {
        dynamic FindByEmail(string email);
        dynamic FindByUserName(string name);
        dynamic FindByAPIKey(string apiKey);
        bool IsValid(string userName, string password);
        string GetAPIKey(string userName, string password);
        List<dynamic> GetAllUsers(string apiKey);
    }
}