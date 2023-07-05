using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading;
using TenmoServer.Exceptions;
using TenmoServer.Models;

namespace TenmoServer.DAO
{
    public class TransferDao : ITransferDao
    {
        private readonly string connectionString;

        public TransferDao(string dbConnectionString)
        {
            connectionString = dbConnectionString;
        }

        public IList<Transfer> GetTransferByUserId(int id)
        {
            List<Transfer> transfers = new List<Transfer>();

            string sql = "SELECT transfer_id, transfer_type_id, transfer_status_id, account_from, " +
                         "account_to, amount FROM transfer WHERE transfer_id = @tranfer_id";

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@transfer_id", id);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while(reader.Read())
                    {
                        Transfer transfer = MapRowToTransfer(reader);
                        transfers.Add(transfer);
                    }    
                    
                }
            }
            catch (SqlException ex)
            {
                throw new DaoException("SQL exception occurred", ex);
            }
            return transfers;
        }
        public IList<Transfer> GetTransferByStatus(int statusId)
        {
            List<Transfer> transfers = null;
            Transfer transfer = null;

            string sql = "SELECT transfer_id, transfer_type, transfer_status_id, account_from, account_to, amount " +
                         "WHERE transfer_status_id = @transfer_status_id";

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@transfer_status_id", statusId);
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        transfer = MapRowToTransfer(reader);
                        transfers.Add(transfer);
                    }
                }
            }
            catch(SqlException ex)
            {
                throw new DaoException("SQL exception occurred", ex);
            }
            return transfers;
        }
        public Transfer GetTransferByTransferId(int id)
        {
            Transfer transfer = null;

            string sql = "SELECT * FROM transfer WHERE transfer_id = @transfer_id";

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@transfer_id", id);
                    SqlDataReader reader = cmd.ExecuteReader();

                    if(reader.Read())
                    {
                        transfer = MapRowToTransfer(reader);
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new DaoException("SQL Exception Occurred", ex);
            }
            return transfer;
        }
        public Transfer CreateSendTransfer(int reciverUserId, Transfer transfer)
        {
            Transfer newTransfer = null;

            string sql = "INSERT INTO transfer (transfer_type_id, transfer_status_id, account_from, account_to, amount)" +
                         "OUTPUT INSERTED.transfer_id " +
                         "VALUES (@transfer_type_id, @transfer_status_id, @account_from, @account_to, @amount)";
            int newTransferId = 0;
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@transfer_type_id", transfer.TransferTypeId);
                    cmd.Parameters.AddWithValue("@transfer_status_id", transfer.TransferStatusId);
                    cmd.Parameters.AddWithValue("@account_from", transfer.AccountFrom);
                    cmd.Parameters.AddWithValue("@account_to", transfer.AccountTo);
                    cmd.Parameters.AddWithValue("@amount", transfer.Amount);
                    newTransferId = Convert.ToInt32(cmd.ExecuteScalar());

                }
                newTransfer = GetTransferByTransferId(newTransferId);
            }
            catch (SqlException ex)
            {
                throw new DaoException("SQL Exception Occurred", ex);
            }
            return newTransfer;
        }
        private Transfer MapRowToTransfer(SqlDataReader reader)
        {
            Transfer transfer = new Transfer();
            transfer.TransferId = Convert.ToInt32(reader["transfer_id"]);
            transfer.TransferTypeId = Convert.ToInt32(reader["transfer_type_id"]);
            transfer.TransferStatusId = Convert.ToInt32(reader["transfer_status_id"]);
            transfer.AccountFrom = Convert.ToInt32(reader["account_from"]);
            transfer.AccountTo = Convert.ToInt32(reader["account_to"]);
            transfer.Amount = Convert.ToDecimal(reader["amount"]);
            return transfer;
        }
    }
}
