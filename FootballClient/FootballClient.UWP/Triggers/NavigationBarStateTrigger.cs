using System;
using FootballClient.UWP.ViewModels;
using Windows.UI.Xaml;
using WindowsStateTriggers;

namespace FootballClient.UWP.Triggers
{
    public class NavigationBarStateTrigger : StateTriggerBase, ITriggerValue
    {
        private AppShellViewModel _viewModel;
        private bool _isActive;

        public event EventHandler IsActiveChanged;

        public AppShellViewModel ViewModel
        {
            get { return _viewModel; }
            set
            {
                if (_viewModel != null)
                {
                    _viewModel.SelectedItemChanged -= OnSelectedItemChanged;
                }

                _viewModel = value;

                if (_viewModel != null)
                {
                    _viewModel.SelectedItemChanged += OnSelectedItemChanged;
                    OnSelectedItemChanged(null, null);
                }
            }
        }

        public bool IsCanView { get; set; }

        public bool IsActive
        {
            get
            {
                return _isActive;
            }
            set
            {
                if (_isActive != value)
                {
                    _isActive = value;
                    IsActiveChanged?.Invoke(this, EventArgs.Empty);
                    SetActive(_isActive);
                }
            }
        }

        private void OnSelectedItemChanged(object sender, MenuItem e)
        {
            IsActive = (IsCanView == (ViewModel?.SelectedItem != null));
        }
    }
}
