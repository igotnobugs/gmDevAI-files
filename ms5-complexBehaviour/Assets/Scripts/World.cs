using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class World 
{
    public static World Instance { get; } = new World();

    private static readonly GameObject[] hidingSpots;

    static World() {
        hidingSpots = GameObject.FindGameObjectsWithTag("Hide");
    }

    private World() { }
 
    public GameObject[] GetHidingSpots() {
        return hidingSpots;
    }

}
