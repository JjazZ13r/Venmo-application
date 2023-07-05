using Microsoft.AspNetCore.SignalR;
using System.Collections.Generic;
using TenmoServer.Models;

namespace TenmoServer.DAO
{
    public interface IAccountDAO
    {
        List<Account> GetAccounts();
        Account GetAccountById(int id);
        Account CreateAccount(Account account);
        decimal GetBalanceById(int id);

    }
}
