using BookStore.BLL.Interfaces;
using DAL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace School.web.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class SubjectController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public SubjectController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var subjects = await _unitOfWork.Repository<Subject>().GetAllEntities();
                return Ok(subjects);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error retrieving data: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Subject subject)
        {
            if (subject == null || string.IsNullOrWhiteSpace(subject.Name))
            {
                return BadRequest("Invalid subject data.");
            }

            try
            {
                await _unitOfWork.Repository<Subject>().CreateEntityAsync(subject);
                await _unitOfWork.Complete();
                return CreatedAtAction(nameof(Get), new { id = subject.Id }, subject);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error saving data: {ex.Message}");
            }
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] Subject subject)
        {
            if (subject == null || subject.Id <= 0 || string.IsNullOrWhiteSpace(subject.Name))
            {
                return BadRequest("Invalid subject data.");
            }

            try
            {
                var existingSubject = await _unitOfWork.Repository<Subject>().GetEntityById(subject.Id);
                if (existingSubject == null)
                {
                    return NotFound($"Subject with ID {subject.Id} not found.");
                }

                _unitOfWork.Repository<Subject>().UpdateEntityAsync(subject);
                await _unitOfWork.Complete();
                return Ok(subject);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error updating data: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Invalid subject ID.");
            }

            try
            {
                var subject = await _unitOfWork.Repository<Subject>().GetEntityById(id);
                if (subject == null)
                {
                    return NotFound($"Subject with ID {id} not found.");
                }

                _unitOfWork.Repository<Subject>().DeleteEntityAsync(subject);
                await _unitOfWork.Complete();
                return NoContent(); // 204 No Content
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error deleting data: {ex.Message}");
            }
        }
    }
}
