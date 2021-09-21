using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.ApplicationCore.Entities
{
    public class Customer : BaseEntity
    {
        #region Properties
        /// <summary>
        /// Khóa chính
        /// </summary>
        [PrimaryKey]
        public Guid? CustomerId { get; set; }
        /// <summary>
        /// Mã Khách hàng
        /// </summary>
        [Required]
        [CheckDuplicate]
        public string CustomerCode { get; set; }
        /// <summary>
        /// Tên khách hàng
        /// </summary>
        [Required]
        public string CustomerName { get; set; }
        /// <summary>
        /// Số điện thoại di động
        /// </summary>
        public string PhoneNumber { get; set; }
        /// <summary>
        /// Số điện thoại công ty
        /// </summary>
        public string CompanyPhoneNumber { get; set; }
        /// <summary>
        /// Mã số thuế các nhân
        /// </summary>
        public string PersonalTaxCode { get; set; }
        /// <summary>
        /// Quốc gia
        /// </summary>
        public string Country { get; set; }
        /// <summary>
        /// Tỉnh/ Thành phố
        /// </summary>
        public string Province { get; set; }
        /// <summary>
        /// Quận/ Huyện
        /// </summary>
        public string District { get; set; }
        /// <summary>
        /// Phường/ Xã
        /// </summary>
        public string Ward { get; set; }
        #endregion
    }
}
