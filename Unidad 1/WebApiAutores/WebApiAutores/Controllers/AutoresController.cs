﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiAutores.Dtos;
using WebApiAutores.Dtos.Autores;
using WebApiAutores.Entities;

namespace WebApiAutores.Controllers
{
    [Route("api/autores")]
    [ApiController]
    public class AutoresController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public AutoresController(ApplicationDbContext context,
            IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<ResponseDto<IReadOnlyList<AutorDto>>>> Get()
        {
            var autoresDb = await _context.Autores.ToListAsync();
            var autoresDto = _mapper.Map<List<AutorDto>>(autoresDb);

            return new ResponseDto<IReadOnlyList<AutorDto>>
            {
                Status = true,
                Data = autoresDto
            };
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<ResponseDto<AutorGetByIdDto>>> GetOneById(int id)
        {
            var autorDb = await _context.Autores
                .Include(a => a.Books)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (autorDb is null)
            {
                return NotFound(new ResponseDto<AutorGetByIdDto>
                {
                    Status = false,
                    Message = $"El autor con el Id {id}, no fue encontrado"
                });
            }

            var autorDto = _mapper.Map<AutorGetByIdDto>(autorDb);

            return new ResponseDto<AutorGetByIdDto>
            {
                Status = true,
                Data = autorDto
            };
        }

        [HttpPost]
        public async Task<ActionResult<ResponseDto<AutorDto>>> Post(AutorCreateDto dto)
        {
            var autor = _mapper.Map<Autor>(dto);

            _context.Add(autor);
            await _context.SaveChangesAsync();

            var autorDto = _mapper.Map<AutorDto>(autor);

            return StatusCode(StatusCodes.Status201Created, new ResponseDto<AutorDto>
            {
                Status = true,
                Message = "Autor creado correctamente",
                Data = autorDto,
            });
        }

        [HttpPut("{id:int}")] // api/autores/2
        public async Task<ActionResult<ResponseDto<AutorDto>>> Put(int id, AutorUpdateDto dto)
        {
            var autorDb = await _context.Autores.FirstOrDefaultAsync(a => a.Id == id);

            if (autorDb is null)
            {
                return NotFound(new ResponseDto<AutorDto>
                {
                    Status = false,
                    Message = $"No existe el autor: {id}"
                });
            }

            _mapper.Map<AutorUpdateDto, Autor>(dto, autorDb);

            _context.Update(autorDb);
            await _context.SaveChangesAsync();

            var autorDto = _mapper.Map<AutorDto>(autorDb);

            return Ok(new ResponseDto<AutorDto>
            {
                Status = true,
                Message = "Autor editado correctamente",
                Data = autorDto,
            });
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<ResponseDto<AutorDto>>> Delete(int id)
        {
            var autor = await _context.Autores.FirstOrDefaultAsync(a => a.Id == id);
            if (autor is null)
            {
                return NotFound(new ResponseDto<AutorDto>
                {
                    Status = false,
                    Message = "Autor no encontrado",
                });
            }

            _context.Remove(autor);
            await _context.SaveChangesAsync();

            return Ok(new ResponseDto<AutorDto>
            {
                Status = true,
                Message = $"Autor {id} borrado correctamente",
            });
        }
    }
}
