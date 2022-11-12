using Avalonia;
using Avalonia.Collections;
using Avalonia.Controls;
using Avalonia.Data;
using Avalonia.Media.Imaging;

namespace Controls
{
    public partial class Scene : UserControl
    {
        private readonly Dictionary<string, Bitmap> _imagesDictionary = new Dictionary<string, Bitmap>();

        public Scene()
        {
            InitializeComponent();
        }

        public static readonly AvaloniaProperty<string[]> ImagesProperty =
            AvaloniaProperty.Register<Scene, string[]>(nameof(Images), defaultBindingMode: BindingMode.TwoWay);

        
        
        public string[] Images
        {
            get => (string[])GetValue(ImagesProperty);
            set
            {
                SetValue(ImagesProperty, value);
                UpdateBitmaps(value);
                SetImageCollection(value);
            }
        }

        private void SetImageCollection(string[] value)
        {
            ImageCollection.Clear();
            foreach (var image in value)
            {
                ImageCollection.Add(_imagesDictionary[image]);
            }
        }

        public AvaloniaList<Bitmap> ImageCollection { get; } = new AvaloniaList<Bitmap>();

        private void UpdateBitmaps(IEnumerable<string> value)
        {
            if (_imagesDictionary.Count > 100)
            {
                _imagesDictionary.Clear();
            }

            foreach (var image in value)
            {
                if (!_imagesDictionary.ContainsKey(image))
                {
                    _imagesDictionary.Add(image, new Bitmap(image));
                }
            }
        }
    }
}