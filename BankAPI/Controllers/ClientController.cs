using Microsoft.AspNetCore.Mvc;
using BankAPI.Services;
using BankAPI.Data.BankModels;

namespace BankAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class ClientController : ControllerBase
{
    private readonly ClientService _services;

    public ClientController(ClientService client)
    {
        _services = client;
    }

    [HttpGet]
    public async Task<IEnumerable<Client>> Get()
    {
        return await _services.GetAll();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Client>> GetById(int id)
    {
        var client = await _services.GetById(id);

        if(client is null){
            return ClientNotFound(id);
        }else{
            return client;
        }
    }

    [HttpPost]
    public async Task<IActionResult> Create(Client client)
    {
        var newClient = await _services.Create(client);

        return CreatedAtAction(nameof(GetById), new {id = newClient.Id}, newClient);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, Client client)
    {
        if(id != client.Id){
            return BadRequest(new {message = $"El ID({id}) de la URL no coincide con el ID({client.Id}) del cuerpo de la solicitud."});
        }

        var clientToUpdate = await _services.GetById(id);

        if (clientToUpdate is not null)
        {
            await _services.Update(id, client);
            return NoContent();
        }else
        {
            return ClientNotFound(id);
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var clientToDelete = await _services.GetById(id);

        if (clientToDelete is not null)
        {
            await _services.Delete(id);
            return Ok();
        }else
        {
            return ClientNotFound(id);
        }
    }

    public NotFoundObjectResult ClientNotFound (int id)
    {
        return NotFound(new {message = $"El cliente con ID = {id} no existe."});
    }
}