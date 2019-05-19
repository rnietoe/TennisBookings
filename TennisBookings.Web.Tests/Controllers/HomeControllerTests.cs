using Microsoft.AspNetCore.Mvc;
using TennisBookings.Web.Controllers;
using TennisBookings.Web.Services;
using TennisBookings.Web.ViewModels;
using Xunit;
using Moq;
using TennisBookings.Web.Configuration;
using Microsoft.Extensions.Options;

namespace TennisBookings.Web.Tests.Controllers
{
    public class HomeControllerTests
    {
        [Fact]
        public void ReturnsExpectedViewModel_WhenWeatherIsSunny()
        {
            var mockWeatherForecaster = new Mock<IWeatherForecaster>();
            mockWeatherForecaster.Setup(w => w.GetCurrentWeather()).Returns(new WeatherResult
            {
                WeatherCondition = WeatherCondition.Sun
            });

            var option = Options.Create(new FeaturesConfiguration { EnableWeatherForecast = true });

            var sut = new HomeController(mockWeatherForecaster.Object, option);

            var result = sut.Index();

            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<HomeViewModel>(viewResult.ViewData.Model);
            Assert.Contains("It's sunny right now.", model.WeatherDescription);
        }

        [Fact]
        public void ReturnsExpectedViewModel_WhenWeatherIsRainy()
        {
            var mockWeatherForecaster = new Mock<IWeatherForecaster>();
            mockWeatherForecaster.Setup(w => w.GetCurrentWeather()).Returns(new WeatherResult
            {
                WeatherCondition = WeatherCondition.Rain
            });

            var option = Options.Create(new FeaturesConfiguration { EnableWeatherForecast = true });

            var sut = new HomeController(mockWeatherForecaster.Object, option);

            var result = sut.Index();

            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<HomeViewModel>(viewResult.ViewData.Model);
            Assert.Contains("We're sorry but it's raining here.", model.WeatherDescription);
        }
    }
}
