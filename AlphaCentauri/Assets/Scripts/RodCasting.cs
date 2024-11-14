using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;

public class RodCasting : MonoBehaviour
{
    [SerializeField] private Transform horizontalBar, verticalBar, fishableArea, target;
    float horizontalPercent = 0f; //right left percent
    float verticalPercent = 0f; //up down percent
    bool clicked = false, playHorizontal = false, playVertical = false;
    float amplitude;
    void CastRod()
    {
        Vector3 areaSize = fishableArea.GetComponent<Renderer>().bounds.size;
        Vector3 areaPos = fishableArea.position;
        Vector3 bobberPosition = new Vector3(areaSize.x * horizontalPercent + areaPos.x - areaSize.x / 2f, areaPos.y, areaSize.z * verticalPercent + areaPos.z - areaSize.z / 2f);
        target.position = bobberPosition;
    }
    void GetPowerLevel()
    {
        amplitude = horizontalBar.parent.GetComponent<RectTransform>().rect.width - horizontalBar.GetComponent<RectTransform>().rect.width;
        playHorizontal = true;
    }
    // Tween the bar of rightLeft or upDown
    void Start()
    {
        GetPowerLevel();
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            clicked = true;
        }
        if (playHorizontal && clicked == false)  // Play Horizontal
        {
            horizontalPercent = Mathf.PingPong(Time.time, 1f);
            horizontalBar.localPosition = new Vector3((horizontalPercent - 0.5f) * amplitude, 0f, 0f);
        }
        if (playVertical && clicked == false)// Play Vertical
        {
            verticalPercent = Mathf.PingPong(Time.time, 1f);
            verticalBar.localRotation = Quaternion.Euler(0f, 0f, -verticalPercent * 90f + 90f);
        }
        if (clicked) // Debounces
        {
            if (playHorizontal) //Play Horizontal after Vertical
            {
                Debug.Log(horizontalPercent);
                playHorizontal = false;
                clicked = false;
                playVertical = true;
            }
            else if (playVertical)
            {
                Debug.Log(verticalPercent);
                playVertical = false;
                clicked = false;
                CastRod();
            }
            else
            {
                clicked = false;
                GetPowerLevel();
            }
        }
    }
}
