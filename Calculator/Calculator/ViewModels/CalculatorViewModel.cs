using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Calculator.Utils;

namespace Calculator.ViewModels
{
    /// <summary>
    /// To jest glowny ViewModel do formatki CalculatorView. 
    /// Binding jest zadeklarowany w widoku poprzez:
    /// <Grid.DataContext>
    ///     <ViewModels:CalculatorViewModel/>
    /// </Grid.DataContext>
    /// 
    /// Tutaj znajdują się deklaracje eventow do Clickow i binding dla tekstu wyswietlanego.
    /// </summary>
    public class CalculatorViewModel : ViewModelBase
    {
        /// <summary>
        /// Nazwa property dla TextBoxa Output.
        /// </summary>
        private const string OutputName = "Output";
        
        /// <summary>
        /// Zmienna przechowujaca aktualne rownanie.
        /// </summary>
        private string _output;

        /// <summary>
        /// Pierwsza wartosc rownania
        /// </summary>
        private int _firstValue;
        /// <summary>
        /// Druga wartosc rownania
        /// </summary>
        private int _secondValue;

        /// <summary>
        /// Flaga czy operacja jest wybrana.
        /// </summary>
        private bool _operationClicked;
        /// <summary>
        /// Wybrana operacja.
        /// </summary>
        private OperationType _operation;

        public CalculatorViewModel()
        {
            // Ustawienie pierwszej wartosci w TextBoxie.
            _output = "0";
            SetNumberCommands();
            SetOperationCommands();
            // Ustawienie Eventa dla klikniecia na znak Rowna Sie.
            EqualCommand = new RelayCommand(EqualClicked, CanMakeEquation);
        }

        /// <summary>
        /// Wartosc wyswietlana w TextBoxie.
        /// Przy ustawianiu wartosci (set) wywolywana jest metoda NotifyPropertyChanged.
        /// Dzieki temu widok wie, ze ma odswiezyc wartosc w TextBoxie.
        /// Dlatego niezbedne jest wprowadzanie zmian w polu _output poprzez property Output, a nie bezposrednio.
        /// </summary>
        public string Output
        {
            get { return _output; }
            private set
            {
                _output = value;
                RaisePropertyChanged(OutputName); // Bardzo wazne.
            }
        }

        /// <summary>
        /// Pomocnicza wlasciwosc.
        /// Zwraca i aktualizuje obecna zmienna w zaleznosci, czy operacja jest juz kliknieta.
        /// </summary>
        private int CurrentValue
        {
            get { return _operationClicked ? _secondValue : _firstValue; }
            set
            {
                if (_operationClicked)
                {
                    _secondValue = value;
                }
                else
                {
                    _firstValue = value;
                }
            }
        }

        /// <summary>
        /// Zbior deklaracji wszystkich Commandow.
        /// </summary>
        #region Commands
        public ICommand ZeroCommand { get; private set; }
        public ICommand OneCommand { get; private set; }
        public ICommand TwoCommand { get; private set; }
        public ICommand ThreeCommand { get; private set; }
        public ICommand FourCommand { get; private set; }
        public ICommand FiveCommand { get; private set; }
        public ICommand SixCommand { get; private set; }
        public ICommand SevenCommand { get; private set; }
        public ICommand EightCommand { get; private set; }
        public ICommand NineCommand { get; private set; }

        public ICommand AddCommand { get; private set; }
        public ICommand SubtractCommand { get; private set; }
        public ICommand MultiplyCommand { get; private set; }
        public ICommand DivideCommand { get; private set; }

        public ICommand EqualCommand { get; private set; }
        #endregion

        /// <summary>
        /// Inicjalizuje metody wywolywane przez Commandy z liczbami.
        /// </summary>
        private void SetNumberCommands()
        {
            ZeroCommand = new RelayCommand(ZeroClicked);
            OneCommand = new RelayCommand(() => NumberClicked(1));
            TwoCommand = new RelayCommand(() => NumberClicked(2));
            ThreeCommand = new RelayCommand(() => NumberClicked(3));
            FourCommand = new RelayCommand(() => NumberClicked(4));
            FiveCommand = new RelayCommand(() => NumberClicked(5));
            SixCommand = new RelayCommand(() => NumberClicked(6));
            SevenCommand = new RelayCommand(() => NumberClicked(7));
            EightCommand = new RelayCommand(() => NumberClicked(8));
            NineCommand = new RelayCommand(() => NumberClicked(9));
        }

        /// <summary>
        /// Inicjalizuje metody wywolywane przez Commandy z operacjami.
        /// </summary>
        private void SetOperationCommands()
        {
            AddCommand = new RelayCommand(() => OperationClicked(OperationType.Add), CanClickOperation);
            SubtractCommand = new RelayCommand(() => OperationClicked(OperationType.Subtract), CanClickOperation);
            MultiplyCommand = new RelayCommand(() => OperationClicked(OperationType.Multiply), CanClickOperation);
            DivideCommand = new RelayCommand(() => OperationClicked(OperationType.Divide), CanClickOperation);
        }

        /// <summary>
        ///  Definiuje czy button "=" jest aktywny.
        /// </summary>
        /// <returns>Zwraca true, jezeli mozna kliknac button "=".</returns>
        private bool CanMakeEquation()
        {
            return _operationClicked &&
                (_operation != OperationType.Divide ||
                    _operation == OperationType.Divide && _secondValue != 0);
        }

        /// <summary>
        /// Metoda wywolywana przy kliknieciu buttona "=".
        /// Nastepuje obliczenie wyniku, wyzerowanie zmiennych i wpisanie wartosci do text boxa.
        /// </summary>
        private void EqualClicked()
        {
            var result = NumberCalculator.GetResult(_firstValue, _secondValue, _operation);
            _firstValue = 0;
            _secondValue = 0;
            _operationClicked = false;
            Output += "="; // Dodanie znaku rowna sie do text boxa.
            Output += result.ToString(); // Dodanie wyniku do text boxa.
        }

        /// <summary>
        /// Definuje czy mozna kliknac guzik operacji.
        /// </summary>
        /// <returns>Zwraca true, jesli mozna kliknac button operacji.</returns>
        private bool CanClickOperation()
        {
            return !_operationClicked;
        }

        /// <summary>
        /// Metoda wywolywana po kliknieciu buttona operacji.
        /// Przyjmuje typ operacji jako parametr.
        /// </summary>
        /// <param name="operation">Typ operacji.</param>
        private void OperationClicked(OperationType operation)
        {
            _operation = operation;
            _operationClicked = true;
            Output += operation.GetSign(); // Dodanie znaku operacji do TextBoxa.
        }

        /// <summary>
        /// Metoda wywolana gdy zostalo nacisniete 0.
        /// </summary>
        private void ZeroClicked()
        {
            if (CurrentValue == 0)
            {
                return;
            }

            NumberClicked(0);
        }

        /// <summary>
        /// Metoda wywolana gdy zostal nacisniety numer.
        /// Akutalizuje obecna wartosc i dodaje numer do TextBoxa.
        /// </summary>
        /// <param name="number">Klikniety numer.</param>
        private void NumberClicked(int number)
        {
            var numberAsString = number.ToString();
            if (CurrentValue == 0 && !_operationClicked)
            {
                Output = numberAsString; // Jesli jest to pierwsza wartosc, nadpisz 0.
            }
            else
            {
                Output += numberAsString; // Dodanie numeru na koniec text boxa.
            }

            CurrentValue = int.Parse(CurrentValue.ToString() + numberAsString); // Zaktualizowanie obecnej wartosci.
        }
    }
}
