using AwesomeNetwork.Models.Users;
using System;
using System.Collections.Generic;

namespace AwesomeNetwork.Data
{
    public class GenetateUsers
    {
        private readonly string[] _maleNames = new string[] { "Александро", "Борис", "Василий", "Игорь", "Даниил", "Сергей", "Евгений", "Алексей", "Геогрий", "Валентин" };
        private readonly string[] _femaleNames = new string[] { "Анна", "Мария", "Станислава", "Елена" };
        private readonly string[] _lastNames = new string[] { "Тестов", "Титов", "Потапов", "Джабаев", "Иванов" };

        public List<User> Populate(int count)
        {
            var users = new List<User>();
            for (int i = 1; i < count; i++)
            {
                string firstName;
                var rand = new Random();

                var male = rand.Next(1, 2) == 1;

                var lastName = _lastNames[rand.Next(0, _lastNames.Length - 1)];
                if (male)
                {
                    firstName = _maleNames[rand.Next(0, _maleNames.Length - 1)];
                }
                else
                {
                    lastName = lastName + "a";
                    firstName = _femaleNames[rand.Next(0, _femaleNames.Length - 1)];
                }

                var item = new User()
                {
                    FirstName = firstName,
                    LastName = lastName,
                    BirthDate = DateTime.Now.AddDays(-rand.Next(1, (DateTime.Now - DateTime.Now.AddYears(-25)).Days)),
                    Email = "test" + rand.Next(0, 1204) + "@test.com",
                };

                item.UserName = item.Email;
                item.Image = "https://thispersondoesnotexist.com/image";

                users.Add(item);
            }

            return users;
        }
    }
}
