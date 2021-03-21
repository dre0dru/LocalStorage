using NUnit.Framework;

namespace LocalStorage.Tests
{
    [TestFixture]
    public abstract class TestsBase
    {
        [SetUp]
        public virtual void SetUp()
        {
            Helpers.DeleteFileIfExists();
        }

        [TearDown]
        public virtual void TearDown()
        {
            Helpers.DeleteFileIfExists();
        }
    }
}