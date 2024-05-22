using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Windows.Input;
using лр27.Models;
using System.Data.SqlClient;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

namespace лр27.ViewModels
{
    // Класс MainViewModel реализует интерфейс INotifyPropertyChanged и предоставляет данные для представления MainView
    public class MainViewModel : INotifyPropertyChanged
    {
        private string connectionString; // Строка подключения к базе данных
        private ObservableCollection<Consultation> consultations; // Коллекция консультаций

        // Конструктор, принимающий строку подключения к базе данных
        public MainViewModel(string connectionString)
        {
            this.connectionString = connectionString; // Сохранение строки подключения
            Consultations = new ObservableCollection<Consultation>(); // Инициализация коллекции консультаций
            LoadConsultations(); // Загрузка списка консультаций из базы данных
            BookCommand = new RelayCommand(Book, CanBook); // Инициализация команды бронирования консультации
            CancelCommand = new RelayCommand(Cancel, CanCancel); // Инициализация команды отмены бронирования консультации
        }

        // Свойство для доступа к коллекции консультаций
        public ObservableCollection<Consultation> Consultations
        {
            get { return consultations; }
            set
            {
                consultations = value;
                OnPropertyChanged();
            }
        }

        // Метод для загрузки списка консультаций из базы данных
        private void LoadConsultations()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("SELECT * FROM Consultations", connection);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var consultation = new Consultation
                    {
                        Id = reader.GetInt32(0),
                        FullName = reader.GetString(1),
                        Subject = reader.GetString(2),
                        Date = reader.GetDateTime(3),
                        TimeSlot = reader.GetString(4),
                        IsBooked = reader.GetBoolean(5)
                    };
                    Consultations.Add(consultation); // Добавление консультации в коллекцию
                }
            }
        }

        // Событие, возникающее при изменении свойств
        public event PropertyChangedEventHandler PropertyChanged;

        // Метод для вызова события PropertyChanged
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        // Свойство для выбранной консультации
        private Consultation selectedConsultation;
        public Consultation SelectedConsultation
        {
            get { return selectedConsultation; }
            set
            {
                if (value != selectedConsultation)
                {
                    selectedConsultation = value;
                    OnPropertyChanged();
                }
            }
        }

        // Команды для бронирования и отмены бронирования консультации
        public ICommand BookCommand { get; }
        public ICommand CancelCommand { get; }

        // Метод для бронирования консультации
        private void Book()
        {
            if (SelectedConsultation != null && !SelectedConsultation.IsBooked)
            {
                SelectedConsultation.IsBooked = true;
                MessageBox.Show("Вы успешно записались на консультацию!");
            }
            else if (SelectedConsultation != null && SelectedConsultation.IsBooked)
            {
                MessageBox.Show("Вы уже записаны на эту консультацию!");
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите консультацию для записи.");
            }
        }

        // Метод для проверки возможности бронирования консультации
        private bool CanBook()
        {
            return SelectedConsultation != null && !SelectedConsultation.IsBooked;
        }

        // Метод для отмены бронирования консультации
        private void Cancel()
        {
            if (SelectedConsultation != null && SelectedConsultation.IsBooked)
            {
                SelectedConsultation.IsBooked = false;
                MessageBox.Show("Вы успешно отменили запись на консультацию!");
            }
            else if (SelectedConsultation != null && !SelectedConsultation.IsBooked)
            {
                MessageBox.Show("Вы еще не записаны на эту консультацию!");
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите консультацию для отмены записи.");
            }
        }

        // Метод для проверки возможности отмены бронирования консультации
        private bool CanCancel()
        {
            return SelectedConsultation != null && SelectedConsultation.IsBooked;
        }
    }
}
