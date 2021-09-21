using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Extensions.Configuration;
using MISA.ApplicationCore.Entities;
using MISA.ApplicationCore.Enums;
using MISA.ApplicationCore.Interfaces.Infarstructure;

namespace MISA.Infarstructure
{
    public class ImportCustomerRepository : BaseRepository<Customer>, IImportCustomerRepository
    {

        #region CONSTRUCTOR
        public ImportCustomerRepository(IConfiguration configuration):base(configuration)
        {

        }
        #endregion

        #region METHODS
        //public int ImportCustomers(List<Customer> customers)
        //{
        //    var rowEffects = 0;
        //    _dbConnection.Open();
        //    using (var transaction = _dbConnection.BeginTransaction())
        //    {
        //        try
        //        {
        //            foreach (var entity in customers)
        //            {
        //                var parameter = MappingDbtype(entity);
        //                var result = 0;
        //                if (entity.EntityState == EntityState.AddNew)
        //                {
        //                    result = _dbConnection.Execute(
        //                        $"Proc_InsertCustomer",
        //                        parameter,
        //                        commandType: CommandType.StoredProcedure);
        //                    if (result > 0)
        //                    {
        //                        rowEffects += result;
        //                    }
        //                    else
        //                    {
        //                        transaction.Rollback();
        //                        return 0;
        //                    }
        //                }
        //                else if (entity.EntityState == EntityState.Update)
        //                {
        //                    result = _dbConnection.Execute(
        //                        $"Proc_UpdateCustomer",
        //                        parameter,
        //                        commandType: CommandType.StoredProcedure);
        //                    if (result > 0)
        //                    {
        //                        rowEffects += result;
        //                    }
        //                    else
        //                    {
        //                        transaction.Rollback();
        //                        return 0;
        //                    }
        //                }
        //            }
        //            transaction.Commit();
        //        }
        //        catch (Exception)
        //        {
        //            transaction.Rollback();
        //            throw;
        //        }
        //    }
        //    return rowEffects;
        //}
        #endregion
    }
}
