using Application.Funds.Queries.GetFunds;
using Application.Funds.Queries.GetFundById;
using Application.Funds.Commands.GetFundById;
using Application.Funds.Commands.CreateFund;
using Application.Funds.Commands.UpdateFund;
using Application.Funds.Commands.DeleteFund;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.Controllers
{
    public class FundsController : ApiControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<List<FundDto>>> GetFunds()
        {
            return await Mediator.Send(new GetFundsQuery());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<FundDto>> GetFundById(int id)
        {
            GetFundByIdQuery query = new GetFundByIdQuery();
            query.Id = id;
            return await Mediator.Send(query);
        }

        [HttpPost]
        public async Task<ActionResult<int>> Create(CreateFundCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpPut]
        public async Task<ActionResult> Update(UpdateFundCommand command)
        {
            await Mediator.Send(command);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await Mediator.Send(new DeleteFundCommand { Id = id });
            return NoContent();
        }
    }
}
