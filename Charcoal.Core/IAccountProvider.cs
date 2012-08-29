using Charcoal.Core.Entities;

namespace Charcoal.Core {
	public interface IAccountProvider
	{
		string Authenticate(string username, string password);
		OperationResponse CreateUser(User user);
	}
}
