using BookStore.BLL.Interfaces;
using DAL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace School.web.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class SubjectController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public SubjectController(IUnitOfWork unitOfWork )
        {
           _unitOfWork = unitOfWork;
        }

        [HttpGet]

        public async Task<IActionResult> Get()
        {
            var subjects = await _unitOfWork.Repository<Subject>().GetAllEntities();
            return Ok(subjects);
        }
        [HttpPost]
        public async Task<IActionResult> Post(Subject subject)
        {
            await _unitOfWork.Repository<Subject>().CreateEntityAsync(subject);
            await _unitOfWork.Complete();
            return StatusCode(StatusCodes.Status201Created);
        }

        [HttpPut]

        public async Task<IActionResult> Put(Subject subject)
        {
            _unitOfWork.Repository<Subject>().UpdateEntityAsync(subject);
            await _unitOfWork.Complete();
            return Ok(subject);
        }

        [HttpDelete("{id}")]

        public async Task<IActionResult> Delete(int id)
        {
          var sub=  await _unitOfWork.Repository<Subject>().GetEntityById(id);
            _unitOfWork.Repository<Subject>().DeleteEntityAsync(sub);
            await _unitOfWork.Complete();
            return Ok(id);
        }





    }
}
