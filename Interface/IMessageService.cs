using ChatApp.ViewModels.MessagesViewModels;

namespace ChatApp.Interface
{
    public interface IMessageService
    {
        Task<IEnumerable<MessagesUsersListViewModel>> GetUsers();
        Task<ChatViewModel> GetMessages(string selectedUserId); 
    }
}
