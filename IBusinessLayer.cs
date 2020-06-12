using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CariBilgi
{
	interface IBusinessLayer<TEntity, TKey> where TEntity:class
	{
		/// <summary>
		/// CRUD işlemleri
		/// </summary>
		/// <returns></returns>
		List<TEntity> GetAll();
		bool Save(TEntity item);
		bool Update(TEntity item);
		bool Delete(TEntity item);

	}
}
