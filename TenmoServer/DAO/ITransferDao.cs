using System.Collections.Generic;
using TenmoServer.Models;

namespace TenmoServer.DAO
{
    public interface ITransferDao
    {
        Transfer GetTransferByTransferId(int id);
        IList<Transfer> GetTransferByUserId(int id);
        List<Transfer> GetTransfersByStatus(int statusId);
        Transfer CreateSendTransfer(int receiverUserId, int senderUserId);
        //Transfer CreateRequestTransfer(int senderUserId);
    }
}
