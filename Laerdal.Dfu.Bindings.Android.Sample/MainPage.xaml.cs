namespace Laerdal.Dfu.Bindings.Android.Sample;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
    }

    private void ReadNordicVersionButtonClicked(object sender, EventArgs e)
    {
        var nordicVersion = new Version($"{Laerdal.Dfu.Bindings.Android.BuildConfig.VersionName}.{Laerdal.Dfu.Bindings.Android.BuildConfig.VersionCode}");
        
        NordicVersionLabel.Text = $"Nordic's DFU version : {nordicVersion}";
        
        SemanticScreenReader.Announce(nordicVersion.ToString());
    }
}
