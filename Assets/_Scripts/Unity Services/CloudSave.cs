using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Unity.Services.CloudSave;
using UnityEngine;

public static class CloudSave
{
    private static readonly ICloudSaveDataClient _client = 
        CloudSaveService.Instance.Data;

    public static async Task SaveDataAsync(string key, object value)
    {
        var data = new Dictionary<string, object> { { key, value } };
        
        try
        {
            await _client.ForceSaveAsync(data);

            Debug.Log("Successful data save.");
        }
        catch (Exception e)
        {
            Debug.LogException(e);
        }
    }

    public static async Task LoadDataAsync(params string[] keys)
    {
        var cao = new HashSet<string>(keys);

        try
        {
            var data = _client.LoadAllAsync().Result;

            Debug.Log("Data successfuly loaded.");
            foreach (KeyValuePair<string, string> test in data)
            {
                Debug.Log($"{test.Key} {test.Value}");
            }
        }
        catch (Exception e)
        {
            Debug.LogException(e);
        }
    }

}
