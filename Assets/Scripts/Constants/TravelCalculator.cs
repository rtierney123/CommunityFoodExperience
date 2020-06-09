using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TravelCalculator
{

    private static Dictionary<Tuple<LocationID, LocationID>, double> locationmap;
    private static double walkScale = 6;
    private static double carScale = 1;
    //TODO:FIX THIS!!

    public TravelCalculator()
    {
        locationmap = generateMapEdges();
    }

    private Dictionary<Tuple<LocationID, LocationID>, double> generateMapEdges()
    {
        Dictionary<Tuple<LocationID, LocationID>, double > distmap = new Dictionary<Tuple<LocationID, LocationID>, double>();
        distmap.Add(new Tuple<LocationID, LocationID>(LocationID.House, LocationID.CommunityKitchen), 2);
        distmap.Add(new Tuple<LocationID, LocationID>(LocationID.House, LocationID.CornerStore), 3);
        distmap.Add(new Tuple<LocationID, LocationID>(LocationID.House, LocationID.LocalNeighborhoodBusStop), 3);
        distmap.Add(new Tuple<LocationID, LocationID> (LocationID.House, LocationID.FoodTiger), 25);
        distmap.Add(new Tuple<LocationID, LocationID>(LocationID.House, LocationID.FoodTigerBusStop), 26);
        distmap.Add(new Tuple<LocationID, LocationID>(LocationID.House, LocationID.VitaServices), 20);
        distmap.Add(new Tuple<LocationID, LocationID> (LocationID.House, LocationID.SnapOffice), 20);
        distmap.Add(new Tuple<LocationID, LocationID> (LocationID.House, LocationID.SnapVitaBusStop), 20);
        distmap.Add(new Tuple<LocationID, LocationID> (LocationID.House, LocationID.FoodPantry), 30);
        distmap.Add(new Tuple<LocationID, LocationID> (LocationID.House, LocationID.WicClinic), 31);
        distmap.Add(new Tuple<LocationID, LocationID> (LocationID.House, LocationID.PantryWicBusStop), 31);

        distmap.Add(new Tuple<LocationID, LocationID> (LocationID.CommunityKitchen, LocationID.CornerStore), 1);
        distmap.Add(new Tuple<LocationID, LocationID> (LocationID.CommunityKitchen, LocationID.LocalNeighborhoodBusStop), 1);
        distmap.Add(new Tuple<LocationID, LocationID> (LocationID.CommunityKitchen, LocationID.FoodTiger), 23);
        distmap.Add(new Tuple<LocationID, LocationID> (LocationID.CommunityKitchen, LocationID.FoodTigerBusStop), 24);
        distmap.Add(new Tuple<LocationID, LocationID> (LocationID.CommunityKitchen, LocationID.VitaServices), 18);
        distmap.Add(new Tuple<LocationID, LocationID> (LocationID.CommunityKitchen, LocationID.SnapOffice), 18);
        distmap.Add(new Tuple<LocationID, LocationID> (LocationID.CommunityKitchen, LocationID.SnapVitaBusStop), 18);
        distmap.Add(new Tuple<LocationID, LocationID> (LocationID.CommunityKitchen, LocationID.FoodPantry), 28);
        distmap.Add(new Tuple<LocationID, LocationID> (LocationID.CommunityKitchen, LocationID.WicClinic), 29);
        distmap.Add(new Tuple<LocationID, LocationID> (LocationID.CommunityKitchen, LocationID.PantryWicBusStop), 29);

        distmap.Add(new Tuple<LocationID, LocationID> (LocationID.CornerStore, LocationID.LocalNeighborhoodBusStop), 1);
        distmap.Add(new Tuple<LocationID, LocationID> ( LocationID.CornerStore, LocationID.FoodTiger), 22);
        distmap.Add(new Tuple<LocationID, LocationID> (LocationID.CornerStore, LocationID.FoodTigerBusStop), 23);
        distmap.Add(new Tuple<LocationID, LocationID> (LocationID.CornerStore, LocationID.VitaServices), 17);
        distmap.Add(new Tuple<LocationID, LocationID> (LocationID.CornerStore, LocationID.SnapOffice), 17);
        distmap.Add(new Tuple<LocationID, LocationID> ( LocationID.CornerStore, LocationID.SnapVitaBusStop), 17);
        distmap.Add(new Tuple<LocationID, LocationID> (LocationID.CornerStore, LocationID.FoodPantry), 27);
        distmap.Add(new Tuple<LocationID, LocationID> (LocationID.CornerStore, LocationID.WicClinic), 28);
        distmap.Add(new Tuple<LocationID, LocationID> (LocationID.CornerStore, LocationID.PantryWicBusStop), 28);

        distmap.Add(new Tuple<LocationID, LocationID> (LocationID.LocalNeighborhoodBusStop, LocationID.FoodTiger), 10);
        distmap.Add(new Tuple<LocationID, LocationID> (LocationID.LocalNeighborhoodBusStop, LocationID.FoodTigerBusStop), 11);
        distmap.Add(new Tuple<LocationID, LocationID> ( LocationID.LocalNeighborhoodBusStop, LocationID.VitaServices ), 9);
        distmap.Add(new Tuple<LocationID, LocationID> ( LocationID.LocalNeighborhoodBusStop, LocationID.SnapOffice ), 8);
        distmap.Add(new Tuple<LocationID, LocationID> ( LocationID.LocalNeighborhoodBusStop, LocationID.SnapVitaBusStop ), 10);
        distmap.Add(new Tuple<LocationID, LocationID> (LocationID.LocalNeighborhoodBusStop, LocationID.FoodPantry), 10);
        distmap.Add(new Tuple<LocationID, LocationID> ( LocationID.LocalNeighborhoodBusStop, LocationID.WicClinic ), 11);
        distmap.Add(new Tuple<LocationID, LocationID> ( LocationID.LocalNeighborhoodBusStop, LocationID.PantryWicBusStop ), 11);

        distmap.Add(new Tuple<LocationID, LocationID> (LocationID.FoodTiger, LocationID.FoodTigerBusStop ), 1);
        distmap.Add(new Tuple<LocationID, LocationID> (LocationID.FoodTiger, LocationID.VitaServices), 2);
        distmap.Add(new Tuple<LocationID, LocationID> (LocationID.FoodTiger, LocationID.SnapOffice), 3);
        distmap.Add(new Tuple<LocationID, LocationID> (LocationID.FoodTiger, LocationID.SnapVitaBusStop ), 3);
        distmap.Add(new Tuple<LocationID, LocationID> (LocationID.FoodTiger, LocationID.FoodPantry ), 3);
        distmap.Add(new Tuple<LocationID, LocationID> (LocationID.FoodTiger, LocationID.WicClinic ), 4);
        distmap.Add(new Tuple<LocationID, LocationID> ( LocationID.FoodTiger, LocationID.PantryWicBusStop), 4);

        distmap.Add(new Tuple<LocationID, LocationID> (LocationID.FoodTigerBusStop, LocationID.VitaServices ), 3);
        distmap.Add(new Tuple<LocationID, LocationID> (LocationID.FoodTigerBusStop, LocationID.SnapOffice ), 4);
        distmap.Add(new Tuple<LocationID, LocationID> (LocationID.FoodTigerBusStop, LocationID.SnapVitaBusStop ), 4);
        distmap.Add(new Tuple<LocationID, LocationID> (LocationID.FoodTigerBusStop, LocationID.FoodPantry ), 4);
        distmap.Add(new Tuple<LocationID, LocationID> (LocationID.FoodTigerBusStop, LocationID.WicClinic ), 5);
        distmap.Add(new Tuple<LocationID, LocationID> (LocationID.FoodTigerBusStop, LocationID.PantryWicBusStop ), 5);

        distmap.Add(new Tuple<LocationID, LocationID> (LocationID.VitaServices, LocationID.SnapOffice ), 2);
        distmap.Add(new Tuple<LocationID, LocationID> (LocationID.VitaServices, LocationID.SnapVitaBusStop ), 1);
        distmap.Add(new Tuple<LocationID, LocationID> (LocationID.VitaServices, LocationID.FoodPantry ), 4);
        distmap.Add(new Tuple<LocationID, LocationID> (LocationID.VitaServices, LocationID.WicClinic ), 5);
        distmap.Add(new Tuple<LocationID, LocationID> (LocationID.VitaServices, LocationID.PantryWicBusStop), 5);

        distmap.Add(new Tuple<LocationID, LocationID> (LocationID.SnapOffice, LocationID.SnapVitaBusStop), 2);
        distmap.Add(new Tuple<LocationID, LocationID> ( LocationID.SnapOffice, LocationID.FoodPantry ), 5);
        distmap.Add(new Tuple<LocationID, LocationID> (LocationID.SnapOffice, LocationID.WicClinic), 6);
        distmap.Add(new Tuple<LocationID, LocationID> ( LocationID.SnapOffice, LocationID.PantryWicBusStop ), 6);

        distmap.Add(new Tuple<LocationID, LocationID> (LocationID.SnapVitaBusStop, LocationID.FoodPantry), 5);
        distmap.Add(new Tuple<LocationID, LocationID> (LocationID.SnapVitaBusStop, LocationID.WicClinic), 6);
        distmap.Add(new Tuple<LocationID, LocationID> (LocationID.SnapVitaBusStop, LocationID.PantryWicBusStop), 6);

        distmap.Add(new Tuple<LocationID, LocationID> (LocationID.FoodPantry, LocationID.WicClinic ), 2);
        distmap.Add(new Tuple<LocationID, LocationID> (LocationID.FoodPantry, LocationID.PantryWicBusStop ), 2);

        distmap.Add(new Tuple<LocationID, LocationID> ( LocationID.WicClinic, LocationID.PantryWicBusStop ), 1);

        return distmap;
    }

    public double calculateTravelTime(LocationID startLocation, LocationID endLocation, TravelType travelType)
    {
        double travelTime = 0;

        if (locationmap.ContainsKey(new Tuple<LocationID, LocationID>(startLocation, endLocation)))
        {
            travelTime = locationmap[new Tuple<LocationID, LocationID>(startLocation, endLocation)];

        } else if (locationmap.ContainsKey(new Tuple<LocationID, LocationID>(endLocation, startLocation)))
        {
            travelTime = locationmap[new Tuple<LocationID, LocationID>(endLocation, startLocation)];
        }

        if (travelType == TravelType.Walk)
        {
            return travelTime * walkScale;
        }
        else if (travelType == TravelType.Car)
        {
            return travelTime * carScale;
        }

        return travelTime;

    }
}
