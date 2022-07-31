using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Task4.Models;

namespace Task4.Services
{
    public class SecurityService
    {
        UsersDAO usersDAO = new UsersDAO();
        public SecurityService()
        {
            
        }
        public bool IsValid(UserModel user)
        {
            return usersDAO.FindUserByNameAndPassword(user);    
        }
        public bool RegistrationCompleted(UserModel user)
        {
            return usersDAO.AddNewUser(user);
        }
        public List<UserModel> UserList()
        {
            return usersDAO.ListUsers();
        }
        public bool UsersBlocked(int users)
        {
            return usersDAO.BlockUsers(users);
        }
        public bool UsersUnblocked(int users)
        {
            return usersDAO.UnblockUsers(users);
        }
        public bool UsersDeleted(int users)
        {
            return usersDAO.DeleteUsers(users);
        }
        public int GetUserId(string user)
        {
            return usersDAO.GetId(user);
        }
    }
}
