using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PetsApi.Data.Models;
using PetsApi.Data.Repos;

namespace PetsApi.Web.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class AuthPetsController : ControllerBase
    {
        private readonly IPetsRepo _petsRepo;

        public AuthPetsController(IPetsRepo petsRepo)
        {
            _petsRepo = petsRepo;
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Pet>> Get(int id)
        {
            return await _petsRepo.GetPetById(id);
        }
        
        [HttpGet]
        public async Task<ActionResult<List<Pet>>> Get()
        {
            return await _petsRepo.GetAll();
        }
    }
}