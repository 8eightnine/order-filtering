using System.Globalization;

namespace ConsoleApp2
{
    public class Order
    {
        // Список допустимых значений района 
        public readonly string[] Districts = ["Central", "Leninsky", "Zaeltsovsky", "Sovetsky", "Zheleznodorozhny"];


        private string _id;
        private int _weight;
        private string _district;
        private DateTime _deliveryDate;

        public string ID
        {
            get { return _id.ToString(); }
        }
        public string Weight
        {
            get { return _weight.ToString(); }
            set
            {
                var temp = _weight;
                if (!Int32.TryParse(value, out _weight))
                {
                    Logger.WriteLog("Ошибка изменения заказа: ID имеет неверный формат", 1);
                    _weight = temp;
                }
            }
        }
        public string District
        {
            get { return _district; }
            set { 
                if (Districts.Contains(value))
                    _district = value;
                else
                    Logger.WriteLog("Ошибка изменения заказа: заданный район не существует", 1);
            }
        }
        public string DeliveryDate
        {
            get
            {
                return _deliveryDate.ToString("yyyy-dd-MM HH:mm:ss");
            }
            set
            {
                var temp = _deliveryDate;
                if (!DateTime.TryParseExact(value, "yyyy-dd-MM HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None, out _deliveryDate))
                {
                    Logger.WriteLog("Ошибка изменения заказа: дата имеет неверный формат", 1);
                    _deliveryDate = temp;
                }
                    
            }
        }

        public Order(string id, string weight, string district, string datetime)
        {
            _id = id;
            
            if (Int32.TryParse(weight, out _weight))
            {
                if (Districts.Contains(district))
                    _district = district;
                else 
                    throw new Exception("Ошибка создания заказа: заданный район не существует");

                if (_weight <= 0 || !DateTime.TryParseExact(datetime, "yyyy-dd-MM HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None, out _deliveryDate))
                {
                    throw new Exception("Ошибка создания заказа: заказ содержит неккоректные данные: " +
                        (_weight <= 0 ? (@"вес не может быть меньше либо равен нулю") : (@"неверный формат даты")));
                }
            }
            else
            {
                throw new Exception("Ошибка создания заказа: вес должен быть записан в числовом формате (например, 50)");
            }
        }

        public DateTime getDate()
        {
            return _deliveryDate;
        }

        // Метод для проверки правильности значений заказа
        public bool isValid()
        {
            if (_weight > 0 && _deliveryDate != DateTime.MinValue)
                return true;

            return false;
        }

        // Метод для представления заказа в строковом формате
        public override string ToString()
        {
            return $"ID = {_id.ToString()}, Weight = {_weight.ToString()}, District = {_district}, Date = {_deliveryDate.ToString("yyyy-dd-MM HH:mm:ss")}";
        }
    }
}
