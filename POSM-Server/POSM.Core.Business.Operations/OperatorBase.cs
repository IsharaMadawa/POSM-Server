using POSM.Core.Data.Db.Models;

namespace POSM.Core.Business.Operations
{
	public class OperatorBase
	{
		protected readonly POSMDbContext context;

        public OperatorBase(POSMDbContext context = null)
        {
            this.context = context;
        }

        public void SaveChanges()
        {
            if (context != null)
            {
                context.SaveChanges();
            }
        }
    }
}
