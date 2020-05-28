using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TravelCalculator
{

    private static Dictionary<Tuple<LocationID, LocationID>, double> locationmap;
    private static double walkScale = 10;
    private static double carScale = 5;
    //TODO:FIX THIS!!

    public TravelCalculator()
    {
        locationmap = generateMapEdges();
    }

    private Dictionary<Tuple<LocationID>, double> generateMapEdges()
    {
        Dictionary<Tuple<LocationID, LocationID>, double > distmap = new Dictionary<Tuple<LocationID>, double>();
        distmap.Add(new Tuple<LocationID, LocationID>(LocationID.House, LocationID.CommunityKitchen), 2);
        distmap.Add(new Tuple<LocationID, LocationID>(LocationID.House, LocationID.CornerStore), 3);
        distmap.Add(new Tuple<LocationID, LocationID>(LocationID.House, LocationID.LocalNeighborhoodBusStop), 3);
        distmap.Add(new Tuple<LocationID, LocationID> (LocationID.House, LocationID.FoodTiger), 7);
        distmap.Add(new Tuple<LocationID, LocationID>(LocationID.House, LocationID.FoodTigerBusStop), 8);
        distmap.Add(new HashSet<LocationID> { LocationID.House, LocationID.VitaServices}, 9);
        distmap.Add(new HashSet<LocationID> { LocationID.House, LocationID.SnapOffice }, 8);
        distmap.Add(new HashSet<LocationID> { LocationID.House, LocationID.SnapVitaBusStop }, 10);
        distmap.Add(new HashSet<LocationID> { LocationID.House, LocationID.FoodPantry}, 10);
        distmap.Add(new HashSet<LocationID> { LocationID.House, LocationID.WicClinic }, 9);
        distmap.Add(new HashSet<LocationID> { LocationID.House, LocationID.PantryWicBusStop}, 9);

        distmap.Add(new HashSet<LocationID> { LocationID.CommunityKitchen, LocationID.CornerStore}, 3);
        distmap.Add(new HashSet<LocationID> { LocationID.CommunityKitchen, LocationID.LocalNeighborhoodBusStop}, 3);
        distmap.Add(new HashSet<LocationID> { LocationID.CommunityKitchen, LocationID.FoodTiger}, 6);
        distmap.Add(new HashSet<LocationID> { LocationID.CommunityKitchen, LocationID.FoodTigerBusStop}, 7);
        distmap.Add(new HashSet<LocationID> { LocationID.CommunityKitchen, LocationID.VitaServices}, 8);
        distmap.Add(new HashSet<LocationID> { LocationID.CommunityKitchen, LocationID.SnapOffice }, 7);
        distmap.Add(new HashSet<LocationID> { LocationID.CommunityKitchen, LocationID.SnapVitaBusStop}, 8);
        distmap.Add(new HashSet<LocationID> { LocationID.CommunityKitchen, LocationID.FoodPantry }, 9);
        distmap.Add(new HashSet<LocationID> { LocationID.CommunityKitchen, LocationID.WicClinic }, 8);
        distmap.Add(new HashSet<LocationID> { LocationID.CommunityKitchen, LocationID.PantryWicBusStop }, 8);

        distmap.Add(new HashSet<LocationID> { LocationID.CornerStore, LocationID.LocalNeighborhoodBusStop }, 3);
        distmap.Add(new HashSet<LocationID> { LocationID.CornerStore, LocationID.FoodTiger}, 12);
        distmap.Add(new HashSet<LocationID> { LocationID.CornerStore, LocationID.FoodTigerBusStop}, 13);
        distmap.Add(new HashSet<LocationID> { LocationID.CornerStore, LocationID.VitaServices}, 10);
        distmap.Add(new HashSet<LocationID> { LocationID.CornerStore, LocationID.SnapOffice}, 9);
        distmap.Add(new HashSet<LocationID> { LocationID.CornerStore, LocationID.SnapVitaBusStop}, 11);
        distmap.Add(new HashSet<LocationID> { LocationID.CornerStore, LocationID.FoodPantry }, 12);
        distmap.Add(new HashSet<LocationID> { LocationID.CornerStore, LocationID.WicClinic }, 13);
        distmap.Add(new HashSet<LocationID> { LocationID.CornerStore, LocationID.PantryWicBusStop }, 13);

        distmap.Add(new HashSet<LocationID> { LocationID.LocalNeighborhoodBusStop, LocationID.FoodTiger }, 10);
        distmap.Add(new HashSet<LocationID> { LocationID.LocalNeighborhoodBusStop, LocationID.FoodTigerBusStop }, 11);
        distmap.Add(new HashSet<LocationID> { LocationID.LocalNeighborhoodBusStop, LocationID.VitaServices }, 9);
        distmap.Add(new HashSet<LocationID> { LocationID.LocalNeighborhoodBusStop, LocationID.SnapOffice }, 8);
        distmap.Add(new HashSet<LocationID> { LocationID.LocalNeighborhoodBusStop, LocationID.SnapVitaBusStop }, 10);
        distmap.Add(new HashSet<LocationID> { LocationID.LocalNeighborhoodBusStop, LocationID.FoodPantry}, 10);
        distmap.Add(new HashSet<LocationID> { LocationID.LocalNeighborhoodBusStop, LocationID.WicClinic }, 11);
        distmap.Add(new HashSet<LocationID> { LocationID.LocalNeighborhoodBusStop, LocationID.PantryWicBusStop }, 11);

        distmap.Add(new HashSet<LocationID> { LocationID.FoodTiger, LocationID.FoodTigerBusStop }, 1);
        distmap.Add(new HashSet<LocationID> { LocationID.FoodTiger, LocationID.VitaServices}, 2);
        distmap.Add(new HashSet<LocationID> { LocationID.FoodTiger, LocationID.SnapOffice}, 3);
        distmap.Add(new HashSet<LocationID> { LocationID.FoodTiger, LocationID.SnapVitaBusStop }, 3);
        distmap.Add(new HashSet<LocationID> { LocationID.FoodTiger, LocationID.FoodPantry }, 3);
        distmap.Add(new HashSet<LocationID> { LocationID.FoodTiger, LocationID.WicClinic }, 4);
        distmap.Add(new HashSet<LocationID> { LocationID.FoodTiger, LocationID.PantryWicBusStop}, 4);

        distmap.Add(new HashSet<LocationID> { LocationID.FoodTigerBusStop, LocationID.VitaServices }, 3);
        distmap.Add(new HashSet<LocationID> { LocationID.FoodTigerBusStop, LocationID.SnapOffice }, 4);
        distmap.Add(new HashSet<LocationID> { LocationID.FoodTigerBusStop, LocationID.SnapVitaBusStop }, 4);
        distmap.Add(new HashSet<LocationID> { LocationID.FoodTigerBusStop, LocationID.FoodPantry }, 4);
        distmap.Add(new HashSet<LocationID> { LocationID.FoodTigerBusStop, LocationID.WicClinic }, 5);
        distmap.Add(new HashSet<LocationID> { LocationID.FoodTigerBusStop, LocationID.PantryWicBusStop }, 5);

        distmap.Add(new HashSet<LocationID> { LocationID.VitaServices, LocationID.SnapOffice }, 2);
        distmap.Add(new HashSet<LocationID> { LocationID.VitaServices, LocationID.SnapVitaBusStop }, 1);
        distmap.Add(new HashSet<LocationID> { LocationID.VitaServices, LocationID.FoodPantry }, 4);
        distmap.Add(new HashSet<LocationID> { LocationID.VitaServices, LocationID.WicClinic }, 5);
        distmap.Add(new HashSet<LocationID> { LocationID.VitaServices, LocationID.PantryWicBusStop }, 5);

        distmap.Add(new HashSet<LocationID> { LocationID.SnapOffice, LocationID.SnapVitaBusStop }, 2);
        distmap.Add(new HashSet<LocationID> { LocationID.SnapOffice, LocationID.FoodPantry }, 5);
        distmap.Add(new HashSet<LocationID> { LocationID.SnapOffice, LocationID.WicClinic }, 6);
        distmap.Add(new HashSet<LocationID> { LocationID.SnapOffice, LocationID.PantryWicBusStop }, 6);

        distmap.Add(new HashSet<LocationID> { LocationID.SnapVitaBusStop, LocationID.FoodPantry }, 5);
        distmap.Add(new HashSet<LocationID> { LocationID.SnapVitaBusStop, LocationID.WicClinic }, 6);
        distmap.Add(new HashSet<LocationID> { LocationID.SnapVitaBusStop, LocationID.PantryWicBusStop }, 6);

        distmap.Add(new HashSet<LocationID> { LocationID.FoodPantry, LocationID.WicClinic }, 2);
        distmap.Add(new HashSet<LocationID> { LocationID.FoodPantry, LocationID.PantryWicBusStop }, 2);

        distmap.Add(new HashSet<LocationID> { LocationID.WicClinic, LocationID.PantryWicBusStop }, 1);

        Debug.Log("generated map edges");
        return distmap;
    }

    public double calculateTravelTime(LocationID startLocation, LocationID endLocation, TravelType travelType)
    {
        HashSet<LocationID> route = new HashSet<LocationID> { startLocation, endLocation };

        if (locationmap.ContainsKey(route))
        {
            double travelTime = locationmap[route];
            if (travelType == TravelType.Walk)
            {
                return travelTime * walkScale;
            }
            else if (travelType == TravelType.Car)
            {
                return travelTime * carScale;
            }
        }
        
        return 0;

    }
}
