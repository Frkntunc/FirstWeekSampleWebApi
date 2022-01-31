using FirstWeekSampleWebApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FirstWeekSampleWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private static List<Customer> _customers = new List<Customer>
        {
            new Customer {ID=1,FirstName="Furkan",LastName="Tunç",Address="İstanbul"},
            new Customer {ID=2,FirstName="Ali",LastName="Kaya",Address="Ankara"},
            new Customer {ID=3,FirstName="Veli",LastName="Yılmaz",Address="İzmir"},
            new Customer {ID=4,FirstName="Ayşe",LastName="Demir",Address="İstanbul"}
        };

        [HttpGet] //Tüm müşterileri getirecek
        public List<Customer> GetCustomers()
        {
            return _customers;
        }

        [HttpGet("id")] //id'ye göre aranan müşteriyi getirecek
        public Customer GetCustomerById(int ID)
        {
            return _customers.FirstOrDefault(c => c.ID == ID);
        }

        [HttpGet("search")] //müşteri ismine göre aranan müşteriyi getirecek
        public IActionResult SearchCustomerByName(string name)
        {
            if (!string.IsNullOrEmpty(name))
            {
                List<Customer> searchedCustomers = _customers.Where(c => c.FirstName.ToLower().Contains(name.ToLower())).ToList();
                if (searchedCustomers.Count != 0)
                {
                    return Ok(searchedCustomers);
                }
            }
            return BadRequest("Böyle bir müşteri bulunmadı.");
        }

        [HttpPost]//müşteri ekleme
        public IActionResult AddCustomer(Customer customer)
        {
            if (_customers.FirstOrDefault(c => c.ID == customer.ID) == null)
            {
                _customers.Add(customer);
                return Ok("Müşteri eklendi.");
            }
            return BadRequest("Bu ID numaralı müşteri zaten var.");
        }

        [HttpPut]//müşteri güncelleme
        public IActionResult UpdateCustomer(Customer customer)
        {
            var updatedCustomer = _customers.FirstOrDefault(c => c.ID == customer.ID);

            if (updatedCustomer != null)
            {
                updatedCustomer.FirstName = customer.FirstName;
                updatedCustomer.LastName = customer.LastName;
                updatedCustomer.Address = customer.Address;
                return Ok("Müşteri güncellendi.");
            }
            return BadRequest("Böyle bir müşteri bulunamadı.");
        }

        [HttpDelete]//müşteri silme
        public IActionResult DeleteCustomer(int ID)
        {
            var deletedCustomer = _customers.FirstOrDefault(c => c.ID == ID);
            if (deletedCustomer != null)
            {
                _customers.Remove(deletedCustomer);
                return Ok("Müşteri silindi.");
            }
            return BadRequest("Böyle bir müşteri bulunamadı.");
        }
    }
}
