using Microsoft.AspNetCore.Mvc;
using SmartIdeia.Database;
using SmartIdeia.Src.Modules.ActionPlans.Entities;
using SmartIdeia.Src.Modules.ActionPlans.UseCases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartIdeia.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ActionPlansController : ControllerBase
    {
        private readonly DatabaseContext context;
        public ActionPlansController(DatabaseContext context)
        {
            this.context = context;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ActionPlan>> GetActionPlan([FromRoute] long id)
        {
            var findActionPlanUseCase = new FindActionPlanUseCase(context);

            var actionPlan = await findActionPlanUseCase.Execute(id);

            return Ok(actionPlan);
        }

        [HttpGet("{ideaId}")]
        public async Task<ActionResult<List<ActionPlan>>> GetActionPlans([FromRoute] long ideaId, [FromQuery] string search)
        {
            var listActionPlansUseCase = new ListActionPlanUseCase(context);

            var actionPlans = await listActionPlansUseCase.Execute(ideaId, search);

            return Ok(actionPlans);
        }

        [HttpPost()]
        public async Task<ActionResult<ActionPlan>> PostActionPlan([FromBody] ActionPlan actionPlan)
        {
            var createActionPlanUseCase = new CreateActionPlanUseCase(context);
            var createdActionPlan = await createActionPlanUseCase.Execute(actionPlan);

            return CreatedAtAction("GetActionPlan", new { Id = createdActionPlan.Id }, createdActionPlan);
        }

        [HttpPut()]
        public async Task<ActionResult> PutActionResult([FromBody] ActionPlan actionPlan)
        {
            var updateActionPlanUseCase = new UpdateActionPlanUseCase(context);

            await updateActionPlanUseCase.Execute(actionPlan);

            return NoContent();
        }

        [HttpPatch()]
        [Route("{id}/finalize")]
        public async Task<ActionResult<ActionPlan>> PatchActionPlan([FromRoute] long id)
        {
            var finalizeActionPlanUseCase = new FinalizeActionPlanUseCase(context);

            var actionPlan = await finalizeActionPlanUseCase.Execute(id);

            return Ok(actionPlan);
        }

        [HttpDelete("{}")]
        public async Task<ActionResult<ActionPlan>> DeleteActionPlan([FromRoute] long id) 
        {
            var deleteActionPlanUseCase = new DeleteActionPlanUseCase(context);

            var actionPlan = await deleteActionPlanUseCase.Execute(id);

            return Ok(actionPlan);
        }

    }
}
