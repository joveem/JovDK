// system / unity
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

// third
using TMPro;

// from company
using JovDK.Debugging;
using JovDK.SafeActions;
using JovDK.SerializingTools.Bson;
using JovDK.SerializingTools.Json;

// from project
// ...


namespace JovDK.Debugging.Events
{
    public partial class EventLogView : MonoBehaviour
    {
        public void SetTitle(string title)
        {
            if (title == null)
                title = "";

            _titleText.DoIfNotNull(() => _titleText.text = title);
        }

        public void SetContent(string content)
        {
            if (content == null)
                content = "";

            _contentText.DoIfNotNull(() => _contentText.text = content);
        }

        public void SetBackgroundColor(Color color)
        {
            _backgroundImage.DoIfNotNull(() => _backgroundImage.color = color);
        }
    }
}
