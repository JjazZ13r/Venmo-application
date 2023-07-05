using System.Collections.Generic;
using TenmoServer.Models;

namespace TenmoServer.DAO
{
    public interface ITransferDao
    {
        IList<Transfer> GetTransferByUserId(int id);
        Transfer GetTransferByStatus(int statusId);
        Transfer CreateSendTransfer(int receiverUserId);
        //Transfer CreateRequestTransfer(int senderUserId);
    }
}
