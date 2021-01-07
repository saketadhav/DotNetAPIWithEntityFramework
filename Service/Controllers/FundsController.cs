using Application.Funds.Queries.GetFunds;
using Application.Funds.Queries.GetFundsByClientId;
using Application.Funds.Queries.ExportFunds;
using Application.Funds.Commands.CreateFund;
using Application.Funds.Commands.UpdateFund;
using Application.Funds.Commands.DeleteFund;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.Controllers
{
    [ApiController]
    public class FundsController : ApiControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<List<FundDto>>> GetFunds()
        {
            return await Mediator.Send(new GetFundsQuery());
        }

        [HttpGet]
        [Route("GetFundsByClientId")]
        public async Task<ActionResult<List<FundDto>>> GetFundsByClientId([FromQuery]int clientId)
        {
            GetFundsByClientIdQuery query = new GetFundsByClientIdQuery();
            query.ClientId = clientId;
            return await Mediator.Send(query);
        }

        [HttpGet("csv")]
        public async Task<FileResult> Export()
        {
            var vm = await Mediator.Send(new ExportFundsQuery());
            return File(vm.Content, vm.ContextType, vm.FileName);
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
