using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Windows.Input;
using лр27.Models;
using System.Runtime.CompilerServices;
using System.Windows;

namespace лр27.ViewModels
{
    public class ConsultationViewModel : INotifyPropertyChanged
    {
        private Consultation consultation; // Поле для хранения объекта Consultation
        private bool isBooked; // Поле для хранения статуса бронирования

        // Конструктор, принимающий объект Consultation
        public ConsultationViewModel(Consultation consultation)
        {
            this.consultation = consultation; // Инициализация объекта Consultation
            BookCommand = new RelayCommand(Book, CanBook); // Инициализация команды BookCommand
            CancelCommand = new RelayCommand(Cancel, CanCancel); // Инициализация команды CancelCommand
        }

        // Свойство для получения и установки статуса бронирования
        public bool IsBooked
        {
            get => isBooked; // Возвращает текущее значение isBooked
            set
            {
                if (isBooked != value) // Проверка на изменение значения
                {
                    isBooked = value; // Установка нового значения
                    OnPropertyChanged(); // Уведомление об изменении свойства
                }
            }
        }

        // Команда для бронирования консультации
        public ICommand BookCommand { get; }

        // Команда для отмены бронирования консультации
        public ICommand CancelCommand { get; }

        // Метод для выполнения бронирования консультации
        public void Book()
        {
            if (!IsBooked) // Проверка, что консультация не забронирована
            {
                IsBooked = true; // Установка статуса бронирования в true
                OnPropertyChanged(nameof(IsBooked)); // Уведомление об изменении свойства IsBooked
                MessageBox.Show("Вы успешно записались на консультацию!"); // Показ сообщения пользователю
            }
            else
            {
                MessageBox.Show("Вы уже записаны на эту консультацию!"); // Показ сообщения пользователю
            }
        }

        // Метод для проверки возможности бронирования консультации
        private bool CanBook()
        {
            return !IsBooked; // Возвращает true, если консультация не забронирована
        }

        // Метод для отмены бронирования консультации
        public void Cancel()
        {
            if (IsBooked) // Проверка, что консультация забронирована
            {
                IsBooked = false; // Установка статуса бронирования в false
                OnPropertyChanged(nameof(IsBooked)); // Уведомление об изменении свойства IsBooked
                MessageBox.Show("Вы успешно отменили запись на консультацию!"); // Показ сообщения пользователю
            }
            else
            {
                MessageBox.Show("Вы уже отменили запись на эту консультацию!"); // Показ сообщения пользователю
            }
        }

        // Метод для проверки возможности отмены бронирования консультации
        private bool CanCancel()
        {
            return IsBooked; // Возвращает true, если консультация забронирована
        }

        // Событие, возникающее при изменении свойства
        public event PropertyChangedEventHandler PropertyChanged;

        // Метод для вызова события PropertyChanged
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
