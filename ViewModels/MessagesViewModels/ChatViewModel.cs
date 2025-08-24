namespace ChatApp.ViewModels.MessagesViewModels
{
    public class ChatViewModel
    {
        public ChatViewModel()
        {
            Messages = new List<UserMessagesListViewModel>();
        }

        public string CurrentUserId { get; set; }
        public string ReciverId { get; set; }
        public string ReciverUserName { get; set; }

        public IEnumerable<UserMessagesListViewModel> Messages { get; set; }
    }
}
