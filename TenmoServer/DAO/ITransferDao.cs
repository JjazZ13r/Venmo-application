﻿using System.Collections.Generic;
using TenmoServer.Models;

namespace TenmoServer.DAO
{
    public interface ITransferDao
    {
        Transfer GetTransferByTransferId(int id);
        IList<Transfer> GetTransferByUserId(int id);
        IList<Transfer> GetTransfersByStatus(int statusId);
        Transfer CreateSendTransfer(int receiverUserId, Transfer transfer);
        //Transfer CreateRequestTransfer(int senderUserId);
    }
}