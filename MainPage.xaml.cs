namespace PMEquipment;

public partial class MainPage : ContentPage
{
    private bool _loaded;

    public MainPage()
    {
        InitializeComponent();
        Loaded += async (_, _) => await LoadLocalAppAsync();
    }

    private async Task LoadLocalAppAsync()
    {
        try
        {
            // فایل‌های برنامه در مسیر ثابت به‌روزرسانی می‌شوند؛ داده‌های LocalStorage وب‌ویو پاک نمی‌شوند.
            var appFolder = FileSystem.AppDataDirectory;
            Directory.CreateDirectory(appFolder);

            foreach (var fileName in new[] { "index.html", "xlsx.full.min.js" })
            {
                var target = Path.Combine(appFolder, fileName);
                await using var input = await FileSystem.OpenAppPackageFileAsync(fileName);
                await using var output = File.Create(target);
                await input.CopyToAsync(output);
            }

            AppWebView.Source = new UrlWebViewSource
            {
                Url = new Uri(Path.Combine(appFolder, "index.html")).AbsoluteUri
            };
            _loaded = true;
        }
        catch (Exception ex)
        {
            await DisplayAlert("خطا در بارگذاری برنامه", ex.Message, "متوجه شدم");
        }
    }

    private async void OnReloadClicked(object sender, EventArgs e)
    {
        if (!_loaded)
            await LoadLocalAppAsync();
        else
            AppWebView.Reload();
    }
}
