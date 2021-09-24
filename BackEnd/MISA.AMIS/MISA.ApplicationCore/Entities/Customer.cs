using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        [DisplayName("Mã khách hàng")]
        [ExportField]
        public string CustomerCode { get; set; }
        /// <summary>
        /// Tên khách hàng
        /// </summary>
        [DisplayName("Tên khách hàng")]
        [ExportField]
        public string CustomerName { get; set; }
        /// <summary>
        /// Số điện thoại di động
        /// </summary>
        [DisplayName("Số điện thoại")]
        [ExportField]
        public string PhoneNumber { get; set; }
        /// <summary>
        /// Số điện thoại công ty
        /// </summary>
        [DisplayName("Số ĐT công ty")]
        [ExportField]
        public string CompanyPhoneNumber { get; set; }
        /// <summary>
        /// Mã số thuế các nhân
        /// </summary>
        [DisplayName("Mã số thuế")]
        [ExportField]
        public string PersonalTaxCode { get; set; }
        /// <summary>
        /// Quốc gia
        /// </summary>
        [DisplayName("Quốc Gia")]
        [ExportField]
        public string Country { get; set; }
        /// <summary>
        /// Tỉnh/ Thành phố
        /// </summary>
        [DisplayName("Tỉnh/Thành phố")]
        [ExportField]
        public string Province { get; set; }
        /// <summary>
        /// Quận/ Huyện
        /// </summary>
        [DisplayName("Quận/Huyện")]
        [ExportField]
        public string District { get; set; }
        /// <summary>
        /// Phường/ Xã
        /// </summary>
        [DisplayName("Phường/Xã")]
        [ExportField]
        public string Ward { get; set; }
        #endregion
    }
}
