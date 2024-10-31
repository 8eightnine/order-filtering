using ConsoleApp2;

namespace TestProject1
{
    [TestFixture]
    public class OrderTests
    {
        [Test]
        public void Constructor_ValidInput_ShouldCreateOrder()
        {
            Order order = new("123", "10", "Central", "2024-01-01 08:00:00");
            Assert.IsNotNull(order);
            Assert.That(order.ID, Is.EqualTo("123"));
            Assert.That(order.Weight, Is.EqualTo("10"));
            Assert.That(order.District, Is.EqualTo("Central"));
            Assert.That(order.DeliveryDate, Is.EqualTo("2024-01-01 08:00:00"));
        }

        [Test]
        public void Constructor_InvalidWeight_ShouldThrowException()
        {
            Assert.Throws<Exception>(() => new Order("123", "-5", "Central", "2024-01-01 08:00:00"),
                "Expected an exception for a negative weight value.");
        }

        [Test]
        public void Constructor_InvalidDistrict_ShouldThrowException()
        {
            Assert.Throws<Exception>(() => new Order("123", "10", "InvalidDistrict", "2024-01-01 08:00:00"),
                "Expected an exception for an invalid district.");
        }

        [Test]
        public void Constructor_InvalidDate_ShouldThrowException()
        {
            Assert.Throws<Exception>(() => new Order("123", "10", "Central", "01-01-2024 08:00:00"),
                "Expected an exception for an invalid date format.");
        }

        [Test]
        public void Weight_SetInvalidFormat_ShouldLogError()
        {
            var order = new Order("123", "10", "Central", "2024-01-01 08:00:00");
            order.Weight = "invalid_weight";
            Assert.That(order.Weight, Is.EqualTo("10"), "Вес не изменяется при вводе некорректного значения.");
        }

        [Test]
        public void IsValid_ValidOrder_ShouldReturnTrue()
        {
            var order = new Order("123", "10", "Central", "2024-01-01 08:00:00");
            Assert.IsTrue(order.isValid(), "Заказ создан и его значения валидные.");
        }
    }
}