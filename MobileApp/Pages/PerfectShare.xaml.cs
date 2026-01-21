namespace MobileApp.Pages;

using Microsoft.Maui.Controls;

public partial class PerfectShare : ContentPage
{
    private int BillAmount;
    private int TipAmount;
    private int NumberOfPersons = 1;
    private int TipPercent;
    private int TotalShareperperson;


    public PerfectShare()
    {
        InitializeComponent();
    }

    private async void CalculateShare()
    {
        TipAmount = (BillAmount * TipPercent) / 100;
        await AnimateLabelChange(labelTipAmount, TipAmount.ToString());
        await AnimateLabelChange(TotalBill, BillAmount.ToString());
        await AnimateLabelChange(perPersonBill, (BillAmount / NumberOfPersons).ToString());
        await AnimateLabelChange(labelTipShare, (TipAmount / NumberOfPersons).ToString());

        TotalShareperperson = ((BillAmount + TipAmount) / NumberOfPersons);

        await AnimateLabelChange(TotalSharePerPerson, TotalShareperperson.ToString());
    }

    private async void TipPercent_Clicked(object sender, EventArgs e)
    {
        var btn = (Button)sender;
        TipPercent = int.Parse(btn.Text.Replace("%", ""));
        tipslider.Value = TipPercent;
        await AnimateLabelChange(tipbuttonlabel, $"Tip: {TipPercent}%");
        CalculateShare();
    }

    private async Task AnimateLabelChange(Label label, string newText)
    {
        await label.ScaleToAsync(0.85, 80, Easing.CubicOut);
        label.Text = newText;
        await label.ScaleToAsync(1, 120, Easing.CubicOut);
    }

    private async void tipslider_ValueChanged(object sender, ValueChangedEventArgs e)
    {
        TipPercent = (int)tipslider.Value;
        await AnimateLabelChange(tiplabel, $"Tip: {TipPercent}%");
        await AnimateLabelChange(tipbuttonlabel, $"Tip: {TipPercent}%");
        CalculateShare();
    }

    private void BillAmount_Completed(object sender, EventArgs e)
    {
        BillAmount = int.Parse(BillAmountentry.Text);
        CalculateShare();
    }

    private async void btnMinus_Clicked(object sender, EventArgs e)
    {
        NumberOfPersons--;
        if (NumberOfPersons < 2)
            btnMinus.IsEnabled = false;

        await AnimateLabelChange(labelNumberOfPersons, NumberOfPersons.ToString());
        CalculateShare();
    }

    private async void btnPlus_Clicked(object sender, EventArgs e)
    {
        btnMinus.IsEnabled = true;
        NumberOfPersons++;
        await AnimateLabelChange(labelNumberOfPersons, NumberOfPersons.ToString());
        CalculateShare();
    }
}