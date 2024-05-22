using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace лр27
{
    // Класс RelayCommand реализует интерфейс ICommand для создания простой команды
    public class RelayCommand : ICommand
    {
        private readonly Action execute; // Делегат для выполнения команды
        private readonly Func<bool> canExecute; // Делегат для проверки возможности выполнения команды

        // Конструктор, принимающий делегат для выполнения команды и делегат для проверки возможности выполнения команды
        public RelayCommand(Action execute, Func<bool> canExecute = null)
        {
            // Проверка наличия делегата execute
            this.execute = execute ?? throw new ArgumentNullException(nameof(execute));

            // Сохранение делегата canExecute
            this.canExecute = canExecute;
        }

        // Метод для проверки возможности выполнения команды
        public bool CanExecute(object parameter)
        {
            // Вызов делегата canExecute, если он существует, иначе возвращает true
            return canExecute == null || canExecute();
        }

        // Метод для выполнения команды
        public void Execute(object parameter)
        {
            execute(); // Вызов делегата execute
        }

        // Событие, возникающее при изменении возможности выполнения команды
        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value; // Подписка на событие CommandManager.RequerySuggested
            remove => CommandManager.RequerySuggested -= value; // Отписка от события CommandManager.RequerySuggested
        }
    }
}
