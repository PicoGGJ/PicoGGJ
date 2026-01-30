using System;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

[System.Serializable]
public enum TypeAufbau
{
    h, k, m, b
}
[System.Serializable]
public class AufbauData
{
    public int Amount;
    public TypeAufbau TypeAufbau;
}
public class PeriodicUtility : EditorWindow
{
    private string symbolElement = "";
    private string elementName = "";
    private int totalElectron = 0;
    private List<AufbauData> aufbauList = new();
    private int length = 0;

    [MenuItem("Utility/Periodic Table")]
    public static void Open()
    {
        GetWindow<PeriodicUtility>("Periodic Table");
    }

    private void OnGUI()
    {
        
        EditorGUILayout.LabelField("Periodic Table Editor");

        EditorGUILayout.BeginVertical();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Symbol Element");
        symbolElement = EditorGUILayout.TextField(symbolElement);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Name Element");
        elementName = EditorGUILayout.TextField(elementName);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Total Electron");
        totalElectron = EditorGUILayout.IntField(totalElectron);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginVertical();
        EditorGUILayout.LabelField("Aufbau");
        for (int i = 0; i <= length; i++)
        {
            aufbauList.Add(new AufbauData());

            EditorGUILayout.BeginHorizontal();
            aufbauList[i].Amount = EditorGUILayout.IntField(aufbauList[i].Amount);
            aufbauList[i].TypeAufbau = (TypeAufbau)EditorGUILayout.EnumPopup(aufbauList[i].TypeAufbau);
            EditorGUILayout.EndHorizontal();
        }
        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Add"))
        {
            length++;
        }
        if(GUILayout.Button("Remove"))
        {
            if(length >0)
            length--;
        }

        GUILayout.EndVertical();

        EditorGUILayout.EndHorizontal();
        if (GUILayout.Button("Save"))
        {
            Save();
        }

        if(GUILayout.Button("Reset View"))
        {
            ResetView();
        }
        EditorGUILayout.EndVertical();
    }

    private void Save()
    {
        aufbauList.RemoveAll(i => i.Amount <= 0);

        GUI.FocusControl(null);
        EditorGUIUtility.editingTextField = false;
        GUIUtility.keyboardControl = 0;

        if (string.IsNullOrEmpty(symbolElement) ||
            string.IsNullOrEmpty(elementName) ||
            totalElectron <= 0 ||
            aufbauList.Count == 0)
            return;

        string folderPath = "Assets/PeriodicData";
        string filePath = $"{folderPath}/periodic.json";

        if (!AssetDatabase.IsValidFolder(folderPath))
        {
            AssetDatabase.CreateFolder("Assets", "PeriodicData");
        }

        PeriodicDatabase database;

        if (System.IO.File.Exists(filePath))
        {
            string json = System.IO.File.ReadAllText(filePath);
            database = JsonUtility.FromJson<PeriodicDatabase>(json);

            if (database == null || database.Periodic == null)
                database = new PeriodicDatabase();
        }
        else
        {
            database = new PeriodicDatabase();
        }

        PeriodicData newData = new PeriodicData
        {
            symbolElement = symbolElement,
            elementName = elementName,
            totalElectron = totalElectron,
            aufbau = new List<AufbauData>(aufbauList)
        };

        int index = database.Periodic.FindIndex(i => i.elementName == elementName);

        if (index >= 0)
        {
            database.Periodic[index] = newData;
            Debug.Log($"Updated element: {elementName}");
        }
        else
        {
            database.Periodic.Add(newData);
            Debug.Log($"Added new element: {elementName}");
        }

        string outputJson = JsonUtility.ToJson(database, true);
        System.IO.File.WriteAllText(filePath, outputJson);
        AssetDatabase.Refresh();

        ResetView();
    }



    private void ResetView()
    {
        symbolElement = "";
        elementName = "";
        totalElectron = 0;
        aufbauList.Clear();
        length = 0;

        GUI.FocusControl(null);
        Repaint();
    }

}

[System.Serializable]
public class PeriodicData
{
    public string symbolElement;
    public string elementName;
    public int totalElectron;
    public List<AufbauData> aufbau;

}

[System.Serializable]
public class PeriodicDatabase
{
    public List<PeriodicData> Periodic = new List<PeriodicData>();
}

