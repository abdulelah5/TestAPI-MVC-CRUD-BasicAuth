using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestAPI_DAC;
using TestAPI_DTO;

namespace TestAPI_BIZ
{
    public class PersonBIZ
    {
        private PersonDAC _dl = new PersonDAC();
        
        public Person PersonName(int id)
        {
            return this._dl.FetchPersonName(id);
        }
        public Person PersonInfo(int id)
        {
            return this._dl.FetchPersonInfo(id);
        }

        public List<Person> AllName()
        {
            return this._dl.FetchAllNames();
        }

        public int InsertPersonInfo(Person person)
        {
            return this._dl.InsertPersonInfo(person);
        }

        public bool UpdatePersonInfo(Person person)
        {
            return this._dl.UpdatePersonInfo(person);
        }

        public bool DeletePersonInfo(int id)
        {
            return this._dl.DeletePersonInfo(id);
        }
    }
}
