using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MercadinhoApi.Data;
using MercadinhoApi.Models;
using MercadinhoApi.DTOs;

namespace MercadinhoApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ItensMercadoController : ControllerBase
    {
        private readonly MarketDbContext _context;

        public ItensMercadoController(MarketDbContext context)
        {
            _context = context;
        }

        // GET: api/itensmercado
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ItemMercadoDto>>> GetItensMercado()
        {
            var itens = await _context.ItensMercado
                .Select(item => new ItemMercadoDto
                {
                    Id = item.Id,
                    Nome = item.Nome,
                    Categoria = item.Categoria,
                    Preco = item.Preco,
                    Quantidade = item.Quantidade,
                    DataCriacao = item.DataCriacao,
                    DataAtualizacao = item.DataAtualizacao
                })
                .ToListAsync();

            return Ok(itens);
        }

        // GET: api/itensmercado/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ItemMercadoDto>> GetItemMercado(int id)
        {
            var item = await _context.ItensMercado.FindAsync(id);

            if (item == null)
            {
                return NotFound();
            }

            var itemDto = new ItemMercadoDto
            {
                Id = item.Id,
                Nome = item.Nome,
                Categoria = item.Categoria,
                Preco = item.Preco,
                Quantidade = item.Quantidade,
                DataCriacao = item.DataCriacao,
                DataAtualizacao = item.DataAtualizacao
            };

            return itemDto;
        }

        // POST: api/itensmercado
        [HttpPost]
        public async Task<ActionResult<ItemMercadoDto>> PostItemMercado(CriarItemMercadoDto criarItemDto)
        {
            var item = new ItemMercado
            {
                Nome = criarItemDto.Nome,
                Categoria = criarItemDto.Categoria,
                Preco = criarItemDto.Preco,
                Quantidade = criarItemDto.Quantidade
            };

            _context.ItensMercado.Add(item);
            await _context.SaveChangesAsync();

            var itemDto = new ItemMercadoDto
            {
                Id = item.Id,
                Nome = item.Nome,
                Categoria = item.Categoria,
                Preco = item.Preco,
                Quantidade = item.Quantidade,
                DataCriacao = item.DataCriacao,
                DataAtualizacao = item.DataAtualizacao
            };

            return CreatedAtAction(nameof(GetItemMercado), new { id = item.Id }, itemDto);
        }

        // PUT: api/itensmercado/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutItemMercado(int id, CriarItemMercadoDto atualizarItemDto)
        {
            var item = await _context.ItensMercado.FindAsync(id);
            if (item == null)
            {
                return NotFound();
            }

            item.Nome = atualizarItemDto.Nome;
            item.Categoria = atualizarItemDto.Categoria;
            item.Preco = atualizarItemDto.Preco;
            item.Quantidade = atualizarItemDto.Quantidade;
            item.DataAtualizacao = DateTime.UtcNow;

            _context.Entry(item).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ItemMercadoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/itensmercado/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItemMercado(int id)
        {
            var item = await _context.ItensMercado.FindAsync(id);
            if (item == null)
            {
                return NotFound();
            }

            _context.ItensMercado.Remove(item);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ItemMercadoExists(int id)
        {
            return _context.ItensMercado.Any(e => e.Id == id);
        }
    }
}