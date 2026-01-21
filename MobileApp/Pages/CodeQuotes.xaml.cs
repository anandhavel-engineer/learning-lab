namespace MobileApp.Pages;

public partial class CodeQuotes : ContentPage
{
    private readonly List<string> Quotes = new()
        {
            "Success is built one small step at a time.",
            "Consistency beats motivation when motivation fades.",
            "Every expert was once a beginner.",
            "Progress matters more than perfection.",
            "Discipline is choosing what you want most over what you want now.",
            "Your future is shaped by what you do today.",
            "Small actions repeated daily create big results.",
            "Failure is feedback, not a final verdict.",
            "Focus on what you can control and improve the rest.",
            "Dreams become goals when backed by action.",
            "Hard work turns talent into skill.",
            "Confidence grows when you keep promises to yourself.",
            "Patience is powerful when paired with effort.",
            "Growth begins at the edge of your comfort zone.",
            "Believe in the process, even when progress feels slow."
        };

    private readonly Random random = new();

    public CodeQuotes()
    {
        InitializeComponent();
        _ = ChangeQuoteAsync();
    }

    // 🔥 MAIN FIX — async Task, not async void
    private async Task ChangeQuoteAsync()
    {
        await MainThread.InvokeOnMainThreadAsync(() =>
        {
            QuoteLabel.Text = Quotes[random.Next(Quotes.Count)];
            GridBackground.Background = GenerateRandomGradient();
        });
    }

    // ❌ Excludes black & near-black shades
    private Color GenerateBrightRandomColor()
    {
        int r, g, b;

        do
        {
            r = random.Next(50, 256);
            g = random.Next(50, 256);
            b = random.Next(50, 256);
        }
        while ((r + g + b) < 200); // brightness threshold

        return Color.FromRgba(r, g, b, 255);
    }

    private LinearGradientBrush GenerateRandomGradient()
    {
        int colorCount = random.Next(2, 5);

        var stops = new GradientStopCollection();
        float step = 1f / (colorCount - 1);

        for (int i = 0; i < colorCount; i++)
        {
            var color = GenerateBrightRandomColor();
            stops.Add(new GradientStop(Color.FromRgba(color.Alpha, color.Red, color.Green, color.Blue), i * step));
        }

        return new LinearGradientBrush(
            stops,
            new Point(0, 0),
            new Point(0, 1)
        );
    }

    // ✅ Event handlers are the ONLY place async void is allowed
    private async void btnGenerateNewQuote_Clicked(object sender, EventArgs e)
    {
        btnGenerateNewQuote.IsEnabled = false;

        try
        {
            await ChangeQuoteAsync();
        }
        finally
        {
            btnGenerateNewQuote.IsEnabled = true;
        }
    }
}
