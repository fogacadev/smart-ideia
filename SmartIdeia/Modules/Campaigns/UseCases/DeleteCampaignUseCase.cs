using Microsoft.EntityFrameworkCore;
using SmartIdeia.Database;
using SmartIdeia.Modules.Campaigns.Entities;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SmartIdeia.Modules.Campaigns.UseCases
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
                throw new Exception("Campaign not exists!");
            }

            context.Campaigns.Remove(campaign);
            await context.SaveChangesAsync();

            return campaign;
        }
    }
}
