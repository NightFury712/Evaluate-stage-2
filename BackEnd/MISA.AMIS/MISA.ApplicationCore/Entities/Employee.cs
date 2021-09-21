using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using MISA.ApplicationCore.Enums;

namespace MISA.ApplicationCore.Entities
{
    /// <summary>
    /// Thông tin nhân viên
    /// </summary>
    /// Author: HHDang (21/7/2021)
    public class Employee : BaseEntity
    {
        #region Properties

        /// <summary>
        /// Khóa chính
        /// </summary>
        [PrimaryKey]
        public Guid? EmployeeId { get; set; }
        /// <summary>
        /// Mã nhân viên
        /// </summary>
        [CheckDuplicate]
        [Required]
        [DisplayName("mã nhân viên")]
        [MaxLength(20)]
        public string EmployeeCode { get; set; }
        /// <summary>
        /// Họ và tên đầy đủ của nhân viên
        /// </summary>
        [Required]
        [DisplayName("tên đầy đủ")]
        [MaxLength(100)]
        public string EmployeeName { get; set; }
        /// <summary>
        /// Mã giới tính 
        /// </summary>
        public Gender? Gender { get; set; }
        /// <summary>
        /// Giới tính
        /// </summary>
        public string GenderName 
        {
            get { 
                switch(Gender)
                {
                    case Enums.Gender.Female:
                        return "Nữ";
                    case Enums.Gender.Male:
                        return "Nam";
                    case Enums.Gender.Other:
                        return "Giới tính khác";
                    default:
                        return null;
                }
            }
            set { }
        }
        /// <summary>
        /// Ngày sinh 
        /// </summary>
        public DateTime? DateOfBirth { get; set; }
        /// <summary>
        /// Số điện thoại di động
        /// </summary>
        [ValidatePhoneNumber]
        [DisplayName("số điện thoại di động")]
        [MaxLength(20)]
        public string PhoneNumber { get; set; }
        /// <summary>
        /// Email 
        /// </summary>
        [ValidateEmail]
        [DisplayName("email")]
        [MaxLength(100)]
        public string Email { get; set; }
        /// <summary>
        /// Địa chỉ 
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// Số CMTND/CCCD
        /// </summary>
        [ValidateNumber]
        [DisplayName("số CMND")]
        [MaxLength(20)]
        public string IdentityNumber { get; set; }
        /// <summary>
        /// Ngày cấp
        /// </summary>
        public DateTime? IdentityDate { get; set; }
        /// <summary>
        /// Nơi cấp
        /// </summary>
        public string IdentityPlace { get; set; }
        /// <summary>
        /// Mã phòng ban
        /// </summary>
        [Required]
        [DisplayName("đơn vị")]
        public Guid? DepartmentId { get; set; }
        /// <summary>
        /// Tên phòng ban
        /// </summary>
        public string DepartmentName { get; set; }
        /// <summary>
        /// Vị trí, chức danh nhân viên
        /// </summary>
        public string EmployeePosition { get; set; }
        /// <summary>
        /// Số tài khoản ngân hàng
        /// </summary>
        [ValidateNumber]
        [DisplayName("tài khoản ngân hàng")]
        [MaxLength(20)]
        public string BankAccountNumber { get; set; }
        /// <summary>
        /// Tên ngân hàng
        /// </summary>
        public string BankName { get; set; }
        /// <summary>
        /// Tên chi nhánh ngân hàng
        /// </summary>
        public string BankBranchName { get; set; }
        /// <summary>
        /// Tên tỉnh đặt ngân hàng
        /// </summary>
        public string BankProvinceName { get; set; }
        /// <summary>
        /// Số điện thoại cố định
        /// </summary>
        [ValidatePhoneNumber]
        [DisplayName("số điện thoại cố định")]
        [MaxLength(20)]
        public string TelephoneNumber { get; set; }

        #endregion
    }
}
