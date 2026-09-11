using UnityEngine;
using System.Collections.Generic;

public class GizmoDrawing : MonoBehaviour
{
    [SerializeField] GameObject _node;
    [SerializeField] int _width = 7;
    [SerializeField] int _height = 7;
    [SerializeField] int _size = 1;
    [SerializeField] int _spacing = 1;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        DrawingGrid();
    }
    void DrawingGrid(){
        foreach(var point in EvaluateGridPoint()){
            Instantiate(_node, point, Quaternion.identity);
        }
    }
    void OnDrawGizmos(){
        foreach (var point in EvaluateGridPoint()){
            Gizmos.DrawWireCube(point, new Vector3(_size, _size, 0));
        }
    }
    IEnumerable<Vector3> EvaluateGridPoint(){
        float step = _size + _spacing;
        float startX = -(_width - 1) * step / 2f;
        float startY = -(_height - 1) * step / 2f;
        for (int x = 0; x < _width; x++){
            for (int y = 0; y<_height; y++){
                yield return new Vector3(startX + (x*step), startY + (y * step), 0);
            }
        }
    }
}
