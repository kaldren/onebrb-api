using AutoFixture;

namespace Onebrb.API.Tests.Services
{
    public abstract class BaseServiceTests
    {
        protected Fixture fixture;

        public BaseServiceTests()
        {
            fixture = new Fixture();
        }
    }
}
