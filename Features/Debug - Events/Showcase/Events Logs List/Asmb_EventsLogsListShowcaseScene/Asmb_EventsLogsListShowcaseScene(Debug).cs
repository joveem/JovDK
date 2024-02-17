// system / unity
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

// third
using TMPro;

// from company
using JovDK.Debugging;
using JovDK.SafeActions;
using JovDK.SerializingTools.Bson;
using JovDK.SerializingTools.Json;
using JovDK.Debugging.Events;

// from project
// ...


public partial class Asmb_EventsLogsListShowcaseScene : MonoBehaviour
{

    void HandleDebugInputs()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            DebugExtension.DevLog("[1]".ToColor(GoodColors.Blue) + " Adding random EventLog...");
            AddRandomEventLog();
        }
    }

    void AddRandomEventLog()
    {
        string title = "Title title title title title";
        string content = "Content content content content content content content content content";
        Color color = GetRandomColor();

        _eventLogList.AddEventLog(title, content, color);
    }

    Color GetRandomColor()
    {
        Color value = default;

        Color[] possibleColorsList =
            new Color[] {
                Color.red,
                Color.yellow,
                Color.green,
                Color.cyan,
                Color.blue,
                Color.magenta,
            };

        int randomColorIndex = Random.Range(0, possibleColorsList.Length);
        value = possibleColorsList[randomColorIndex];

        return value;
    }
}
