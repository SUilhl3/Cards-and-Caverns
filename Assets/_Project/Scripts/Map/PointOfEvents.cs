using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class PointOfEvents : MonoBehaviour
{

    public List<PointOfEvents> NextPointsOfEvents { get; set; } = new();
}
