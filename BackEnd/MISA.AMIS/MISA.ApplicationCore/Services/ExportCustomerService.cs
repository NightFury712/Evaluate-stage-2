using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using MISA.ApplicationCore.Entities;
using MISA.ApplicationCore.Interfaces.Infarstructure;
using MISA.ApplicationCore.Interfaces.Service;
using OfficeOpenXml;

namespace MISA.ApplicationCore.Services
{
    public class ExportCustomerService : BaseService<Customer>, IExportCustomerService
    {
        #region CONSTRUCTOR
        public ExportCustomerService(IExportCustomerRepository exportCustomerRepository) : base(exportCustomerRepository)
        {

        }
        #endregion

        #region METHODS
        public byte[] ExportCustomers(int start, int end)
        {
            var entities = base.ExportEntities(start, end);

            byte[] fileBytes = WriteFile(entities);

            return fileBytes;
        }

        private byte[] WriteFile(IEnumerable<Customer> customers)
        {
            using(var stream = new MemoryStream())
            {
                using (var package = new ExcelPackage(stream))
                {
                    // Đặt tên người tạo file
                    package.Workbook.Properties.Author = "Hoàng Hải Đăng";

                    // Đặt tiêu đề cho file
                    package.Workbook.Properties.Title = "Danh sách khách hàng";

                    // Tạo 1 sheet để làm việc
                    package.Workbook.Worksheets.Add("DS khách hàng");

                    // Lấy sheet vừa add ra để thao tác
                    ExcelWorksheet ws = package.Workbook.Worksheets[0];

                    // Fontsize mặc định cho cả sheet
                    ws.Cells.Style.Font.Size = 11;
                    // Font family mặc định cho cả sheet
                    ws.Cells.Style.Font.Name = "Calibri";

                    int rowIndex = 1;

                    var properties = customers.First().GetType().GetProperties();
                    int propsLength = properties.Length;
                    int index = 1;
                    // Khởi tạo thông tin header cho các cột
                    foreach(var property in properties)
                    {
                        
                        if(property.IsDefined(typeof(ExportField), false))
                        {
                            var displayName = string.Empty;
                            DisplayNameAttribute displayNameAttribute = property.GetCustomAttributes(typeof(DisplayNameAttribute), true).Cast<DisplayNameAttribute>().SingleOrDefault();
                            if (displayNameAttribute != null)
                            {
                                displayName = displayNameAttribute.DisplayName;
                            }
                            ws.Cells[rowIndex, index].Value = displayName;
                            index++;
                        }
                    }

                    foreach(var customer in customers)
                    {
                        index = 1;
                        var props = customer.GetType().GetProperties();
                        rowIndex++;
                        foreach(var property in properties)
                        {
                            if (property.IsDefined(typeof(ExportField), false))
                            {
                                var propertyValue = property.GetValue(customer);
                                ws.Cells[rowIndex, index].Value = propertyValue == null ? string.Empty : propertyValue;
                                index++;
                            }
                        }
                    }
                    package.SaveAs(stream);
                }
                return stream.ToArray();
            }
        }
        #endregion
    }
}
