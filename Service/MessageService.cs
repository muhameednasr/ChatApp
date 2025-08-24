﻿using ChatApp.Data;
using ChatApp.Helper;
using ChatApp.Interface;
using ChatApp.ViewModels.MessagesViewModels;
using Microsoft.EntityFrameworkCore;

namespace ChatApp.Service
{
    public class MessageService : IMessageService
    {
        private readonly ApplicationDbContext _context;
        private readonly ICurrentUserService _currentUserService;
        public MessageService(ApplicationDbContext context,ICurrentUserService currentUserService)
        {
            _context = context;
            _currentUserService = currentUserService;
        }
        public async Task<ChatViewModel> GetMessages(string selectedUserId)
        {
            var currentUserId = _currentUserService.UserId;

            var selectedUser = await _context.Users.FirstOrDefaultAsync(u => u.Id == selectedUserId);
            var selectedUserName =  "";
            if (selectedUser != null)
            {
                selectedUserName = selectedUser.UserName;
            }

            var chatViewModel = new ChatViewModel()
            {
                CurrentUserId = currentUserId,
                ReciverId = selectedUserId,
                ReciverUserName = selectedUserName
            };

            var messages = await _context.Messages
                .Where(i=>(i.SenderId==currentUserId || i.SenderId==selectedUserId)&&
                          (i.ReceiverId == currentUserId || i.ReceiverId == selectedUserId)).Select(i=>new UserMessagesListViewModel()
                          {
                              Id = i.Id,
                              Text = i.Text,
                              Date = i.Date.ToShortDateString(),
                              Time = i.Date.ToShortTimeString(),
                              IsCurrentUserSentMessage = i.SenderId == currentUserId
                          }).ToListAsync();

            chatViewModel.Messages = messages;
            return chatViewModel;
        }

        public async Task<IEnumerable<MessagesUsersListViewModel>> GetUsers()
        {
            var currentUserId = _currentUserService.UserId;
            var users = await _context.Users
                .Where(u => u.Id != currentUserId)
                .Select(u => new MessagesUsersListViewModel
                {
                    Id = u.Id,
                    UserName = u.UserName,
                    LastMessage = _context.Messages.Where(m=>(m.SenderId == currentUserId || m.SenderId == u.Id)
                                                          && (m.ReceiverId == currentUserId || m.ReceiverId == u.Id)).OrderByDescending(m => m.Id).Select(m => m.Text).FirstOrDefault()
                }).ToListAsync();

            return users;
        }
    }
}
