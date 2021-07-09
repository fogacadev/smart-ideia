using Microsoft.EntityFrameworkCore;
using SmartIdeia.Database;
using SmartIdeia.Src.Errors;
using SmartIdeia.Src.Modules.Campaigns.Entities;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SmartIdeia.Src.Modules.Campaigns.UseCases
{
    public class DeleteCampaignUseCase
    {
        private readonly DatabaseContext context;
        public DeleteCampaignUseCase(DatabaseContext context)
        {
            this.context = context;
        }

        public async Task<Campaign> Execute(long id)
        {
            var campaign = await context
                .Campaigns
                .Where(c => c.Id == id)
                .FirstOrDefaultAsync();

            if (campaign == null)
            {
                throw new AppError("Campaign not exists!");
            }

            //Verify if campaign is in use

            var campaignInUse = await context
                .Ideas
                .Where(i => i.CampaignId == id)
                .AnyAsync();

            if (campaignInUse)
            {
                throw new AppError("The campaign cannot be deleted as it is in use.");
            }


            context.Campaigns.Remove(campaign);
            await context.SaveChangesAsync();

            return campaign;
        }
    }
}
