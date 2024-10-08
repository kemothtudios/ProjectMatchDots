﻿using System.Collections.Generic;
using UnityEngine;

namespace KemothStudios.Board
{
    public class BoardLines : MonoBehaviour, IBoardGraphic
    {
        [SerializeField] private BoardDataSO _boardData;
        [SerializeField] private GameObject _linePrefab;

        private Dictionary<int, GameObject> _lineVisuals;

        private void Start()
        {
            GameManager.Instance.OnLineClicked += ShowLine;
            _lineVisuals = new();
        }

        private void OnDestroy()
        {
            if (GameManager.Instance != null)
                GameManager.Instance.OnLineClicked -= ShowLine;
        }

        private void ShowLine(Line line)
        {
            _lineVisuals[line.GetHashCode()].SetActive(true);
        }

        public void DrawBoardGraphic()
        {
            foreach (Line line in _boardData.Lines)
            {
                GameObject obj;
                if (_linePrefab != null) obj = Instantiate(_linePrefab);
                else
                {
                    obj = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    Destroy(obj.GetComponent<Collider>());
                }
                obj.transform.position = line.LinePosition;
                obj.transform.localScale = new Vector3(line.LineScale.x, line.LineScale.y, 1f);
                obj.SetActive(false);
                _lineVisuals.Add(line.GetHashCode(), obj);
                obj.transform.parent = _boardData.BoardParent;
            }
        }
    }
}