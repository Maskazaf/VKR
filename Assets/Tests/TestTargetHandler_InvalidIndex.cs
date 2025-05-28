using UnityEngine;
using NUnit.Framework;
using System.Reflection;

public class TestTargetHandler_InvalidIndex
{
    [Test]
    public void SetSelectedTargetPositionWithDropdown_InvalidIndex_SetsZeroVector()
    {
        // Arrange
        var navigationGO = new GameObject("Navigation");
        var navigation = navigationGO.AddComponent<NavigationController>();

        var handlerGO = new GameObject("Handler");
        var handler = handlerGO.AddComponent<TargetHandler>();

        // Только один элемент в списке
        var target = new GameObject("Target").AddComponent<TargetFacade>();
        target.transform.position = new Vector3(10, 0, 0);
        target.Name = "Test";
        target.PlaceNumber = 0;

        var currentTargetItems = new System.Collections.Generic.List<TargetFacade> { target };

        // Подставляем зависимости через reflection
        typeof(TargetHandler).GetField("navigationController", BindingFlags.NonPublic | BindingFlags.Instance)
            .SetValue(handler, navigation);
        typeof(TargetHandler).GetField("currentTargetItems", BindingFlags.NonPublic | BindingFlags.Instance)
            .SetValue(handler, currentTargetItems);

        // Act — передаём несуществующий индекс
        handler.SetSelectedTargetPositionWithDropdown(999);

        // Assert — позиция должна сброситься в Vector3.zero
        Assert.AreEqual(Vector3.zero, navigation.TargetPosition);
    }
}
