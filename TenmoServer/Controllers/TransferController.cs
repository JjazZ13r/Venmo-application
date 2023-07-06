using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using TenmoServer.DAO;
using TenmoServer.Exceptions;
using TenmoServer.Models;
using TenmoServer.Security;

namespace TenmoServer.Controllers
{

    [Route("[controller]")]
    [ApiController]
    public class TransferController : Controller
    {
        private readonly ITransferDao dao;
        public TransferController(ITransferDao transferDao)
        {
            dao = transferDao;
        }

        [HttpGet("{id}")]
        public ActionResult<Transfer> GetTransferByTransferId(int id)
        {
            Transfer transfer = dao.GetTransferByTransferId(id);
            if (transfer != null)
            {
                return Ok(transfer);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet("tenmo_user/{id}")]
        public ActionResult<List<Transfer>> GetTransferByUserId(int id)
        {
            IList<Transfer> transfers = dao.GetTransferByUserId(id);
            if (transfers != null)
            {
                return Ok(transfers);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet("tenmo_user/{id}")]
        public ActionResult<List<Transfer>> GetTransferByStatus(int id)
        {
            IList<Transfer> transfers = dao.GetTransfersByStatus(id);
            if (transfers != null)
            {
                return Ok(transfers);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost()]
        public ActionResult<Transfer> CreateSendTransfer(int recieverUserId, int senderUserId)
        {
            if(senderUserId == recieverUserId)
            {
                return Conflict();
            }

            Transfer transfer1 = dao.CreateSendTransfer(recieverUserId, senderUserId);
            return Created($"/transfer/{transfer1.TransferId}", transfer1);
        }

    }
}
