using UnityEngine;
using System.Collections;

[SerializeField]
public interface IClockEventCaller
{
    void hourPassed();
    void minutePassed();
    void hourBeforeEndGame();
}
