using ReactiveUI;

namespace AEngine.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private bool state;
        private  string[] _images;

        public string[] Images
        {
            get { return _images; }
            set { this.RaiseAndSetIfChanged(ref _images,value);}
        }

        public void Do()
        {
            if (state)
            {
                Images = new[] { "1.png", "1.jpg" };
            }
            else
            {
                Images = new[] { "2.png", "2.jpg" };
            }
        }
    }
}