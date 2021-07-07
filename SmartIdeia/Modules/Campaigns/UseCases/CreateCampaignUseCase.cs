using Microsoft.EntityFrameworkCore;
using SmartIdeia.Database;
using SmartIdeia.Modules.Campaigns.Entities;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SmartIdeia.Modules.Campaigns.UseCases
{
    public class CreateCampaignUseCase
    {
        private readonly DatabaseContext context;
             
        public CreateCampaignUseCase(DatabaseContext context)
        {
            this.context = context;
        }

        public async Task<Campaign> Execute(Campaign campaign)
        {

            var campaignAlreadyExists = await context
                .Campaigns
                .Where(c => c.Title == campaign.Title)
                .FirstOrDefaultAsync();

            if(campaignAlreadyExists != null)
            {
                throw new Exception("Campaign already exists.");
            }

            campaign.CreatedAt = DateTime.Now;

            context.Campaigns.Add(campaign);
            await context.SaveChangesAsync();

            return campaign;
        }
    }
}
