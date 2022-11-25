using RestWithASPNETUdemy1.Model;
using System;
using System.Collections.Generic;
using System.Threading;

namespace RestWithASPNETUdemy1.Services.Implementations
{
    public class PersonServiceImplementation : IPersonService
    {
        private volatile int count;
        public Person Create(Person person) {
            return person;
        }

        public void Delete(long id) 
        {

        }

        public List<Person> FindAll() {
            List<Person> persons = new List<Person>();
            for (int i = 0; i < 8; i++) {
                Person person = MockPerson(i);
            }
            return persons;
        }

        public Person FindByID(long id) {
            return new Person {
                Id = 1,
                FirstName = "Jarleson",
                LastName = "Costa",
                Address = "Uberlandia - Minas Gerais - Brasil",
                Gender = "Male"
            };
        }

        public Person Update(Person person) 
        {
            return person;
        }

        private Person MockPerson(int i) {
            return new Person {
                Id = ImcrementAndGet(),
                FirstName = "Person Name" + i,
                LastName = "Person LastName" + i,
                Address = "Some Address" + i,
                Gender = "Male"
            };
        }

        private int ImcrementAndGet() 
        {
            return Interlocked.Increment(ref count);
        }
    }
}