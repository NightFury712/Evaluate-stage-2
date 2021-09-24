using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Extensions.Configuration;
using MISA.ApplicationCore.Entities;
using MISA.ApplicationCore.Enums;
using MISA.ApplicationCore.Interfaces.Infarstructure;
using MySqlConnector;

namespace MISA.Infarstructure
{
    public class BaseRepository<MISAEntity> : IBaseRepository<MISAEntity>, IDisposable where MISAEntity : BaseEntity
    {
        #region DECLARE
        private readonly IConfiguration _configuration;
        string _connectionString = string.Empty;
        protected IDbConnection _dbConnection = null;
        protected string _tableName;
        #endregion

        #region Constructor
        public BaseRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("MISACukCukConnectionString");// Chuỗi kết nối database
            _dbConnection = new MySqlConnection(_connectionString);
            _tableName = typeof(MISAEntity).Name;
        }
        #endregion

        #region Method
        /// <summary>
        /// Lấy toàn bộ thực thể
        /// </summary>
        /// <returns>Danh sách thực thể</returns>
        /// Author: HHDang(30/6/2021)
        public IEnumerable<MISAEntity> GetEntities()
        {
            try
            {
                // Khởi tạo các commandText:
                var entities = _dbConnection.Query<MISAEntity>($"Proc_Get{_tableName}s", commandType: CommandType.StoredProcedure);

                // Trả về dữ liệu:
                return entities;
            }
            catch (Exception)
            {
                return null;
            }

        }

        /// <summary>
        /// Lấy thực thể theo id
        /// </summary>
        /// <param name="entityId">id của thực thể</param>
        /// <returns>Thực thể theo id tìm kiếm</returns>
        /// Author: HHDang (30/7/2021)
        public MISAEntity GetEntityById(Guid entityId)
        {
            try
            {
                var parameter = new DynamicParameters();
                parameter.Add($"@{_tableName}Id", entityId, DbType.String);
                var entity = _dbConnection.Query<MISAEntity>($"Proc_Get{_tableName}ById", parameter, commandType: CommandType.StoredProcedure).FirstOrDefault();
                return entity;
            }
            catch (Exception)
            {
                return null;
                throw;
            }

        }

        /// <summary>
        /// Thêm mới bản ghi
        /// </summary>
        /// <param name="entity">Thông tin bản ghi</param>
        /// <returns>Số cột bị ảnh hưởng</returns>
        /// Author: HHDang (30/7/2021)
        public int Save(MISAEntity entity)
        {
            var rowAffects = 0;
            _dbConnection.Open();
            using (var transaction = _dbConnection.BeginTransaction())
            {
                try
                {
                    // Xử lý các kiểu dữ liệu (mapping dataType):
                    var parameters = MappingDbtype(entity);
                    // Thực thi commandText
                    rowAffects = _dbConnection.Execute($"Proc_Insert{_tableName}",
                        parameters,
                        commandType: CommandType.StoredProcedure,
                        transaction: transaction);
                    if (rowAffects > 0)
                    {
                        transaction.Commit();
                    }
                    else
                    {
                        transaction.Rollback();
                    }
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
            // Trả về kết quả (Số bản ghi thêm mới được)
            return rowAffects;
        }

        /// <summary>
        /// Cập nhật bản ghi
        /// </summary>
        /// <param name="entity">Thông tin bản ghi cập nhật</param>
        /// <returns>Số bản ghi cập nhật được</returns>
        /// Author: HHDang (30/7/2021)
        public int Update(MISAEntity entity)
        {

            var rowAffects = 0;
            _dbConnection.Open();
            using (var transaction = _dbConnection.BeginTransaction())
            {
                try
                {
                    // Xử lý các kiểu dữ liệu (mapping dataType):
                    var parameters = MappingDbtype(entity);
                    // Thực thi command
                    rowAffects = _dbConnection.Execute($"Proc_Update{_tableName}",
                        parameters,
                        commandType: CommandType.StoredProcedure, 
                        transaction: transaction);
                    if (rowAffects > 0)
                    {
                        transaction.Commit();
                    }
                    else
                    {
                        transaction.Rollback();
                    }
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }

            }
            // Trả về kết quả (Số bản ghi cập nhật được)
            return rowAffects;


        }

        /// <summary>
        /// Xóa bản ghi
        /// </summary>
        /// <param name="entityId">Id của bản ghi cần xóa</param>
        /// <returns>Số cột bị ảnh hưởng</returns>
        /// Author: HHDang (30/7/2021)
        public int Delete(Guid entityId)
        {
            var rowEffects = 0;
            _dbConnection.Open();
            using (var transaction = _dbConnection.BeginTransaction())
            {
                try
                {
                    var parameter = new DynamicParameters();
                    parameter.Add($"@{_tableName}Id", entityId, DbType.String);

                    rowEffects = _dbConnection.Execute($"Proc_Delete{_tableName}ById",
                        parameter,
                        commandType: CommandType.StoredProcedure,
                        transaction: transaction);
                    if (rowEffects > 0)
                    {
                        transaction.Commit();
                    }
                    else
                    {
                        transaction.Rollback();
                    }
                }
                catch (Exception)
                {
                    transaction.Rollback();
                }

            }
            return rowEffects;
        }

        /// <summary>
        /// Thực hiện nhập khẩu danh sách thực thể
        /// </summary>
        /// <param name="entities">Danh sách thực thể</param>
        /// <returns>Số cột bị ảnh hưởng</returns>
        /// Author: HHDang (17/09/2021)
        public int ImportEntities(List<MISAEntity> entities)
        {
            var rowEffects = 0;
            _dbConnection.Open();
            using (var transaction = _dbConnection.BeginTransaction())
            {
                try
                {
                    foreach (var entity in entities)
                    {
                        // Xử lý các kiểu dữ liệu (mapping dataType)
                        var parameter = MappingDbtype(entity);
                        var result = 0;
                        // Kiểm tra nếu trạng thái là thêm mới:
                        if (entity.EntityState == EntityState.AddNew)
                        {
                            // Thực hiện command thêm mới dữ liệu
                            result = _dbConnection.Execute(
                                $"Proc_Insert{_tableName}",
                                parameter,
                                commandType: CommandType.StoredProcedure,
                                transaction: transaction);
                            if (result > 0)
                            {
                                rowEffects += result;
                            }
                            else
                            {
                                // Nếu có lỗi thì rollback
                                transaction.Rollback();
                                return 0;
                            }
                        }
                        // Nếu trạng thái là sửa: 
                        else if (entity.EntityState == EntityState.Update)
                        {
                            // Thực hiện command sửa dữ liệu
                            result = _dbConnection.Execute(
                                $"Proc_Update{_tableName}",
                                parameter,
                                commandType: CommandType.StoredProcedure,
                                transaction: transaction);
                            if (result > 0)
                            {
                                rowEffects += result;
                            }
                            else
                            {
                                // Nếu có lỗi thì rollback
                                transaction.Rollback();
                                return 0;
                            }
                        }
                    }
                    // Commit để thực thi các hành vi trên nếu không có lỗi
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
            return rowEffects;
        }

        /// <summary>
        /// Thực hiện xuất khẩu danh sách thực thể
        /// </summary>
        /// <param name="start">Vị trí bản ghi bắt đầu</param>
        /// <param name="end">Vị trí bản ghi cuối cùng</param>
        /// <returns>Danh sách thực thể xuất khẩu</returns>
        /// Author: HHDang (21/09/2021)
        public IEnumerable<MISAEntity> ExportEntities(int start, int end)
        {
            try
            {
                var parameter = new DynamicParameters();
                var totalRecord = (end - start) + 1;
                parameter.Add("StartIndex", start - 1, DbType.Int32);
                parameter.Add("TotalRecord", totalRecord, DbType.Int32);

                // Thực hiện command
                var entities = _dbConnection.Query<MISAEntity>(
                    $"Proc_Export{_tableName}s",
                    parameter,
                    commandType: CommandType.StoredProcedure);
                // Trả về dữ liệu
                return entities;
            }
            catch (Exception)
            {

                throw;
            }
            
        }

        /// <summary>
        /// Kiểm tra thực thể đã tồn tại hay chưa
        /// </summary>
        /// <param name="entityId">Id của thực thể</param>
        /// <returns>Kết quả (true/false)</returns>
        /// Author: HHDang (17/09/2021)
        public bool CheckEntityExist(Guid? entityId)
        {
            var parameter = new DynamicParameters();
            parameter.Add($"{_tableName}Id", entityId);

            bool result = _dbConnection.Query<bool>(
                $"Proc_Check{_tableName}Exist",
                parameter,
                commandType: CommandType.StoredProcedure).FirstOrDefault();
            return result;
        }

        /// <summary>
        /// Kiểm tra trường địa chỉ có hợp lệ
        /// </summary>
        /// <param name="addressName">Tên trường</param>
        /// <param name="value">Giá trị</param>
        /// <returns></returns>
        public bool CheckAddressValid(string addressName, string value)
        {
            var parameter = new DynamicParameters();
            parameter.Add($"{addressName}Name", value, DbType.String);

            bool result = _dbConnection.Query<bool>(
                $"Proc_Check{addressName}",
                parameter,
                commandType: CommandType.StoredProcedure).FirstOrDefault();
            return result;
        }

        /// <summary>
        /// Kiểm tra địa chỉ có thỏa mãn (Ex: Quận Ba Đình ở thành Phố Hà Nội)
        /// </summary>
        /// <param name="addrChild">Tên địa chỉ con</param>
        /// <param name="addrParent">Tên địa chỉ cha</param>
        /// <param name="valueChild">Giá trị địa chỉ con</param>
        /// <param name="valueParent">Giá trị địa chỉ cha</param>
        /// <returns></returns>
        public bool CheckAddressBelongTo(string addrChild, string addrParent, string valueChild, string valueParent)
        {
            var parameter = new DynamicParameters();
            parameter.Add($"{addrChild}Name", valueChild, DbType.String);
            parameter.Add($"{addrParent}Name", valueParent, DbType.String);

            bool result = _dbConnection.Query<bool>(
                $"Proc_Check{addrChild}In{addrParent}",
                parameter,
                commandType: CommandType.StoredProcedure).FirstOrDefault();
            return result;
        }

        /// <summary>
        /// Kiểm tra trùng lặp thực thể
        /// </summary>
        /// <param name="entity">thông tin thực thể</param>
        /// <param name="property">Thông tin thuộc tính của thực thể</param>
        /// <returns>Thông tin thực thể</returns>
        /// Author: HHDang (06/08/2021)
        public MISAEntity GetEntityByProperty(MISAEntity entity, PropertyInfo property)
        {
            // Tạo dynamic parameters
            var parameter = new DynamicParameters();
            var propertyName = property.Name;
            var propertyValue = property.GetValue(entity);
            var keyValue = entity.GetType().GetProperty($"{_tableName}Id").GetValue(entity);
            parameter.Add("@propertyName", propertyValue, DbType.String);
            parameter.Add("@propertyId", keyValue, DbType.String);

            // Tạo mới commandText
            var query = string.Empty;
            if (entity.EntityState == EntityState.AddNew)
            {
                query = $"SELECT * from {_tableName} WHERE {propertyName} = @propertyName";
            }
            else if (entity.EntityState == EntityState.Update)
            {
                query = $"SELECT * from {_tableName} WHERE {propertyName} = @propertyName AND {_tableName}Id <> @propertyId";
            }
            // Thực thi commandText
            var data = _dbConnection.Query<MISAEntity>(query, param: parameter, commandType: CommandType.Text).FirstOrDefault();
            // Trả về dữ liệu:
            return data;
        }

        /// <summary>
        /// Mapping dữ liệu
        /// </summary>
        /// <param name="entity">Thông tin về thực thể</param>
        /// <returns>parameter mang thông tin thực thể</returns>
        /// Author: HHDang(30/7/2021)
        protected DynamicParameters MappingDbtype(MISAEntity entity)
        {
            var properties = entity.GetType().GetProperties();
            var parameters = new DynamicParameters();

            foreach (var property in properties)
            {
                var propertyName = property.Name;
                var propertyValue = property.GetValue(entity);
                var propertyType = property.PropertyType;
                if (propertyType == typeof(Guid) || propertyType == typeof(Guid?))
                {
                    parameters.Add($"@{propertyName}", propertyValue, DbType.String);
                }
                else if (propertyType == typeof(bool) || propertyType == typeof(bool?))
                {
                    var dbValue = (bool)propertyValue == true ? 1 : 0;
                    parameters.Add($"@{propertyName}", dbValue, DbType.Int32);
                }
                else
                {
                    parameters.Add($"@{propertyName}", propertyValue);
                }
            }
            return parameters;
        }
        /// <summary>
        /// Đóng kết nối đến database
        /// </summary>
        /// Author: HHDang (06/08/2021)
        public void Dispose()
        {
            if (_dbConnection.State == ConnectionState.Open)
            {
                _dbConnection.Close();
            }
        }
        #endregion
    }
}
