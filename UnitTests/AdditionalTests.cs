using ConsoleApp2;
using System.Globalization;

namespace TestProject1
{
    [TestFixture]
    public class OrderAdditionalTests
    {
        [Test]
        public void Constructor_ZeroWeight_ShouldThrowException()
        {
            Assert.Throws<Exception>(() => new Order("123", "0", "Central", "2024-01-01 08:00:00"),
                "Исключение если вес отрицательный.");
        }

        [Test]
        public void Constructor_BorderlineValidDate_ShouldCreateOrder()
        {
            var validDate = DateTime.Now.ToString("yyyy-dd-MM HH:mm:ss");
            var order = new Order("123", "10", "Leninsky", validDate);
            Assert.IsNotNull(order, "Дата заказа должна быть валидной.");
            Assert.That(order.DeliveryDate, Is.EqualTo(validDate));
        }

        [Test]
        public void Set_InvalidDeliveryDate_ShouldLogError()
        {
            var order = new Order("123", "10", "Zaeltsovsky", "2024-01-01 08:00:00");
            order.DeliveryDate = "2024/01/01 08:00:00";  // Неверный формат даты
            Assert.That(order.DeliveryDate, Is.EqualTo("2024-01-01 08:00:00"),
                "Дата заказа не должна изменяться при вводе некорректного значения.");
        }

        [Test]
        public void ToString_ValidOrder_ShouldReturnFormattedString()
        {
            var order = new Order("123", "20", "Sovetsky", "2024-02-01 09:00:00");
            var expectedOutput = $"ID = 123, Weight = 20, District = Sovetsky, Date = 2024-02-01 09:00:00";
            Assert.That(order.ToString(), Is.EqualTo(expectedOutput),
                "Метод ToString возвращает данные в неверном формате.");
        }

        [Test]
        public void District_SetInvalidDistrict_ShouldLogError()
        {
            var order = new Order("125", "15", "Central", "2024-01-01 08:00:00");
            order.District = "UnknownDistrict";
            Assert.That(order.District, Is.Not.EqualTo("UnknownDistrict"),
                "Район заказа должен быть валидным.");
        }

        [Test]
        public void getDate_ValidDate_ReturnsCorrectDateTime()
        {
            var order = new Order("126", "25", "Zheleznodorozhny", "2024-03-01 10:00:00");
            DateTime expectedDate = DateTime.ParseExact("2024-03-01 10:00:00", "yyyy-dd-MM HH:mm:ss", CultureInfo.InvariantCulture);
            Assert.That(order.getDate(), Is.EqualTo(expectedDate),
                "getDate должен возвращать корректное значение даты в корректном формате.");
        }
    }
}
