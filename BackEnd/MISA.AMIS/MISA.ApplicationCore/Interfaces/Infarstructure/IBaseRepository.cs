using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
namespace MISA.ApplicationCore.Interfaces.Infarstructure
{
    public interface IBaseRepository<MISAEntity>
    {

        #region Methods
        /// <summary>
        /// Lấy danh sách thực thể
        /// </summary>
        /// <returns>Danh sách thực thể</returns>
        /// Author: HHDang (26/7/2021)
        IEnumerable<MISAEntity> GetEntities();

        /// <summary>
        /// Lấy thông tin thực thể theo id
        /// </summary>
        /// <param name="entityId">Id của thực thể</param>
        /// <returns>Thông tin thực thể</returns>
        /// Author: HHDang (26/7/2021)
        MISAEntity GetEntityById(Guid entityId);

        /// <summary>
        /// Thêm mới thực thể
        /// </summary>
        /// <param name="entity">Obj chứa thông tin thực thể thêm mới</param>
        /// <returns>Số cột bị ảnh hưởng</returns>
        /// Author: HHDang (26/7/2021)
        int Save(MISAEntity entity);

        /// <summary>
        /// Cập nhật thông tin thực thể
        /// </summary>
        /// <param name="entity">Object chứa thông tin thực thể được cập nhật</param>
        /// <returns>Số cột bị ảnh hưởng</returns>
        /// Author: HHDang (21/7/2021)
        int Update(MISAEntity entity);

        /// <summary>
        /// Xóa thực thể 
        /// </summary>
        /// <param name="entityId">Id thực thể cần xóa</param>
        /// <returns>Số bản ghi bị ảnh hưởng</returns>
        /// Author: HHDang (21/7/2021)
        int Delete(Guid entityId);

        /// <summary>
        /// Thực hiện nhập khẩu danh sách thực thể
        /// </summary>
        /// <param name="entities">Danh sách thực thể</param>
        /// <returns>Số cột bị ảnh hưởng</returns>
        /// Author: HHDang (17/09/2021)
        int ImportEntities(List<MISAEntity> entities);

        /// <summary>
        /// Kiểm tra trùng lặp thực thể
        /// </summary>
        /// <param name="entity">thông tin thực thể</param>
        /// <param name="property">Thông tin thuộc tính của thực thể</param>
        /// <returns>Thông tin thực thể</returns>
        /// Author: HHDang (06/08/2021)
        MISAEntity GetEntityByProperty(MISAEntity entity, PropertyInfo property);

        /// <summary>
        /// Kiểm tra thực thể đã tồn tại hay chưa
        /// </summary>
        /// <param name="entityId">Id của thực thể</param>
        /// <returns>Kết quả (true/false)</returns>
        /// Author: HHDang (17/09/2021)
        bool CheckEntityExist(Guid? entityId);

        /// <summary>
        /// Kiểm tra trường địa chỉ có hợp lệ
        /// </summary>
        /// <param name="addressName">Tên trường</param>
        /// <param name="value">Giá trị</param>
        /// <returns></returns>
        bool CheckAddressValid(string addressName, string value);

        /// <summary>
        /// Kiểm tra địa chỉ có thỏa mãn (Ex: Quận Ba Đình ở thành Phố Hà Nội)
        /// </summary>
        /// <param name="addrChild">Tên địa chỉ con</param>
        /// <param name="addrParent">Tên địa chỉ cha</param>
        /// <param name="valueChild">Giá trị địa chỉ con</param>
        /// <param name="valueParent">Giá trị địa chỉ cha</param>
        /// <returns></returns>
        bool CheckAddressBelongTo(string addrChild, string addrParent, string valueChild, string valueParent);

        /// <summary>
        /// Thực hiện xuất khẩu danh sách thực thể
        /// </summary>
        /// <param name="start">Vị trí bản ghi bắt đầu</param>
        /// <param name="end">Vị trí bản ghi cuối cùng</param>
        /// <returns>Danh sách thực thể xuất khẩu</returns>
        /// Author: HHDang (21/09/2021)
        IEnumerable<MISAEntity> ExportEntities(int start, int end);
    }
    #endregion
}

