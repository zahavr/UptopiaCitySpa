using Core.Entities;
using System;

namespace Core.Specification.BuildingSpecification
{
	public class RandomAppartamentsSpecification : BaseSpecification<Appartament>
	{
		private const int TAKE = 3;

		public RandomAppartamentsSpecification(int maxAppartament)
			: base(ap => ap.UserAppartaments.Count == 0)
		{
			if (TAKE < maxAppartament)
			{
				TakeRandom(TAKE, CreateRandom(maxAppartament));
			}
			AddOrderBy(ap => ap.Cost);
			AddInclude(ap => ap.UserAppartaments);
		}

		private int CreateRandom(int maxAppartament)
		{
			int randomNumber = new Random().Next(1, maxAppartament - TAKE);

			return randomNumber;
		}
	}
}
