using BookStore.BLL.Interfaces;
using DAL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace School.web.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public StudentController(IUnitOfWork unitOfWork )
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Student>>> GetAllStudents()
        {
            var students = await _unitOfWork.Repository<Student>().GetAllEntities();

            if (students is null)
                return NotFound();

            return Ok(students);
        }
        [HttpPost]
        public async Task<IActionResult> AddStudent([FromBody] Student student)
        {
            await _unitOfWork.Repository<Student>().CreateEntityAsync(student);
            await _unitOfWork.Complete();
            return Ok();
        }

        [HttpPost]
        public async Task<ActionResult> AddGroupOfStudents([FromBody] IEnumerable<Student> students)
        {
            if (!students.Any())
                return BadRequest();

            await _unitOfWork.Repository<Student>().AddRangeAsync(students);
            await _unitOfWork.Complete();
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            var student = await _unitOfWork.Repository<Student>().GetEntityById(id);
            if (student is null)
                return NotFound();
            _unitOfWork.Repository<Student>().DeleteEntityAsync(student);
            await _unitOfWork.Complete();
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateStudent([FromBody] Student student)
        {
            if (student is null)
                return BadRequest();
            _unitOfWork.Repository<Student>().UpdateEntityAsync(student);
            await _unitOfWork.Complete();
            return Ok();
        }

        [HttpGet("{id}")]

        public async Task<ActionResult<Student>> GetStudentById(int id)
        {
            var student = await _unitOfWork.Repository<Student>().GetEntityById(id);
            if (student is null)
                return NotFound();
            return Ok(student);
        }

    }
}
