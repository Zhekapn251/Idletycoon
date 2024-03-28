using UnityEngine;

namespace Employees
{
    public class StaffPanel : MonoBehaviour
    {
        public Transform employeesContainer; // Родительский объект для UI элементов сотрудников
        public GameObject employeeUIPrefab; // Префаб UI элемента сотрудника

        // Методы для добавления, удаления и обновления элементов сотрудников
        public void AddEmployeeUI(Employee employee)
        {
            var uiItem = Instantiate(employeeUIPrefab, employeesContainer);
            //uiItem.GetComponentInChildren<Text>().text = $"{employee.Name} - Level: {employee.Level}";
            // Добавить обработчики событий для кнопок повышения, увольнения и т.д.
        }

        // ... Другие методы для обновления и управления панелью
    }
}