using Microsoft.AspNetCore.Mvc;
using SmartIdeia.Database;
using SmartIdeia.Modules.Campaigns.Entities;
using SmartIdeia.Modules.Campaigns.UseCases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartIdeia.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CampaignsController : ControllerBase
    {
        private readonly DatabaseContext context;

        public CampaignsController(DatabaseContext context)
        {
            this.context = context;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Campaign>> GetCampaign([FromRoute] long id)
        {
            var findCampaignUseCase = new FindCampaignUseCase(context);

            var campaign = await findCampaignUseCase.Execute(id);

            return Ok(campaign);
        }

        [HttpGet]
        public async Task<ActionResult<List<Campaign>>> GetCampaigns([FromQuery] string research)
        {
            var listCampaignUseCase = new ListCampaignUseCase(context);

            var campaigns = await listCampaignUseCase.Execute(research);

            return Ok(campaigns);
        }

        [HttpPost]
        public async Task<ActionResult<Campaign>> PostCampaign([FromBody] Campaign campaign)
        {
            var createCampaignUseCase = new CreateCampaignUseCase(context);

            var createdCampaign = await createCampaignUseCase.Execute(campaign);

            return CreatedAtAction("GetCampaign", new { Id = createdCampaign.Id }, createdCampaign);
        }

        [HttpPut]
        public async Task<ActionResult> PutCampaign([FromBody] Campaign campaign)
        {
            var updateCampaignUseCase = new UpdateCampaignUseCase(context);

            await updateCampaignUseCase.Execute(campaign);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Campaign>> DeleteCampaign([FromRoute] long id)
        {
            var deleteCampaignUseCase = new DeleteCampaignUseCase(context);

            var campaign = await deleteCampaignUseCase.Execute(id);

            return campaign;
        }
    }
}
