using NUnit.Framework;
using UnityEngine.TestTools;

[TestFixture]
public class NavigationTests
{
    private GameObject testObject;

    [SetUp]
    public void SetUp()
    {
        testObject = new GameObject();
    }

    [Test]
    public void TestObjectIsCreated()
    {
        Assert.IsNotNull(testObject);
    }

    [TearDown]
    public void TearDown()
    {
        GameObject.Destroy(testObject);
    }
}
