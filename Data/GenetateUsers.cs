using AwesomeNetwork.Models.Users;

namespace AwesomeNetwork.Data
{
    public class GenetateUsers
    {
        public readonly string[] MaleNames = { "Александро", "Борис", "Василий", "Игорь", "Даниил", "Сергей", "Евгений", "Алексей", "Геогрий", "Валентин" };
        public readonly string[] FemaleNames = { "Анна", "Мария", "Станислава", "Елена" };
        public readonly string[] LastNames = { "Тестов", "Титов", "Потапов", "Джабаев", "Иванов" };

        public List<User> Populate(int count)
        {
            var users = new List<User>();
            for (var i = 1; i < count; i++)
            {
                string firstName;
                var rand = new Random();

                var male = rand.Next(1, 2) == 1;

                var lastName = LastNames[rand.Next(0, LastNames.Length - 1)];
                if (male)
                {
                    firstName = MaleNames[rand.Next(0, MaleNames.Length - 1)];
                }
                else
                {
                    lastName = lastName + "a";
                    firstName = FemaleNames[rand.Next(0, FemaleNames.Length - 1)];
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
