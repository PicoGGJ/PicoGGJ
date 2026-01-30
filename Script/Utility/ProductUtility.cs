using System;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class ProductUtility : EditorWindow
{
    private string nameProduct = "";

    private List<string> allElements = new();
    private List<int> selectedElementIndices = new();
    private List<float> ElementAmount = new();


    [MenuItem("Utility/Product")]
    public static void Open()
    {
        GetWindow<ProductUtility>("Product");
    }

    private void OnEnable()
    {
        LoadElements();
    }

    private void LoadElements()
    {
        allElements.Clear();
        ElementAmount.Clear();
        allElements.Add("Empty");

        string filePath = "Assets/PeriodicData/periodic.json";
        if (!File.Exists(filePath)) return;

        string json = File.ReadAllText(filePath);
        PeriodicDatabase database = JsonUtility.FromJson<PeriodicDatabase>(json);

        foreach (var data in database.Periodic)
        {
            allElements.Add(data.symbolElement);
        }
    }

    private void OnGUI()
    {
        EditorGUILayout.LabelField("Product Editor", EditorStyles.boldLabel);

        nameProduct = EditorGUILayout.TextField("Name Product", nameProduct);

        EditorGUILayout.Space();

        for (int i = 0; i < selectedElementIndices.Count; i++)
        {
            EditorGUILayout.BeginHorizontal();
            selectedElementIndices[i] = EditorGUILayout.Popup(
                $"Element {i + 1}",
                selectedElementIndices[i],
                allElements.ToArray()
            );
            ElementAmount.Add(0);

            ElementAmount[i] = EditorGUILayout.FloatField(ElementAmount[i]);
            EditorGUILayout.EndHorizontal();
        }

        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Add Element"))
            selectedElementIndices.Add(0);

        if (GUILayout.Button("Remove Element") && selectedElementIndices.Count > 0)
            selectedElementIndices.RemoveAt(selectedElementIndices.Count - 1);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.Space();

        if (GUILayout.Button("Save"))
            Save();

        if (GUILayout.Button("Reset View"))
            ResetView();
    }

    private void Save()
    {
        if (string.IsNullOrEmpty(nameProduct)) return;

        ProductDatabase database;
        string folderPath = "Assets/ProductData";
        string filePath = $"{folderPath}/products.json";

        if (!AssetDatabase.IsValidFolder(folderPath))
            AssetDatabase.CreateFolder("Assets", "ProductData");

        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            database = JsonUtility.FromJson<ProductDatabase>(json) ?? new ProductDatabase();
        }
        else
        {
            database = new ProductDatabase();
        }

        ProductData data = new()
        {
            NameProduct = nameProduct,
            Elements = new List<string>(),
            Amount = new List<float>()
        };

        foreach (int index in selectedElementIndices)
        {
            data.Elements.Add(allElements[index]);
        }

        int existingIndex = database.Data.FindIndex(p => p.NameProduct == nameProduct);

        if (existingIndex >= 0)
            database.Data[existingIndex] = data;
        else
            database.Data.Add(data);

        File.WriteAllText(filePath, JsonUtility.ToJson(database, true));
        AssetDatabase.Refresh();

        ResetView();
    }

    private void ResetView()
    {
        nameProduct = "";
        selectedElementIndices.Clear();
        ElementAmount.Clear();
        GUI.FocusControl(null);
        Repaint();
    }
}

[Serializable]
public class ProductData
{
    public string NameProduct;
    public List<string> Elements;
    public List<float> Amount;
}

[Serializable]
public class ProductDatabase
{
    public List<ProductData> Data = new();
}