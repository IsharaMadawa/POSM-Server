using System.Linq;
using POSM.Core.Bussines.Model.Item;
using POSM.Core.Data.Db.Models;

namespace POSM.Core.Business.Operations.Interfaces
{
	public interface IItemOperator
	{
		public IQueryable<Item> GetItems();

		public int AddItem(ItemModel item);
	}
}
