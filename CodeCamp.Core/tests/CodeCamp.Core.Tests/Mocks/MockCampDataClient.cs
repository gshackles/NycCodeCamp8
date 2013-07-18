using System;
using System.Threading.Tasks;
using CodeCamp.Core.Data.Entities;
using CodeCamp.Core.Network;

namespace CodeCamp.Core.Tests.Mocks
{
    public class MockCampDataClient : ICampDataClient
    {
        public Func<Task<CampData>> GetDataBody { get; set; }

        public Task<CampData> GetData()
        {
            return GetDataBody();
        }
    }
}
