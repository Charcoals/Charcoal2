using Charcoal.Core.Entities;

namespace Charcoal.Models
{
    public class MemberViewModel
    {
        public MemberViewModel()
        {
            
        }
        public MemberViewModel(User user)
        {
            UserName = user.UserName;
            Email = user.Email;
            Id = user.Id;
        }

        public long Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
    }
}