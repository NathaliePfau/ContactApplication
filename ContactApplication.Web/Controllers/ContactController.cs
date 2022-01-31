using ContactApplication.Application.Models.Contacts;
using ContactApplication.Application.Services.Contacts;
using ContactApplication.Domain.AppFlowControl;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ContactApplication.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContactController : ControllerBase
    {
        private const string COMMUNICATION_ERROR = "Erro na comunicação";
        private readonly IContactService _contactService;

        public ContactController(IContactService contactService)
        {
            _contactService = contactService;
        }

        [HttpGet]
        public async Task<IEnumerable<ContactResponseModel>> Get()
        {
            try
            {
                return await _contactService.GetAll();
            }
            catch (Exception ex)
            {
                throw new ServicesException(COMMUNICATION_ERROR, ex);
            }
        }


        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<ContactResponseModel>> Get([FromRoute] int id)
        {
            try
            {
                return await _contactService.Get(id);
            }
            catch (Exception ex)
            {
                throw new ServicesException(COMMUNICATION_ERROR, ex);
            }
        }


        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ContactRequestModel model)
        {
            try
            {
                var contact = await _contactService.Create(model);
                return CreatedAtRoute("", contact);
            }
            catch (Exception ex)
            {
                throw new ServicesException(COMMUNICATION_ERROR, ex);
            }
        }


        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Put([FromRoute] int id, [FromBody] ContactRequestModel model)
        {
            try
            {
                await _contactService.Update(id, model);
                return Ok("Contato Atualizado");
            }
            catch (Exception ex)
            {
                throw new ServicesException(COMMUNICATION_ERROR, ex);
            }
        }


        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            try
            {
                await _contactService.Delete(id);
                return Ok("Contato Deletado");
            }
            catch (Exception ex)
            {
                throw new ServicesException(COMMUNICATION_ERROR, ex);
            }
        }
    }
}
