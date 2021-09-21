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
        public void ExportCustomers(int start, int end)
        {
            var entities = base.ExportEntities(start, end);
        }

        private ExcelPackage WriteFile(IEnumerable<Customer> customers)
        {
            using(var stream = new MemoryStream())
            {
                using(var package = new ExcelPackage(stream))
                {
                    // Đặt tên người tạo file
                    package.Workbook.Properties.Author = "Hoàng Hải Đăng";

                    // Đặt tiêu đề cho file
                    package.Workbook.Properties.Title = "Danh sách khách hàng";

                    // Tạo 1 sheet để làm việc
                    package.Workbook.Worksheets.Add("DS khách hàng");

                    // Lấy sheet vừa add ra để thao tác
                    ExcelWorksheet ws = package.Workbook.Worksheets[1];

                    // Fontsize mặc định cho cả sheet
                    ws.Cells.Style.Font.Size = 11;
                    // Font family mặc định cho cả sheet
                    ws.Cells.Style.Font.Name = "Calibri";

                    int rowIndex = 1;

                    var properties = customers.First().GetType().GetProperties();
                    int propsLength = properties.Length;

                    // Khởi tạo thông tin header cho các cột
                    for(int index = 1; index <= propsLength; index++)
                    {
                        var property = properties[index - 1];
                        var displayName = string.Empty;
                        DisplayNameAttribute displayNameAttribute = property.GetCustomAttributes(typeof(DisplayNameAttribute), true).Cast<DisplayNameAttribute>().SingleOrDefault();
                        if (displayNameAttribute != null)
                        {
                            displayName = displayNameAttribute.DisplayName;
                        }
                        ws.Cells[rowIndex, index].Value = displayName;
                    }

                    foreach(var customer in customers)
                    {
                        var props = customer.GetType().GetProperties();
                        rowIndex++;
                        for(int index = 1; index <= propsLength; index++)
                        {
                            var propValue = props[index - 1].GetValue(customer);
                            ws.Cells[rowIndex, index].Value = propValue;
                        }
                    }
                    return package;
                }
            }
        }
        #endregion
    }
}
