using Newtonsoft.Json;
using System.Collections.Generic;
using UnityEngine;

public class ShareMenu : MonoBehaviour
{
    public VkBridgeController Bridge;

    private void Awake()
    {
        Bridge.VKWebAppInit();
    }

    public void ClickShare()
    {
        Dictionary<string, string> data = new Dictionary<string, string>
{
    { "background_type", "image" },
    { "url", "https://sun9-65.userapi.com/c850136/v850136098/1b77eb/0YK6suXkY24.jpg" },
    { "attachment[text]", "book" },
    { "attachment[type]", "photo" },
    { "attachment[owner_id]", "743784474" },
    { "attachment[id]", "12345678" }
};

        Bridge.Send("VKWebAppShowStoryBox", data, ResultClickShare);
    }

    public void ResultClickShare(string stringResult)
    {
        Dictionary<string, object> result = JsonConvert.DeserializeObject<Dictionary<string, object>>(stringResult);
    }
}