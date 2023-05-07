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
    { "url", "https://sun9-49.userapi.com/impg/orOVhVHSOJ0WXKpnAm5KHQ2Ur-ZX6k7wJ1hxCg/Y8i_mwZcwtA.jpg?size=720x1280&quality=96&sign=e50ab6cf3cf76ebfa17988d54051b642&type=album" },
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