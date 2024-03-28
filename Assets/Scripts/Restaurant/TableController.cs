using System.Collections.Generic;
using Furnitures;
using UnityEngine;

namespace Restaurant
{
    public class TableController: MonoBehaviour
    {
        [SerializeField] private Table[] _tables; // Предполагается, что Table имеет свойство IsOccupied
        
        private List<Table> _freeTables = new List<Table>();
        private List<Table> _occupiedTables = new List<Table>();

        private void Awake()
        {
           // InitializeTables();
        }

        private void InitializeTables()
        {
            foreach (var table in _tables)
            {
                if (table.IsOccupied)
                {
                    _occupiedTables.Add(table);
                }
                else
                {
                    _freeTables.Add(table);
                }
            }
        }

        // Метод для занятия стола
        public void OccupyTable(Table table)
        {
            if (_freeTables.Remove(table))
            {
                _occupiedTables.Add(table);
                table.Occupy(); // Предполагается, что у Table есть метод для изменения его статуса на занятый
            }
        }

        // Метод для освобождения стола
        public void FreeTable(Table table)
        {
            if (_occupiedTables.Remove(table))
            {
                _freeTables.Add(table);
                table.Free(); // Аналогично, предполагается метод для освобождения стола
            }
        }

        // Получить список свободных столов
        public List<Table> GetFreeTables()
        {
            return _freeTables;
        }

        // Получить список занятых столов
        public List<Table> GetOccupiedTables()
        {
            return _occupiedTables;
        }
        
    }
}