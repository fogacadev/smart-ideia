using Microsoft.EntityFrameworkCore;
using SmartIdeia.Database;
using SmartIdeia.Modules.Campaigns.Entities;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SmartIdeia.Modules.Campaigns.UseCases
{
    public class UpdateCampaignUseCase
    {
        private readonly DatabaseContext context;
        public UpdateCampaignUseCase(DatabaseContext context)
        {
            this.context = context;
        }

        public async Task Execute(Campaign campaign)
        {
            var campaignExists = await context
                .Campaigns
                .Where(c => c.Id == campaign.Id)
                .FirstOrDefaultAsync();

            if (campaignExists == null)
            {
                throw new Exception("Campaign not exists!");
            }

            context.Entry(campaignExists).State = EntityState.Detached;
            context.Entry(campaign).State = EntityState.Modified;

            campaign.CreatedAt = campaignExists.CreatedAt;
            campaign.CreatedByUserId = campaignExists.CreatedByUserId;
            campaign.CreatedByUser = null;

            await context.SaveChangesAsync();
        }
    }
}
