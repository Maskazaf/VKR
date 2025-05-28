using NUnit.Framework;
using TMPro;
using UnityEngine;
using System.Reflection;
using System.Collections.Generic;

[TestFixture]
public class TestTargetHandler_DropdownFilledCorrectly
{
    [Test]
    public void DropdownFilledCorrectly_AddsExpectedOptions()
    {
        // Arrange
        var handlerGO = new GameObject("Handler");
        var dropdownGO = new GameObject("Dropdown");
        var dropdown = dropdownGO.AddComponent<TMP_Dropdown>();
        var handler = handlerGO.AddComponent<TargetHandler>();

        var facade1 = new GameObject("Target1").AddComponent<TargetFacade>();
        facade1.Name = "RoomA";
        facade1.PlaceNumber = 0;

        var facade2 = new GameObject("Target2").AddComponent<TargetFacade>();
        facade2.Name = "RoomB";
        facade2.PlaceNumber = 1;

        typeof(TargetHandler).GetField("targetDataDropdown", BindingFlags.NonPublic | BindingFlags.Instance)
            .SetValue(handler, dropdown);
        typeof(TargetHandler).GetField("currentTargetItems", BindingFlags.NonPublic | BindingFlags.Instance)
            .SetValue(handler, new List<TargetFacade> { facade1, facade2 });

        // Act
        MethodInfo method = typeof(TargetHandler).GetMethod("FillDropdownWithTargetItems", BindingFlags.NonPublic | BindingFlags.Instance);
        method.Invoke(handler, null);

        // Assert
        Assert.AreEqual(2, dropdown.options.Count);
        Assert.AreEqual("0 - RoomA", dropdown.options[0].text);
        Assert.AreEqual("1 - RoomB", dropdown.options[1].text);
    }
}
