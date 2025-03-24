
using Xunit;

namespace RotasTests.IntegrationTests;

[CollectionDefinition("IntegrationTests")]
public class IntegrationTestCollection : ICollectionFixture<TestDatabaseFixture>
{
    // This class has no code and is never instantiated.
    // Its purpose is to define the collection for the fixture.
}

