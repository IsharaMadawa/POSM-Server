using System.Linq;
using POSM.Core.Business.Operations.Interfaces;
using POSM.Core.Bussines.Model.Item;
using POSM.Core.Data.Db.Models;

namespace POSM.Core.Business.Operations.Items
{
	public class ItemOperator : OperatorBase, IItemOperator
	{
		public ItemOperator(POSMDbContext context) : base(context)
		{
		}

		public IQueryable<Item> GetItems()
		{
			return dbContext.Items;
		}

		public int AddItem(ItemModel item)
		{
			Item newitem = new Item
			{
				ItemCode = item.ItemCode,
				ItemName = item.ItemName,
				UnitPrice = item.UnitPrice
			};

			dbContext.Items.Add(newitem);
			dbContext.SaveChanges();

			return newitem.Id;
		}
	}
}
