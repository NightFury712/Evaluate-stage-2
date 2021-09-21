using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.ApplicationCore.Entities;
using MISA.ApplicationCore.Enums;
using MISA.ApplicationCore.Interfaces;
using MISA.ApplicationCore.Interfaces.Service;
using MISA.ApplicationCore;

namespace MISA.CukCuk.Web.Api
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class BaseApiController<MISAEntity> : ControllerBase
    {
        #region Declare
        private readonly IBaseService<MISAEntity> _baseService;
        protected ServiceResult _serviceResult;
        string _entityName = string.Empty;
        #endregion

        #region Constructor
        public BaseApiController(IBaseService<MISAEntity> baseService)
        {
            _baseService = baseService;
            _entityName = typeof(MISAEntity).Name;
            _serviceResult = new ServiceResult();
        }
        #endregion

        #region Api
        // <summary>
        /// Lấy danh sách thực thể
        /// </summary>
        /// <returns>Danh sách thực thể</returns>
        /// Author: HHDang (26/7/2021)
        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                var employees = _baseService.GetEntities();
                if (employees != null)
                {
                    if (employees.Count() > 0)
                    {
                        return Ok(employees);
                    }
                }
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, InitExceptionResult(ex));

            }

        }

        /// <summary>
        /// Lấy thông tin thực thể theo id
        /// </summary>
        /// <param name="entityId">Id của thực thể</param>
        /// <returns>Thông tin thực thể</returns>
        /// Author: HHDang (26/7/2021)
        [HttpGet("{entityId}")]
        public IActionResult GetEmployeeById(Guid entityId)
        {
            try
            {
                var employee = _baseService.GetEntityById(entityId);
                if (employee != null)
                {
                    return Ok(employee);
                } else
                {
                    return NoContent();
                }
            }
            catch (Exception ex)
            {

                return StatusCode(500, InitExceptionResult(ex));
            }
        }

        /// <summary>
        /// Thêm mới thực thể
        /// </summary>
        /// <param name="entity">Obj chứa thông tin thực thể thêm mới</param>
        /// <returns>Thông điệp</returns>
        /// Author: HHDang (26/7/2021)
        [HttpPost]
        public IActionResult Post(MISAEntity entity)
        {
            try
            {
                _serviceResult = _baseService.Save(entity);
                if (_serviceResult.MISACode == MISACode.Created && (int)_serviceResult.Data > 0)
                {
                    return Created("Create successfully! ", _serviceResult);
                }
                else if(_serviceResult.MISACode == MISACode.NotValid)
                {
                    return Ok(_serviceResult);
                } else
                {
                    _serviceResult.MISACode = MISACode.Exception;
                    _serviceResult.Messenger = MISA.ApplicationCore.Properties.Resources.ErrorException;
                    return StatusCode(500, _serviceResult);
                }
            }
            catch (Exception ex)
            {

                return StatusCode(500, InitExceptionResult(ex));
            }
        }

        /// <summary>
        /// Cập nhật thông tin thực thể
        /// </summary>
        /// <param name="entity">Thông tin thực thể</param>
        /// <param name="entityId">Id của thực thể</param>
        /// <returns>Thông điệp</returns>
        /// Author: HHDang (21/7/2021)
        [HttpPut("{entityId}")]
        public IActionResult Put([FromBody]MISAEntity entity, Guid entityId)
        {
            try
            {
                entity.GetType().GetProperty($"{_entityName}Id").SetValue(entity, entityId);
                _serviceResult = _baseService.Update(entity);
                return StatusCode(200, _serviceResult);
            }
            catch (Exception ex)
            {

                return StatusCode(500, InitExceptionResult(ex));
            }
        }

        /// <summary>
        /// Xóa thực thể 
        /// </summary>
        /// <param name="entityId">Id thực thể cần xóa</param>
        /// <returns>Thông điệp</returns>
        /// Author: HHDang (21/7/2021)
        [HttpDelete("{entityId}")]
        public IActionResult Delete(Guid entityId)
        {
            try
            {
                _serviceResult = _baseService.Delete(entityId);

                if (_serviceResult.MISACode == MISACode.Ok)
                {
                    return Ok(_serviceResult);
                }
                else
                {
                    return Ok(_serviceResult);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, InitExceptionResult(ex));
                //return NoContent();
            }
        }

        /// <summary>
        /// Khởi tạo thông điệp khi có exception
        /// </summary>
        /// <param name="ex">Thông tin về exception</param>
        /// <returns>Thông điệp</returns>
        /// Author: HHDang (21/7/2021)
        protected ServiceResult InitExceptionResult(Exception ex)
        {
            
            _serviceResult.MISACode = MISACode.Exception;
            _serviceResult.Messenger = MISA.ApplicationCore.Properties.Resources.ErrorException;
            _serviceResult.Data = ex.Message;
            return _serviceResult;
        }
        #endregion
    }
}
