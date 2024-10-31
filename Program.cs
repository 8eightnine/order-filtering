using System.Globalization;
using System;
using System.IO;
using System.Text;

namespace ConsoleApp2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 5)
            {
                // Задаем значения для файла с входными данными, выходными данными и файла логов
                string _inputFile = args[0];
                string _cityDistrict = args[1];
                string _deliveryOrder = args[3];
                string _deliveryLog = args[4];

                Logger.setFilename(_deliveryLog);

                // Проверка файла на существование
                if (!File.Exists(_inputFile))
                {
                    Logger.WriteLog("Ошибка в переданном параметре '_inputFile': файл не существует", 1);
                    return;
                }

                // Проверка на корректность ввода даты
                DateTime _firstDeliveryDateTime;
                if (!DateTime.TryParseExact(args[2], "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None, out _firstDeliveryDateTime)) 
                {
                    Logger.WriteLog("Ошибка в переданном параметре '_firstDeliveryDateTime': неправильный формат записи", 1);
                    return;
                }
                
                // Создаем экземпляр заказа для дальнейшего сравнения
                Order toSort = new((999).ToString(), (999).ToString(), args[1], args[2]);

                var contents = File.ReadAllLines(_inputFile);
                // Считаем что каждый новый заказ начинается на новой строке
                List<Order> orders = new List<Order>();
                // Считаываем данные заказов
                foreach (var line in contents)
                {
                    try
                    {
                        string[] values = line.Split(' ');
                        Order order = new(
                            Guid.NewGuid().ToString(),
                            values[0],                    // Вес
                            values[1],                    // Район
                            values[2] + ' ' + values[3]   // Дата
                        );

                        // Если заказ записан в правильном формате - добавляем его в массив
                        if (order.isValid())
                        {
                            Logger.WriteLog($"Добавлен новый заказ, ID = {order.ID}", 0);
                            orders.Add(order);
                        }

                    }
                    catch (Exception ex)
                    {
                        Logger.WriteLog(ex.Message, 1);
                    }
                }
                
                Console.WriteLine("Всего обработано заказов: " + orders.Count());

                // Выбираем те заказы, где район заказа совпадает с районом фильтрации и разница между временем меньше 30 минут
                 var sortedOrders = orders.Where(o => o.District == _cityDistrict)
                                         .Select(o => o)
                                         .Where(o => o.getDate().DayOfYear == toSort.getDate().DayOfYear)
                                         .Select(o => o)
                                         .Where(o => DateTime.Parse(o.DeliveryDate).Subtract(_firstDeliveryDateTime).TotalMinutes <= 30 &&
                                                     DateTime.Parse(o.DeliveryDate).Subtract(_firstDeliveryDateTime).TotalMinutes >= 0)
                                         .ToList();

                using (var sw = new StreamWriter(_deliveryOrder))
                {
                    // Записываем результаты в файл
                    sw.WriteLine($"Параметры фильтрации: {_cityDistrict}, {_firstDeliveryDateTime}");
                    sw.WriteLine("Результат фильтрации:");
                    foreach (Order order in sortedOrders)
                    {
                        sw.WriteLine(order.ToString());
                    }
                }
                Logger.WriteLog($"Заказов отфильтровано = {sortedOrders.Count()}", 0);
            }
            else
            {
                Console.WriteLine($"Некорректные аргументы при заупске.\n" +
                    $"Пример использования: Solution.exe 'input.txt' 'Central' '2024-11-01 08:45:27' 'output.txt' 'history.log'");
            }

        }
    }
}
