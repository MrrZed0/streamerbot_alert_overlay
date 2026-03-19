using System;

public class CPHInline
{
    public bool Execute()
    {
        CPH.TryGetArg("targetUserProfileImageUrl", out string targetUserProfileImageUrl);
        CPH.TryGetArg("triggerName", out string triggerName);
        CPH.TryGetArg("targetUser", out string targetUser);

        if (string.IsNullOrWhiteSpace(targetUserProfileImageUrl))
            targetUserProfileImageUrl = "https://static-cdn.jtvnw.net/jtv_user_pictures/xarth/404_user_300x300.png";

        if (string.IsNullOrWhiteSpace(triggerName))
            triggerName = "Alert";

        if (string.IsNullOrWhiteSpace(targetUser))
            targetUser = "Unknown User";

        string json = "{" +
            "\"type\":\"custom.alert.overlay\"," +
            "\"payload\":{" +
                "\"targetUserProfileImageUrl\":\"" + EscapeJson(targetUserProfileImageUrl) + "\"," +
                "\"triggerName\":\"" + EscapeJson(triggerName) + "\"," +
                "\"targetUser\":\"" + EscapeJson(targetUser) + "\"" +
            "}" +
        "}";

        CPH.WebsocketBroadcastJson(json);
        CPH.LogInfo("[Alert Overlay] Broadcast sent: " + json);
        return true;
    }

    private string EscapeJson(string value)
    {
        if (string.IsNullOrEmpty(value))
            return string.Empty;

        return value
            .Replace("\\", "\\\\")
            .Replace("\"", "\\\"")
            .Replace("\r", "\\r")
            .Replace("\n", "\\n")
            .Replace("\t", "\\t");
    }
}
