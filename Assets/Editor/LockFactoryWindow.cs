using UnityEditor;
using UnityEngine;

public class LockFactoryWindow : EditorWindow
{
    private int _triggerValue = 512;
    private int _initialValue = 128;
    private int _cylinders = 4;

    private GameObject _cylinderPrefab;
    private GameObject _lockPrefab;
    private GameObject _caseLeftPrefab;
    private GameObject _caseMiddlePrefab;
    private GameObject _caseRightPrefab;

    [MenuItem("Tools/Lock Factory")]
    public static void ShowWindow()
    {
        LockFactoryWindow window = GetWindow<LockFactoryWindow>("Lock Factory");
        window.Initialize();
    }

    private void Initialize()
    {
        _cylinderPrefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Cylinder/Prefabs/Cylinder.prefab");
        _lockPrefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Cylinder/Prefabs/Lock.prefab");
        _caseLeftPrefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Cylinder/Prefabs/Case_Side_L.prefab");
        _caseMiddlePrefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Cylinder/Prefabs/Case_Middle.prefab");
        _caseRightPrefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Cylinder/Prefabs/Case_Side_R.prefab");
    }

    private void OnGUI()
    {
        _triggerValue = EditorGUILayout.IntField("Trigger Value", _triggerValue);
        _initialValue = EditorGUILayout.IntField("Initial Value", _initialValue);
        _cylinders = EditorGUILayout.IntField("Cylinders", _cylinders);

        if (GUILayout.Button("Create Lock"))
        {
            Create();
        }
    }

    private void Create()
    {
        GameObject parentLock = CreateRootObject();

        CreateCase(parentLock);

        for (var i = 0; i < _cylinders; i++)
        {
            CreateCylinder(i, parentLock);
        }
    }

    private GameObject CreateRootObject()
    {
        var root = PrefabUtility.InstantiatePrefab(_lockPrefab) as GameObject;

        var lockScript = root.GetComponent<Lock>();
        lockScript.triggerValue = _triggerValue;
        lockScript.value = _initialValue;

        UnpackPrefab(root);
        
        return root;
    }

    private void UnpackPrefab(GameObject gameObject)
    {
        PrefabUtility.UnpackPrefabInstance(gameObject, PrefabUnpackMode.Completely, InteractionMode.AutomatedAction);
    }

    private void CreateCase(GameObject parentLock)
    {
        var left = PrefabUtility.InstantiatePrefab(_caseLeftPrefab) as GameObject;
        left.transform.position = Vector3.back * (0.5f * _cylinders);
        left.transform.SetParent(parentLock.transform);
        UnpackPrefab(left);
        
        var right = PrefabUtility.InstantiatePrefab(_caseRightPrefab) as GameObject;
        right.transform.position = Vector3.back * -0.5f;
        right.transform.SetParent(parentLock.transform);
        UnpackPrefab(right);

        var middle = PrefabUtility.InstantiatePrefab(_caseMiddlePrefab) as GameObject;
        middle.transform.position = Vector3.back * ((_cylinders - 1) * 0.25f);
        var scale = middle.transform.localScale;
        scale.y = _cylinders * 100f;
        middle.transform.localScale = scale;
        middle.transform.SetParent(parentLock.transform);
        UnpackPrefab(middle);
    }

    private void CreateCylinder(int i, GameObject parentLock)
    {
        var cylinder = PrefabUtility.InstantiatePrefab(_cylinderPrefab) as GameObject;
        cylinder.transform.position += Vector3.back * (0.5f * i);
        cylinder.transform.SetParent(parentLock.transform);
        UnpackPrefab(cylinder);
    }
}