﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PetsApi.Data.Models;
using PetsApi.Data.Repos;

namespace PetsApi.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PetsController : ControllerBase
    {
        private readonly IPetsRepo _petsRepo;

        public PetsController(IPetsRepo petsRepo)
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