using Domain.Aggregates.Drivers;

namespace Test.Domain.Aggregates.Drivers;
public class DriverTests
{

    [Fact]
    public void Driver_Create_ShouldCreateDriver()
    {
        var firstName = "first";
        var lastName = "last";
        var dateOfBirth = new DateTime();
        var countryCode = "POL";
        var image = "image_url";
        var descriptionHtml = "description";

        var driver = Driver.Create(firstName, lastName, dateOfBirth, countryCode, image, descriptionHtml);

        Assert.Equal(firstName, driver.FirstName);
        Assert.Equal(lastName, driver.LastName);
        Assert.Equal(dateOfBirth, driver.DateOfBirth);
        Assert.Equal(countryCode, driver.CountryCode);
        Assert.Equal(image, driver.Image);
        Assert.Equal(descriptionHtml, driver.DescriptionHtml);
    }

    [Fact]
    public void Driver_Update_ShouldUpddateDriver()
    {
        var firstName = "first";
        var lastName = "last";
        var dateOfBirth = new DateTime(2000, 1, 1);
        var countryCode = "POL";
        var image = "image_url";
        var descriptionHtml = "description";

        var driver = Driver.Create(firstName, lastName, dateOfBirth, countryCode, image, descriptionHtml);

        var firstNameUpdate = "first update";
        var lastNameUpdate = "last update";
        var dateOfBirthUpdate = new DateTime(2001, 1, 5);
        var countryCodeUpdate = "GER";
        var imageUpdate = "image_url_update";
        var descriptionHtmlUpdate = "description update";

        driver.Update(firstNameUpdate, lastNameUpdate, dateOfBirthUpdate, countryCodeUpdate, imageUpdate, descriptionHtmlUpdate);

        Assert.Equal(firstNameUpdate, driver.FirstName);
        Assert.Equal(lastNameUpdate, driver.LastName);
        Assert.Equal(dateOfBirthUpdate, driver.DateOfBirth);
        Assert.Equal(countryCodeUpdate, driver.CountryCode);
        Assert.Equal(imageUpdate, driver.Image);
        Assert.Equal(descriptionHtmlUpdate, driver.DescriptionHtml);
    }
}
