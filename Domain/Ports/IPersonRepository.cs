using EmployeeManager.Domain.Entities;

namespace EmployeeManager.Domain.Ports
{
    public interface IPersonRepository
    {
        void Add(Person person);
        void Update(Person person);
        Person GetById(int id);
        List<Person> GetAll();
        void Delete(int id);
    }
}
