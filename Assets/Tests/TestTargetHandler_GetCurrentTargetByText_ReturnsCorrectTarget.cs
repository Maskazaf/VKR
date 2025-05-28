using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using System.Reflection;

[TestFixture]
public class TestTargetHandler_GetCurrentTargetByText_ReturnsCorrectTarget
{
    [Test]
    public void GetCurrentTargetByTargetText_ReturnsCorrectTarget()
    {
        // Arrange
        var handlerGO = new GameObject("Handler");
        var handler = handlerGO.AddComponent<TargetHandler>();

        var facade = new GameObject("Target").AddComponent<TargetFacade>();
        facade.Name = "LabRoom";
        facade.PlaceNumber = 0;

        // Устанавливаем private поле currentTargetItems
        typeof(TargetHandler).GetField("currentTargetItems", BindingFlags.NonPublic | BindingFlags.Instance)
            .SetValue(handler, new List<TargetFacade> { facade });

        // Act
        var result = handler.GetCurrentTargetByTargetText("labroom");

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual("LabRoom", result.Name);
    }
}
