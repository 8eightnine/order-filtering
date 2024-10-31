using System;
using System.Globalization;

namespace ConsoleApp2
{
    internal class Order
    {
        public readonly string[] Districts = ["Central", "Leninsky", "Zaeltsovsky", "Sovetsky", "Zheleznodorozhny"];


        private string _id;
        private int _weight;
        private string _district;
        private DateTime _deliveryDate;

        public string ID
        {
            get { return _id.ToString(); }
            set
            {
                _id = value;
            }
        }
        public string Weight
        {
            get { return _weight.ToString(); }
            set
            {
                if (!Int32.TryParse(value, out _weight))
                    Logger.WriteLog("Ошибка изменения заказа: ID имеет неверный формат", 1);
            }
        }
        public string District
        {
            get { return _district; }
            set { _district = value; }
        }
        public string DeliveryDate
        {
            get
            {
                return _deliveryDate.ToString("yyyy-dd-MM HH:mm:ss");
            }
            set
            {
                if (DateTime.TryParseExact(value, "yyyy-dd-MM HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None, out _deliveryDate))
                    Logger.WriteLog("Ошибка изменения заказа: дата имеет неверный формат", 1);
            }
        }

        public Order(string id, string weight, string district, string datetime)
        {
            if (Int32.TryParse(weight, out _weight) && Districts.Contains(district))
            {
                _district = district;
                _id = id;

                if (_weight <= 0 || !DateTime.TryParseExact(datetime, "yyyy-dd-MM HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None, out _deliveryDate))
                {
                    throw new Exception("Ошибка создания заказа: заказ содержит неккоректные данные: " +
                        (_weight <= 0 ? (@"вес не может быть меньше либо равен нулю") : (@"неверный формат даты")));
                }
            }
        }

        public bool isValid()
        {
            if (_weight > 0 && _deliveryDate != DateTime.MinValue)
                return true;

            return false;
        }

        public override string ToString()
        {
            return $"ID = {_id.ToString()}, Weight = {_weight.ToString()}, District = {_district}, Date = {_deliveryDate.ToString()}";
        }

        public DateTime getDate()
        {
            return _deliveryDate;
        }
    }
}
