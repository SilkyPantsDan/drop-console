﻿using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class TestScript : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

        DropConsole.RegisterCommand("floorCol", ChangeFloorColor, "[r g b] Change the floor color as integers from 0 - 255");

        DropConsole.RegisterCommand("testError", LogError, "Logs error to console");
        DropConsole.RegisterCommand("testWarn", LogWarning, "Logs warning to console");
        DropConsole.RegisterCommand("testInfo", LogInfo, "Logs info to console");
    }

    string LogError(params string[] args)
    {
        #if USE_CLEAN_LOGGER
		CleanLog.LogError (string.Join (" ", args));
        #else
        Debug.LogError(string.Join(" ", args));
        #endif
        return "OK";
    }

    string LogWarning(params string[] args)
    {

        #if USE_CLEAN_LOGGER
        CleanLog.LogWarning (string.Join (" ", args));
        #else
        Debug.LogWarning(string.Join(" ", args));
        #endif

        return "OK";
    }

    string LogInfo(params string[] args)
    {

        #if USE_CLEAN_LOGGER
        CleanLog.Log (string.Join (" ", args));
        #else
        Debug.Log(string.Join(" ", args));
        #endif
        return "OK";
    }

    string ChangeFloorColor(params string[] args)
    {

        if (args.Length == 3) {
            int r, g, b;

            if (int.TryParse(args[0], out r) && int.TryParse(args[1], out g) && int.TryParse(args[2], out b)) {

                var newColor = new Color(r / 255f, g / 255f, b / 255f);
                var meshRenderer = gameObject.GetComponent<MeshRenderer>();

                if (meshRenderer != null) {

                    meshRenderer.material.color = newColor;

                    return "Floor colour changed to " + newColor.ToString();
                }
            }

            return "Could not parse colours";
        }

        return "Arguments are not correct";
    }
}
