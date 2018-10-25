using System.Windows;
using TSKProject.ViewModel;

namespace TSKProject.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void OnOpenFileClick(object sender, RoutedEventArgs e)
        {
            MainWindowViewModel vm = MainGrid.DataContext as MainWindowViewModel;
            vm.OnOpenFileClick();
        }

        private void OnPlayClick(object sender, RoutedEventArgs e)
        {
            MainWindowViewModel vm = MainGrid.DataContext as MainWindowViewModel;
            vm.OnPlayClick();
        }

        private void OnPlayUnprocessedClick(object sender, RoutedEventArgs e)
        {
            MainWindowViewModel vm = MainGrid.DataContext as MainWindowViewModel;
            vm.OnPlayUnprocessedClick();
        }

        private void DelaySamplesSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            MainWindowViewModel vm = MainGrid.DataContext as MainWindowViewModel;
            vm.OnPropertyChanged("DelaySamples");
        }

        private void DelayGainSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            MainWindowViewModel vm = MainGrid.DataContext as MainWindowViewModel;
            vm.OnPropertyChanged("DelayGain");
        }

        private void DelayVolumeSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            MainWindowViewModel vm = MainGrid.DataContext as MainWindowViewModel;
            vm.OnPropertyChanged("DelayVolume");
        }

        private void OnStopClick(object sender, RoutedEventArgs e)
        {
            MainWindowViewModel vm = MainGrid.DataContext as MainWindowViewModel;
            vm.OnStopClick();
        }
    }
}
