using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Vimo.ApplicationCore.Queries;
using Vimo.ApplicationCore.Queries.Models;
using Vimo.ApplicationCore.Services.UserServices;

namespace Vimo.Web.Api.Controllers
{ 
    public class UserController : BaseController
    { 
        private readonly IMediator _mediator;
        private readonly IUserQueries _userQueries;
        public UserController(IMediator mediator, IUserQueries userQueries)
        {
            _mediator = mediator;
            _userQueries = userQueries;
        }

        [HttpGet]
        public ActionResult<IEnumerable<UserModel>> Get()
        {
            return _userQueries.Get().ToArray();
        }

        [HttpGet("{id}")]
        public ActionResult<UserModel> Get(int id)
        {
            return _userQueries.Get(id);
        }

        [HttpPost]
        public async Task<IActionResult> Post(SaveUserCommand requestModel)
        {
            await _mediator.Send(requestModel);

            return Ok(requestModel.Id);
        }

        [HttpPut("{id}")]
        public async Task Put(int id, SaveUserCommand requestModel)
        {
            requestModel.Id = id;
            await _mediator.Send(requestModel);
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _mediator.Send(new DeleteUserCommand(id));
        }
    }
}