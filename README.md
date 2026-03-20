# 🚨 Streamer.bot Alert Overlay (Animated Profile + Banner)

A **custom animated alert overlay** for **Streamer.bot** using a **WebSocket HTML overlay** and C# broadcast system.

This overlay is designed for:

* Follows
* Subs
* Gifted Subs
* Cheers
* Raids
* Custom alerts

It features a **circular animated avatar ring**, glowing text, and a dynamic banner display.

![Image](https://github.com/MrrZed0/streamerbot_alert_overlay/blob/main/0318%20(5).gif?raw=true)

---

## ✨ Features

* 🔵 Animated circular avatar ring (gradient spin effect)
* 🖼️ Twitch profile picture support
* ✨ Glowing alert text (trigger name)
* 🎯 Dynamic banner with username auto-fit
* 🎬 Smooth IN/OUT animations
* ⚡ WebSocket real-time updates via Streamer.bot
* 🧠 Supports all alert types via `triggerName`

---

## 📁 Files

```
overlay/
│
├── alert_overlay.html
├── banner_background.png
```

---

## ⚙️ Requirements

* Streamer.bot (v1.0.4+)
* OBS Studio (or Streamer.bot overlay app)
* WebSocket enabled in Streamer.bot

---

## 🔧 Setup

### 1. Enable WebSocket in Streamer.bot

* Go to: **Servers / Clients → WebSocket Server**
* Enable it
* Default:

```
ws://127.0.0.1:8080
```

---

### 2. Add Overlay to OBS

Add a **Browser Source**:

```
file:///C:/path/to/alert_overlay.html
```

Recommended settings:

* ✔ Shutdown source when not visible → OFF
* ✔ Refresh browser when scene becomes active → OFF

---

### 3. Required Image

Place this file in the same folder:

```
banner_background.png
```

This is used for the username banner.

---

## 📡 Streamer.bot C# Integration

The overlay listens for WebSocket messages.

Use a C# action to send alert data:

### Required Args

| Argument                    | Description                    |
| --------------------------- | ------------------------------ |
| `targetUserProfileImageUrl` | Twitch profile image URL       |
| `triggerName`               | Alert type (Follow, Sub, etc.) |
| `targetUser`                | Username                       |

---

### Example C# Broadcast

```csharp
using System;

public class CPHInline
{
    public bool Execute()
    {
        try
        {
            string profile = "";
            string trigger = "";
            string user = "";

            CPH.TryGetArg("targetUserProfileImageUrl", out profile);
            CPH.TryGetArg("triggerName", out trigger);
            CPH.TryGetArg("targetUser", out user);

            string json =
                "{"
                + "\"type\":\"custom.alert.overlay\","
                + "\"payload\":{"
                + "\"targetUserProfileImageUrl\":\"" + Escape(profile) + "\","
                + "\"triggerName\":\"" + Escape(trigger) + "\","
                + "\"targetUser\":\"" + Escape(user) + "\""
                + "}"
                + "}";

            CPH.WebsocketBroadcastJson(json);

            return true;
        }
        catch (Exception ex)
        {
            CPH.LogError(ex.ToString());
            return false;
        }
    }

    private string Escape(string s)
    {
        return (s ?? "")
            .Replace("\\", "\\\\")
            .Replace("\"", "\\\"");
    }
}
```

---

## 🎨 Overlay Behavior

When triggered:

1. Profile image loads into circular avatar
2. Animated blue gradient ring spins around avatar
3. `triggerName` appears with glow text
4. `targetUser` appears centered in banner
5. Overlay animates IN
6. Displays for ~6 seconds
7. Animates OUT

---

## 🎬 Animation Details

* Avatar ring uses **conic-gradient rotation**
* Entry animation:

  * Fade in
  * Slide down
  * Scale up
* Exit animation:

  * Fade out
  * Slight upward movement
  * Blur effect

---

## 🧪 Testing

Open browser console and run:

```javascript
testAlert();
```

---

## ⚠️ Notes

* Overlay requires active WebSocket connection
* Ensure Streamer.bot is running
* If nothing shows:

  * Check console (`F12`)
  * Verify WebSocket connection
  * Confirm args are passed correctly

---

## 🚀 Optional Upgrades

* Add sound alerts
* Add multiple alert queue system
* Add alert stacking
* Add custom colors per alert type
* Add animated backgrounds
* Add emote support

---

## ❤️ Credits

* Streamer.bot
* OBS Studio
* Twitch API (profile images)

---

## 📜 License

Free to use, modify, and share.

