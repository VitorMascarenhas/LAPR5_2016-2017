using PortoGO.DB.Domain;
using PortoGO.DB.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortoGO.DB.Tests
{
    //public class PointOfInterestRepositoryMock : BaseRepositoryMock<PointOfInterest, int>, IPointOfInterestRepository
    //{
    //    public PointOfInterestRepositoryMock()
    //    {
    //        Setup();
    //    }

    //    public override void Setup()
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public override PointOfInterest Get(int id)
    //    {
    //        var data = this.mockData.Where(x => x.Id == id).FirstOrDefault();

    //        return data;
    //    }

    //    public IEnumerable<PointOfInterest> GetUserPointsOfInterest(string userId, bool includeHashtags = false)
    //    {
    //        return this.Find(x => x.UserId == userId);
    //    }

    //    public override PointOfInterest Insert(PointOfInterest entity)
    //    {
    //        var data = this.mockData.ToList();

    //        entity.Id = data.Count + 1;

    //        data.Add(entity);

    //        this.mockData = data.AsQueryable();

    //        return entity;
    //    }

    //    public override void Update(PointOfInterest entity)
    //    {
    //        var oldEntity = this.Get(entity.Id);

    //        var data = this.mockData.ToList();

    //        data.Remove(oldEntity);

    //        data.Add(entity);

    //        this.mockData = data.AsQueryable();
    //    }
    //}
}
