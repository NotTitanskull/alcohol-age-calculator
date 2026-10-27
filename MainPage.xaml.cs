namespace alcohol_age_calculator;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
    }

    private void OnCalculateClicked(object? sender, EventArgs e)
    {
        if (CountryPicker.SelectedItem is not string country)
        {
            ResultLabel.Text = "Select a country.";
            return;
        }

        int legalAge;

        switch (country)
        {
            case "United States":
                legalAge = 21;
                break;

            case "United Kingdom":
            case "New Zealand":
            case "Singapore":
                legalAge = 18;
                break;

            default:
                ResultLabel.Text = "Select a supported country.";
                return;
        }

        DateTime? selectedBirthday = BirthdayPicker.Date;

        if (!selectedBirthday.HasValue)
        {
            ResultLabel.Text = "Select your date of birth.";
            return;
        }

        DateTime birthday = selectedBirthday.Value.Date;

        DateTime today = DateTime.Today;

        if (birthday > today)
        {
            ResultLabel.Text = "Date of birth cannot be in the future.";
            return;
        }

        int age = today.Year - birthday.Year;

        if (birthday.AddYears(age) > today)
        {
            age--;
        }

        DateTime eligibilityDate = birthday.AddYears(legalAge);

        if (today >= eligibilityDate)
        {
            ResultLabel.Text =
                $"{country}: minimum purchase age is {legalAge}.\n" +
                $"Your age: {age}.\n" +
                "You meet the age requirement.";
        }
        else
        {
            int yearsRemaining = eligibilityDate.Year - today.Year;

            if (today.AddYears(yearsRemaining) > eligibilityDate)
            {
                yearsRemaining--;
            }

            DateTime afterYears = today.AddYears(yearsRemaining);

            int monthsRemaining =
                (eligibilityDate.Year - afterYears.Year) * 12
                + eligibilityDate.Month - afterYears.Month;

            if (afterYears.AddMonths(monthsRemaining) > eligibilityDate)
            {
                monthsRemaining--;
            }

            DateTime afterMonths = afterYears.AddMonths(monthsRemaining);

            int daysRemaining = (eligibilityDate - afterMonths).Days;

            ResultLabel.Text =
                $"{country}: minimum purchase age is {legalAge}.\n" +
                $"Your age: {age}.\n" +
                $"Time remaining: {yearsRemaining} years, " +
                $"{monthsRemaining} months, and {daysRemaining} days.";
        }
    }
}