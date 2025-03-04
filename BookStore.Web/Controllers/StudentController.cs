using BookStore.BLL.Interfaces;
using DAL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace School.web.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public StudentController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Student>>> GetAllStudents()
        {
            try
            {
                var students = await _unitOfWork.Repository<Student>().GetAllEntities();
                if (students == null || !students.Any())
                    return NotFound("No students found.");

                return Ok(students);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error retrieving students: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Student>> GetStudentById(int id)
        {
            if (id <= 0)
                return BadRequest("Invalid student ID.");

            try
            {
                var student = await _unitOfWork.Repository<Student>().GetEntityById(id);
                if (student == null)
                    return NotFound($"Student with ID {id} not found.");

                return Ok(student);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error retrieving student: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddStudent([FromBody] Student student)
        {
            if (student == null || string.IsNullOrWhiteSpace(student.Name))
                return BadRequest("Invalid student data.");

            try
            {
                await _unitOfWork.Repository<Student>().CreateEntityAsync(student);
                await _unitOfWork.Complete();

                return CreatedAtAction(nameof(GetStudentById), new { id = student.Id }, student);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error adding student: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<ActionResult> AddGroupOfStudents([FromBody] IEnumerable<Student> students)
        {
            if (students == null || !students.Any())
                return BadRequest("Student list cannot be empty.");

            try
            {
                await _unitOfWork.Repository<Student>().AddRangeAsync(students);
                await _unitOfWork.Complete();
                return Ok("Students added successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error adding students: {ex.Message}");
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateStudent([FromBody] Student student)
        {
            if (student == null || student.Id <= 0 || string.IsNullOrWhiteSpace(student.Name))
                return BadRequest("Invalid student data.");

            try
            {
                var existingStudent = await _unitOfWork.Repository<Student>().GetEntityById(student.Id);
                if (existingStudent == null)
                    return NotFound($"Student with ID {student.Id} not found.");

                _unitOfWork.Repository<Student>().UpdateEntityAsync(student);
                await _unitOfWork.Complete();

                return Ok(student);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error updating student: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            if (id <= 0)
                return BadRequest("Invalid student ID.");

            try
            {
                var student = await _unitOfWork.Repository<Student>().GetEntityById(id);
                if (student == null)
                    return NotFound($"Student with ID {id} not found.");

                _unitOfWork.Repository<Student>().DeleteEntityAsync(student);
                await _unitOfWork.Complete();

                return NoContent(); // 204 No Content
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error deleting student: {ex.Message}");
            }
        }
    }
}
