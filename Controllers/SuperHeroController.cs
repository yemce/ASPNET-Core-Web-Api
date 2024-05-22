﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.Data;
using WebApi.Entities;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuperHeroController : ControllerBase
    {
        private readonly DataContext _context;

        public SuperHeroController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllHeroes()
        {
            var heroes = await _context.SuperHeroes.ToListAsync();
            return Ok(heroes);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetHero(int id)
        {
            var hero = await _context.SuperHeroes.FindAsync(id);

            if (hero == null)
            {
                return NotFound("Hero not found");
            }
            return Ok(hero);
        }

        [HttpPost]
        public async Task<ActionResult<List<SuperHero>>> AddHero(SuperHero hero)
        {
            _context.SuperHeroes.Add(hero);
            await _context.SaveChangesAsync();

            return Ok(await _context.SuperHeroes.ToListAsync());
        }

        [HttpPut]
        public async Task<ActionResult<List<SuperHero>>> UpdateHero(SuperHero updateHero)
        {
            var dbHero = await _context.SuperHeroes.FindAsync(updateHero.Id);
            if (dbHero is null)
                return NotFound("Hero not found");
            dbHero.Name = updateHero.Name;
            dbHero.FirstName = updateHero.FirstName;
            dbHero.LastName = updateHero.LastName;
            dbHero.Place= updateHero.Place;

            await _context.SaveChangesAsync();
            return Ok(await _context.SuperHeroes.ToListAsync());
        }


        [HttpDelete]
        public async Task<ActionResult<List<SuperHero>>> DeleteHero(int id)
        {
            var dbHero = await _context.SuperHeroes.FindAsync(id);
            if (dbHero is null)
                return NotFound("Hero not found");

            _context.SuperHeroes.Remove(dbHero);
            await _context.SaveChangesAsync();

            return Ok(await _context.SuperHeroes.ToListAsync());
        }


    }
}
