using UnityEngine;
using NUnit.Framework;
using System.Reflection;

public class TestTargetHandler_CreateTarget
{
    [Test]
    public void CreateTargetFacade_CreatesCorrectGameObject()
    {
        // Arrange
        var target = new Target
        {
            Name = "TestRoom",
            PlaceNumber = 1,
            Position = new Vector3(1, 2, 3),
            Rotation = new Vector3(0, 90, 0)
        };

        var parent = new GameObject("Parent").transform;
        var prefab = new GameObject("TargetPrefab");
        prefab.AddComponent<TargetFacade>();

        var handlerGO = new GameObject("Handler");
        var handler = handlerGO.AddComponent<TargetHandler>();

        // Установка сериализуемых полей через reflection
        typeof(TargetHandler).GetField("targetObjectPrefab", BindingFlags.NonPublic | BindingFlags.Instance)
            .SetValue(handler, prefab);
        typeof(TargetHandler).GetField("targetObjectsParentTransforms", BindingFlags.NonPublic | BindingFlags.Instance)
            .SetValue(handler, new[] { parent, parent });

        // Активация метода через reflection
        var method = typeof(TargetHandler).GetMethod("CreateTargetFacade", BindingFlags.NonPublic | BindingFlags.Instance);
        var result = method.Invoke(handler, new object[] { target }) as TargetFacade;

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual("TestRoom", result.Name);
        Assert.AreEqual(1, result.PlaceNumber);
        Assert.AreEqual(new Vector3(1, 2, 3), result.transform.localPosition);
    }
}
