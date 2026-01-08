using System.Diagnostics;

namespace MobileApp.Pages;

public partial class ColorMaker : ContentPage
{
    private bool isRandomcolor;
    public ColorMaker()
    {
        InitializeComponent();
        SetColor(new Color());
    }

    private void RandomColorButton_Clicked(object sender, EventArgs e)
    {
        isRandomcolor = true;

        Random random = new Random();
        int red = random.Next(256);
        int green = random.Next(256);
        int blue = random.Next(256);

        RedSlider.Value = red;
        GreenSlider.Value = green;
        BlueSlider.Value = blue;

        isRandomcolor = false;

        SetColor(Color.FromRgb(red, green, blue));
    }


    private void Slider_ValueChanged(object sender, ValueChangedEventArgs e)
    {
        if (isRandomcolor)
            return;

        var color = Color.FromRgb(
            (int)RedSlider.Value,
            (int)GreenSlider.Value,
            (int)BlueSlider.Value);

        SetColor(color);
    }

    private void SetColor(Color color)
    {
        if (!isRandomcolor)
        {
            Container.BackgroundColor = color;
            ColorLabel.Text = $"{color.ToHex().ToUpper()}";
            Debug.WriteLine($"Color changed to: {color.ToHex().ToUpper()} with RGB code {color.ToRgbaHex()}");
        }
    }
}