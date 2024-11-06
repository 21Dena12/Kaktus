using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaktus.DBMODEL
{
    public class ConnectionClass
    {
        public CactusCollectionEntities db = new CactusCollectionEntities();

        // Пример метода для проверки логина и пароля
        public Users AuthenticateUser(string login, string password)
        {
            return db.Users.FirstOrDefault(user => user.Логин == login && user.Пароль == password);
        }

        // Пример метода для добавления кактуса
        public void AddCactus(string вид, string происхождение, int возраст, decimal стоимость, string инструкцииПоУходу)
        {
            Cacti newCactus = new Cacti
            {
                Вид = вид,
                Происхождение = происхождение,
                Возраст = возраст,
                Стоимость = стоимость,
                Инструкции_по_уходу = инструкцииПоУходу
            };
            db.Cacti.Add(newCactus);
            db.SaveChanges();
        }

        // Пример метода для удаления кактуса
        public void DeleteCactus(int id)
        {
            var cactus = db.Cacti.Find(id);
            if (cactus != null)
            {
                db.Cacti.Remove(cactus);
                db.SaveChanges();
            }
        }
    }
}
