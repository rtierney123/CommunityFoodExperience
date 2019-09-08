using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clock : MonoBehaviour
{
    private const float GAME_TIME_LIMIT = 18f;

    public Transform hourHandTransform;
    public float day;

    public void Awake() {
        hourHandTransform = transform.Find("HourHand");
    }

    public void Update() {
        if (day > 5f/6f) {
            return;
        }
        day += Time.deltaTime / GAME_TIME_LIMIT;
        float dayNormalized = day % 1f;
        float rotationDegrees = 360f;
        hourHandTransform.eulerAngles = new Vector3(0, 0, -dayNormalized * rotationDegrees);
    }
}
