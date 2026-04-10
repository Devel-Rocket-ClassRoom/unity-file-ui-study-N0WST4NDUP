using System.IO;
using UnityEngine;

public class Q1 : MonoBehaviour
{
    private string _directoryPath = "SaveData";
    private string _fileName = "data";
    private int _id = 1;
    private string _ext = ".txt";

    private FileVisualizor _selectedFileVisualizor;
    private FileVisualizor _copiedFileVisualizor;

    private float _distance = 2f;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            string directoryPath = Path.Combine(Application.persistentDataPath, _directoryPath);
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            string filePath = Path.Combine(directoryPath, _fileName + _id + _ext);
            File.WriteAllText(filePath, $"Hello, World! {_id}번째 파일");
            Debug.Log($"[{_id}] 경로: \"{filePath}\"");
            _id++;

            GameObject go = GameObject.CreatePrimitive(PrimitiveType.Cube);
            go.transform.position += Vector3.right * _distance * _id;
            FileVisualizor fv = go.AddComponent<FileVisualizor>();
            fv.Path = filePath;
        }

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                _selectedFileVisualizor = hit.collider.gameObject.GetComponent<FileVisualizor>();
            }
        }

        if (Input.GetKey(KeyCode.LeftControl))
        {
            if (Input.GetKeyDown(KeyCode.C))
            {
                if (_selectedFileVisualizor == null) return;

                _copiedFileVisualizor = _selectedFileVisualizor;
            }

            if (Input.GetKeyDown(KeyCode.V))
            {
                if (_copiedFileVisualizor == null) return;

                GameObject go = GameObject.CreatePrimitive(PrimitiveType.Cube);
                go.transform.position = _copiedFileVisualizor.transform.position + Vector3.up * _distance;
                FileVisualizor fv = go.AddComponent<FileVisualizor>();
                fv.Path = _copiedFileVisualizor.Path.Replace(_ext, $"_COPY{_ext}");
                File.Copy(_copiedFileVisualizor.Path, fv.Path);
                _copiedFileVisualizor = fv;
            }
        }

        if (Input.GetKey(KeyCode.Delete))
        {
            if (_selectedFileVisualizor == null) return;

            File.Delete(_selectedFileVisualizor.Path);
            Destroy(_selectedFileVisualizor);
        }
    }
}
