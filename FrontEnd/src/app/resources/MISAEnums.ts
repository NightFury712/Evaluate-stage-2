// Trạng thái của form
export const MISAFormMode = {
  // Thêm mới dữ liệu
  Save: 0,
  // Cập nhật dữ liệu
  Update: 1,
  // Xóa dữ liệu
  Delete: 2,
}

// Trạng thái request
export  const MISACode = {
  // Thành công
  Ok: 200,
  // Thêm mới thành công
  Created: 201,
  // Dữ liệu gửi lên sai định dạng
  BadRequest: 400,
  // Dữ liệu gửi lên không hợp lệ
  NotValid: 900,
  // Có lỗi xảy ra
  Exception: 500,
}

// Mã giới tính
export const Gender = {
  // Nam
  Male: 0,
  // Nữ
  Female: 1,
  // Giới tính khác
  Other: 2,
}

// Tên hiển thị cho gới tính
export const GenderName ={
  Male: "Nam",
  Female: "Nữ",
  Other: "Giới tính khác",
}
