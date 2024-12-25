using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace Community
{
    class MainViewModel : INotifyPropertyChanged
    {
        public MainViewModel()
        {
            SetRateCommand = new Command(SetRate);
            SetAmountCommand = new Command(SetAmount);
            //NotifyCommand = new Command(() => App.Current?.MainPage?.DisplayAlert("Alert", "Command Executed", "ok"));
        }
        public ICommand SetRateCommand { get; private set; }
        public ICommand SetAmountCommand { get; private set; }
        public ICommand? NotifyCommand { get; private set; } = null;
        private void SetRate() => Rate = Amount / 100;
        private void SetAmount() => Amount = Rate * 100;

        public decimal Rate
        {
            get => rate;
            set => SetProperty(ref rate, value);
        }

        private decimal amount = 200;
        private decimal rate;

        public decimal Amount
        {
            get => amount;
            set => SetProperty(ref amount, value);
        }
        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string name = "") =>
             PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        protected bool SetProperty<T>(ref T backingStore, T value, Action? onChanged = null, [CallerMemberName] string propertyName = "")
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
                return false;

            backingStore = value;
            onChanged?.Invoke();
            OnPropertyChanged(propertyName);
            return true;
        }

    }
}
