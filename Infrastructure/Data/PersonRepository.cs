using EmployeeManager.Domain.Entities;
using EmployeeManager.Domain.Ports;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManager.Infrastructure.Data
{
    public class PersonRepository : IPersonRepository
    {
        private readonly AppDbContext _context;

        public PersonRepository(AppDbContext context)
        {
            _context = context;
        }

        public void Add(Person person)
        {
            _context.Persons.Add(person);
            _context.SaveChanges();
        }

        public void Update(Person person)
        {
            _context.Persons.Update(person);
            _context.SaveChanges();
        }

        public Person GetById(int id)
        {
            return _context.Persons.Include(p => p.Jobs).FirstOrDefault(p => p.Id == id);
        }

        public List<Person> GetAll()
        {
            return _context.Persons.Include(p => p.Jobs).ToList();
        }
        public void Delete(int id)
        {
            var person = _context.Persons
                .Include(p => p.Jobs) 
                .FirstOrDefault(p => p.Id == id);

            if (person != null)
            {
                _context.Persons.Remove(person);
                _context.SaveChanges();
            }
        }
    }
}
